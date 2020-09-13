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

        internal Assertion Marshal()
        {
            if (this.IsInvalid)
            {
                return null;
            }

            return System.Runtime.InteropServices.Marshal.PtrToStructure<Assertion>(this.handle);
        }
    }
}
