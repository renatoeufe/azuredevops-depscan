using depscan.Apis;
using depscan.Application;
using depscan.Application.Collector;
using depscan.Application.Scanner;
using depscan.Application.VersionChecker;
using System.CommandLine;
using System.Diagnostics;
using System.Text.Json;
using depscan.Model.Output;

var userOption = new Option<string>(
    "--user",
    description: "username@pottencial.com.br");

var accessTokenOption = new Option<string>(
    "--accesstoken",
    "Your access token");

var organizationOption = new Option<string>(
    "--organization",
    getDefaultValue: () => "pottencial",
    description: "Your organization name");

var artifactsFeedOption = new Option<string>(
    "--feed",
    getDefaultValue: () => "Pottencial",
    description: "Azure Artifacts feed name");

var projectOption = new Option<string>(
    "--project",
    getDefaultValue: () => "*",
    description: "Search within the informed project");

var repoOption = new Option<string>(
    "--repo",
    getDefaultValue: () => "*",
    description: "Search within the informed repository");

var verboseOption = new Option<bool>(
    "--verbose",
    getDefaultValue: () => true,
    description: "Enable verbose output");

var rootCommand = new RootCommand
{
    userOption,
    accessTokenOption,
    organizationOption,
    artifactsFeedOption,
    projectOption,
    repoOption,
    verboseOption
};

rootCommand.Description = "Depscan CLI";

var parseResult = rootCommand.Parse(args);
if (parseResult.Errors.Count > 0)
{
    Console.WriteLine(string.Join(",", parseResult.Errors.Select(x => x.Message)));
}

var user = parseResult.GetValueForOption(userOption);
var accessToken = parseResult.GetValueForOption(accessTokenOption);
var organization = parseResult.GetValueForOption(organizationOption);
var feed = parseResult.GetValueForOption(artifactsFeedOption) ?? string.Empty;
var project = parseResult.GetValueForOption(projectOption) ?? "*";
var repo = parseResult.GetValueForOption(repoOption) ?? "*";
var verbose = parseResult.GetValueForOption(verboseOption);

if (string.IsNullOrWhiteSpace(user) ||
    string.IsNullOrWhiteSpace(accessToken) ||
    string.IsNullOrWhiteSpace(organization))
{
    Console.WriteLine("Required params: --user --accesstoken --organization");
    return;
}

if (verbose)
{
    Console.WriteLine(
        @$"
    --user is: {user}
    --accessToken is: {accessToken}
    --organization is: {organization}
    --project is: {project}
    --repo is: {repo}");
}

var scanParameters = new ScanParameters(new Credentials(user, accessToken, organization), feed, project, repo);
var clients = new Clients(organization);

IPackageCollector packageCollector = new PackageCollector(clients, scanParameters);
IPackageVersionChecker packageVersionChecker = new PackageVersionChecker(clients, scanParameters);
IScanner scanner = new DefaultScanner(packageCollector, packageVersionChecker);

// run
var sw = Stopwatch.StartNew();
var projectFiles = await scanner.Run().ConfigureAwait(false);
sw.Stop();

if (verbose)
{
    Console.WriteLine($"Data retrieved in {sw.Elapsed:g} ... generating output ...");
}

// generate output
sw = Stopwatch.StartNew();
IOutput output = new SummaryOutput();
var summary = output.GenerateSummary(projectFiles, packageVersionChecker.GetCachedPackageNames());
sw.Stop();

static string Serialize(object x) =>
    JsonSerializer.Serialize(x, new JsonSerializerOptions
    {
        WriteIndented = true
    });

Console.WriteLine(Serialize(summary));

if (verbose)
{
    Console.WriteLine($"Output rendered in {sw.Elapsed:g}");
}