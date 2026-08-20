# 01-upgrade-personalwebsite-net10: Upgrade PersonalWebSite to net10.0 and align package/runtime compatibility

Upgrade the PersonalWebSite WebAssembly project from `net8.0` to `net10.0`, update recommended framework-related package versions, and apply required code-level compatibility fixes identified by the assessment. This task covers the project file and affected source files where API changes require adjustments.

Scope includes validating restore/build behavior and ensuring the upgraded project remains functionally consistent after behavioral API changes. Because this is an all-at-once single-project upgrade, prerequisite checks and upgrade edits are executed within this same task.

**Done when**: The project targets `net10.0`, recommended package updates are applied, build succeeds without errors/warnings, and tests (if present) pass.

## Scope Inventory
- **Projects affected**: `PersonalWebSite.csproj` (Blazor WebAssembly, SDK-style, current TFM `net8.0`)
- **Distinct concerns**:
  - Target framework retargeting (`net8.0` -> `net10.0`)
  - Explicit package version updates in project file
  - Build/runtime compatibility validation for generated Razor code usage
- **Change signals from assessment**:
  - 25 total issues: 19 mandatory, 6 potential
  - `Project.0002`: target framework must be changed to `net10.0`
  - `NuGet.0002`: recommended updates for `Microsoft.AspNetCore.Components.WebAssembly` and `.DevServer` (and transitive `Microsoft.NET.ILLink.Tasks`)
  - `Api.0001`/`Api.0003`: configuration binder and URI-related compatibility findings in Razor-generated paths

## Research Findings
- `PersonalWebSite.csproj` is the package/version source (no CPM in use); explicit references are defined directly in project file.
- `get_project_dependencies` confirmed package definitions in `PersonalWebSite.csproj` for:
  - `Blazored.LocalStorage` 4.4.0
  - `Microsoft.AspNetCore.Components.WebAssembly` 8.0.1
  - `Microsoft.AspNetCore.Components.WebAssembly.DevServer` 8.0.1
  - `MudBlazor` 6.15.0
- No `// STUB:` markers were found in repository C# files.
- Assessment API findings reference generated Razor output under `obj/`; validation will confirm whether source-level changes are required after retargeting and package update.

## Planned File Changes
- `PersonalWebSite.csproj`
  - Update `<TargetFramework>` to `net10.0`
  - Update explicit `Microsoft.AspNetCore.Components.WebAssembly` package to `10.0.11`
  - Update explicit `Microsoft.AspNetCore.Components.WebAssembly.DevServer` package to `10.0.11`
