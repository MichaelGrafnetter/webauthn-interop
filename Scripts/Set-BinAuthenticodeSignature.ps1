$cer = Get-ChildItem -Path Cert:\CurrentUser\My -CodeSigningCert
$target = "$PSScriptRoot\..\Build\bin\Release\Fido2UI\net472\Fido2UI.exe"
$timeStamp = 'http://timestamp.comodoca.com'
Set-AuthenticodeSignature -Certificate $cer -HashAlgorithm SHA256 -FilePath $target -TimestampServer $timeStamp