using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Inputs for Chromium's remoteDesktopClientOverride WebAuthn extension.
/// </summary>
public sealed class RemoteDesktopClientOverride
{
    /// <summary>
    /// Origin of the remote caller that issued the original WebAuthn request.
    /// </summary>
    [JsonPropertyName("origin")]
    [JsonRequired]
    public required string Origin { get; set; }

    /// <summary>
    /// Indicates whether the remote caller is same-origin with all of its ancestors.
    /// </summary>
    [JsonPropertyName("sameOriginWithAncestors")]
    public bool SameOriginWithAncestors { get; set; }

    /// <summary>
    /// Accepts the singular spelling used in early explainer examples while serializing the Chromium spelling.
    /// </summary>
    [JsonPropertyName("sameOriginWithAncestor")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [Obsolete("This property is only for deserializing the early explainer version of the extension input. Use SameOriginWithAncestors for new code.", error: false)]
    public bool? SameOriginWithAncestor
    {
        get => null;
        set
        {
            if (value.HasValue)
            {
                SameOriginWithAncestors = value.Value;
            }
        }
    }

    /// <summary>
    /// Deserializes a JSON string into remote desktop client override inputs.
    /// </summary>
    /// <param name="json">JSON representation of remote desktop client override inputs.</param>
    /// <returns>Remote desktop client override inputs if deserialization is successful; otherwise, null.</returns>
    public static RemoteDesktopClientOverride? FromJson(string json)
    {
        return WebAuthnJsonModel.FromJson(json, WebAuthnJsonContext.Default.RemoteDesktopClientOverride);
    }

    /// <summary>
    /// Serializes the remote desktop client override inputs to JSON.
    /// </summary>
    /// <returns>JSON representation of these remote desktop client override inputs.</returns>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.RemoteDesktopClientOverride);
    }
}
