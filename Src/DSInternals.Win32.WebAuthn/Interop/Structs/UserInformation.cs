using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// Information about a User Entity
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_USER_ENTITY_INFORMATION.</remarks>
    /// <see>https://www.w3.org/TR/webauthn-2/#dictdef-publickeycredentialuserentity</see>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class UserInformation : IDisposable
    {
        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        private UserInformationVersion Version = UserInformationVersion.Current;

        private int _idLength;

        private ByteArrayIn _id;

        /// <summary>
        /// Contains a detailed name for this account, such as "john.p.smith@example.com".
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Optional URL that can be used to retrieve an image containing the user's current avatar, or a data URI that contains the image data.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Contains the friendly name associated with the user account by the Relying Party, such as "John P. Smith".
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Identifier for the User.
        /// </summary>
        public byte[] Id
        {
            get
            {
                return _id?.Read(_idLength);
            }
            set
            {
                _id?.Dispose();
                _idLength = value?.Length ?? 0;
                _id = new ByteArrayIn(value);
            }
        }

        public void Dispose()
        {
            _id?.Dispose();
            _id = null;
        }
    }
}
