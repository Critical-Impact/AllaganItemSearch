# Changelog

All notable changes to this project will be documented in this file.

The log versioning the plugin versioning will not match as 1.0.0.0 technically does not match semantic versioning but
the headache of trying to change this would be too much.
Instead the changelog reader and automation surrounding plugin PRs will add the 1. back in

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.html).

## [2.0.1] - 2026-02-07

### Added
- Source/Uses added for Anima Shops, Anima Weapons, Zodiac Weapons, Gearsets, Pilgrims Traverse, Oizys,
- Some of the data required by Allagan Item Search is now cached in dalamud's DataShare.
- Boot time after the initial load will be increased and other plugins relying on this will take advantage of it once updated.
- If you prefer the data is not persisted, you can turn it off in settings via Troubleshooting -> Persist Cached Data

## [2.0.0] - 2025-12-22

### Fixed

- Support for 7.4
