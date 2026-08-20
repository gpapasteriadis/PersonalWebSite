# .NET Version Upgrade Plan

## Overview

**Target**: Upgrade PersonalWebSite Blazor WebAssembly app from .NET 8 to .NET 10.
**Scope**: 1 SDK-style project, ~106 LOC, with 3 recommended package updates and API compatibility findings to validate.

## Tasks

### 01-upgrade-personalwebsite-net10: Upgrade PersonalWebSite to net10.0 and align package/runtime compatibility

Upgrade the PersonalWebSite WebAssembly project from `net8.0` to `net10.0`, update recommended framework-related package versions, and apply required code-level compatibility fixes identified by the assessment. This task covers the project file and affected source files where API changes require adjustments.

Scope includes validating restore/build behavior and ensuring the upgraded project remains functionally consistent after behavioral API changes. Because this is an all-at-once single-project upgrade, prerequisite checks and upgrade edits are executed within this same task.

**Done when**: The project targets `net10.0`, recommended package updates are applied, build succeeds without errors/warnings, and tests (if present) pass.

---

### 02-final-validation: Run full validation and capture upgrade outcomes

Run final solution-level validation after upgrade edits are complete, including full build and available tests, and verify no unresolved compatibility blockers remain. Document final outcomes and any non-blocking follow-up recommendations discovered during validation.

This task ensures the scenario exits with a clean, reproducible validation result suitable for merge and release planning.

**Done when**: Solution build/test validation completes successfully and scenario artifacts reflect final upgrade status.
