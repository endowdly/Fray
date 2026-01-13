<#
.Synopsis
    Build script for Fray; allows for building and installing PowerShell Cmdlet and the Command-Line Application.
.Description

#>

task ShowRoot {
    if ($BuildRoot -eq (Location).Path) {
        print 'The build root is:' -color magenta
        (Location).Path
    }
}

task Test {
    exec { dotnet test --logger console -v m }
}

task Clean {
    exec { dotnet clean }
    remove Dist
}

task SetModulesPath -Before Install, Reinstall {
    $modulesPath = split-path $profile | join-path -child modules
    requires -path $modulesPath
}

task Build Test, Clean, {
    exec { dotnet pack Fray -v m -c release -o .\dist\nupkg }
    exec { dotnet publish Fray.Command -c release -v m -o .\dist\fray }
}

task Install Build, {
    copy .\dist\fray\ -destination $modulesPath -recurse -ea silentlycontinue
    exec { dotnet tool install -v m -g --add-source .\dist\nupkg fray }
}

task Reinstall Build, {
    if (command 'fray.exe' -commandtype application -ea silentlycontinue) {
        print 'Uninstall old fray' -color magenta
        exec { dotnet tool uninstall -g fray }
    }

    print 'Install fray tool' -color magenta
    exec { dotnet tool install -v m -g --add-source .\dist\nupkg fray }
    print 'Force copy module' -color magenta
    copy .\dist\fray\ -destination $modulesPath -recurse -force

}

task . ShowRoot
