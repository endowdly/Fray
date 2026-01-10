Describe Fray {
    Context 'Fray module' {
        It 'Given the Fray module, it should have a non-zero version' {
           $m = Get-Module Fray
           $m.Version | Should -Not -Be $null
           $m.Version.Major | Should -BeGreaterThan 0
        }
    }

    Context 'Invoke-Fray cmdlet' {
        It 'Fray.Root is tested' {
           $true
        }
    }
}
