namespace depscan.Model.Output;

public sealed class SummaryOutput : IOutput
{
    public Summary GenerateSummary(
        List<ProjectFileInfo> projectFiles,
        IEnumerable<string> packages)
    {
        var summary = new Summary
        {
            Packages = packages.OrderBy(x => x),
            Projects = projectFiles
                .Select(x => x.Project)
                .Distinct()
                .Select(projectName => new Summary.SummaryProject
                {
                    Project = projectName,
                    Repos = projectFiles
                        .Where(x => x.Project.Equals(projectName, StringComparison.InvariantCulture))
                        .Select(x => x.Repository)
                        .Distinct()
                        .Select(repositoryName => new Summary.SummaryRepo
                        {
                            Repository = repositoryName,
                            Paths = projectFiles
                                .Where(x => x.Project.Equals(projectName, StringComparison.InvariantCulture) &&
                                            x.Repository.Equals(repositoryName, StringComparison.InvariantCulture))
                                .Select(x => x.Path)
                                .Distinct()
                                .Select(pathName => new Summary.SummaryPath
                                {
                                    Path = pathName,
                                    Packages = projectFiles
                                        .Where(x => x.Project.Equals(projectName, StringComparison.InvariantCulture) &&
                                                    x.Repository.Equals(repositoryName, StringComparison.InvariantCulture) &&
                                                    x.Path.Equals(pathName, StringComparison.InvariantCulture))
                                        .SelectMany(x => x.PackageReferences)
                                        .ToList()
                                })
                                .ToList()
                        })
                        .ToList()
                })
                .ToList()
        };

        return summary;
    }
}