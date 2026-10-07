# 0003. Content catalog and responsive markup without duplication

- **Status**: accepted
- **Date**: 2026-10-08

## Context
Experience, Projects and Skills rendered every item twice (one `MudHidden` branch for phones, one for larger screens), and seven components carried copy-pasted per-breakpoint `Typo` switch methods. Adding a job meant editing two near-identical markup blocks, and the skill details lived in a `switch` statement inside a component.

## Decision
- Describe content as records (`Models/PortfolioModels.cs`) in one catalog (`Data/PortfolioContent.cs`). The catalog holds resx keys and configuration keys, never display text.
- Sections loop over the catalog. Where phone and desktop need different widgets, both branches share one item template.
- Responsive font sizes live in the theme (`clamp()`). Components use fixed `Typo` values.

## Consequences
- Adding a job, skill or tech tag touches the catalog plus the resx files, with no markup changes.
- Text stays fully localizable. Visible text never lives in the catalog, except proper nouns (company and technology names).
- Ordering and grouping are explicit in one place.
