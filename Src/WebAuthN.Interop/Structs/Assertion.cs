using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// Assertion
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_ASSERTION.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    public class Assertion
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_ASSERTION_CURRENT_VERSION.</remarks>
        private const uint CurrentVersion = 1;

        /// <summary>
        /// // Version of this structure, to allow for modifications in the future.
        /// </summary>
        uint Version;

        /// <summary>
        /// Size of cbAuthenticatorData.
        /// </summary>
        uint AuthenticatorDataLength;

        /// <summary>
        /// Authenticator data that was created for this assertion.
        /// </summary>
        byte[] AuthenticatorData;

        /// <summary>
        /// Size of pbSignature.
        /// </summary>
        uint SignatureLength;

        /// <summary>
        /// Signature that was generated for this assertion.
        /// </summary>
        byte[] Signature;

        /// <summary>
        /// Credential that was used for this assertion.
        /// </summary>
        Credential Credential;

        /// <summary>
        /// Size of User Id.
        /// </summary>
        uint UserIdLength;

        /// <summary>
        /// UserId
        /// </summary>
        byte[] UserId;
    }
}
