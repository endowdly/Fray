
<#PSScriptInfo
.VERSION 2.0
.GUID 9f19602c-b5ce-47ca-b9af-74f1c73a1f6e
.AUTHOR endowdly
.TAGS fray powershell
.PROJECTURI https://github.com/endowdly/fray
#>

<#

.DESCRIPTION
 Builds Fray Solution and or Individual Projects

#>

Param(
    # The Build Task to Execute
    [Parameter(Position = 0)]
    [ValidateSet(
        'ShowRoot',
        'Test',
        'Clean',
        'Build',
        'Install',
        'Uninstall',
        'Reinstall'
    )]
    [string] $Task = 'ShowRoot')

if ($MyInvocation.InvocationName -eq '.' -or $MyInvocation.Line -eq '') {
    throw 'Build was sourced! You should only use the call operator (&) to invoke this script.'
}

$PSNativeCommandArgumentPassing = 'Standard'
$PSNativeCommandUseErrorActionPreference = $true
$ErrorActionPreference = 'Stop'

pushd $PSScriptRoot

$Build = @{
    FrayPath = Split-Path $PROFILE | Join-Path -Child Modules | Join-Path -Child Fray
    IsBuilt = $false
}

function ShowRoot { 'The build root is: '; (Location).Path }
function Test { dotnet test --logger console -v d }
function Clean {
    dotnet clean
    rm -rec -for .\dist -ea continue
}
function Build {
    if ($Build.IsBuilt) { return }

    data buildExe {
        'pack', 'Fray'
        '-c', 'release'
        # '-p:SelfContained=true'
        '-p:RuntimeIdentifier=win-x64'
        '-p:PackAsTool=true'
        # '-p:PublishTrimmed=true'
        # '-p:TrimMode=partial'
        # '-p:PublishSingleFile=true'
        # '-p:EnableCompressionInSingleFile=true'
        '-p:InvariantGlobalization=true'
        '-p:DebugType=none'
        '-p:DebugSymbols=false'
        '-o', '.\dist\pkg'
    }
    data buildCmdlet {
        'publish', 'Fray.Command'
        '-c', 'release'
        '--self-contained', 'false'
        '-p:InvariantGlobalization=true'
        '-p:DebugType=none'
        '-p:DebugSymbols=false'
        '-o', '.\dist\cmdlet'
    }

    test
    clean

    & dotnet @buildExe
    & dotnet @buildCmdlet

    $Build.IsBuilt = $true
}
function Cmdlet { build; copy .\dist\cmdlet -dest $Build.FrayPath -rec  }
function UnCmdlet { rm -rec -for $Build.FrayPath }
function Exe { build; dotnet tool install -v m -g --add-source .\dist\pkg fray }
function UnExe {
    if (command fray) { dotnet tool uninstall -g fray }
}

function Install { Cmdlet; Exe }
function Uninstall { UnCmdlet; UnExe }
function Reinstall { Uninstall; Install }

& $Task

popd
