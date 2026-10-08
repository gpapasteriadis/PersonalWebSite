# 0006. CV generated from an HTML source

- **Status**: accepted
- **Date**: 2026-10-08

## Context
The downloadable CV (`wwwroot/George-CV.pdf`) was exported from an online resume builder. The repository had only the PDF, so adding a job (Indeavor) or changing the design meant rebuilding it by hand somewhere else. Its colors also didn't match the site. Browsers cached the PDF under a fixed URL, so visitors could keep seeing an old version after an update.

## Decision
- Keep the CV source in the repository as `docs/cv/George-CV.html`: plain HTML and CSS, styled with the site's palette (teal primary, burlywood accent) and laid out to fit one A4 page.
- Generate the PDF with `docs/cv/build-cv.sh`, which prints the HTML with headless Edge or Chrome (`--print-to-pdf`).
- Version the link: `PortfolioContent.CvPath` carries `?v=<first 8 chars of the PDF's SHA-256>`, and the script rewrites it on each run, so the URL changes only when the content does.
- The site's Resume button opens the PDF in a new tab.

## Consequences
- CV edits are ordinary diffs, and the design follows the site's colors.
- Generating the PDF needs Edge or Chrome on the author's machine. CI and the Static Web Apps build don't run the script; the generated PDF is committed.
- The job texts live in two places: the site's resx files and the CV HTML (a tighter wording for print). When facts change, both must be updated.
- Fitting one page is checked by eye after each change; long additions may need older roles condensed.
