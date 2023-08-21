using System.Xml.Linq;

namespace depscan.Application.Collector;

public static class ProjectFileReader
{
    private const string ItemGroup = "ItemGroup";
    private const string PackageReference = "PackageReference";
    private const string IncludeAttribute = "Include";
    private const string VersionAttribute = "Version";
    private const string NotFound = "NotFound";

    public static IEnumerable<PackageReference> Read(string? text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return Enumerable.Empty<PackageReference>();
        }

        var document = XDocument.Parse(text);
        if (document.Root == null)
        {
            return Enumerable.Empty<PackageReference>();
        }

        return document.Root
            .Elements(ItemGroup)
            .Elements(PackageReference)
            .Select(x => new PackageReference
            {
                Name = x.GetAttribute(IncludeAttribute),
                InstalledVersion = x.GetAttribute(VersionAttribute),
                LastVersion = NotFound,
                StableVersion = NotFound
            });
    }

    private static string GetAttribute(this XElement that, string attribute)
    {
        var attr = that.Attribute(attribute);
        if (attr == null) return string.Empty;
        return (string)attr;
    }
}