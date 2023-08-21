namespace depscan;

public sealed class Summary
{
    [JsonPropertyName("packages")]
    public IEnumerable<string>? Packages { get; set; }

    [JsonPropertyName("projects")]
    public List<SummaryProject>? Projects { get; set; }

    public sealed class SummaryProject
    {
        [JsonPropertyName("project")]
        public string? Project { get; init; }

        [JsonPropertyName("repositories")]
        public List<SummaryRepo>? Repos { get; set; }
    }

    public sealed class SummaryRepo
    {
        [JsonPropertyName("repository")]
        public string? Repository { get; init; }

        [JsonPropertyName("paths")]
        public List<SummaryPath>? Paths { get; set; }
    }

    public sealed class SummaryPath
    {
        [JsonPropertyName("path")]
        public string? Path { get; init; }

        [JsonPropertyName("packages")]
        public List<PackageReference>? Packages { get; set; }
    }
}