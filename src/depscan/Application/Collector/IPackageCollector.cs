namespace depscan.Application.Collector;

public interface IPackageCollector
{
    Task<List<ProjectFileInfo>> Collect();
}