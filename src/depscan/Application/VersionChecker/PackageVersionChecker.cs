using depscan.Apis;
using depscan.Apis.AzureDevops.AzureArtifacts.Models.GetArtifacts.Response;
using System.Collections.Concurrent;

namespace depscan.Application.VersionChecker;

public class PackageVersionChecker : IPackageVersionChecker
{
    private readonly ScanParameters _scanParameters;
    private readonly Clients _clients;
    private readonly object _lock = new();
    private readonly ConcurrentDictionary<string, string> _cachedVersions = new();
    private readonly ConcurrentDictionary<string, string> _cachedStableVersions = new();
    private static GetArtifactsResponse? _getArtifactsResponse;

    private const string NuGet = "NuGet";
    private const string NotFound = "NotFound";

    public PackageVersionChecker(
        Clients clients,
        ScanParameters scanParameters)
    {
        _clients = clients;
        _scanParameters = scanParameters;
    }

    public ICollection<string> GetCachedPackageNames()
    {
        return _cachedVersions.Keys;
    }

    public async Task<(string packageName, string lastVersion, string stableVersion)> GetVersion(string packageName)
    {
        if (_cachedVersions.ContainsKey(packageName))
            return (
                packageName,
                _cachedVersions[packageName],
                _cachedStableVersions[packageName]);

        if (_getArtifactsResponse == null &&
            !string.IsNullOrWhiteSpace(_scanParameters.AzureArtifactsFeedName))
        {
            var getArtifactsResponse =
                await _clients.AzureDevopsArtifactsClient
                    .GetArtifacts(_scanParameters.AzureArtifactsFeedName, _scanParameters.Credentials.Token)
                    .ConfigureAwait(false);

            if (!getArtifactsResponse.IsSuccessStatusCode)
            {
                throw new Exception("Unable to check azure artifacts:" + getArtifactsResponse.Error?.Content);
            }

            lock (_lock)
            {
                _getArtifactsResponse = getArtifactsResponse.Content;
            }
        }

        var (last, stable) = await GetCurrentVersionOnNuGet(packageName).ConfigureAwait(false);

        var lastVersion =
            last
            ?? VersionFoundOnAzureArtifacts(packageName)
            ?? NotFound;

        var stableVersion =
            stable
            ?? NotFound;

        _cachedVersions[packageName] = lastVersion;
        _cachedStableVersions[packageName] = stableVersion;

        return (packageName, lastVersion, stableVersion);
    }

    private static string? VersionFoundOnAzureArtifacts(string packageName)
    {
        var versionFoundOnAzureArtifacts = _getArtifactsResponse?.Value
            .FirstOrDefault(x =>
                x.ProtocolType == NuGet &&
                x.NormalizedName == packageName.ToLowerInvariant())
            ?.Versions.FirstOrDefault(v => v.IsLatest)
            ?.VersionNumber;
        return versionFoundOnAzureArtifacts;
    }

    private async Task<(string? last, string? stable)> GetCurrentVersionOnNuGet(string packageName)
    {
        if (string.IsNullOrWhiteSpace(packageName))
        {
            throw new ArgumentNullException(nameof(packageName));
        }

        var getPackageOnNuGet =
            await _clients.NugetClient
                .GetPackage(packageName.ToLowerInvariant())
                .ConfigureAwait(false);

        var lastVersion =
            getPackageOnNuGet.IsSuccessStatusCode
                ? getPackageOnNuGet.Content.Versions.LastOrDefault()
                : null;

        var stableVersion =
            getPackageOnNuGet.IsSuccessStatusCode
                ? getPackageOnNuGet.Content.Versions.LastOrDefault(x => !x.Contains("-dev") && !x.Contains("-preview"))
                : null;

        return (lastVersion, stableVersion);
    }
}