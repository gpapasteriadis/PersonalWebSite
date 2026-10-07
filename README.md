# George Papasteriadis: Personal Website

My portfolio: experience, projects, skills and contact details, in **English** and **Greek**, with light and dark themes.

Built with **Blazor WebAssembly (.NET 10)** and **MudBlazor 9**, and hosted on **Azure Static Web Apps**.

## Tech stack
| Area | Technology |
|---|---|
| UI framework | Blazor WebAssembly, .NET 10 |
| Component library | MudBlazor 9 |
| Localization | `IStringLocalizer` + `.resx` (en, el) |
| Styling | MudBlazor theme (`Theme/AppTheme.cs`) + `wwwroot/css/app.css`, animate.css keyframes |
| Hosting / CI | Azure Static Web Apps, GitHub Actions |

## Getting started
Prerequisites: [.NET SDK 10](https://dotnet.microsoft.com/download/dotnet/10.0). Visual Studio 2026, VS Code (C# Dev Kit) or Rider are optional.

```bash
git clone https://github.com/gpapasteriadis/PersonalWebSite.git
cd PersonalWebSite
dotnet watch            # http://localhost:5288 with hot reload
```

Other commands:
```bash
dotnet build -c Release     # warnings are treated as errors
dotnet publish -c Release   # static output in bin/Release/net10.0/publish/wwwroot
```

In Visual Studio, open `PersonalWebSite.sln` and press <kbd>F5</kbd>.

## Project structure
```
Components/Sections/   Home-page sections (About, Experience, Projects, Skills, Contact)
Components/Shared/     Reusable building blocks (PortfolioSection, SectionHeader, ProjectCard, TechTags)
Components/            App bar, drawer, language and theme toggles, scroll-to-top
Data/                  Content catalog (jobs, projects, skills, nav, social links) + skill icons
Layout/                MainLayout (theme provider, app bar, drawer)
Pages/                 Home (/) and ServiceBooking (/ServiceBooking)
Resources/             Localized strings (neutral/en/el .resx)
SharedState/           UiState: theme, culture, drawer
Theme/                 AppTheme: palettes and typography
wwwroot/               index.html, css/app.css, js/site.js, images, CV, video
```
For how the pieces fit together, see [ARCHITECTURE.md](ARCHITECTURE.md).

## Updating content
- **Text**: edit the key in **all three** files in `Resources/` (`Localize.Resource.resx`, `.en.resx`, `.el.resx`). Values may contain simple HTML (`<b>`, `<br/>`).
- **A new job**: add the texts to the resx files, the company URL under `JobLinks` in `wwwroot/appsettings.json`, and a `Job` entry in `Data/PortfolioContent.cs` (newest first).
- **A new skill**: add the SVG to `Data/SkillIcons.cs`, the description to the resx files, and a `Skill` entry in `PortfolioContent.Skills`.
- **A project's tech tags**: edit the `Tech` list of the project in `PortfolioContent`.
- **Links / email**: `wwwroot/appsettings.json`.
- **CV**: replace `wwwroot/George-CV.pdf`.

## Deployment
Every push to `main` triggers `.github/workflows/azure-static-web-apps-*.yml`, which builds the app and deploys it to Azure Static Web Apps. Pull requests get a preview environment. `.github/workflows/ci.yml` builds every PR with warnings as errors. `wwwroot/staticwebapp.config.json` rewrites unknown routes to `index.html` so deep links work.

## Working with Claude Code
`CLAUDE.md` holds the project conventions. `.claude/agents/` defines three review agents: `architecture-reviewer` (run after big changes), `i18n-checker` and `ui-reviewer`. Changes are tracked in [CHANGELOG.md](CHANGELOG.md), and design decisions in [docs/decisions/](docs/decisions/).

## License
[MIT](LICENSE)

## Credits
Icons by [flaticon](https://www.flaticon.com/) and [SVG Repo](https://www.svgrepo.com/). UI components by [MudBlazor](https://mudblazor.com/). Animations by [animate.css](https://animate.style/).

## Contact
- Email: giorgospapasteriadis@gmail.com
- LinkedIn: [in/gpapasteriadis](https://www.linkedin.com/in/gpapasteriadis/)
