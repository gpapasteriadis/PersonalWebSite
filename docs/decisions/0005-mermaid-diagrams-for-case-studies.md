# 0005. Mermaid diagrams for project case studies

- **Status**: accepted
- **Date**: 2026-10-08

## Context
The Service Booking page showed its system design as three exported PNG images. They didn't follow the light/dark theme, couldn't be translated, and had to be re-exported for every change. The new Fieldmatics case study needs diagrams in English and Greek, and its source repository already keeps its diagrams as Mermaid. The page deliberately keeps them non-technical (no product or protocol names): one shows the path from the field to the grower, one shows the multi-tenant model.

## Decision
- Render diagrams client-side with **Mermaid**. The source of each diagram is a resx value, so every language has its own translated diagram.
- Load Mermaid as an ES module from jsDelivr, pinned to an exact version, **only on first use** (`site.renderDiagram`), so other pages don't download it.
- Theme Mermaid with `theme: 'base'` and variables read from the live MudBlazor CSS palette. Mermaid derives shades from fill colors and ignores alpha, so tints are blended into solid colors in JS.
- `MermaidDiagram` re-renders when the theme changes. Renders run one at a time, because Mermaid's `initialize`/`render` share global state.
- Use `securityLevel: 'strict'`. Diagram sources are author-written resx content, never user input.

## Consequences
- Diagrams are text: easy to review, translate and keep in step with the Fieldmatics docs.
- First view of a case-study page downloads Mermaid (cached afterwards). If the CDN fails, the diagram shows a localized error text and the rest of the page still works.
- Wide diagrams keep a minimum width on phones and scroll sideways inside their card instead of shrinking to unreadable text.
- Upgrading Mermaid means changing `mermaidUrl` in `site.js` and checking every diagram in both themes.
