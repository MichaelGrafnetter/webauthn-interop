using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop.Tests
{
    /// <summary>Owns synthetic native allocations used to exercise credential list marshaling.</summary>
    internal sealed class ManagementInteropNativeCredentialList : ManagementCredentialListSafeHandle
    {
        /// <summary>Tracks every allocation that must be released with the synthetic native list.</summary>
        private readonly List<IntPtr> _allocations = new List<IntPtr>();

        /// <summary>Creates a fixture around an already allocated native list header.</summary>
        /// <param name="pointer">Pointer to the native list header to own.</param>
        private ManagementInteropNativeCredentialList(IntPtr pointer)
        {
            Pointer = pointer;
            _allocations.Add(pointer);
            SetHandle(pointer);
        }

        /// <summary>Gets the pointer passed to the production marshaler.</summary>
        internal IntPtr Pointer { get; }

        /// <summary>Gets the number of times the synthetic native list was released.</summary>
        internal int ReleaseCount { get; private set; }

        /// <summary>Builds one credential record with the specified private ABI version and ID.</summary>
        /// <param name="version">Display record version from one through four, or another value for rejection tests.</param>
        /// <param name="id">Credential ID bytes, or null to create an invalid nonempty null-pointer field.</param>
        /// <returns>An owned native list suitable for marshaler tests.</returns>
        internal static ManagementInteropNativeCredentialList Create(uint version, byte[]? id)
        {
            int recordSize = version switch
            {
                3 => Marshal.SizeOf<ManagementCredentialV3>(),
                4 => Marshal.SizeOf<ManagementCredentialV4>(),
                _ => Marshal.SizeOf<ManagementCredentialV1>()
            };
            var result = new ManagementInteropNativeCredentialList(
                Marshal.AllocHGlobal(Marshal.SizeOf<ManagementCredentialList>()));
            IntPtr record = result.Alloc(recordSize);
            IntPtr rp = result.AllocString("example.com");
            IntPtr name = result.AllocString("alice");
            IntPtr display = result.AllocString("Alice Example");
            IntPtr credentialId = id == null ? IntPtr.Zero : result.AllocBytes(id);
            var prefix = new ManagementCredentialV1
            {
                Version = version,
                RelyingPartyId = rp,
                UserName = name,
                DisplayName = display,
                CredentialIdLength = id == null ? 1u : checked((uint)id.Length),
                CredentialId = credentialId,
                Unknown30 = 7,
                Unknown34 = 9
            };
            if (version == 3)
            {
                Marshal.StructureToPtr(new ManagementCredentialV3
                {
                    Version = prefix.Version,
                    RelyingPartyId = prefix.RelyingPartyId,
                    UserName = prefix.UserName,
                    DisplayName = prefix.DisplayName,
                    CredentialIdLength = prefix.CredentialIdLength,
                    CredentialId = prefix.CredentialId,
                    Unknown30 = prefix.Unknown30,
                    Unknown34 = prefix.Unknown34
                }, record, false);
            }
            else if (version == 4)
            {
                Marshal.StructureToPtr(new ManagementCredentialV4
                {
                    Version = prefix.Version,
                    RelyingPartyId = prefix.RelyingPartyId,
                    UserName = prefix.UserName,
                    DisplayName = prefix.DisplayName,
                    CredentialIdLength = prefix.CredentialIdLength,
                    CredentialId = prefix.CredentialId,
                    Unknown30 = prefix.Unknown30,
                    Unknown34 = prefix.Unknown34
                }, record, false);
            }
            else
            {
                Marshal.StructureToPtr(prefix, record, false);
            }
            IntPtr records = result.Alloc(IntPtr.Size);
            Marshal.WriteIntPtr(records, record);
            Marshal.StructureToPtr(new ManagementCredentialList { Count = 1, Records = records }, result.Pointer, false);
            return result;
        }

        /// <summary>Builds a list header with caller-controlled count and record-array pointer.</summary>
        /// <param name="count">Reported native item count.</param>
        /// <param name="records">Native array pointer, including null or sentinel pointers for invalid cases.</param>
        /// <returns>An owned native list header.</returns>
        internal static ManagementInteropNativeCredentialList CreateEmpty(uint count, IntPtr records)
        {
            var result = new ManagementInteropNativeCredentialList(
                Marshal.AllocHGlobal(Marshal.SizeOf<ManagementCredentialList>()));
            Marshal.StructureToPtr(new ManagementCredentialList { Count = count, Records = records }, result.Pointer, false);
            return result;
        }

        /// <summary>Builds one v1 record with caller-controlled credential length and ID pointer.</summary>
        /// <param name="length">Reported credential-ID length.</param>
        /// <param name="id">Credential-ID pointer, including null or sentinel values.</param>
        /// <returns>An owned list that exposes the supplied bounds to the marshaler.</returns>
        internal static ManagementInteropNativeCredentialList CreateWithCredentialLength(uint length, IntPtr id)
        {
            var result = new ManagementInteropNativeCredentialList(
                Marshal.AllocHGlobal(Marshal.SizeOf<ManagementCredentialList>()));
            IntPtr record = result.Alloc(Marshal.SizeOf<ManagementCredentialV1>());
            IntPtr records = result.Alloc(IntPtr.Size);
            Marshal.StructureToPtr(new ManagementCredentialV1
            {
                Version = 1,
                CredentialIdLength = length,
                CredentialId = id
            }, record, false);
            Marshal.WriteIntPtr(records, record);
            Marshal.StructureToPtr(new ManagementCredentialList { Count = 1, Records = records }, result.Pointer, false);
            return result;
        }

        /// <summary>Allocates and tracks an unmanaged block for fixture data.</summary>
        /// <param name="size">Number of bytes to allocate.</param>
        /// <returns>The allocated unmanaged address.</returns>
        private IntPtr Alloc(int size)
        {
            IntPtr pointer = Marshal.AllocHGlobal(size);
            _allocations.Add(pointer);
            return pointer;
        }

        /// <summary>Allocates and tracks a null-terminated UTF-16 string.</summary>
        /// <param name="value">String value stored in native memory.</param>
        /// <returns>The unmanaged string address.</returns>
        private IntPtr AllocString(string value)
        {
            IntPtr pointer = Marshal.StringToHGlobalUni(value);
            _allocations.Add(pointer);
            return pointer;
        }

        /// <summary>Allocates and tracks a copy of credential bytes.</summary>
        /// <param name="value">Bytes to copy to unmanaged memory.</param>
        /// <returns>The unmanaged byte-buffer address.</returns>
        private IntPtr AllocBytes(byte[] value)
        {
            IntPtr pointer = Marshal.AllocHGlobal(value.Length);
            Marshal.Copy(value, 0, pointer, value.Length);
            _allocations.Add(pointer);
            return pointer;
        }

        /// <summary>Releases every unmanaged allocation exactly once.</summary>
        /// <returns>True after releasing all fixture allocations.</returns>
        protected override bool ReleaseHandle()
        {
            ReleaseCount++;
            foreach (IntPtr allocation in _allocations)
            {
                Marshal.FreeHGlobal(allocation);
            }
            return true;
        }
    }
}
