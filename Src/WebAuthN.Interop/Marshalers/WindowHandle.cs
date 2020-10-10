using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowHandle
    {
        private IntPtr _handle;

        private WindowHandle(IntPtr handle)
        {
            _handle = handle;
        }

        public static WindowHandle ForegroundWindow => NativeMethods.GetForegroundWindow();

        public static WindowHandle MainWindow => new WindowHandle(Process.GetCurrentProcess().MainWindowHandle);

        public bool IsValid => _handle != IntPtr.Zero;
    }
}
