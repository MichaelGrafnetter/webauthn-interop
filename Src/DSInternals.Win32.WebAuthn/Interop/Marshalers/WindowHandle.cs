using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public struct WindowHandle
    {
        private IntPtr _handle;

        public WindowHandle(IntPtr handle)
        {
            _handle = handle;
        }

        public bool IsValid => _handle != IntPtr.Zero;

        public static WindowHandle ForegroundWindow => NativeMethods.GetForegroundWindow();

        public static WindowHandle MainWindow => new WindowHandle(Process.GetCurrentProcess().MainWindowHandle);
    }
}
