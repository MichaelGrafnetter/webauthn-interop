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
        /// // Version of this structure, to allow for modifications in the future.
        /// </summary>
        private protected AssertionVersion Version = AssertionVersion.Current;

        /// <summary>
        /// Authenticator data that was created for this assertion.
        /// </summary>
        VariableByteArray AuthenticatorData;

        /// <summary>
        /// Signature that was generated for this assertion.
        /// </summary>
        VariableByteArray Signature;

        /// <summary>
        /// Credential that was used for this assertion.
        /// </summary>
        Credential Credential;

        /// <summary>
        /// UserId
        /// </summary>
        VariableByteArray UserId;
    }
}
