# 0004. One theme, palette-driven colors and CSS tokens

- **Status**: accepted
- **Date**: 2026-10-08

## Context
The theme was defined inside `MainLayout`, while other components injected a separate default `MudTheme` for font sizes. Colors such as `burlywood` and `black` were inlined in about eight places. In dark mode some icons were invisible (a black SVG, and an `ActionDefault` close to the background), and in light mode cards had the same color as the page.

## Decision
- A single static `AppTheme.Theme` (`Theme/AppTheme.cs`) defines light and dark palettes. The identity colors stay: teal for light, blue-grey for dark, burlywood as `Tertiary`.
- Markup uses `Color.*` parameters only. CSS uses `var(--mud-palette-*)` and a small set of `--app-*` tokens in `app.css`.
- Light `Surface` is white on a tinted `Background`, so cards stand out. Text/background pairs are chosen for WCAG AA contrast.
- `site.js` sets `data-theme` on `<html>` before Blazor starts, so the loading screen matches the user's theme.

## Consequences
- Both themes come from one place. A new component gets dark mode for free if it follows the rules.
- Hard-coded colors are review findings (see the `ui-reviewer` and `architecture-reviewer` agents).
