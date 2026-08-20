# Upgrade Options — PersonalWebSite

Assessment: 1 SDK-style project on net8.0, 3 package upgrades recommended, and 21 API issues (18 binary incompatible, 3 behavioral changes).

## Strategy

### Upgrade Strategy
Single-project modern-to-modern upgrade; an atomic pass is the most direct fit.

| Value | Description |
|-------|-------------|
| **All-at-Once** (selected) | Upgrade all projects simultaneously in a single atomic pass. Fastest approach, no multi-targeting overhead, but the solution may be temporarily broken until all projects are updated. |
| Top-Down | Upgrade entry-point applications first, temporarily multi-targeting shared libraries so the solution stays buildable throughout; consolidate libraries in a second phase. |

## Compatibility

### Unsupported API Handling
Assessment reports binary and behavioral API changes that must be addressed during the upgrade.

| Value | Description |
|-------|-------------|
| **Fix Inline** (selected) | Resolve every API change in the same task, including complex ones, leaving no deferred stub cleanup. |
| Defer Complex Changes | Apply simple replacements inline; for complex changes create minimal compilable stubs and follow-up resolution subtasks. |
