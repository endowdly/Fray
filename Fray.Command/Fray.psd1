@{
    RootModule             = 'Fray.Command.dll'
    ModuleVersion          = '0.0.2'
    GUID                   = '88692681-3e7c-4beb-ad5f-d17398bf8607'
    Author                 = 'endowdly'
    Description            = 'Fray is a semi-performant string tranformer for code cases.'
    PowerShellVersion      = '7.0'
    DotNetFrameworkVersion = '9.0'
    FunctionsToExport      = @()
    CmdletsToExport        = @('Invoke-Fray')
    AliasesToExport        = @()
    PrivateData            = @{
        PSData = @{
            Tags                       = @('fsharp', 'string-transform', 'string', 'code-case')
            LicenseUri                 = ''
            ProjectUri                 = 'https://github.com/endowdly/fray'
            IconUri                    = ''
            ReleaseNotes               = @'
# Changelog
## [0.0.1] Initial
## [0.0.2] 2026-02-09
### Changed
- InvokeFrayCommand.fs to InvokeFrayCommand.cs
'@
            Prerelease                 = ''
            RequireLicenseAcceptance   = $false
            ExternalModuleDependencies = @()
        }
    }
}

