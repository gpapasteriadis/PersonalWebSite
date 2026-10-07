# 0002. Upgrade MudBlazor 6 → 9 and drop Blazored.LocalStorage

- **Status**: accepted
- **Date**: 2026-10-08

## Context
The app had moved to .NET 10 but still used MudBlazor 6.15, three majors behind and not built for .NET 10. A visual refresh was planned that would touch nearly every component. `Blazored.LocalStorage` was used for only two values.

## Decision
- Upgrade to MudBlazor 9.x **before** the visual work, so markup is rewritten once against the current API.
- Use MudBlazor's analyzer (`MUD0002`) together with `TreatWarningsAsErrors` to catch invalid parameters.
- Replace `MudLocalizer` with `IStringLocalizer<Resource>` so localization doesn't depend on MudBlazor internals.
- Replace `Blazored.LocalStorage` with two small functions in `wwwroot/js/site.js`. Existing stored values (`BlazorCulture`, `DarkModeIsOn`) stay compatible.

## Consequences
- One fewer dependency, and preferences can be read before the first render (no theme flash).
- Future MudBlazor upgrades: check its migration notes and fix analyzer errors. A build failure points straight at the renamed parameters.
