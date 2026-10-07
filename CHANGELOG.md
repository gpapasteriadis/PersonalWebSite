# Changelog

All notable changes to this project are documented here.
The format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

## [Unreleased]

### Added
- `CLAUDE.md`, `ARCHITECTURE.md`, `CHANGELOG.md` and architecture decision records in `docs/decisions/`.
- Claude Code agents: `architecture-reviewer`, `i18n-checker`, `ui-reviewer` (`.claude/agents/`).
- Shared Claude Code permissions (`.claude/settings.json`), `.editorconfig`, PR template.
- CI workflow that builds every PR with warnings as errors.

### Changed
- Bumped `actions/checkout` to v4 in the deploy workflow.
