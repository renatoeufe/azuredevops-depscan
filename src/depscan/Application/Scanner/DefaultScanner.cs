namespace depscan.Application.Scanner;

public class DefaultScanner : IScanner
{
    private readonly IPackageCollector _packageCollector;
    private readonly IPackageVersionChecker _packageVersionChecker;

    public DefaultScanner(
        IPackageCollector packageCollector,
        IPackageVersionChecker packageVersionChecker)
    {
        _packageCollector = packageCollector;
        _packageVersionChecker = packageVersionChecker;
    }

    public async Task<List<ProjectFileInfo>> Run()
    {
        var projectFiles =
            await _packageCollector
                .Collect()
                .ConfigureAwait(false);

        var fillPackageVersionTasks =
            projectFiles
                .SelectMany(projectFileInfo => projectFileInfo.PackageReferences)
                .Select(packageReference =>
                {
                    return _packageVersionChecker
                        .GetVersion(packageReference.Name)
                        .ContinueWith(task =>
                        {
                            packageReference.LastVersion = task.Result.lastVersion;
                            packageReference.StableVersion = task.Result.stableVersion;
                        });
                })
                .ToArray();

        Task.WaitAll(fillPackageVersionTasks);

        return projectFiles;
    }
}