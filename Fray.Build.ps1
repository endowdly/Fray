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

$modulesPath = split-path $profile | join-path -child modules

task Test {
    exec { dotnet test --logger console -v m }
}

task Clean {
    exec { dotnet clean }
    remove dist
}

task Build Test, Clean, {
    exec { dotnet pack Fray -v m -c release -o .\dist\nupkg }
    exec { dotnet publish Fray.Command -c release -v m -o .\dist\fray }
}

task Cmdlet Build, {
    copy .\dist\fray -destination $modulesPath -recurse -ea silentlycontinue
}

task ReCmdlet Build, {
    print 'Force copy module' -color magenta
    copy .\dist\fray\ -destination $modulesPath -recurse -force
}

task Exe Build, {
    exec { dotnet tool install -v m -g --add-source .\dist\nupkg fray }
}

task ReExe Build, {
    if (command 'fray.exe' -commandtype application -ea silentlycontinue) {
        print 'Uninstall old fray' -color magenta
        exec { dotnet tool uninstall -g fray }
    }

    print 'Install fray tool' -color magenta
    exec { dotnet tool install -v m -g --add-source .\dist\nupkg fray }
}

task Install Cmdlet, Exe
task Reinstall ReCmdlet, ReExe
task . ShowRoot
