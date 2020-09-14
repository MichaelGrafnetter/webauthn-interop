﻿using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// Information about a User Entity
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_USER_ENTITY_INFORMATION.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class UserInformation
    {
        /// <summary>
        /// Maximum length of the Identifier for the User, in bytes.
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_MAX_USER_ID_LENGTH.</remarks>
        private const int MaxIdLength = 64;

        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        private protected UserInformationVersion Version = UserInformationVersion.Current;

        /// <summary>
        /// Identifier for the User.
        /// </summary>
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(BufferMarshaler))]
        public Buffer Id;

        /// <summary>
        /// Contains a detailed name for this account, such as "john.p.smith@example.com".
        /// </summary>
        public string Name;

        /// <summary>
        /// Optional URL that can be used to retrieve an image containing the user's current avatar, or a data URI that contains the image data.
        /// </summary>
        public string Icon;

        /// <summary>
        /// Contains the friendly name associated with the user account by the Relying Party, such as "John P. Smith".
        /// </summary>
        public string DisplayName;
    }
}
