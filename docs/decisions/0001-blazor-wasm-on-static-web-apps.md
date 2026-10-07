# 0001. Blazor WebAssembly hosted on Azure Static Web Apps

- **Status**: accepted
- **Date**: 2026-10-08 (recorded retroactively)

## Context
The site is a personal portfolio with static content and no server-side data. The author works mainly in .NET and wants the site itself to show Blazor skills.

## Decision
Build it as a standalone Blazor WebAssembly app and deploy the static output to Azure Static Web Apps through the GitHub Actions workflow that SWA generates.

## Consequences
- Free or cheap hosting, global CDN, PR preview environments.
- No backend. Contact goes through `mailto:`, and content changes need a redeploy.
- The first load downloads the .NET runtime, which the loading screen covers. SEO is limited to the static `index.html` meta tags.
- Deep links need the `navigationFallback` rewrite in `staticwebapp.config.json`.
