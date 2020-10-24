$cer = Get-ChildItem -Path Cert:\CurrentUser\My -CodeSigningCert
$target = 'C:\CQURE\Fido2Cmd\Fido2UI.exe'
$timeStamp = 'http://timestamp.comodoca.com'
Set-AuthenticodeSignature -Certificate $cer -HashAlgorithm SHA256 -FilePath $target #-TimestampServer $timeStamp