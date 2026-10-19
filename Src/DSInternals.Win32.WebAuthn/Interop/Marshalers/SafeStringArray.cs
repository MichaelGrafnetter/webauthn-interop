using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>Owns a count-plus-pointer string array and each of its copied UTF-16 strings.</summary>
    [StructLayout(LayoutKind.Sequential)]
    internal class SafeStringArrayIn : IDisposable
    {
        /// <summary>Number of pointer slots allocated for the native array.</summary>
        protected int _length;

        /// <summary>Owned pointer array; each non-null slot owns one HGlobal string.</summary>
        protected IntPtr _nativeArray;

        /// <summary>Copies strings into native storage and cleans up partially allocated arrays on failure.</summary>
        /// <param name="items">Strings to copy, or null/empty for an empty native array.</param>
        public SafeStringArrayIn(string[]? items)
        {
            if (items == null || items.Length == 0)
            {
                return;
            }

            _length = items.Length;

            // Allocate memory
            int ptrSize = Marshal.SizeOf<IntPtr>();
            int arraySize = _length * ptrSize;
            _nativeArray = Marshal.AllocHGlobal(arraySize);

            // Zero every slot so Dispose can safely scan the entire array if a string allocation fails.
            for (int i = 0; i < _length; i++)
            {
                Marshal.WriteIntPtr(_nativeArray, ptrSize * i, IntPtr.Zero);
            }
            try
            {
                for (int i = 0; i < _length; i++)
                {
                    IntPtr stringPtr = Marshal.StringToHGlobalUni(items[i]);
                    Marshal.WriteIntPtr(_nativeArray, ptrSize * i, stringPtr);
                }
            }
            catch
            {
                // A failed constructor leaves no owner for the caller to dispose.
                // Free the strings allocated so far and their pointer array before rethrowing.
                Dispose();
                throw;
            }
        }

        /// <summary>Copies the owned native strings back into a managed array, or null when empty.</summary>
        public string[]? Items
        {
            get
            {
                if (_length == 0 || _nativeArray == IntPtr.Zero)
                {
                    return null;
                }

                string[] items = new string[_length];
                for (int i = 0; i < _length; i++)
                {
                    IntPtr stringPtr = Marshal.ReadIntPtr(_nativeArray + (Marshal.SizeOf<IntPtr>() * i));
                    items[i] = Marshal.PtrToStringUni(stringPtr) ?? string.Empty;
                }

                return items;
            }
        }

        /// <summary>Gets the borrowed native string-pointer array without extending its lifetime.</summary>
        /// <remarks>The owner must remain undisposed while the pointer is in use. The caller must not free the pointer.</remarks>
        /// <returns>The native string-pointer array, or zero when empty or disposed.</returns>
        internal IntPtr DangerousGetPointer() => _nativeArray;

        /// <summary>Frees all owned strings and their pointer array; repeated calls are safe.</summary>
        public void Dispose()
        {
            if (_nativeArray != IntPtr.Zero)
            {
                for (int i = 0; i < _length; i++)
                {
                    IntPtr stringPtr = Marshal.ReadIntPtr(_nativeArray + (Marshal.SizeOf<IntPtr>() * i));
                    if (stringPtr != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(stringPtr);
                    }
                }

                Marshal.FreeHGlobal(_nativeArray);
                _nativeArray = IntPtr.Zero;
            }
        }
    }
}
