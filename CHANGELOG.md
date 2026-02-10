# Changelog

## [0.0.1] Initial

## [0.0.2] 2026 Feb 09
### Changed
- `Fray.Command` from `fsproj` to `csproj`: c-sharp works better for PowerShell Cmdlets than f-sharp
- `Seed.fs`: nothing significant
- `Program.fs`: dropped unneeded `[<ParamArray>]` attribute
### Removed
- `Fray.Build.ps1`: drop dependence on [Invoke-Build](https://github.com/nightroman/Invoke-Build)
### Added
- `Fray.Tasks.ps1`: Simple, fully-native PowerShell Task Runner
- this changelog
