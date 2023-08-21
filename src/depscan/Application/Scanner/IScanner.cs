namespace depscan.Application.Scanner;

public interface IScanner
{
    Task<List<ProjectFileInfo>> Run();
}