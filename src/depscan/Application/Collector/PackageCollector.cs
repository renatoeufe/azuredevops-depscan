using depscan.Apis;
using depscan.Apis.AzureDevops.Search.Models.Search.Request;
using depscan.Apis.AzureDevops.Search.Models.Search.Response;
using System.Collections.Concurrent;

namespace depscan.Application.Collector;

public sealed class PackageCollector : IPackageCollector
{
    private readonly ScanParameters _scanParameters;
    private readonly Clients _clients;

    private const string Main = "main";
    private const string Master = "master";

    public PackageCollector(
        Clients clients,
        ScanParameters scanParameters)
    {
        _clients = clients;
        _scanParameters = scanParameters;
    }

    public async Task<List<ProjectFileInfo>> Collect()
    {
        var repoNames = new List<string>(1);
        var projectFiles = new ConcurrentBag<ProjectFileInfo>();

        if (_scanParameters.TargetRepo.Equals("*"))
        {
            repoNames = await GetAllRepoNames().ConfigureAwait(false);
        }
        else
        {
            repoNames.Add(_scanParameters.TargetRepo);
        }

        var tasks =
            repoNames
                .Select(repoName => SearchByRepo(repoName)
                    .ContinueWith(searchByRepoTaskResult =>
                    {
                        var retrieveProjectFilesTasks =
                            searchByRepoTaskResult.Result
                                .Select(result =>
                                    _clients.AzureDevopsRepoClient
                                        .GetItem(result.Repository.Id, result.Path, _scanParameters.Credentials.Token)
                                .ContinueWith(getItemTaskResult =>
                                {
                                    if (!getItemTaskResult.Result.IsSuccessStatusCode)
                                    {
                                        return;
                                    }

                                    var projectFile = new ProjectFileInfo
                                    {
                                        Project = result.Project.Name,
                                        Repository = result.Repository.Name,
                                        Path = result.Path
                                    };

                                    projectFile.PackageReferences.AddRange(
                                        ProjectFileReader.Read(getItemTaskResult.Result.Content));

                                    if (projectFile.PackageReferences.Count > 0)
                                    {
                                        projectFiles.Add(projectFile);
                                    }
                                }))
                            .ToArray();

                        Task.WaitAll(retrieveProjectFilesTasks);
                    }))
                .ToArray();

        Task.WaitAll(tasks);

        return projectFiles.ToList();
    }

    private async Task<List<Result>> SearchByRepo(string repoName)
    {
        var searchText =
            _scanParameters.TargetProject.Equals("*")
                ? $"path:*.csproj repo:{repoName}"
                : $"path:*.csproj proj:{_scanParameters.TargetProject} repo:{repoName}";

        var searchResponse =
            await _clients.AzureDevopsSearchClient
                .Search(new SearchRequest
                {
                    SearchText = searchText,
                }, _scanParameters.Credentials.Token)
                .ConfigureAwait(false);

        if (!searchResponse.IsSuccessStatusCode)
        {
            throw new Exception($"Unable to perform search {searchText}: {searchResponse.Error?.Content}");
        }

        if (searchResponse.Content?.Count == 0 ||
            searchResponse.Content?.Results == null)
        {
            return new List<Result>();
        }

        return
            searchResponse.Content.Results
                .Where(x => x.Versions.Any(v => v.BranchName is Master or Main))
                .ToList();
    }

    private async Task<List<string>> GetAllRepoNames()
    {
        var repoResponse =
            await _clients.AzureDevopsRepoClient
                .GetRepositories(_scanParameters.Credentials.Token)
                .ConfigureAwait(false);

        if (!repoResponse.IsSuccessStatusCode)
        {
            throw new Exception("Unable to retrieve any repository");
        }

        if (repoResponse.Content == null ||
            repoResponse.Content.Count == 0)
        {
            return new List<string>();
        }

        return repoResponse.Content.Value.Select(x => x.Name).OrderByDescending(x => x).ToList();
    }
}