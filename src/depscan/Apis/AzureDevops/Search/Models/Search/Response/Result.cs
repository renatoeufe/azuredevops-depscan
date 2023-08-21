namespace depscan.Apis.AzureDevops.Search.Models.Search.Response;

public class Result
{
    [JsonPropertyName("fileName")]
    public string FileName { get; set; }

    [JsonPropertyName("path")]
    public string Path { get; set; }

    [JsonPropertyName("matches")]
    public Matches Matches { get; set; }

    [JsonPropertyName("collection")]
    public Collection Collection { get; set; }

    [JsonPropertyName("project")]
    public Project Project { get; set; }

    [JsonPropertyName("repository")]
    public Repository Repository { get; set; }

    [JsonPropertyName("versions")]
    public List<Version> Versions { get; set; }

    [JsonPropertyName("contentId")]
    public string ContentId { get; set; }
}