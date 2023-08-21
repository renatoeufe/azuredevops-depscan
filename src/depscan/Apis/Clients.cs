using depscan.Apis.AzureDevops.AzureArtifacts;
using depscan.Apis.AzureDevops.AzureRepos;
using depscan.Apis.AzureDevops.Search;
using depscan.Apis.NuGet;
using Refit;

namespace depscan.Apis;

public class Clients
{
    public IAzureDevopsSearchClient AzureDevopsSearchClient { get; }

    public IAzureDevopsArtifactsClient AzureDevopsArtifactsClient { get; }

    public IAzureDevopsRepoClient AzureDevopsRepoClient { get; }

    public INuGetClient NugetClient { get; }

    public Clients(string organization)
    {
        AzureDevopsSearchClient =
            RestService
                .For<IAzureDevopsSearchClient>(
                    $"https://almsearch.dev.azure.com/{organization}/_apis/search");

        AzureDevopsRepoClient =
            RestService
                .For<IAzureDevopsRepoClient>(
                    $"https://dev.azure.com/{organization}/_apis/git");

        AzureDevopsArtifactsClient =
            RestService
                .For<IAzureDevopsArtifactsClient>(
                    $"https://feeds.dev.azure.com/{organization}/_apis");

        NugetClient =
            RestService
                .For<INuGetClient>(
                    "https://api.nuget.org");
    }
}