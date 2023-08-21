namespace depscan.Model.Output;

public interface IOutput
{
    Summary GenerateSummary(List<ProjectFileInfo> projectFiles, IEnumerable<string> packages);
}