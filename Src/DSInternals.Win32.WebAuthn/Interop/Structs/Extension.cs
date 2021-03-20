﻿using System;
using System.Runtime.InteropServices;
using DSInternals.Win32.WebAuthn.FIDO;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// Information about Extension.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_EXTENSION.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class  ExtensionIn : IDisposable
    {
        /// <summary>
        /// Extension identifier.
        /// </summary>
        public string Identifier { get; private set; }

        private int _dataLength;

        /// <summary>
        /// Extension data.
        /// </summary>
        private ByteArrayIn _data;

        public ExtensionIn(string id, byte[] data)
        {
            Identifier = id;
            _dataLength = data?.Length ?? 0;
            _data = new ByteArrayIn(data);
        }

        public static ExtensionIn CreateHmacSecret()
        {
            // Below type definitions is for WEBAUTHN_EXTENSIONS_IDENTIFIER_HMAC_SECRET
            // MakeCredential Input Type:   BOOL.
            //      - pvExtension must point to a BOOL with the value TRUE.
            //      - cbExtension must contain the sizeof(BOOL).
            byte[] binaryTrue = BitConverter.GetBytes((int)1);
            return new ExtensionIn(ApiConstants.ExtensionsIdentifierHmacSecret, binaryTrue);
        }

        public static ExtensionIn CreateCredProtect(UserVerification uv, bool enforce)
        {
            // Below type definitions is for WEBAUTHN_EXTENSIONS_IDENTIFIER_CRED_PROTECT
            // MakeCredential Input Type:   WEBAUTHN_CRED_PROTECT_EXTENSION_IN.
            //      - pvExtension must point to a WEBAUTHN_CRED_PROTECT_EXTENSION_IN struct
            //      - cbExtension will contain the sizeof(WEBAUTHN_CRED_PROTECT_EXTENSION_IN).
            int structSize = sizeof(UserVerification) + sizeof(int);
            byte[] extensionData = new byte[structSize];
            BitConverter.GetBytes((int)uv).CopyTo(extensionData, 0);
            BitConverter.GetBytes(enforce ? (int)1 : (int)0).CopyTo(extensionData, sizeof(UserVerification));
            return new ExtensionIn(ApiConstants.ExtensionsIdentifierCredProtect, extensionData);
        }

        public void Dispose()
        {
            _data?.Dispose();
            _data = null;
        }
    }

    /// <summary>
    /// Information about Extension.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_EXTENSION.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class ExtensionOut
    {
        /// <summary>
        /// Extension identifier.
        /// </summary>
        public string Identifier { get; private set; }

        private int _dataLength;

        private ByteArrayOut _data;

        /// <summary>
        /// Extension data.
        /// </summary>
        public byte[] Data => _data?.Read(_dataLength);

        private ExtensionOut() { }
    }
}
