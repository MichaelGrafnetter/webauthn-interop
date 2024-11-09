---
external help file: DSInternals.Passkeys-help.xml
Module Name: DSInternals.Passkeys
online version:
schema: 2.0.0
---

# Connect-Okta

## SYNOPSIS
{{ Fill in the Synopsis }}

## SYNTAX

### AuthorizationCode
```
Connect-Okta -Tenant <String> -ClientId <String> [-Scopes <String[]>] [-ProgressAction <ActionPreference>]
 [<CommonParameters>]
```

### ClientCredentials
```
Connect-Okta -Tenant <String> -ClientId <String> [-Scopes <String[]>]
 -JsonWebKey <ValidateNotNullOrEmptyAttribute> [-ProgressAction <ActionPreference>] [<CommonParameters>]
```

## DESCRIPTION
{{ Fill in the Description }}

## EXAMPLES

### Example 1
```powershell
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -ClientId
{{ Fill ClientId Description }}

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -JsonWebKey
{{ Fill JsonWebKey Description }}

```yaml
Type: ValidateNotNullOrEmptyAttribute
Parameter Sets: ClientCredentials
Aliases: jwk

Required: True
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -ProgressAction
{{ Fill ProgressAction Description }}

```yaml
Type: ActionPreference
Parameter Sets: (All)
Aliases: proga

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Scopes
{{ Fill Scopes Description }}

```yaml
Type: String[]
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Tenant
{{ Fill Tenant Description }}

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS
