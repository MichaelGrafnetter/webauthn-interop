# <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeCredential"></a> Class CredentialExchangeCredential

Namespace: [DSInternals.Win32.WebAuthn.Cryptography](DSInternals.Win32.WebAuthn.Cryptography.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Base type for entries inside a CXF item's credentials array.

```csharp
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type", UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToBaseType, IgnoreUnrecognizedTypeDiscriminators = true)]
[JsonDerivedType(typeof(CredentialExchangePasskey), "passkey")]
public class CredentialExchangeCredential
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CredentialExchangeCredential](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeCredential.md)

#### Derived

[CredentialExchangePasskey](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangePasskey.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

CXF defines several credential types (e.g. <code>passkey</code>, <code>basic-auth</code>, <code>ssh-key</code>)
distinguished by a <code>type</code> discriminator. Only credential types that are explicitly
modeled by derived classes are deserialized into a typed instance; unknown discriminators
fall back to a bare <xref href="DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeCredential" data-throw-if-not-resolved="false"></xref> so the surrounding payload
still parses cleanly.

