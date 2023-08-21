using depscan.Apis.NuGet.Models.GetPackage;
using Refit;

namespace depscan.Apis.NuGet;

public interface INuGetClient
{
    [Get("/v3-flatcontainer/{packageName}/index.json")]
    public Task<ApiResponse<GetPackageResponse>> GetPackage(string packageName);
}