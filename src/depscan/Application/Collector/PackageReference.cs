namespace depscan.Application.Collector;

public sealed class PackageReference
{
    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("installed_version")]
    public string InstalledVersion { get; init; }

    [JsonPropertyName("last_version")]
    public string LastVersion { get; set; }

    [JsonPropertyName("stable_version")]
    public string StableVersion { get; set; }

    [JsonPropertyName("updated")]
    public bool Updated => InstalledVersion.Equals(LastVersion, StringComparison.InvariantCulture) ||
                           InstalledVersion.Equals(StableVersion, StringComparison.InvariantCulture);
}