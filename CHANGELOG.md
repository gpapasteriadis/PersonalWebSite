# Changelog

All notable changes to this project are documented here.
The format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

## [Unreleased]

### Added
- Fieldmatics project: a featured card in Projects and a business-first case-study page at `/Fieldmatics`: the problem it solves, what the SaaS brings, with unfinished features and steps marked “In progress” or “Coming next”, a short “where it stands” note, how it works for a grower, two non-technical Mermaid diagrams (from the field to the grower; one platform, many customers), trust principles and a compact tech list. Fully localized (en/el), including the diagram labels.
- Shared components `FeatureGrid`, `Journey` and `MermaidDiagram`; `site.renderDiagram` lazy-loads Mermaid from the CDN and themes it from the MudBlazor palette (re-rendered on theme change).

### Changed
- `NavItem` takes an optional page, so in-page navigation works on pages other than Home.
- Status chips use the burlywood accent (readable contrast), and focus rings use the primary color in light mode.

### Removed
- The Service Booking project: its card, the `/ServiceBooking` page, its three diagram images and its texts. `/ServiceBooking` now redirects to `/Fieldmatics`.

## 2026-10-08: Visual refresh ([#17](https://github.com/gpapasteriadis/PersonalWebSite/pull/17))

### Added
- `CLAUDE.md`, `ARCHITECTURE.md`, `CHANGELOG.md` and architecture decision records in `docs/decisions/`.
- Claude Code agents: `architecture-reviewer`, `i18n-checker`, `ui-reviewer` (`.claude/agents/`).
- Shared Claude Code permissions (`.claude/settings.json`), `.editorconfig`, PR template.
- CI workflow that builds every PR with warnings as errors.
- Content catalog (`Data/PortfolioContent.cs`, `Models/`) and shared components (`PortfolioSection`, `SectionHeader`, `ProjectCard`, `TechTags`).
- `wwwroot/js/site.js`: preferences, hide-on-scroll app bar, scroll-reveal, active-section highlighting in the nav.
- Styled and localized 404 page, navigation on the `/ServiceBooking` page, SEO and Open Graph meta tags, localized `aria-label`s and gallery alt text.

### Changed
- MudBlazor 6.15 → 9.11. Localization uses `IStringLocalizer<Resource>` instead of `MudLocalizer`.
- Visual refresh within the existing identity: single theme in `Theme/AppTheme.cs` with improved light and dark contrast, fluid typography, hero section, timeline cards, project card grid, skill chips with brand icons, contact card, themed loading screen.
- Theme and language are applied before the first render (no flash), and the system dark-mode preference is respected when nothing is stored.
- Video and images load lazily (`preload="metadata"`, `loading="lazy"`).
- Project tech tags show ".NET 10" instead of ".NET8".
- Nullable warnings fixed; the build now treats warnings as errors.
- Bumped `actions/checkout` to v4 in the deploy workflow.

### Removed
- Bootstrap CSS, `Blazored.LocalStorage`, unused images (`optimization.png`, `ServiceBookingDesign.png`), template CSS, the unused `Contact1` resource and the `.github/upgrades` tooling leftovers.
