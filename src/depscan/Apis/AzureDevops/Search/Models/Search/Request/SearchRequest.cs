namespace depscan.Apis.AzureDevops.Search.Models.Search.Request;

public sealed record SearchRequest
{
    [JsonPropertyName("searchText")] public string SearchText { get; set; }
    [JsonPropertyName("$skip")] public int Skip { get; init; }
    [JsonPropertyName("$top")] public int Top { get; init; } = 1000;
}