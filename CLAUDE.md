# CLAUDE.md

Guidance for Claude Code (and humans) working in this repository. Keep this file in step with the code: update it in the same change whenever the layout, a convention, the checklist or a gotcha changes.

## Project
Personal portfolio site of Giorgos Papasteriadis: a single-page **Blazor WebAssembly** app (.NET 10) using **MudBlazor 9**, localized in **English and Greek**, and deployed as static files to **Azure Static Web Apps** on every push to `main`.

See [ARCHITECTURE.md](ARCHITECTURE.md) for how it fits together and [docs/decisions/](docs/decisions/) for why.

## Commands
```bash
dotnet restore
dotnet watch                       # dev server with hot reload (http://localhost:5288)
dotnet build -c Release            # must stay at 0 warnings (warnings are errors)
dotnet publish -c Release          # output: bin/Release/net10.0/publish/wwwroot
sh docs/cv/build-cv.sh             # regenerate wwwroot/George-CV.pdf from docs/cv/George-CV.html
```
There is no test project. Verify changes by building **and running** the site (see the checklist below).

## Layout
```
Program.cs                    DI + culture bootstrap (reads culture from localStorage before startup)
App.razor                     Router + styled 404
Layout/MainLayout.razor       Theme provider, app bar, drawer, scroll-to-top. Shared by all pages
Pages/                        Home (/), Fieldmatics (/Fieldmatics case study), ServiceBookingRedirect (/ServiceBooking → /Fieldmatics)
Components/Sections/          One component per home-page section (About, Experience, Projects, Skills, Contact)
Components/Shared/            Reusable building blocks (PortfolioSection, SectionHeader, ProjectCard, TechTags, FeatureGrid, Journey, MermaidDiagram)
Components/*.razor            App-bar pieces (SiteAppBar, CultureSelector, ThemeSelector, SideBar, ScrollToTop)
Data/PortfolioContent.cs      The content catalog: jobs, projects, skills, nav items, social links, CV/avatar/logo paths
Data/FieldmaticsContent.cs    Content of the /Fieldmatics case study (value, journey, diagrams, trust principles)
Data/SkillIcons.cs            Inline SVG icons for skill chips and the skill snackbar
Models/PortfolioModels.cs     Records used by the catalog
Theme/AppTheme.cs             The single MudTheme (light + dark palettes, typography, radius)
SharedState/UiState.cs        App-wide UI state: dark mode, drawer, culture toggle, GoHome (logo = full reload of /)
Localize/                     Resource.cs (marker for IStringLocalizer<Resource>) + LocalizerExtensions.cs (L.Html(key))
Resources/*.resx              Localized strings: neutral (= English), .en, .el
wwwroot/index.html            Host page: meta tags, CSS/JS includes, loading screen
wwwroot/js/site.js            All JS interop (preferences, app-bar hide, scroll reveal, scrollspy, scrollToHash, Mermaid diagrams)
wwwroot/css/app.css           Design tokens + global styles + animations
wwwroot/appsettings.json      External links (jobs, socials, projects) and email
wwwroot/George-CV.pdf         Generated CV (do not edit; see docs/cv/)
docs/cv/                      CV source (George-CV.html) + build-cv.sh (headless Edge/Chrome → PDF, bumps the ?v= in CvPath)
docs/decisions/               ADRs (0001–0006) + template
.claude/agents/               Review agents: architecture-reviewer, i18n-checker, ui-reviewer
```

## Conventions
- **Don't change displayed information** (texts, dates, links, job/skill lists) unless the user asks for it. Visual and code changes are fine.
- **MudBlazor first**: build UI from MudBlazor components (`MudButton`, `MudLink`, `MudIcon`, `MudText`, `MudGrid`, `MudChip`, …) and restyle them with a `Class` + CSS, rather than hand-rolling `<a>`, `<button>` or `<div>` widgets. Use plain HTML only where no component fits (e.g. text/lists inside resx values).
- **Text**: every user-visible string comes from `IStringLocalizer<Resource>` (injected as `L`). When you add or rename a key, update **all three** resx files. Values may contain HTML; render them with `L.Html(key)`.
- **Rich descriptions**: job and project descriptions in resx are structured HTML: `<p class="rich__lead">` (optional intro), `<p class="rich__group">` (small heading), `<ul class="rich__list" role="list"><li>…</li></ul>`. No `<br/>` + `•` bullets. Render them in `MudText HtmlTag="div"` (a `<p>` can't contain a `<ul>`). Keep en and el structurally identical.
- **Content**: jobs, projects, skills and nav items are data in `Data/PortfolioContent.cs`; a project case-study page has its own catalog (e.g. `Data/FieldmaticsContent.cs`). Components loop over it. Don't hand-write repeated markup per item or per breakpoint.
- **Links/email**: read from `IConfiguration` (`wwwroot/appsettings.json`), never hard-coded.
- **CV**: edit `docs/cv/George-CV.html`, then run `sh docs/cv/build-cv.sh`; it rewrites the PDF and the `?v=` in `PortfolioContent.CvPath`. Commit both. Keep it to one A4 page. Job facts also live in the resx keys (`IndeavorDesc`, `PwCDesc`, …); update both when facts change.
- **Colors**: never inline (`style="color:burlywood"`, hex). Use MudBlazor `Color.*`, `var(--mud-palette-*)`, or the `--app-*` tokens in `app.css`. The burlywood accent is `Color.Tertiary` / `var(--mud-palette-tertiary)`; it is under 3:1 on light surfaces, so light mode uses primary for focus rings and list dots (`:root:not([data-theme="dark"])`).
- **Typography**: pick a fixed `Typo`. Responsive sizes come from the theme's `clamp()` values. Don't add per-breakpoint `Typo` switch methods or hard-code font sizes on components.
- **Responsive**: prefer `MudGrid` `xs/sm/md` and CSS. Use `MudHidden` only when mobile and desktop need genuinely different widgets, and then both branches must render the same data.
- **Sections**: wrap a page section in `<PortfolioSection Id="..." Title="...">`. It handles the anchor, container width, header and scroll reveal.
- **State**: subscribe to `UiState` events in `OnInitialized` and unsubscribe in `Dispose` (`@implements IDisposable`). No `async void`.
- **JS**: add functions to `wwwroot/js/site.js` under `window.site`. No inline scripts and no `window.onscroll =` assignments. Remove functions that are no longer called.
- **Diagrams**: use `<MermaidDiagram>` with the Mermaid source in a resx key (one per language, labels translated). Never put markup inside its canvas; JS owns it.
- **Private projects**: Fieldmatics is a private, proprietary repository. Its page is business-first (problem, value, how it works) with light technical detail: no code, configuration, protocol/topic names, internal decision IDs or repository links. Mark roadmap features as planned; never present them as done.
- **Accessibility**: icon-only buttons need `aria-label`; images need `alt`; decorative icons are `aria-hidden`; `target="_blank"` links need `rel="noopener"` and a visually hidden `@L["AriaOpensNewTab"]`; tap targets are at least 48px; hover effects go inside `@media (hover: hover)`; animations must respect `prefers-reduced-motion`.
- **Nullable** is on and warnings fail the build. Fix them; don't suppress them.
- MudBlazor ships an analyzer: `MUD0002` warnings mean a parameter name is wrong for this MudBlazor version.

## Workflow
- Work on a feature branch off `main`. `main` deploys to production.
- Every PR gets a staging (preview) environment from the Static Web Apps workflow; the bot comments its URL on the PR (`https://<app>-<PR number>.<region>.azurestaticapps.net`). When you open or update a PR, wait for "Build and Deploy Job" to pass and give the user both the PR link and the staging link.
- After any change: `dotnet build -c Release`, then run the app and check it in a browser (no console errors) before calling it done.
- After any **big implementation** (multi-file change, new section or component, refactor, dependency upgrade), run the **`architecture-reviewer`** agent and address its findings.
- When `.resx` files or visible text change, run the **`i18n-checker`** agent.
- After UI changes, run the **`ui-reviewer`** agent.
- Record user-facing or structural changes in `CHANGELOG.md` under `[Unreleased]`. Update `ARCHITECTURE.md` and this file when structure or conventions change, and add an ADR in `docs/decisions/` for significant decisions.

## Manual verification checklist
Run the site and check at ~375px and ~1440px: English and Greek, light and dark (reload to confirm the theme persists without a flash), nav links scroll to and highlight sections, the drawer works on mobile, the logo reloads the home page at the top, skill chips open the snackbar, Projects shows Fieldmatics first, the mobile-app card flips to the gallery, the "View resume" button opens the CV in a new tab, `/Fieldmatics` renders with nav and both diagrams in both themes, `/ServiceBooking` redirects to it, and an unknown URL shows the 404 page.

## Gotchas
- Culture switching writes `BlazorCulture` to localStorage and **reloads** the app. `Program.cs` reads it before `RunAsync`. The theme is stored as `DarkModeIsOn`.
- Run **one** dev server at a time. A leftover `dotnet watch` keeps rebuilding `bin/Debug` under another server, which makes `_framework/*.wasm` requests 404 and fail SRI ("Failed to find a valid digest"). Chrome then caches the 404 (Blazor fetches with `force-cache`): fix with DevTools → right-click reload → "Empty Cache and Hard Reload".
- Hot reload can't apply resx changes; restart the app after editing `.resx` files.
- `wwwroot/staticwebapp.config.json` rewrites unknown routes to `index.html`, so deep links like `/Fieldmatics` work in production.
- The SWA deploy workflow builds the app itself (Oryx). `.github/workflows/ci.yml` only verifies the build on PRs. Neither generates the CV; the committed PDF is what ships.
- Azure builds with an older SDK (10.0.200) than a dev machine or CI (latest 10.0.x), and its Razor compiler is stricter. Never name a Razor variable after a directive keyword (`section`, `code`, `functions`, `inject`, …): `@section.Href` compiled locally but broke the production deploy.
- Mermaid is pinned in `site.js` (`mermaidUrl`) and loaded from jsDelivr only on pages with diagrams. Mermaid derives shades from fill colors and ignores alpha, so pass solid colors (see `blend`).
- `wwwroot/Video/FoodWasteApp.mp4` is ~30 MB. Keep `preload="metadata"` (first frame only) and never autoplay it.
- The app bar hides while scrolling down (`site.js`); scroll up to reach the logo and nav.
