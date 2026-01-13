<#
.Synopsis
    Build script for Fray; allows for building and installing PowerShell Cmdlet and the Command-Line Application.
.Description

#>

task ShowRoot {
    if ($BuildRoot -eq (Location).Path) {
        print 'the build root is:' -color magenta
        (Location).Path
    }
}

task Test {
    exec { dotnet test --logger console --verbosity minimal }
}

task Clean {
    exec { dotnet clean }
    remove Dist
}

task Build Test, Clean, {
    exec { dotnet publish Fray -c release -o .\Dist }
    exec { dotnet publish Fray.Command -c release -o .\Dist\Fray }
}

task Install Build, {
    $modulesPath = split-path $profile | join-path -child modules
    requires -path $modulesPath
    copy .\Dist\Fray\ -destination $modulesPath -recurse
}

task . ShowRoot
