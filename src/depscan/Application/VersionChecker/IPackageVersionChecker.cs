namespace depscan.Application.VersionChecker;

public interface IPackageVersionChecker
{
    ICollection<string> GetCachedPackageNames();

    Task<(string packageName, string lastVersion, string stableVersion)> GetVersion(string packageName);
}