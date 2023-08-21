using depscan.Apis.AzureDevops.AzureRepos.Models.GetRepositories.Response;
using Refit;

namespace depscan.Apis.AzureDevops.AzureRepos;

public interface IAzureDevopsRepoClient
{
    [Get("/repositories?api-version=6.0")]
    public Task<ApiResponse<GetRepositoriesResponse>> GetRepositories(
        [Authorize("Basic")] string token);

    [Get("/repositories/{repositoryId}/items/?path={filePath}&api-version=6.0")]
    public Task<ApiResponse<string>> GetItem(
        Guid repositoryId,
        string filePath,
        [Authorize("Basic")] string token);
}