# CLAUDE.md

Guidance for Claude Code (and humans) working in this repository.

## Project
Personal portfolio site of Giorgos Papasteriadis: a single-page **Blazor WebAssembly** app (.NET 10) using **MudBlazor 9**, localized in **English and Greek**, and deployed as static files to **Azure Static Web Apps** on every push to `main`.

See [ARCHITECTURE.md](ARCHITECTURE.md) for how it fits together and [docs/decisions/](docs/decisions/) for why.

## Commands
```bash
dotnet restore
dotnet watch                       # dev server with hot reload (http://localhost:5288)
dotnet build -c Release            # must stay at 0 warnings (warnings are errors)
dotnet publish -c Release          # output: bin/Release/net10.0/publish/wwwroot
```
There is no test project. Verify changes by running the site (see the checklist below).

## Layout
```
Program.cs                 DI + culture bootstrap (reads culture from localStorage before startup)
App.razor                  Router + styled 404
Layout/MainLayout.razor    Theme provider, app bar, drawer, scroll-to-top. Shared by all pages
Pages/                     Home (/) and ServiceBooking (/ServiceBooking)
Components/Sections/       One component per home-page section (About, Experience, Projects, Skills, Contact)
Components/Shared/         Reusable building blocks (PortfolioSection, SectionHeader, ProjectCard, TechTags)
Components/*.razor         App-bar pieces (SiteAppBar, CultureSelector, ThemeSelector, SideBar, ScrollToTop)
Data/PortfolioContent.cs   The content catalog: jobs, projects, skills, nav items
Models/                    Records used by the catalog
Theme/AppTheme.cs          The single MudTheme (light + dark palettes, typography, radius)
SharedState/UiState.cs     App-wide UI state (dark mode, drawer), with change events
Localize/                  Resource (marker for IStringLocalizer<Resource>) + L.Html(key) for resx values with HTML
Resources/*.resx           Localized strings: neutral (= English), .en, .el
wwwroot/index.html         Host page: meta tags, CSS/JS includes, loading screen
wwwroot/js/site.js         All JS interop (culture storage, app-bar hide, scroll reveal, scrollspy)
wwwroot/css/app.css        Design tokens + global styles + animations
wwwroot/appsettings.json   External links (jobs, socials, projects) and email
```

## Conventions
- **Don't change displayed information** (texts, dates, links, job/skill lists) unless the user asks for it. Visual and code changes are fine.
- **Text**: every user-visible string comes from `IStringLocalizer<Resource>` (injected as `L`). When you add or rename a key, update **all three** resx files. Values may contain HTML; render them with `(MarkupString)`.
- **Content**: jobs, projects, skills and nav items are data in `Data/PortfolioContent.cs`. Components loop over it. Don't hand-write repeated markup per item or per breakpoint.
- **Links/email**: read from `IConfiguration` (`wwwroot/appsettings.json`), never hard-coded.
- **Colors**: never inline (`style="color:burlywood"`, hex). Use MudBlazor `Color.*`, `var(--mud-palette-*)`, or the `--app-*` tokens in `app.css`. The burlywood accent is `Color.Tertiary` / `var(--mud-palette-tertiary)`.
- **Typography**: pick a fixed `Typo`. Responsive sizes come from the theme's `clamp()` values. Don't add per-breakpoint `Typo` switch methods.
- **Responsive**: prefer `MudGrid` `xs/sm/md` and CSS. Use `MudHidden` only when mobile and desktop need genuinely different widgets, and then both branches must render the same data.
- **Sections**: wrap a home-page section in `<PortfolioSection Id="..." Title="...">`. It handles the anchor, container width, header and scroll reveal.
- **State**: subscribe to `UiState` events in `OnInitialized` and unsubscribe in `Dispose` (`@implements IDisposable`). No `async void`.
- **JS**: add functions to `wwwroot/js/site.js` under `window.site`. No inline scripts and no `window.onscroll =` assignments.
- **Accessibility**: icon-only buttons need `aria-label`; images need `alt`; `target="_blank"` links need `rel="noopener"`; animations must respect `prefers-reduced-motion`.
- **Nullable** is on and warnings fail the build. Fix them; don't suppress them.
- MudBlazor ships an analyzer: `MUD0002` warnings mean a parameter name is wrong for this MudBlazor version.

## Workflow
- Work on a feature branch off `main`. `main` deploys to production.
- After any **big implementation** (multi-file change, new section or component, refactor, dependency upgrade), run the **`architecture-reviewer`** agent and address its findings.
- When `.resx` files or visible text change, run the **`i18n-checker`** agent.
- After UI changes, run the **`ui-reviewer`** agent.
- Record user-facing or structural changes in `CHANGELOG.md` under `[Unreleased]`. Update `ARCHITECTURE.md` when structure changes, and add an ADR in `docs/decisions/` for significant decisions.

## Manual verification checklist
Run `dotnet watch` and check at ~375px and ~1440px: English and Greek, light and dark (reload to confirm the theme persists without a flash), nav links scroll to and highlight sections, the drawer works on mobile, skill chips open the snackbar, the mobile-app card flips to the gallery, the CV downloads, `/ServiceBooking` renders with nav, and an unknown URL shows the 404 page.

## Gotchas
- Culture switching writes `BlazorCulture` to localStorage and **reloads** the app. `Program.cs` reads it before `RunAsync`.
- `wwwroot/staticwebapp.config.json` rewrites unknown routes to `index.html`, so deep links like `/ServiceBooking` work in production.
- The SWA deploy workflow builds the app itself (Oryx). `.github/workflows/ci.yml` only verifies the build on PRs.
- `wwwroot/Video/FoodWasteApp.mp4` is ~30 MB. Keep `preload="metadata"` (first frame only) and never autoplay it.
