namespace depscan.Application.Collector;

public sealed record ProjectFileInfo
{
    public string Project { get; init; }
    public string Repository { get; init; }
    public string Path { get; init; }

    public List<PackageReference> PackageReferences { get; } = new();
}