using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>Copies borrowed native data while retaining its owning safe handle.</summary>
    internal static class SafeHandleExtensions
    {
        /// <summary>Retains the allocation while copying its borrowed data into managed storage.</summary>
        /// <typeparam name="T">The pointer-free managed result type.</typeparam>
        /// <param name="owner">The safe handle that owns the allocation.</param>
        /// <param name="copy">Copies data without taking ownership of the borrowed pointer.</param>
        /// <returns>The copied managed result.</returns>
        /// <remarks>Retention protects lifetime, not concurrent native context refreshes.</remarks>
        internal static T Read<T>(this SafeHandle owner, Func<IntPtr, T> copy)
        {
            bool held = false;
            try
            {
                owner.DangerousAddRef(ref held);
                if (owner.IsInvalid)
                {
                    throw new InvalidOperationException("The management API returned an invalid allocation.");
                }
                return copy(owner.DangerousGetHandle());
            }
            finally
            {
                if (held)
                {
                    owner.DangerousRelease();
                }
            }
        }
    }
}
