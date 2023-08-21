# Azure Devops Depscan

Azure Devops Depscan is a command-line interface (CLI) tool built with .NET Core, designed to search for outdated NuGet packages within an Azure DevOps environment. By utilizing Azure DevOps search APIs, it allows you to efficiently scan without downloading the entire codebase.

## Features

- Scan projects in Azure DevOps for outdated NuGet packages.
- Quick analysis without the need for downloading the whole codebase.
- Easy-to-use command-line interface.

## Prerequisites

- NET6 or higher
- Access to Azure DevOps with appropriate permissions.

## Installation

1. Clone the repository.

```bash
git clone https://github.com/renatoeufe/azuredevops-depscan.git
```

2. Navigate to the project folder.

```bash
cd azuredevops-depscan
cd src
```
Build the project.
```bash
dotnet build
```

## Usage

Use the following command to search for outdated NuGet packages:

```bash
depscan-cli-netcore 
  --user your_email (required)
  --accesstoken your_azure_devops_personal_access_token (required)
  --organization your_org_name (required)
  --feed your_feed_name (optional)
```

```bash
depscan-cli-netcore 
  --user your_email
  --accesstoken your_azure_devops_personal_access_token 
  --organization your_org_name 
  --feed your_feed_name (optional)
  --project azure_devops_project_name (optional)
  --repo azure_devops_repo_name (optional)
```

## Contributing
We welcome contributions! 