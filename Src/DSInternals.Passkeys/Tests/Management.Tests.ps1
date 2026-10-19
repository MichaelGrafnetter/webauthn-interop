<#
.SYNOPSIS
    Offline Pester coverage for authenticator management cmdlets.
.PARAMETER ModulePath
    Path to the compiled module directory.
.PARAMETER Configuration
    The build configuration of the module being tested.
#>

#Requires -Version 5.1
#Requires -Modules @{ ModuleName = 'Pester'; ModuleVersion = '5.0' }

param(
    [Parameter(Mandatory = $false)]
    [ValidateNotNullOrEmpty()]
    [string] $ModulePath,

    [Parameter(Mandatory = $false)]
    [ValidateNotNullOrEmpty()]
    [ValidateSet('Debug', 'Release')]
    [string] $Configuration = 'Release'
)

if ([string]::IsNullOrWhiteSpace($ModulePath)) {
    $ModulePath = Join-Path -Path $PSScriptRoot -ChildPath "..\..\..\Build\bin\PSModule\$Configuration\DSInternals.Passkeys" -Resolve -ErrorAction Stop
}

Import-Module -Name $ModulePath -Force -ErrorAction Stop

Describe 'Automatic authenticator management sessions' {
    InModuleScope 'DSInternals.Passkeys.Core' {
        BeforeAll {
            $script:pin = ConvertTo-SecureString 'offline-test-pin' -AsPlainText -Force
            $script:operations = @(
                @{ Name = 'Unlock-PasskeyAuthenticatorSession'; Parameters = @{ Pin = $script:pin; Confirm = $false } },
                @{ Name = 'Get-PasskeyAuthenticatorCapabilities'; Parameters = @{} },
                @{ Name = 'Get-PasskeyAuthenticatorPinPolicy'; Parameters = @{} },
                @{ Name = 'Get-PasskeySecurityKey'; Parameters = @{} },
                @{ Name = 'Remove-PasskeySecurityKey'; Parameters = @{ CredentialId = [byte[]]@(1); Confirm = $false } },
                @{ Name = 'Set-PasskeyAuthenticatorPin'; Parameters = @{ NewPin = $script:pin; Confirm = $false } },
                @{ Name = 'Set-PasskeyAuthenticatorPinPolicy'; Parameters = @{ MinimumPinLength = 8; Confirm = $false } },
                @{ Name = 'Set-PasskeyAuthenticatorConfiguration'; Parameters = @{ AlwaysUv = $true; Confirm = $false } },
                @{ Name = 'Get-PasskeyAuthenticatorLargeBlob'; Parameters = @{} },
                @{ Name = 'Set-PasskeyAuthenticatorLargeBlob'; Parameters = @{ Data = [byte[]]@(1); Confirm = $false } }
            )
        }

        AfterAll {
            $script:pin.Dispose()
        }

        BeforeEach {
            $script:session = [System.Runtime.Serialization.FormatterServices]::GetUninitializedObject(
                [DSInternals.Win32.WebAuthn.AuthenticatorManagementSession])
            $script:session | Add-Member -NotePropertyName Fail -NotePropertyValue $false
            $script:session | Add-Member -NotePropertyName Calls -NotePropertyValue 0
            foreach ($method in @('AuthenticatePin', 'GetCapabilities', 'GetPinPolicy', 'GetCredentials',
                'DeleteCredential', 'SetPin', 'ChangePin', 'SetPinPolicy', 'SetAlwaysUv',
                'EnableEnterpriseAttestation', 'GetLargeBlobs', 'SetLargeBlobs')) {
                $script:session | Add-Member -MemberType ScriptMethod -Name $method -Force -Value {
                    $this.Calls++
                    if ($this.Fail) { throw 'Synthetic management failure.' }
                }
            }
            Mock Open-PasskeyAuthenticatorSession { $script:session }
            Mock Close-PasskeyAuthenticatorSession {}
        }

        It 'makes Session optional on operations but mandatory on Close' {
            foreach ($operation in $script:operations) {
                $attributes = (Get-Command $operation.Name).Parameters['Session'].Attributes |
                    Where-Object { $_ -is [System.Management.Automation.ParameterAttribute] }
                @($attributes | Where-Object Mandatory).Count | Should -Be 0
            }
            (Get-Command Close-PasskeyAuthenticatorSession).Parameters['Session'].Attributes |
                Where-Object { $_ -is [System.Management.Automation.ParameterAttribute] -and $_.Mandatory } |
                Should -Not -BeNullOrEmpty
        }

        It 'opens and closes one owned session per operation' {
            foreach ($operation in $script:operations) {
                $parameters = $operation.Parameters
                & $operation.Name @parameters
            }
            Should -Invoke Open-PasskeyAuthenticatorSession -Times 10 -Exactly
            Should -Invoke Close-PasskeyAuthenticatorSession -Times 10 -Exactly
            $script:session.Calls | Should -Be 10
        }

        It 'does not create or close supplied sessions' {
            foreach ($operation in $script:operations) {
                $parameters = $operation.Parameters
                & $operation.Name -Session $script:session @parameters
            }
            Should -Invoke Open-PasskeyAuthenticatorSession -Times 0 -Exactly
            Should -Invoke Close-PasskeyAuthenticatorSession -Times 0 -Exactly
            $script:session.Calls | Should -Be 10
        }

        It 'closes owned sessions when operations fail' {
            $script:session.Fail = $true
            foreach ($operation in $script:operations) {
                $parameters = $operation.Parameters
                { & $operation.Name @parameters } | Should -Throw '*Synthetic management failure*'
            }
            Should -Invoke Close-PasskeyAuthenticatorSession -Times 10 -Exactly
        }

        It 'does not close supplied sessions when operations fail' {
            $script:session.Fail = $true
            foreach ($operation in $script:operations) {
                $parameters = $operation.Parameters
                { & $operation.Name -Session $script:session @parameters } | Should -Throw
            }
            Should -Invoke Close-PasskeyAuthenticatorSession -Times 0 -Exactly
        }

        It 'uses one session for all pipeline credentials' {
            $first = [DSInternals.Win32.WebAuthn.DiscoverableCredentialDetails]::new()
            $first.CredentialId = [byte[]]@(1)
            $second = [DSInternals.Win32.WebAuthn.DiscoverableCredentialDetails]::new()
            $second.CredentialId = [byte[]]@(2)
            @($first, $second) | Remove-PasskeySecurityKey -Confirm:$false
            Should -Invoke Open-PasskeyAuthenticatorSession -Times 1 -Exactly
            Should -Invoke Close-PasskeyAuthenticatorSession -Times 1 -Exactly
            $script:session.Calls | Should -Be 2
        }

        It 'cleans up owned sessions without performing mutations under WhatIf' {
            foreach ($operation in $script:operations | Where-Object { $_.Parameters.ContainsKey('Confirm') }) {
                $parameters = $operation.Parameters
                & $operation.Name @parameters -WhatIf
            }
            Should -Invoke Open-PasskeyAuthenticatorSession -Times 6 -Exactly
            Should -Invoke Close-PasskeyAuthenticatorSession -Times 6 -Exactly
            $script:session.Calls | Should -Be 0
        }

        It 'closes the owned session when downstream stops enumeration' {
            $script:session | Add-Member -MemberType ScriptMethod -Name GetCredentials -Force -Value { 1; 2; 3 }
            Get-PasskeySecurityKey | Select-Object -First 1 | Should -Be 1
            Should -Invoke Close-PasskeyAuthenticatorSession -Times 1 -Exactly
        }

        It 'does not close a session when creation fails' {
            Mock Open-PasskeyAuthenticatorSession { throw 'Synthetic selection failure.' }
            { Get-PasskeyAuthenticatorCapabilities } | Should -Throw '*Synthetic selection failure*'
            Should -Invoke Close-PasskeyAuthenticatorSession -Times 0 -Exactly
        }
    }
}

Describe 'Authenticator management PowerShell surface' {
    BeforeAll {
        $module = Get-Module -Name DSInternals.Passkeys
        $managementSessionType = [DSInternals.Win32.WebAuthn.AuthenticatorManagementSession]
        $uninitializedSession = [System.Runtime.Serialization.FormatterServices]::GetUninitializedObject($managementSessionType)
    }

    It 'exports all planned management commands' {
        $expectedCommands = @(
            'Open-PasskeyAuthenticatorSession',
            'Close-PasskeyAuthenticatorSession',
            'Unlock-PasskeyAuthenticatorSession',
            'Get-PasskeySupportedTransport',
            'Get-PasskeyAuthenticatorCapabilities',
            'Get-PasskeyAuthenticatorPinPolicy',
            'Get-PasskeySecurityKey',
            'Remove-PasskeySecurityKey',
            'Set-PasskeyAuthenticatorPin',
            'Set-PasskeyAuthenticatorPinPolicy',
            'Set-PasskeyAuthenticatorConfiguration',
            'Get-PasskeyAuthenticatorLargeBlob',
            'Set-PasskeyAuthenticatorLargeBlob'
        )

        $exportedCommands = @(Get-Command | Select-Object -ExpandProperty Name)
        foreach ($name in $expectedCommands) {
            $exportedCommands | Should -Contain $name
        }
    }

    It 'does not expose an API-version bypass' {
        (Get-Command Open-PasskeyAuthenticatorSession).Parameters.ContainsKey('AllowUntestedApiVersion') | Should -BeFalse
        (Get-Command Get-PasskeySupportedTransport).Parameters.ContainsKey('AllowUntestedApiVersion') | Should -BeFalse
    }

    It 'exports the platform authenticator command and its compatibility aliases' {
        (Get-Command Get-PasskeyPlatformAuthenticator).CommandType | Should -Be 'Function'
        foreach ($name in @('Get-PasskeyAuthenticator', 'GetPasskeyWindowsHelloAuthenticator')) {
            $alias = Get-Command $name
            $alias.CommandType | Should -Be 'Alias'
            $alias.ResolvedCommand.Name | Should -Be 'Get-PasskeyPlatformAuthenticator'
        }
    }

    It 'uses explicit disposable sessions' {
        [System.IDisposable].IsAssignableFrom($managementSessionType) | Should -BeTrue
        (Get-Command Close-PasskeyAuthenticatorSession).Parameters['Session'].Attributes |
            Where-Object { $_ -is [System.Management.Automation.ParameterAttribute] -and $_.ValueFromPipeline } |
            Should -Not -BeNullOrEmpty
        (Get-Command Get-PasskeyAuthenticatorCapabilities).Parameters.ContainsKey('Session') | Should -BeTrue
    }

    It 'accepts SecureString PIN inputs without prompt parameters' {
        $pinParameter = (Get-Command Unlock-PasskeyAuthenticatorSession).Parameters['Pin']
        $pinParameter.ParameterType | Should -Be ([System.Security.SecureString])
        (Get-Command Set-PasskeyAuthenticatorPin).ParameterSets.Name | Should -Contain 'Initial'
        (Get-Command Set-PasskeyAuthenticatorPin).ParameterSets.Name | Should -Contain 'Change'
        (Get-Command Unlock-PasskeyAuthenticatorSession).Parameters.ContainsKey('Credential') | Should -BeFalse
    }

    It 'protects every authenticator mutation with WhatIf before dispatch' {
        $pin = ConvertTo-SecureString 'offline-test-pin' -AsPlainText -Force
        $bytes = [byte[]]@(1, 2, 3)

        { Unlock-PasskeyAuthenticatorSession -Session $uninitializedSession -Pin $pin -WhatIf } | Should -Not -Throw
        { Remove-PasskeySecurityKey -Session $uninitializedSession -CredentialId $bytes -WhatIf } | Should -Not -Throw
        { Set-PasskeyAuthenticatorPin -Session $uninitializedSession -NewPin $pin -WhatIf } | Should -Not -Throw
        { Set-PasskeyAuthenticatorPin -Session $uninitializedSession -CurrentPin $pin -NewPin $pin -WhatIf } | Should -Not -Throw
        { Set-PasskeyAuthenticatorPinPolicy -Session $uninitializedSession -MinimumPinLength 8 -WhatIf } | Should -Not -Throw
        { Set-PasskeyAuthenticatorConfiguration -Session $uninitializedSession -AlwaysUv -WhatIf } | Should -Not -Throw
        { Set-PasskeyAuthenticatorConfiguration -Session $uninitializedSession -EnableEnterpriseAttestation -WhatIf } | Should -Not -Throw
        { Set-PasskeyAuthenticatorLargeBlob -Session $uninitializedSession -Data $bytes -WhatIf } | Should -Not -Throw

        foreach ($name in @(
            'Unlock-PasskeyAuthenticatorSession',
            'Remove-PasskeySecurityKey',
            'Set-PasskeyAuthenticatorPin',
            'Set-PasskeyAuthenticatorPinPolicy',
            'Set-PasskeyAuthenticatorConfiguration',
            'Set-PasskeyAuthenticatorLargeBlob'
        )) {
            (Get-Command $name).Parameters['WhatIf'].ParameterType | Should -Be ([switch])
            $binding = (Get-Command $name).ScriptBlock.Attributes |
                Where-Object { $_ -is [System.Management.Automation.CmdletBindingAttribute] }
            $binding.ConfirmImpact | Should -Be ([System.Management.Automation.ConfirmImpact]::High)
        }
    }

    It 'preserves a large-blob result as one byte-array object' {
        [byte[]] $data = @(1, 2, 3, 4)
        $result = & $module { param($bytes) Write-PasskeyManagementByteArray -Data $bytes } -bytes $data
        $result.GetType() | Should -Be ([byte[]])
        $result.Length | Should -Be 4
        $result[0] | Should -Be 1
    }

    It 'supports credential-object pipeline input and raw or Base64Url identifiers' {
        $remove = Get-Command Remove-PasskeySecurityKey
        $remove.ParameterSets.Name | Should -Contain 'ByCredentialId'
        $remove.ParameterSets.Name | Should -Contain 'ByCredential'
        $remove.Parameters['CredentialId'].ParameterType | Should -Be ([object])
        $remove.Parameters['InputObject'].ParameterType |
            Should -Be ([DSInternals.Win32.WebAuthn.DiscoverableCredentialDetails])
    }
}
