namespace depscan.Application;

public class ScanParameters
{
    public Credentials Credentials { get; }
    public string AzureArtifactsFeedName { get; }
    public string TargetProject { get; }
    public string TargetRepo { get; }

    public ScanParameters(
        Credentials credentials,
        string azureArtifactsFeedName,
        string targetProject,
        string targetRepo)
    {
        Credentials = credentials;
        AzureArtifactsFeedName = azureArtifactsFeedName;
        TargetProject = targetProject;
        TargetRepo = targetRepo;
    }
}