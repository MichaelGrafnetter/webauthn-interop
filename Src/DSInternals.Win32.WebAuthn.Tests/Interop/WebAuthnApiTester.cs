using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using DSInternals.Win32.WebAuthn.FIDO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Win32.WebAuthn.Interop.Tests
{
    [TestClass]
    public class WebAuthnApiTester
    {
        [TestMethod]
        [SuppressMessage("MSTest.Analyzers", "MSTEST0025", Justification = "Uses AssertInconclusiveException when WebAuthn is unavailable on this OS.")]
        public void WebAuthnApi_ApiVersion()
        {
            var version = WebAuthnApi.ApiVersion;
            if (version == null)
            {
                throw new AssertInconclusiveException("The WebAuthn API is not supported on this OS.");
            }
        }

        [TestMethod]
        public void WebAuthnApi_IsAvailable()
        {
            // Should not throw
            bool isAvailable = WebAuthnApi.IsAvailable;
        }

        [TestMethod]
        public void WebAuthnApi_IsCredProtectExtensionSupported()
        {
            // Should not throw
            bool result = WebAuthnApi.IsCredProtectExtensionSupported;
        }


        [TestMethod]
        public void WebAuthnApi_IsPlatformAuthenticatorAvailable()
        {
            // Should not throw
            bool helloAvailable = WebAuthnApi.IsUserVerifyingPlatformAuthenticatorAvailable;
        }

        [TestMethod]
        public void WebAuthnApi_IsCancellationSupported()
        {
            // Should not throw
            bool asyncSupported = new WebAuthnApi().IsCancellationSupported;
        }

        [TestMethod]
        public void CollectedClientData_CreateCollectedClientData_RemoteDesktopClientOverride()
        {
            var extensions = new AuthenticationExtensionsClientAssertionInputs
            {
                RemoteDesktopClientOverride = new RemoteDesktopClientOverride
                {
                    Origin = "https://accounts.example.com",
                    SameOriginWithAncestors = false
                }
            };

            var clientData = CollectedClientData.Create(
                ApiConstants.ClientDataCredentialGet,
                new byte[] { 1, 2, 3 },
                "example.com",
                "example.com",
                null,
                extensions.RemoteDesktopClientOverride);

            Assert.AreEqual("https://accounts.example.com", clientData.Origin);
            Assert.IsTrue(clientData.CrossOrigin);
        }

        [TestMethod]
        [TestCategory("Interactive")]
        public void WebAuthnApi_Register_Vector1()
        {
            var rp = new RelyingPartyInformation()
            {
                Id = "login.microsoft.com",
                Name = "Microsoft"
            };

            var user = new UserInformation()
            {
                Name = "john.doe@outlook.com",
                DisplayName = "John Doe",
                Id = Base64UrlConverter.FromBase64UrlString("TUY65dH-Otl4jMdTRvlFQ1aApACYsuqGKSPQDQc1Bd4WVyw")
            };

            var challenge = new byte[] { 0, 1, 2, 3 };

            var api = new WebAuthnApi();

            var response = RunInteractiveWebAuthnTest(() =>
                api.AuthenticatorMakeCredential(rp, user, challenge, UserVerificationRequirement.Required, AuthenticatorAttachment.Any));
            Assert.IsNotNull(response);
        }

        [TestMethod]
        [TestCategory("Interactive")]
        public void WebAuthnApi_Authenticate_Vector1()
        {
            var api = new WebAuthnApi();
            var challenge = new byte[] { 0, 1, 2, 3 };
            var response = RunInteractiveWebAuthnTest(() =>
                api.AuthenticatorGetAssertion("login.microsoft.com", challenge, UserVerificationRequirement.Required, AuthenticatorAttachment.CrossPlatform));
            Assert.IsNotNull(response);
        }

        [SuppressMessage("MSTest.Analyzers", "MSTEST0025", Justification = "Uses AssertInconclusiveException when interactive WebAuthn UI is unavailable.")]
        private static T RunInteractiveWebAuthnTest<T>(Func<T> action)
        {
            try
            {
                return action();
            }
            catch (Exception ex) when (IsInteractiveWebAuthnUnavailable(ex))
            {
                throw new AssertInconclusiveException("Interactive WebAuthn UI is unavailable or was cancelled in this test environment.", ex);
            }
        }

        private static bool IsInteractiveWebAuthnUnavailable(Exception ex) =>
            ex is UnauthorizedAccessException or OperationCanceledException or Win32Exception
            || ex.InnerException is Win32Exception { NativeErrorCode: 5 or 1223 };
    }
}
