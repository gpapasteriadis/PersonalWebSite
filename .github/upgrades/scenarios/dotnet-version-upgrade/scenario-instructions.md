# .NET Version Upgrade

## Strategy
**Selected**: All-At-Once
**Rationale**: Single SDK-style Blazor WebAssembly project already on modern .NET (net8.0), with straightforward TFM/package updates and no inter-project dependency tiers.

### Execution Constraints
- Upgrade in one atomic pass: project TFM update, package updates, then code compatibility fixes.
- Validate with full solution build and tests after upgrade changes are complete.
- Resolve API compatibility issues inline during upgrade; avoid deferring stub-based follow-ups.
- Keep scope focused on automated, verifiable changes required for net10.0 compatibility.

## Preferences
- **Flow Mode**: Automatic
- **Target Framework**: net10.0

## User Preferences
### Technical Preferences
- Exclude .github upgrade artifact file references from project/repo contents when possible.

## Upgrade Options
**Source**: .github/upgrades/scenarios/dotnet-version-upgrade/upgrade-options.md

### Strategy
- Upgrade Strategy: All-at-Once

### Compatibility
- Unsupported API Handling: Fix Inline

## Source Control
- **Source Branch**: feature/Add_New_Job_Description
- **Working Branch**: dotnet-version-upgrade-net10
- **Commit Strategy**: Single Commit at End
- **Branch Sync**: Auto (Merge)
