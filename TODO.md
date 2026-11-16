# Active work

Request: remove obsolete OldStage content and shader experiments.
Work on native; commit and push the verified cleanup.

## Cleanup completed

- Removed twelve unused OldStage definitions and native imports, exclusive assets,
  scenery presets, console entries and archived-stage tests.
- Removed unused shader experiments, background test state, effect loader,
  exclusive noise texture and empty SpacePlatform; cleaned legacy project entries.
- Updated the audit: OldStage4 no longer requires a replacement texture.
- Full headless suite, native build and software-render check pass.
- Retained campaign sources are unchanged; legacy project XML parses.

## Remaining verification limits

Identified active-campaign audit blockers are repaired. Human playtesting,
physical controllers, web/phone builds and exhaustive legacy frame traces remain
unverified. See CONTENT_AUDIT.md and CONTENT_PARITY.md.
