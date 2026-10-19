using System;
using System.Runtime.InteropServices;
using DSInternals.Win32.WebAuthn.FIDO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Windows.Win32.Foundation;

namespace DSInternals.Win32.WebAuthn.Interop.Tests
{
    /// <summary>Verifies native management structure layouts and bounded pointer copying.</summary>
    [TestClass]
    public class ManagementInteropTester
    {
        /// <summary>Allows policy initialization while preserving null defaults for unreported settings.</summary>
        [TestMethod]
        public void AuthenticatorPinPolicy_SupportsObjectInitialization()
        {
            var empty = new AuthenticatorPinPolicy();
            Assert.AreEqual("MinPinLength=N/A, ForcePinChange=N/A, AlwaysUV=N/A", empty.ToString());
            Assert.IsNull(empty.MinimumPinLength);
            Assert.IsNull(empty.ForcePinChangeRequired);
            Assert.IsNull(empty.AuthenticatorConfigSupported);
            Assert.IsNull(empty.SetMinPinLengthSupported);
            Assert.IsNull(empty.MaximumRpIds);
            Assert.IsNull(empty.EnterpriseAttestationEnabled);
            Assert.IsNull(empty.AlwaysUvEnabled);
            Assert.IsNull(empty.MaximumLargeBlobBytes);

            var policy = new AuthenticatorPinPolicy
            {
                MinimumPinLength = 8,
                ForcePinChangeRequired = false,
                AuthenticatorConfigSupported = true,
                SetMinPinLengthSupported = true,
                MaximumRpIds = 0,
                EnterpriseAttestationEnabled = false,
                AlwaysUvEnabled = true,
                MaximumLargeBlobBytes = 4096
            };
            Assert.AreEqual(8u, policy.MinimumPinLength);
            Assert.AreEqual(false, policy.ForcePinChangeRequired);
            Assert.AreEqual(true, policy.AuthenticatorConfigSupported);
            Assert.AreEqual(true, policy.SetMinPinLengthSupported);
            Assert.AreEqual(0u, policy.MaximumRpIds);
            Assert.AreEqual(false, policy.EnterpriseAttestationEnabled);
            Assert.AreEqual(true, policy.AlwaysUvEnabled);
            Assert.AreEqual(4096u, policy.MaximumLargeBlobBytes);
            Assert.AreEqual("MinPinLength=8, ForcePinChange=False, AlwaysUV=True", policy.ToString());
        }

        /// <summary>Allows credential records to be created with init-only properties and optional account details.</summary>
        [TestMethod]
        public void DiscoverableCredentialDetails_SupportsObjectInitialization()
        {
            byte[] id = { 1, 2, 3 };
            var credential = new DiscoverableCredentialDetails
            {
                CredentialId = id,
                RelyingPartyId = "example.com",
                UserName = "user",
                UserDisplayName = "Example User"
            };
            Assert.AreSame(id, credential.CredentialId);
            Assert.AreEqual("example.com", credential.RelyingPartyId);
            Assert.AreEqual("user", credential.UserName);
            Assert.AreEqual("Example User", credential.UserDisplayName);

            var minimal = new DiscoverableCredentialDetails { CredentialId = id };
            Assert.IsNull(minimal.RelyingPartyId);
            Assert.IsNull(minimal.UserName);
            Assert.IsNull(minimal.UserDisplayName);
        }

        /// <summary>Checks conversion between nullable booleans and native reported setting states.</summary>
        /// <param name="value">The managed setting value.</param>
        /// <param name="expected">The corresponding native setting state.</param>
        [TestMethod]
        [DataRow(false, -1)]
        [DataRow(null, 0)]
        [DataRow(true, 1)]
        public void ReportedBooleanConversions_RoundTrip(bool? value, int expected)
        {
            ReportedBoolean native = value.ToReportedBoolean();
            Assert.AreEqual((ReportedBoolean)expected, native);
            Assert.AreEqual(value, native.ToNullableBoolean());
        }

        /// <summary>Checks fingerprint sensor kinds against the CTAP2 bio-enrollment response values.</summary>
        [TestMethod]
        public void FingerprintKind_UsesCtap2SensorValues()
        {
            Assert.AreEqual(1u, (uint)AuthenticatorFingerprintKind.Touch);
            Assert.AreEqual(2u, (uint)AuthenticatorFingerprintKind.Swipe);
        }

        /// <summary>Checks context and display-record alignment for the current process's pointer size.</summary>
        [TestMethod]
        public void ManagementStructs_UseNativePointerAlignment()
        {
            bool pointers64Bit = IntPtr.Size == 8;
            Assert.AreEqual(pointers64Bit ? 48 : 36, Marshal.SizeOf<ManagementContextV1>());
            Assert.AreEqual(pointers64Bit ? 88 : 80, Marshal.SizeOf<ManagementContextV2>());
            int policyOffset = pointers64Bit ? 44 : 36;
            Assert.AreEqual(policyOffset, Offset<ManagementContextV2>("_authenticatorConfig"));
            Assert.AreEqual(policyOffset + 4, Offset<ManagementContextV2>("_enterpriseAttestation"));
            Assert.AreEqual(policyOffset + 12, Offset<ManagementContextV2>("_minimumPinLength"));
            Assert.AreEqual(policyOffset + 40, Offset<ManagementContextV2>("_remainingCredentials"));

            Assert.AreEqual(pointers64Bit ? 56 : 32, Marshal.SizeOf<ManagementCredentialV1>());
            Assert.AreEqual(IntPtr.Size, Offset<ManagementCredentialV1>(nameof(ManagementCredentialV1.RelyingPartyId)));
            Assert.AreEqual(IntPtr.Size * 4, Offset<ManagementCredentialV1>(nameof(ManagementCredentialV1.CredentialIdLength)));
            Assert.AreEqual(IntPtr.Size * 5, Offset<ManagementCredentialV1>(nameof(ManagementCredentialV1.CredentialId)));
            Assert.AreEqual(pointers64Bit ? 72 : 40, Marshal.SizeOf<ManagementCredentialV3>());
            Assert.AreEqual(Marshal.SizeOf<ManagementCredentialV1>(), Offset<ManagementCredentialV3>(nameof(ManagementCredentialV3.AdditionalDataLength)));
            Assert.AreEqual(pointers64Bit ? 64 : 36, Offset<ManagementCredentialV3>(nameof(ManagementCredentialV3.AdditionalData)));
            Assert.AreEqual(pointers64Bit ? 104 : 72, Marshal.SizeOf<ManagementCredentialV4>());
            Assert.AreEqual(Marshal.SizeOf<ManagementCredentialV3>(), Offset<ManagementCredentialV4>(nameof(ManagementCredentialV4.Unknown48)));
            foreach (var field in typeof(ManagementCredentialV1).GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic))
            {
                Assert.AreEqual(Offset<ManagementCredentialV1>(field.Name), Offset<ManagementCredentialV3>(field.Name), field.Name);
                Assert.AreEqual(Offset<ManagementCredentialV1>(field.Name), Offset<ManagementCredentialV4>(field.Name), field.Name);
            }
            Assert.AreEqual(Offset<ManagementCredentialV3>(nameof(ManagementCredentialV3.AdditionalDataLength)),
                Offset<ManagementCredentialV4>(nameof(ManagementCredentialV4.AdditionalDataLength)));
            Assert.AreEqual(Offset<ManagementCredentialV3>(nameof(ManagementCredentialV3.AdditionalData)),
                Offset<ManagementCredentialV4>(nameof(ManagementCredentialV4.AdditionalData)));
            Assert.AreEqual(IntPtr.Size * 2, Marshal.SizeOf<ManagementCredentialList>());
        }

        /// <summary>Checks v2 field interpretation, nullable values, and signed state decoding.</summary>
        [TestMethod]
        public void CopyContext_UsesVersionedLayoutAndSeparatesSupportFromSignedStates()
        {
            IntPtr memory = Marshal.AllocHGlobal(Marshal.SizeOf<ManagementContextV2>());
            try
            {
                var context = new ManagementContextV2
                {
                    Version = 2,
                    Transport = AuthenticatorTransport.USB,
                    PinState = AuthenticatorPinState.Set,
                    PinRetries = 0,
                    FingerprintState = AuthenticatorFingerprintState.Empty,
                    FingerprintKind = AuthenticatorFingerprintKind.Touch,
                    CredentialManagementState = AuthenticatorCredentialManagementState.PinRequired
                };
                Marshal.StructureToPtr(context, memory, false);
                WriteContextField(memory, "_authenticatorConfig", 1);
                WriteContextField(memory, "_enterpriseAttestation", -1);
                WriteContextField(memory, "_forcePinChange", 1);
                WriteContextField(memory, "_maximumRpIds", 5);
                WriteContextField(memory, "_remainingCredentialsPresent", 1);

                var copied = ReadContext(memory);

                Assert.AreEqual(2u, copied.ContextVersion);
                Assert.AreEqual(AuthenticatorTransport.USB, copied.Transport);
                Assert.AreEqual(AuthenticatorPinState.Set, copied.PinState);
                Assert.AreEqual(0u, copied.PinRetries);
                Assert.AreEqual(AuthenticatorFingerprintState.Empty, copied.FingerprintState);
                Assert.AreEqual(AuthenticatorFingerprintKind.Touch, copied.FingerprintKind);
                Assert.AreEqual(AuthenticatorCredentialManagementState.PinRequired, copied.CredentialManagementState);
                Assert.AreEqual(true, copied.PinPolicy.AuthenticatorConfigSupported);
                Assert.AreEqual(false, copied.PinPolicy.EnterpriseAttestationEnabled);
                Assert.IsNull(copied.PinPolicy.AlwaysUvEnabled);
                Assert.AreEqual(true, copied.PinPolicy.ForcePinChangeRequired);
                Assert.IsNull(copied.PinPolicy.SetMinPinLengthSupported);
                Assert.IsNull(copied.PinPolicy.MinimumPinLength);
                Assert.AreEqual(5u, copied.PinPolicy.MaximumRpIds);
                Assert.IsNull(copied.PinPolicy.MaximumLargeBlobBytes);
                Assert.IsNull(copied.MaximumMessageSize);
                Assert.AreEqual(0u, copied.RemainingDiscoverableCredentials);

                WriteContextField(memory, "_remainingCredentialsPresent", 0);
                WriteContextField(memory, "_remainingCredentials", 9);
                Assert.IsNull(ReadContext(memory).RemainingDiscoverableCredentials);

                WriteContextField(memory, "_remainingCredentialsPresent", 2);
                Assert.AreEqual(9u, ReadContext(memory).RemainingDiscoverableCredentials);
            }
            finally
            {
                Marshal.FreeHGlobal(memory);
            }
        }

        /// <summary>Ensures v1 context padding is not interpreted as a v2 policy suffix.</summary>
        [TestMethod]
        public void CopyContext_V1DoesNotPromoteTailPaddingIntoV2Fields()
        {
            IntPtr memory = Marshal.AllocHGlobal(Marshal.SizeOf<ManagementContextV2>());
            try
            {
                var v2 = new ManagementContextV2 { Version = 1 };
                Marshal.StructureToPtr(v2, memory, false);
                WriteContextField(memory, "_authenticatorConfig", 1);
                WriteContextField(memory, "_enterpriseAttestation", 1);
                WriteContextField(memory, "_alwaysUv", 1);
                WriteContextField(memory, "_minimumPinLength", 8);
                WriteContextField(memory, "_remainingCredentialsPresent", 1);
                var copied = ReadContext(memory);
                Assert.AreEqual(1u, copied.ContextVersion);
                Assert.IsNull(copied.PinPolicy.AuthenticatorConfigSupported);
                Assert.IsNull(copied.PinPolicy.EnterpriseAttestationEnabled);
                Assert.IsNull(copied.PinPolicy.MinimumPinLength);
                Assert.IsNull(copied.RemainingDiscoverableCredentials);

                Marshal.WriteInt32(memory, 3);
                Assert.ThrowsExactly<NotSupportedException>(() => ReadContext(memory));
                Assert.ThrowsExactly<InvalidOperationException>(() => ReadContext(IntPtr.Zero));
            }
            finally
            {
                Marshal.FreeHGlobal(memory);
            }
        }

        /// <summary>Checks v1-v4 credential prefixes and managed-copy lifetime after native release.</summary>
        [TestMethod]
        public void CopyCredentials_AcceptsReviewedVersionsAndReturnsIndependentCopies()
        {
            foreach (uint version in new uint[] { 1, 2, 3, 4 })
            {
                byte[] originalId = { 0x10, 0x20, 0x30 };
                using var allocation = ManagementInteropNativeCredentialList.Create(version, originalId);
                var copied = allocation.ToManaged();

                Assert.AreEqual(1, copied.Count);
                CollectionAssert.AreEqual(originalId, copied[0].CredentialId);
                Assert.AreEqual("example.com", copied[0].RelyingPartyId);
                Assert.AreEqual("alice", copied[0].UserName);
                Assert.AreEqual("Alice Example", copied[0].UserDisplayName);

                allocation.Dispose();
                allocation.Dispose();
                CollectionAssert.AreEqual(originalId, copied[0].CredentialId);
                Assert.AreEqual("alice", copied[0].UserName);
                Assert.AreEqual(1, allocation.ReleaseCount);
            }
        }

        /// <summary>Rejects unknown credential versions and malformed count, array, and ID bounds.</summary>
        [TestMethod]
        public void CopyCredentials_RejectsUnknownVersionsAndMalformedBounds()
        {
            using var invalid = new ManagementCredentialListSafeHandle();
            Assert.ThrowsExactly<InvalidOperationException>(() => invalid.ToManaged());

            using (var empty = ManagementInteropNativeCredentialList.CreateEmpty(0, IntPtr.Zero))
            {
                Assert.AreEqual(0, empty.ToManaged().Count);
            }
            using (var nullArray = ManagementInteropNativeCredentialList.CreateEmpty(1, IntPtr.Zero))
            {
                Assert.ThrowsExactly<InvalidOperationException>(() => nullArray.ToManaged());
            }
            using (var oversized = ManagementInteropNativeCredentialList.CreateEmpty(uint.MaxValue, new IntPtr(1)))
            {
                Assert.ThrowsExactly<OverflowException>(() => oversized.ToManaged());
            }
            using (var nullId = ManagementInteropNativeCredentialList.Create(1, null))
            {
                Assert.ThrowsExactly<InvalidOperationException>(() => nullId.ToManaged());
            }
            using (var oversizedId = ManagementInteropNativeCredentialList.CreateWithCredentialLength(uint.MaxValue, new IntPtr(1)))
            {
                Assert.ThrowsExactly<OverflowException>(() => oversizedId.ToManaged());
            }
            using (var unknown = ManagementInteropNativeCredentialList.Create(5, new byte[] { 1 }))
            {
                Assert.ThrowsExactly<NotSupportedException>(() => unknown.ToManaged());
            }
        }

        /// <summary>Copies credential identifiers larger than the former local one-MiB limit.</summary>
        [TestMethod]
        public void CopyCredentials_AcceptsLargeIdentifiers()
        {
            var id = new byte[1024 * 1024 + 1];
            id[0] = 0x10;
            id[id.Length - 1] = 0x20;
            using var allocation = ManagementInteropNativeCredentialList.Create(1, id);
            var copied = allocation.ToManaged();
            CollectionAssert.AreEqual(id, copied[0].CredentialId);
        }

        /// <summary>Checks that all setting wrappers preserve the three reported boolean states.</summary>
        /// <param name="nativeValue">The native setting encoding.</param>
        /// <param name="expected">The corresponding nullable boolean.</param>
        [TestMethod]
        [DataRow(-1, false)]
        [DataRow(0, null)]
        [DataRow(1, true)]
        public void ContextProperties_DecodeReportedBooleans(int nativeValue, bool? expected)
        {
            IntPtr memory = Marshal.AllocHGlobal(Marshal.SizeOf<ManagementContextV2>());
            try
            {
                Marshal.StructureToPtr(new ManagementContextV2(), memory, false);
                WriteContextField(memory, "_enterpriseAttestation", nativeValue);
                WriteContextField(memory, "_alwaysUv", nativeValue);
                WriteContextField(memory, "_forcePinChange", nativeValue);
                var context = Marshal.PtrToStructure<ManagementContextV2>(memory);
                Assert.AreEqual(expected, context.EnterpriseAttestation);
                Assert.AreEqual(expected, context.AlwaysUv);
                Assert.AreEqual(expected, context.ForcePinChange);
            }
            finally
            {
                Marshal.FreeHGlobal(memory);
            }
        }

        /// <summary>Checks that support and capacity wrappers preserve reported and unreported values.</summary>
        /// <param name="reported">Whether native support and capacity values are supplied.</param>
        [TestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public void ContextProperties_DecodeSupportAndCounts(bool reported)
        {
            IntPtr memory = Marshal.AllocHGlobal(Marshal.SizeOf<ManagementContextV2>());
            try
            {
                Marshal.StructureToPtr(new ManagementContextV2(), memory, false);
                int support = reported ? 1 : 0;
                int count = reported ? 8 : 0;
                WriteContextField(memory, "_authenticatorConfig", support);
                WriteContextField(memory, "_setMinPinLength", support);
                WriteContextField(memory, "_minimumPinLength", count);
                WriteContextField(memory, "_maximumRpIds", count);
                WriteContextField(memory, "_maximumLargeBlobBytes", count);
                WriteContextField(memory, "_maximumMessageSize", count);
                WriteContextField(memory, "_remainingCredentialsPresent", support);
                var context = Marshal.PtrToStructure<ManagementContextV2>(memory);
                bool? expectedSupport = reported ? true : null;
                uint? expectedCount = reported ? 8u : null;
                Assert.AreEqual(expectedSupport, context.AuthenticatorConfig);
                Assert.AreEqual(expectedSupport, context.SetMinPinLength);
                Assert.AreEqual(expectedCount, context.MinimumPinLength);
                Assert.AreEqual(expectedCount, context.MaximumRpIds);
                Assert.AreEqual(expectedCount, context.MaximumLargeBlobBytes);
                Assert.AreEqual(expectedCount, context.MaximumMessageSize);
                Assert.AreEqual(reported ? (uint?)0 : null, context.RemainingCredentials);
            }
            finally
            {
                Marshal.FreeHGlobal(memory);
            }
        }

        /// <summary>Preserves a zero RP-ID limit when minimum PIN length configuration is supported.</summary>
        /// <param name="nativeValue">The native support flag, with nonzero values indicating support.</param>
        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(-1)]
        public void ContextProperties_PreserveZeroRpIdLimit(int nativeValue)
        {
            IntPtr memory = Marshal.AllocHGlobal(Marshal.SizeOf<ManagementContextV2>());
            try
            {
                Marshal.StructureToPtr(new ManagementContextV2 { Version = 2 }, memory, false);
                WriteContextField(memory, "_setMinPinLength", nativeValue);
                WriteContextField(memory, "_authenticatorConfig", nativeValue);
                var context = Marshal.PtrToStructure<ManagementContextV2>(memory);
                bool? expectedSupport = nativeValue != 0 ? true : null;
                Assert.AreEqual(expectedSupport, context.AuthenticatorConfig);
                Assert.AreEqual(expectedSupport, context.SetMinPinLength);
                uint? expected = nativeValue != 0 ? 0u : null;
                Assert.AreEqual(expected, context.MaximumRpIds);
                Assert.AreEqual(expected, ReadContext(memory).PinPolicy.MaximumRpIds);
            }
            finally
            {
                Marshal.FreeHGlobal(memory);
            }
        }

        /// <summary>Rejects unsupported native state encodings through the nullable wrappers.</summary>
        /// <param name="field">The private native field to populate with an unrecognized value.</param>
        [TestMethod]
        [DataRow("_enterpriseAttestation")]
        [DataRow("_alwaysUv")]
        [DataRow("_forcePinChange")]
        public void CopyContext_RejectsUnknownStateValues(string field)
        {
            IntPtr memory = Marshal.AllocHGlobal(Marshal.SizeOf<ManagementContextV2>());
            try
            {
                Marshal.StructureToPtr(new ManagementContextV2 { Version = 2 }, memory, false);
                WriteContextField(memory, field, 2);
                Assert.ThrowsExactly<NotSupportedException>(() => ReadContext(memory));
            }
            finally
            {
                Marshal.FreeHGlobal(memory);
            }
        }

        /// <summary>Copies a native large-blob store independently of the buffer's lifetime.</summary>
        /// <param name="length">The store size, including a value above the former local limit.</param>
        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(16)]
        [DataRow(17)]
        [DataRow(16777217)]
        public void LargeBlobBuffer_ReadBytesCopiesAndReleasesAllocation(int length)
        {
            byte[] original = new byte[length];
            if (length > 0)
            {
                original[0] = 0x80;
            }
            IntPtr memory = Marshal.AllocCoTaskMem(Math.Max(1, original.Length));
            byte[] copied;
            using var buffer = new ManagementLargeBlobSafeHandle(memory);
            using (buffer)
            {
                Marshal.Copy(original, 0, memory, original.Length);
                copied = buffer.ReadBytes((uint)length);
                Marshal.WriteByte(memory, 0, 0);
            }
            CollectionAssert.AreEqual(original, copied);
            Assert.IsTrue(buffer.IsClosed);
        }

        /// <summary>Rejects native lengths that cannot be represented by a managed array length.</summary>
        [TestMethod]
        public void LargeBlobBuffer_ReadBytesRejectsLengthOverflow()
        {
            using var buffer = new ManagementLargeBlobSafeHandle();
            Assert.ThrowsExactly<OverflowException>(() => buffer.ReadBytes(uint.MaxValue));
        }

        /// <summary>Rejects invalid large-blob pointers even when Windows reports an otherwise valid length.</summary>
        /// <param name="pointer">An invalid native allocation address.</param>
        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        public void LargeBlobBuffer_ReadBytesRejectsInvalidBuffer(int pointer)
        {
            using var buffer = new ManagementLargeBlobSafeHandle(new IntPtr(pointer));
            Assert.IsTrue(buffer.IsInvalid);
            Assert.ThrowsExactly<InvalidOperationException>(() => buffer.ReadBytes(17));
        }

        /// <summary>Exercises context-handle conversion while leaving fixture memory owned by the caller.</summary>
        /// <param name="memory">Borrowed synthetic context allocation.</param>
        /// <returns>The independently copied capability snapshot.</returns>
        private static AuthenticatorManagementCapabilities ReadContext(IntPtr memory)
        {
            using var context = new ManagementTestContextSafeHandle(memory);
            try
            {
                return context.ToManaged();
            }
            finally
            {
                context.SetHandleAsInvalid();
            }
        }

        /// <summary>Writes a native context field without exposing its private storage to production callers.</summary>
        /// <param name="memory">The allocated native context.</param>
        /// <param name="field">The private field whose platform-dependent offset is used.</param>
        /// <param name="value">The native four-byte value to write.</param>
        private static void WriteContextField(IntPtr memory, string field, int value) =>
            Marshal.WriteInt32(memory, Offset<ManagementContextV2>(field), value);

        /// <summary>Returns the byte offset of a declared sequential-structure field.</summary>
        /// <typeparam name="T">Native structure whose field offset is inspected.</typeparam>
        /// <param name="field">Managed field name declared on the structure.</param>
        /// <returns>The field's byte offset from the structure base.</returns>
        private static int Offset<T>(string field) => checked((int)Marshal.OffsetOf<T>(field));
    }
}
