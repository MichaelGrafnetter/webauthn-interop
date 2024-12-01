BeforeAll {
    Add-Type -Path "./build/bin/DSInternals.Win32.WebAuthn.Tests/debug/DSInternals.Win32.WebAuthn.Tests.dll" -ErrorAction Stop
    Import-Module .\Build\bin\PSModule\Debug\DSInternals.Passkeys\DSInternals.Passkeys.psm1 -Force
}

Describe 'EntraID Tests' {
    BeforeAll {
        $SecureClientSecret = ConvertTo-SecureString -String $EntraIdClientSecret -AsPlainText -Force
        $ClientSecretCredential = New-Object -TypeName System.Management.Automation.PSCredential -ArgumentList $EntraIdClientId, $SecureClientSecret
        Connect-MgGraph -TenantId $EntraIdTenantId -ClientSecretCredential $ClientSecretCredential
    }

    It "Registers passkeys to Entra ID for each credential parameter option" {
        $options = Get-PasskeyRegistrationOptions -UserId $EntraIdUserId
        $options | Should -BeOfType [DSInternals.Win32.WebAuthn.EntraID.MicrosoftGraphWebauthnCredentialCreationOptions]
        $options.ChallengeTimeout | Should -BeGreaterThan (Get-Date)
        $options.PublicKeyOptions.RelyingParty.Id | Should -Be "login.microsoft.com"
        $options.PublicKeyOptions.PublicKeyCredentialParameters.Count | Should -Be 2

        $factory0 = [DSInternals.Win32.WebAuthn.Tests.PasskeyFactory]::new()
        $factory0 | Should -BeOfType [DSInternals.Win32.WebAuthn.Tests.PasskeyFactory]
        $passkey0 = $factory0.MakePasskey($options, 0)
        $passkey0 | Should -BeOfType [DSInternals.Win32.WebAuthn.EntraID.MicrosoftGraphWebauthnAttestationResponse]
        $passkey0.DisplayName | Should -BeLike 'DSInternals.Passkeys*'

        $factory1 = [DSInternals.Win32.WebAuthn.Tests.PasskeyFactory]::new()
        $factory1 | Should -BeOfType [DSInternals.Win32.WebAuthn.Tests.PasskeyFactory]
        $passkey1 = $factory1.MakePasskey($options, 1)
        $passkey1 | Should -BeOfType [DSInternals.Win32.WebAuthn.EntraID.MicrosoftGraphWebauthnAttestationResponse]
        $passkey1.DisplayName | Should -BeLike 'DSInternals.Passkeys*'

        $result0 = Register-Passkey -Passkey $passkey0 -UserId $EntraIdUserId
        $result0 | Should -Not -BeNullOrEmpty
        $result0 | Should -BeOfType [Microsoft.Graph.PowerShell.Models.MicrosoftGraphFido2AuthenticationMethod]
        $result0.AaGuid | Should -Be "4453496e-7465-726e-616c-730000000000"
        $result0.CreatedDateTime | Should -BeLessThan $options.ChallengeTimeout
        $result0.AttestationCertificates.Count | Should -Be 1
        $credentialId0 = $result0.Id

        $result1 = Register-Passkey -Passkey $passkey1 -UserId $EntraIdUserId
        $result1 | Should -Not -BeNullOrEmpty
        $result1 | Should -BeOfType [Microsoft.Graph.PowerShell.Models.MicrosoftGraphFido2AuthenticationMethod]
        $result1.AaGuid | Should -Be "4453496e-7465-726e-616c-730000000000"
        $result1.CreatedDateTime | Should -BeLessThan $options.ChallengeTimeout
        $result1.AttestationCertificates.Count | Should -Be 1
        $credentialId1 = $result1.Id

        $result0.AttestationCertificates[0] | Should -Not -Be $result1.AttestationCertificates[0]
        $result0.Id | Should -Not -Be $result1.Id

        [string] $credentialDeletionUrl0 = '/beta/users/{0}/authentication/fido2Methods/{1}' -f [uri]::EscapeDataString($EntraIdUserId), $credentialId0
        $response0 = Invoke-MgGraphRequest -Method Delete -Uri $credentialDeletionUrl0
        $response0 | Should -BeNullOrEmpty

        [string] $credentialDeletionUrl1 = '/beta/users/{0}/authentication/fido2Methods/{1}' -f [uri]::EscapeDataString($EntraIdUserId), $credentialId1
        $response1 = Invoke-MgGraphRequest -Method Delete -Uri $credentialDeletionUrl1
        $response1 | Should -BeNullOrEmpty
    }

    AfterAll {
        Disconnect-MgGraph
    }
}

Describe "Okta Tests" {
    BeforeAll {
        Connect-Okta -Tenant $OktaTenantId -ClientId $OKtaClientId -Scopes @('okta.users.manage') -JsonWebKey $OktaJsonWebKey
    }

    It "Registers passkeys to Okta for each credential parameter option" {
        $options0 = Get-PasskeyRegistrationOptions -UserId $OktaUserId
        $options0 | Should -BeOfType [DSInternals.Win32.WebAuthn.Okta.OktaWebauthnCredentialCreationOptions]
        $options0.PublicKeyOptions.RelyingParty.Id | Should -Be $OktaTenantId
        $options0.PublicKeyOptions.PublicKeyCredentialParameters.Count | Should -Be 2

        $factory0 = [DSInternals.Win32.WebAuthn.Tests.PasskeyFactory]::new()
        $factory0 | Should -BeOfType [DSInternals.Win32.WebAuthn.Tests.PasskeyFactory]

        $passkey0 = $factory0.MakePasskey($options0, $0)
        $passkey0 | Should -BeOfType [DSInternals.Win32.WebAuthn.Okta.OktaWebauthnAttestationResponse]
        $passkey0 | Should -Not -BeNullOrEmpty
        $factorId0 = $passkey0.FactorId

        $result0 = Register-Passkey -Passkey $passkey0 -UserId $OktaUserId
        $result0 | Should -Not -BeNullOrEmpty
        $result0 | Should -BeOfType [DSInternals.Win32.WebAuthn.Okta.OktaFido2AuthenticationMethod]
        $result0.Id | Should -Be $factorId0
        $result0.FactorType | Should -Be "webauthn"
        $result0.Provider | Should -Be "FIDO"
        $result0.VendorName | Should -Be "FIDO"
        $result0.Status | Should -Be "ACTIVE"
        $result0.Created | Should -BeLessThan (Get-Date -AsUTC)
        $result0.Profile.CredentialId | Should -Not -BeNullOrEmpty
        $result0.Profile.CredentialId | Should -Be ([Convert]::ToBase64String($passkey0.PublicKeyCred.Id)).Replace('+', '-').Replace('/', '_').Replace('=', '')

        [string] $credentialDeletionPath0 = "/api/v1/users/${OktaUserId}/factors/${factorId0}"
        $response0 = Invoke-OktaWebRequest -Method ([Microsoft.PowerShell.Commands.WebRequestMethod]::Delete) -Path $credentialDeletionPath0
        $response0.StatusCode | Should -Be 204
        $response0.RawContentLength | Should -Be 0

        $options1 = Get-PasskeyRegistrationOptions -UserId $OktaUserId
        $options1 | Should -BeOfType [DSInternals.Win32.WebAuthn.Okta.OktaWebauthnCredentialCreationOptions]
        $options1.PublicKeyOptions.RelyingParty.Id | Should -Be $OktaTenantId
        $options1.PublicKeyOptions.PublicKeyCredentialParameters.Count | Should -Be 2

        $factory1 = [DSInternals.Win32.WebAuthn.Tests.PasskeyFactory]::new()
        $factory1 | Should -BeOfType [DSInternals.Win32.WebAuthn.Tests.PasskeyFactory]

        $passkey1 = $factory1.MakePasskey($options1, $1)
        $passkey1 | Should -BeOfType [DSInternals.Win32.WebAuthn.Okta.OktaWebauthnAttestationResponse]
        $passkey1 | Should -Not -BeNullOrEmpty
        $factorId1 = $passkey1.FactorId

        $result1 = Register-Passkey -Passkey $passkey1 -UserId $OktaUserId
        $result1 | Should -Not -BeNullOrEmpty
        $result1 | Should -BeOfType [DSInternals.Win32.WebAuthn.Okta.OktaFido2AuthenticationMethod]
        $result1.Id | Should -Be $factorId1
        $result1.FactorType | Should -Be "webauthn"
        $result1.Provider | Should -Be "FIDO"
        $result1.VendorName | Should -Be "FIDO"
        $result1.Status | Should -Be "ACTIVE"
        $result1.Created | Should -BeLessThan (Get-Date -AsUTC)
        $result1.Profile.CredentialId | Should -Not -BeNullOrEmpty
        $result1.Profile.CredentialId | Should -Be ([Convert]::ToBase64String($passkey1.PublicKeyCred.Id)).Replace('+', '-').Replace('/', '_').Replace('=', '')

        [string] $credentialDeletionPath1 = "/api/v1/users/${OktaUserId}/factors/${factorId1}"
        $response1 = Invoke-OktaWebRequest -Method ([Microsoft.PowerShell.Commands.WebRequestMethod]::Delete) -Path $credentialDeletionPath1
        $response1.StatusCode | Should -Be 204
        $response1.RawContentLength | Should -Be 0

        $result0.Profile.CredentialId | Should -Not -Be $result1.Profile.CredentialId
    }

    AfterAll {
        Disconnect-Okta
    }
}

