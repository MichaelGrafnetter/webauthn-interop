using System;
using System.Collections.Generic;
using DSInternals.Win32.WebAuthn.Interop;
using Microsoft.Win32;

namespace DSInternals.Win32.WebAuthn
{
    public partial class WebAuthnApi
    {
        /// <summary>
        /// Indicates the availability of user-verifying platform authenticator (e.g. Windows Hello).
        /// </summary>
        public static bool IsUserVerifyingPlatformAuthenticatorAvailable
        {
            get
            {
                try
                {
                    var result = NativeMethods.IsUserVerifyingPlatformAuthenticatorAvailable(out var value);
                    ApiHelper.Validate(result);
                    return value;
                }
                catch (TypeLoadException)
                {
                    // If the IsUserVerifyingPlatformAuthenticatorAvailable function cannot be found, the feature is definitely not supported.
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets the list of stored credentials.
        /// </summary>
        /// <param name="rpId">Optional Id of the relying party that is making the request.</param>
        /// <param name="browserInPrivateMode">Indicates whether the browser is in private mode.</param>
        /// <returns>The list of platform credentials matching the filter, or <see langword="null"/> when none are present.</returns>
        /// <exception cref="NotSupportedException">Thrown when the running OS does not support platform credential management (added in API V4).</exception>
        public static IReadOnlyList<CredentialDetails>? GetPlatformCredentialList(string? rpId = null, bool browserInPrivateMode = false)
        {
            if (IsPlatformCredentialManagementSupported == false)
            {
                // This feature is only supported in API V4.
                throw new NotSupportedException("Credential API is not supported on this OS.");
            }

            var options = new GetCredentialsOptions()
            {
                RpId = string.IsNullOrEmpty(rpId) ? null : rpId,
                BrowserInPrivateMode = browserInPrivateMode
            };

            // Perform the Win32 API call
            var result = NativeMethods.GetPlatformCredentialList(options, out var credentialListHandle);
            ApiHelper.Validate(result);

            try
            {
                // Wrap the raw results
                var credentialList = credentialListHandle.ToManaged();
                return ApiHelper.Translate(credentialList);
            }
            finally
            {
                // Release native buffers.
                credentialListHandle.Dispose();
            }
        }

        /// <summary>
        /// Removes a public key credential stored on the platform authenticator.
        /// </summary>
        /// <param name="credentialId">The ID of the credential to be removed.</param>
        /// <exception cref="NotSupportedException">Thrown when the running OS does not support platform credential management (added in API V4).</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="credentialId"/> is <see langword="null"/>.</exception>
        public static void DeletePlatformCredential(byte[] credentialId)
        {
            if (IsPlatformCredentialManagementSupported == false)
            {
                // This feature is only supported in API V4.
                throw new NotSupportedException("Credential API is not supported on this OS.");
            }

            ArgumentNullException.ThrowIfNull(credentialId);

            // Perform the Win32 API call
            var result = NativeMethods.DeletePlatformCredential(credentialId.Length, credentialId);

            ApiHelper.Validate(result);
        }

        /// <summary>
        /// Gets the list of available authenticators.
        /// </summary>
        /// <returns>The list of authenticators currently visible to the platform, or <see langword="null"/> when none are present.</returns>
        /// <exception cref="NotSupportedException">Thrown when the running OS does not support the authenticator list API (added in API V9).</exception>
        public static IList<AuthenticatorDetails>? GetAuthenticatorList()
        {
            if (IsAuthenticatorListSupported == false)
            {
                // This feature is only supported in API V9.
                throw new NotSupportedException("Authenticator list API is not supported on this OS.");
            }

            GetAuthenticatorListOptions options = new();

            // Perform the Win32 API call
            var result = NativeMethods.GetAuthenticatorList(options, out var authenticatorListHandle);
            ApiHelper.Validate(result);

            try
            {
                // Wrap the raw results
                var authenticatorList = authenticatorListHandle.ToManaged();
                return ApiHelper.Translate(authenticatorList);
            }
            finally
            {
                // Release native buffers.
                authenticatorListHandle.Dispose();
            }
        }

        /// <summary>
        /// Gets the list of registered authenticator plugins from the Windows registry.
        /// </summary>
        /// <returns>A list of authenticator plugin information, or null if no plugins are registered.</returns>
        /// <remarks>
        /// Authenticator plugins (e.g., 1Password, Bitwarden) are registered under
        /// HKLM\SOFTWARE\Microsoft\FIDO\{UserSID}\Plugins\{PluginGuid}.
        /// </remarks>
        public static IList<AuthenticatorPluginInformation>? GetPluginAuthenticators()
        {
            const string FidoRegistryPath = @"SOFTWARE\Microsoft\FIDO";
            var plugins = new List<AuthenticatorPluginInformation>();

            using var fidoKey = Registry.LocalMachine.OpenSubKey(FidoRegistryPath);
            if (fidoKey == null)
            {
                return null;
            }

            // Enumerate user SID subkeys
            foreach (var userSid in fidoKey.GetSubKeyNames())
            {
                using var userKey = fidoKey.OpenSubKey(userSid);
                if (userKey == null)
                {
                    continue;
                }

                using var pluginsKey = userKey.OpenSubKey("Plugins");
                if (pluginsKey == null)
                {
                    continue;
                }

                // Enumerate plugin GUID subkeys
                foreach (var pluginGuidString in pluginsKey.GetSubKeyNames())
                {
                    if (!Guid.TryParse(pluginGuidString, out var pluginGuid))
                    {
                        continue;
                    }

                    using var pluginKey = pluginsKey.OpenSubKey(pluginGuidString);
                    if (pluginKey == null)
                    {
                        continue;
                    }

                    var plugin = ReadPluginFromRegistry(pluginKey, userSid, pluginGuid);
                    plugins.Add(plugin);
                }
            }

            return plugins.Count > 0 ? plugins : null;
        }

        /// <summary>
        /// Reads authenticator plugin information from a registry key.
        /// </summary>
        /// <param name="pluginKey">The registry key that stores the plugin's values.</param>
        /// <param name="userSid">Security identifier of the user the plugin is registered for.</param>
        /// <param name="pluginGuid">CLSID that uniquely identifies the plugin.</param>
        private static AuthenticatorPluginInformation ReadPluginFromRegistry(RegistryKey pluginKey, string userSid, Guid pluginGuid)
        {
            var plugin = new AuthenticatorPluginInformation
            {
                UserSid = userSid,
                UserName = ApiHelper.ResolveSidToUserName(userSid),
                PluginClsid = pluginGuid,
                Name = pluginKey.GetValue("Name") as string,
                RpId = pluginKey.GetValue("RpId") as string,
                PackageFullName = pluginKey.GetValue("PackageFullName") as string,
                PackageFamilyName = pluginKey.GetValue("PackageFamilyName") as string,
                PublisherDisplayName = pluginKey.GetValue("PublisherDisplayName") as string,
                SigningKeyAlgorithm = pluginKey.GetValue("SigningKeyAlgorithm") as string,
                LightLogo = ApiHelper.DecodeBase64Logo(pluginKey.GetValue("Base64EncodedUtf16LightLogo") as string),
                DarkLogo = ApiHelper.DecodeBase64Logo(pluginKey.GetValue("Base64EncodedUtf16DarkLogo") as string),
                AuthenticatorInfo = pluginKey.GetValue("AuthenticatorInfo") as byte[]
            };

            // Read DWORD values
            if (pluginKey.GetValue("PackageSignatureKind") is int packageSignatureKind)
            {
                plugin.PackageSignatureKind = (PackageSignatureKind)packageSignatureKind;
            }

            if (pluginKey.GetValue("State") is int state)
            {
                plugin.Enabled = state != 0;
            }

            if (pluginKey.GetValue("StateToggled") is int stateToggled)
            {
                plugin.StateToggled = stateToggled != 0;
            }

            if (pluginKey.GetValue("UvCount") is int uvCount)
            {
                plugin.UvCount = (uint)uvCount;
            }

            // Read AaGuid - stored as a string in registry format "{GUID}"
            var aaGuidString = pluginKey.GetValue("AaGuid") as string;
            if (!string.IsNullOrEmpty(aaGuidString) && Guid.TryParse(aaGuidString, out var aaGuid))
            {
                plugin.AaGuid = aaGuid;
            }

            return plugin;
        }
    }
}
