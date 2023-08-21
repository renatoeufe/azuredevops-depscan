using depscan.Apis.AzureDevops.Search.Models.Search.Request;
using depscan.Apis.AzureDevops.Search.Models.Search.Response;
using Refit;

namespace depscan.Apis.AzureDevops.Search;

public interface IAzureDevopsSearchClient
{
    [Post("/codesearchresults?api-version=6.0-preview.1")]
    public Task<ApiResponse<SearchResponse>> Search(
        [Body] SearchRequest searchRequest,
        [Authorize("Basic")] string token);
}