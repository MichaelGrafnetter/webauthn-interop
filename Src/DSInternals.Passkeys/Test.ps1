Import-Module -Name "$PSScriptRoot\DSInternals.Passkeys.psd1"

Connect-MgGraph -TenantId dev.dsinternals.com -Scopes 'UserAuthenticationMethod.ReadWrite.All'

<#
$opts = Get-PasskeyRegistrationOptions -UserId 'AdeleV@dev.dsinternals.com'
$passkey = New-Passkey -Options $opts -DisplayName 'Lightning'

Register-Passkey -UserId 'AdeleV@dev.dsinternals.com' -Passkey $passkey
#>

Register-Passkey -UserId 'AdeleV@dev.dsinternals.com' -DisplayName 'Lightning'

Remove-Module -Name DSInternals.Passkeys


#Get-MgUserAuthenticationFido2Method -UserId 'AdeleV@dev.dsinternals.com'
#Remove-MgUserAuthenticationFido2Method -UserId  'AdeleV@dev.dsinternals.com' -Fido2AuthenticationMethodId uPrFS_gYhY86sRxhrncCiwK3Ks3G8XHwqsnGr0BVE-s1