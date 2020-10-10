using System;
using Fido2NetLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebAuthN.Interop.Test
{
    [TestClass]
    public class Interactive
    {
        [TestMethod]
        public void WebAuthN_MakeCredential_Vector1()
        {
            var config = new Fido2Configuration()
            {
                ChallengeSize = 32,
                Origin = "https://foo.net/bar",
                ServerDomain = "foo.net",
                ServerName = "Foo Server",
            };

            var user = new Fido2User
            {
                Name = "Testuser1",
                Id = new byte[32]
            };

            new Random().NextBytes(user.Id);

            var authenticator = new AuthenticatorSelection
            {
                AuthenticatorAttachment = Fido2NetLib.Objects.AuthenticatorAttachment.Platform,
                RequireResidentKey = false,
                UserVerification = Fido2NetLib.Objects.UserVerificationRequirement.Preferred,
            };

            byte[] challenge = new byte[config.ChallengeSize];
            new Random().NextBytes(challenge);

            //generate valid Fido2 request with Fido2Net
            var options = CredentialCreateOptions.Create(
                config,
                challenge,
                user,
                authenticator,
                Fido2NetLib.Objects.AttestationConveyancePreference.None,
                null,
                null
            );

            var fido = new WebAuthN();
            fido.AuthenticatorMakeCredential(options);
        }
    }
}
