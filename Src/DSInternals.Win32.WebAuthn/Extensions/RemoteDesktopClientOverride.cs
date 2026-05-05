using System;
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
}
