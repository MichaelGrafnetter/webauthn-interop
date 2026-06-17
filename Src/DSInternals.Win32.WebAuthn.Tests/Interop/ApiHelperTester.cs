using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Windows.Win32.Foundation;

namespace DSInternals.Win32.WebAuthn.Interop.Tests
{
    [TestClass]
    public class ApiHelperTester
    {
        [TestMethod]
        public void ApiHelper_Validate_Success()
        {
            // Should not throw
            ApiHelper.Validate(HRESULT.S_OK);
        }

        [TestMethod]
        public void ApiHelper_Validate_Cancelled()
        {
            Assert.ThrowsExactly<OperationCanceledException>(() =>
            {
                ApiHelper.Validate(HRESULT.NTE_USER_CANCELLED);
            });
        }

        [TestMethod]
        public void ApiHelper_Validate_OtherError()
        {
            Assert.ThrowsExactly<Win32Exception>(() =>
            {
                ApiHelper.Validate(HRESULT.NTE_TOKEN_KEYSET_STORAGE_FULL);
            });
        }

        [TestMethod]
        public void ApiHelper_Translate_CredentialDetails_MapsCurrentNativeFields()
        {
            const string authenticatorName = "Windows Hello";
            const string authenticatorLogo = "<svg></svg>";
            byte[] authenticatorLogoBytes = Encoding.UTF8.GetBytes(authenticatorLogo);

            using var nativeAuthenticatorLogo = new ByteArrayIn(authenticatorLogoBytes);
            var nativeCredential = new CredentialDetailsOut
            {
                BackedUp = true,
                Removable = true
            };

            SetPrivateProperty(nativeCredential, nameof(CredentialDetailsOut.AuthenticatorName), authenticatorName);
            SetPrivateField(nativeCredential, "_authenticatorLogoLength", authenticatorLogoBytes.Length);
            SetPrivateField(nativeCredential, "_authenticatorLogo", nativeAuthenticatorLogo);
            SetPrivateProperty(nativeCredential, nameof(CredentialDetailsOut.ThirdPartyPayment), true);
            SetPrivateProperty(nativeCredential, nameof(CredentialDetailsOut.Transports), AuthenticatorTransport.Internal | AuthenticatorTransport.Hybrid);

            var credentials = ApiHelper.Translate(new[] { nativeCredential });

            Assert.IsNotNull(credentials);
            Assert.AreEqual(1, credentials.Count);

            CredentialDetails credential = credentials[0];
            Assert.IsTrue(credential.BackedUp);
            Assert.IsTrue(credential.Removable);
            Assert.AreEqual(authenticatorName, credential.AuthenticatorName);
            Assert.AreEqual(authenticatorLogo, credential.AuthenticatorLogo);
            Assert.IsTrue(credential.ThirdPartyPayment);
            Assert.AreEqual(AuthenticatorTransport.Internal | AuthenticatorTransport.Hybrid, credential.Transports);
        }

        private static void SetPrivateProperty<T>(object target, string propertyName, T value)
        {
            PropertyInfo? property = target.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            Assert.IsNotNull(property);
            property.SetValue(target, value);
        }

        private static void SetPrivateField<T>(object target, string fieldName, T value)
        {
            FieldInfo? field = target.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(field);
            field.SetValue(target, value);
        }
    }
}
