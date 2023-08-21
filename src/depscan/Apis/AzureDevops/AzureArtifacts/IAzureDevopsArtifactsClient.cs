using depscan.Apis.AzureDevops.AzureArtifacts.Models.GetArtifacts.Response;
using Refit;

namespace depscan.Apis.AzureDevops.AzureArtifacts;

public interface IAzureDevopsArtifactsClient
{
    [Get("/packaging/Feeds/{feed}/Packages?api-version=6.0-preview.1")]
    public Task<ApiResponse<GetArtifactsResponse>> GetArtifacts(
        string feed,
        [Authorize("Basic")] string token);
}