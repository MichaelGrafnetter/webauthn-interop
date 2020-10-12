using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace WebAuthN.Interop
{
    internal class AssertionSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        private AssertionSafeHandle() : base(true) { }

        protected override bool ReleaseHandle()
        {
            NativeMethods.FreeAssertion(this.handle);
            return true;
        }

        internal Assertion ToManaged()
        {
            if (this.IsInvalid)
            {
                return null;
            }

            return Marshal.PtrToStructure<Assertion>(this.handle);
        }
    }
}
