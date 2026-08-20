# Projects and dependencies analysis

This document provides a comprehensive overview of the projects and their dependencies in the context of upgrading to .NETCoreApp,Version=v10.0.

## Table of Contents

- [Executive Summary](#executive-Summary)
  - [Highlevel Metrics](#highlevel-metrics)
  - [Projects Compatibility](#projects-compatibility)
  - [Package Compatibility](#package-compatibility)
  - [API Compatibility](#api-compatibility)
  - [Binding Redirect Configuration](#binding-redirect-configuration)
- [Aggregate NuGet packages details](#aggregate-nuget-packages-details)
- [Top API Migration Challenges](#top-api-migration-challenges)
  - [Technologies and Features](#technologies-and-features)
  - [Most Frequent API Issues](#most-frequent-api-issues)
- [Projects Relationship Graph](#projects-relationship-graph)
- [Project Details](#project-details)

  - [PersonalWebSite.csproj](#personalwebsitecsproj)


## Executive Summary

### Highlevel Metrics

| Metric | Count | Status |
| :--- | :---: | :--- |
| Total Projects | 1 | All require upgrade |
| Total NuGet Packages | 33 | 3 need upgrade |
| Total Code Files | 3 |  |
| Total Code Files with Incidents | 5 |  |
| Total Lines of Code | 106 |  |
| Total Number of Issues | 25 |  |
| Estimated LOC to modify | 21+ | at least 19,8% of codebase |

### Projects Compatibility

| Project | Target Framework | Difficulty | Package Issues | API Issues | Binding Issues | Est. LOC Impact | Description |
| :--- | :---: | :---: | :---: | :---: | :---: | :---: | :--- |
| [PersonalWebSite.csproj](#personalwebsitecsproj) | net8.0 | 🟢 Low | 3 | 21 | 0 | 21+ | AspNetCore, Sdk Style = True |

### Package Compatibility

| Status | Count | Percentage |
| :--- | :---: | :---: |
| ✅ Compatible | 30 | 90,9% |
| ⚠️ Incompatible | 0 | 0,0% |
| 🔄 Upgrade Recommended | 3 | 9,1% |
| ***Total NuGet Packages*** | ***33*** | ***100%*** |

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 18 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 3 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 10116 |  |
| ***Total APIs Analyzed*** | ***10137*** |  |

## Aggregate NuGet packages details

| Package | Current Version | Suggested Version | Projects | Description |
| :--- | :---: | :---: | :--- | :--- |
| Blazored.LocalStorage | 4.4.0 |  | [PersonalWebSite.csproj](#personalwebsitecsproj) | ✅Compatible |
| Microsoft.AspNetCore.Authorization | 8.0.1 |  | [PersonalWebSite.csproj](#personalwebsitecsproj) | ✅Compatible |
| Microsoft.AspNetCore.Components | 8.0.1 |  | [PersonalWebSite.csproj](#personalwebsitecsproj) | ✅Compatible |
| Microsoft.AspNetCore.Components.Analyzers | 8.0.1 |  | [PersonalWebSite.csproj](#personalwebsitecsproj) | ✅Compatible |
| Microsoft.AspNetCore.Components.Forms | 8.0.1 |  | [PersonalWebSite.csproj](#personalwebsitecsproj) | ✅Compatible |
| Microsoft.AspNetCore.Components.Web | 8.0.1 |  | [PersonalWebSite.csproj](#personalwebsitecsproj) | ✅Compatible |
| Microsoft.AspNetCore.Components.WebAssembly | 8.0.1 | 10.0.11 | [PersonalWebSite.csproj](#personalwebsitecsproj) | NuGet package upgrade is recommended |
| Microsoft.AspNetCore.Components.WebAssembly.DevServer | 8.0.1 | 10.0.11 | [PersonalWebSite.csproj](#personalwebsitecsproj) | NuGet package upgrade is recommended |
| Microsoft.AspNetCore.Metadata | 8.0.1 |  | [PersonalWebSite.csproj](#personalwebsitecsproj) | ✅Compatible |
| Microsoft.Extensions.Configuration | 8.0.0 |  | [PersonalWebSite.csproj](#personalwebsitecsproj) | ✅Compatible |
| Microsoft.Extensions.Configuration.Abstractions | 8.0.0 |  | [PersonalWebSite.csproj](#personalwebsitecsproj) | ✅Compatible |
| Microsoft.Extensions.Configuration.Binder | 8.0.1 |  | [PersonalWebSite.csproj](#personalwebsitecsproj) | ✅Compatible |
| Microsoft.Extensions.Configuration.FileExtensions | 8.0.0 |  | [PersonalWebSite.csproj](#personalwebsitecsproj) | ✅Compatible |
| Microsoft.Extensions.Configuration.Json | 8.0.0 |  | [PersonalWebSite.csproj](#personalwebsitecsproj) | ✅Compatible |
| Microsoft.Extensions.DependencyInjection | 8.0.0 |  | [PersonalWebSite.csproj](#personalwebsitecsproj) | ✅Compatible |
| Microsoft.Extensions.DependencyInjection.Abstractions | 8.0.0 |  | [PersonalWebSite.csproj](#personalwebsitecsproj) | ✅Compatible |
| Microsoft.Extensions.FileProviders.Abstractions | 8.0.0 |  | [PersonalWebSite.csproj](#personalwebsitecsproj) | ✅Compatible |
| Microsoft.Extensions.FileProviders.Physical | 8.0.0 |  | [PersonalWebSite.csproj](#personalwebsitecsproj) | ✅Compatible |
| Microsoft.Extensions.FileSystemGlobbing | 8.0.0 |  | [PersonalWebSite.csproj](#personalwebsitecsproj) | ✅Compatible |
| Microsoft.Extensions.Localization | 8.0.1 |  | [PersonalWebSite.csproj](#personalwebsitecsproj) | ✅Compatible |
| Microsoft.Extensions.Localization.Abstractions | 8.0.1 |  | [PersonalWebSite.csproj](#personalwebsitecsproj) | ✅Compatible |
| Microsoft.Extensions.Logging | 8.0.0 |  | [PersonalWebSite.csproj](#personalwebsitecsproj) | ✅Compatible |
| Microsoft.Extensions.Logging.Abstractions | 8.0.0 |  | [PersonalWebSite.csproj](#personalwebsitecsproj) | ✅Compatible |
| Microsoft.Extensions.Options | 8.0.1 |  | [PersonalWebSite.csproj](#personalwebsitecsproj) | ✅Compatible |
| Microsoft.Extensions.Primitives | 8.0.0 |  | [PersonalWebSite.csproj](#personalwebsitecsproj) | ✅Compatible |
| Microsoft.JSInterop | 8.0.1 |  | [PersonalWebSite.csproj](#personalwebsitecsproj) | ✅Compatible |
| Microsoft.JSInterop.WebAssembly | 8.0.1 |  | [PersonalWebSite.csproj](#personalwebsitecsproj) | ✅Compatible |
| Microsoft.NET.ILLink.Tasks | 8.0.30 | 10.0.11 | [PersonalWebSite.csproj](#personalwebsitecsproj) | NuGet package upgrade is recommended |
| Microsoft.NET.Sdk.WebAssembly.Pack | 10.0.11 |  | [PersonalWebSite.csproj](#personalwebsitecsproj) | ✅Compatible |
| MudBlazor | 6.15.0 |  | [PersonalWebSite.csproj](#personalwebsitecsproj) | ✅Compatible |
| System.IO.Pipelines | 8.0.0 |  | [PersonalWebSite.csproj](#personalwebsitecsproj) | ✅Compatible |
| System.Text.Encodings.Web | 8.0.0 |  | [PersonalWebSite.csproj](#personalwebsitecsproj) | ✅Compatible |
| System.Text.Json | 8.0.0 |  | [PersonalWebSite.csproj](#personalwebsitecsproj) | ✅Compatible |

## Top API Migration Challenges

### Technologies and Features

| Technology | Issues | Percentage | Migration Path |
| :--- | :---: | :---: | :--- |

### Most Frequent API Issues

| API | Count | Percentage | Category |
| :--- | :---: | :---: | :--- |
| M:Microsoft.Extensions.Configuration.ConfigurationBinder.GetValue''1(Microsoft.Extensions.Configuration.IConfiguration,System.String) | 18 | 85,7% | Binary Incompatible |
| T:System.Uri | 2 | 9,5% | Behavioral Change |
| M:System.Uri.#ctor(System.String) | 1 | 4,8% | Behavioral Change |

## Projects Relationship Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart LR
    P1["<b>📦&nbsp;PersonalWebSite.csproj</b><br/><small>net8.0</small>"]
    click P1 "#personalwebsitecsproj"

```

## Project Details

<a id="personalwebsitecsproj"></a>
### PersonalWebSite.csproj

#### Project Info

- **Current Target Framework:** net8.0
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** AspNetCore
- **Dependencies**: 0
- **Dependants**: 0
- **Number of Files**: 39
- **Number of Files with Incidents**: 5
- **Lines of Code**: 106
- **Estimated LOC to modify**: 21+ (at least 19,8% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph current["PersonalWebSite.csproj"]
        MAIN["<b>📦&nbsp;PersonalWebSite.csproj</b><br/><small>net8.0</small>"]
        click MAIN "#personalwebsitecsproj"
    end

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 18 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 3 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 10116 |  |
| ***Total APIs Analyzed*** | ***10137*** |  |

