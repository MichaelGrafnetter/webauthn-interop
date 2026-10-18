using System.Collections.Generic;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Merges OS-produced credential JSON with fields returned through the native WebAuthn API.
/// </summary>
internal static class CredentialResponseMerger
{
    /// <summary>
    /// Adds native attestation fields and extension outputs missing from the parsed JSON credential.
    /// </summary>
    /// <param name="json">Credential parsed from the OS-produced JSON.</param>
    /// <param name="native">Credential assembled from native response fields.</param>
    /// <returns>The merged attestation credential.</returns>
    internal static AttestationPublicKeyCredential Merge(
        this AttestationPublicKeyCredential json,
        AttestationPublicKeyCredential native)
    {
        return new AttestationPublicKeyCredential
        {
            Id = json.Id,
            UnsignedExtensionOutputs = json.UnsignedExtensionOutputs ?? native.UnsignedExtensionOutputs,
            RawId = json.RawId ?? native.RawId,
            Type = json.Type,
            AuthenticatorAttachment = json.AuthenticatorAttachment != AuthenticatorAttachment.Any
                ? json.AuthenticatorAttachment
                : native.AuthenticatorAttachment,
            Response = new AuthenticatorAttestationResponse
            {
                ClientData = json.Response.ClientData ?? native.Response.ClientData,
                AttestationObject = json.Response.AttestationObject ?? native.Response.AttestationObject,
                AuthenticatorData = json.Response.AuthenticatorData ?? native.Response.AuthenticatorData,
                Transports = json.Response.Transports ?? native.Response.Transports,
                PublicKey = json.Response.PublicKey ?? native.Response.PublicKey,
                PublicKeyAlgorithm = json.Response.PublicKeyAlgorithm ?? native.Response.PublicKeyAlgorithm
            },
            ClientExtensionResults = Merge(json.ClientExtensionResults, native.ClientExtensionResults)
        };
    }

    /// <summary>
    /// Adds native assertion fields and extension outputs missing from the parsed JSON credential.
    /// </summary>
    /// <param name="json">Credential parsed from the OS-produced JSON.</param>
    /// <param name="native">Credential assembled from native response fields.</param>
    /// <returns>The merged assertion credential.</returns>
    internal static AssertionPublicKeyCredential Merge(
        this AssertionPublicKeyCredential json,
        AssertionPublicKeyCredential native)
    {
        return new AssertionPublicKeyCredential
        {
            Id = json.Id,
            UnsignedExtensionOutputs = json.UnsignedExtensionOutputs ?? native.UnsignedExtensionOutputs,
            RawId = json.RawId ?? native.RawId,
            Type = json.Type,
            AuthenticatorAttachment = json.AuthenticatorAttachment != AuthenticatorAttachment.Any
                ? json.AuthenticatorAttachment
                : native.AuthenticatorAttachment,
            Response = new AuthenticatorAssertionResponse
            {
                ClientData = json.Response.ClientData ?? native.Response.ClientData,
                AuthenticatorData = json.Response.AuthenticatorData ?? native.Response.AuthenticatorData,
                Signature = json.Response.Signature ?? native.Response.Signature,
                UserHandle = json.Response.UserHandle ?? native.Response.UserHandle
            },
            ClientExtensionResults = Merge(json.ClientExtensionResults, native.ClientExtensionResults)
        };
    }

    /// <summary>
    /// Merges attestation extension outputs, preferring populated JSON values and filling gaps from native output.
    /// </summary>
    /// <param name="json">Extension outputs parsed from JSON.</param>
    /// <param name="native">Extension outputs assembled from native fields.</param>
    /// <returns>The merged outputs, or <see langword="null"/> when both inputs are empty.</returns>
    private static AuthenticationExtensionsClientAttestationOutputs? Merge(
        AuthenticationExtensionsClientAttestationOutputs? json,
        AuthenticationExtensionsClientAttestationOutputs? native)
    {
        if (json == null)
        {
            return native;
        }

        if (native == null)
        {
            return json;
        }

        var merged = new AuthenticationExtensionsClientAttestationOutputs
        {
            AppIDExclude = json.AppIDExclude ?? native.AppIDExclude,
            CredentialProperties = Merge(json.CredentialProperties, native.CredentialProperties),
            HmacSecret = json.HmacSecret || native.HmacSecret,
            CredProtect = json.CredProtect != UserVerification.Any ? json.CredProtect : native.CredProtect,
            MinimumPinLength = json.MinimumPinLength ?? native.MinimumPinLength,
            CredentialBlobCreated = json.CredentialBlobCreated ?? native.CredentialBlobCreated,
            LargeBlob = Merge(json.LargeBlob, native.LargeBlob),
            Prf = Merge(json.Prf, native.Prf),
            RemoteClientDataJson = json.RemoteClientDataJson ?? native.RemoteClientDataJson,
            Uvm = json.Uvm ?? native.Uvm,
            AdditionalProperties = Merge(json.AdditionalProperties, native.AdditionalProperties)
        };

        return merged.IsEmpty ? null : merged;
    }

    /// <summary>
    /// Merges assertion extension outputs, preferring populated JSON values and filling gaps from native output.
    /// </summary>
    /// <param name="json">Extension outputs parsed from JSON.</param>
    /// <param name="native">Extension outputs assembled from native fields.</param>
    /// <returns>The merged outputs, or <see langword="null"/> when both inputs are empty.</returns>
    private static AuthenticationExtensionsClientAssertionOutputs? Merge(
        AuthenticationExtensionsClientAssertionOutputs? json,
        AuthenticationExtensionsClientAssertionOutputs? native)
    {
        if (json == null)
        {
            return native;
        }

        if (native == null)
        {
            return json;
        }

        var merged = new AuthenticationExtensionsClientAssertionOutputs
        {
            AppID = json.AppID ?? native.AppID,
            HmacGetSecret = Merge(json.HmacGetSecret, native.HmacGetSecret),
            CredentialBlob = json.CredentialBlob ?? native.CredentialBlob,
            LargeBlob = Merge(json.LargeBlob, native.LargeBlob),
            Prf = Merge(json.Prf, native.Prf),
            RemoteClientDataJson = json.RemoteClientDataJson ?? native.RemoteClientDataJson,
            Uvm = json.Uvm ?? native.Uvm,
            AdditionalProperties = Merge(json.AdditionalProperties, native.AdditionalProperties)
        };

        return merged.IsEmpty ? null : merged;
    }

    /// <summary>
    /// Merges credential properties outputs, preferring populated JSON values.
    /// </summary>
    /// <param name="json">Credential properties parsed from JSON.</param>
    /// <param name="native">Credential properties assembled from native fields.</param>
    /// <returns>The merged output, or <see langword="null"/> when neither input is present.</returns>
    private static CredentialPropertiesOutputs? Merge(
        CredentialPropertiesOutputs? json,
        CredentialPropertiesOutputs? native)
    {
        if (json == null)
        {
            return native;
        }

        if (native == null)
        {
            return json;
        }

        return new CredentialPropertiesOutputs
        {
            ResidentKey = json.ResidentKey ?? native.ResidentKey
        };
    }

    /// <summary>
    /// Merges large-blob attestation outputs, preferring a positive JSON result.
    /// </summary>
    /// <param name="json">Large-blob outputs parsed from JSON.</param>
    /// <param name="native">Large-blob outputs assembled from native fields.</param>
    /// <returns>The merged output, or <see langword="null"/> when neither input is present.</returns>
    private static LargeBlobAttestationOutputs? Merge(
        LargeBlobAttestationOutputs? json,
        LargeBlobAttestationOutputs? native)
    {
        if (json == null)
        {
            return native;
        }

        if (native == null || json.Supported)
        {
            return json;
        }

        return new LargeBlobAttestationOutputs
        {
            Supported = native.Supported
        };
    }

    /// <summary>
    /// Merges large-blob assertion outputs, preferring populated JSON values.
    /// </summary>
    /// <param name="json">Large-blob outputs parsed from JSON.</param>
    /// <param name="native">Large-blob outputs assembled from native fields.</param>
    /// <returns>The merged output, or <see langword="null"/> when neither input is present.</returns>
    private static LargeBlobAssertionOutputs? Merge(
        LargeBlobAssertionOutputs? json,
        LargeBlobAssertionOutputs? native)
    {
        if (json == null)
        {
            return native;
        }

        if (native == null)
        {
            return json;
        }

        return new LargeBlobAssertionOutputs
        {
            Blob = json.Blob ?? native.Blob,
            Written = json.Written ?? native.Written
        };
    }

    /// <summary>
    /// Merges PRF attestation outputs, retaining results from either source and enabled state.
    /// </summary>
    /// <param name="json">PRF outputs parsed from JSON.</param>
    /// <param name="native">PRF outputs assembled from native fields.</param>
    /// <returns>The merged output, or <see langword="null"/> when neither input is present.</returns>
    private static PRFAttestationOutputs? Merge(
        PRFAttestationOutputs? json,
        PRFAttestationOutputs? native)
    {
        if (json == null)
        {
            return native;
        }

        if (native == null)
        {
            return json;
        }

        return new PRFAttestationOutputs
        {
            Enabled = json.Enabled || native.Enabled,
            Results = Merge(json.Results, native.Results)
        };
    }

    /// <summary>
    /// Merges PRF assertion outputs, preferring populated JSON values.
    /// </summary>
    /// <param name="json">PRF outputs parsed from JSON.</param>
    /// <param name="native">PRF outputs assembled from native fields.</param>
    /// <returns>The merged output, or <see langword="null"/> when neither input is present.</returns>
    private static PRFAssertionOutputs? Merge(
        PRFAssertionOutputs? json,
        PRFAssertionOutputs? native)
    {
        if (json == null)
        {
            return native;
        }

        if (native == null)
        {
            return json;
        }

        return new PRFAssertionOutputs
        {
            Results = Merge(json.Results, native.Results)
        };
    }

    /// <summary>
    /// Merges HMAC-secret outputs, preferring populated JSON values.
    /// </summary>
    /// <param name="json">HMAC-secret outputs parsed from JSON.</param>
    /// <param name="native">HMAC-secret outputs assembled from native fields.</param>
    /// <returns>The merged output, or <see langword="null"/> when neither input is present.</returns>
    private static HMACGetSecretOutput? Merge(
        HMACGetSecretOutput? json,
        HMACGetSecretOutput? native)
    {
        if (json == null)
        {
            return native;
        }

        if (native == null)
        {
            return json;
        }

        return new HMACGetSecretOutput
        {
            Output1 = json.Output1 ?? native.Output1,
            Output2 = json.Output2 ?? native.Output2
        };
    }

    /// <summary>
    /// Merges PRF values, preferring populated JSON values.
    /// </summary>
    /// <param name="json">PRF values parsed from JSON.</param>
    /// <param name="native">PRF values assembled from native fields.</param>
    /// <returns>The merged values, or <see langword="null"/> when neither input is present.</returns>
    private static PRFValues? Merge(PRFValues? json, PRFValues? native)
    {
        if (json == null)
        {
            return native;
        }

        if (native == null)
        {
            return json;
        }

        return new PRFValues
        {
            First = json.First ?? native.First,
            Second = json.Second ?? native.Second
        };
    }

    /// <summary>
    /// Combines unrecognized extension properties, with JSON properties taking precedence.
    /// </summary>
    /// <param name="json">Additional properties parsed from JSON.</param>
    /// <param name="native">Additional properties assembled from native fields.</param>
    /// <returns>The combined property dictionary.</returns>
    private static Dictionary<string, System.Text.Json.JsonElement> Merge(
        Dictionary<string, System.Text.Json.JsonElement>? json,
        Dictionary<string, System.Text.Json.JsonElement>? native)
    {
        if (json == null)
        {
            return native ?? new Dictionary<string, System.Text.Json.JsonElement>();
        }

        if (native == null)
        {
            return json;
        }

        var merged = new Dictionary<string, System.Text.Json.JsonElement>(native);
        foreach (var property in json)
        {
            merged[property.Key] = property.Value;
        }

        return merged;
    }
}
