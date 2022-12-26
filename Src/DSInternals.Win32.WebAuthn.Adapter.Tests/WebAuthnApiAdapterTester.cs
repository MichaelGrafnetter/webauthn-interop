using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fido2NetLib;
using Fido2NetLib.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Win32.WebAuthn.Adapter.Tests
{
    /// <summary>
    /// These tests require user interaction and should not be executed as part of CI.
    /// </summary>
    [TestClass]
    [TestCategory("Interactive")]
    public class WebAuthnApiAdapterTester
    {
        [TestMethod]
        public void WebAuthN_MakeCredential_MSAccount()
        {
            var config = new Fido2Configuration()
            {
                ChallengeSize = 32,
                Origin = "login.microsoft.com",
                ServerDomain = "login.microsoft.com",
                ServerName = "Microsoft",
                Timeout = 60000
            };

            var user = new Fido2User
            {
                Name = "michel.grafnetter@outlook.com",
                DisplayName = "Michael Grafnetter",
                Id = Base64Url.Decode("TUY65dH-Otl4jMdTRvlFQ1aApACYsuqGKSPQDQc1Bd4WVyw")
            };

            var authenticator = new AuthenticatorSelection
            {
                AuthenticatorAttachment = AuthenticatorAttachment.CrossPlatform,
                RequireResidentKey = true,
                UserVerification = UserVerificationRequirement.Required,
            };

            byte[] challenge = Encoding.ASCII.GetBytes("CbWTU93Ppbgok1glyka*K9sZSWkqpK3qS1ldeLJxsI4k3jMLIi3dl8VDx10siTGd8U5SNj8yyMIbqXQH!apXGnrhWmYlg2GNdEGddIkO03cql!kKVgKi*MqEIl9aPqmJdYuRMjrEYlIyzi4*wP0YSyA$");

            var excludedCredentials = new List<PublicKeyCredentialDescriptor>()
            {
                new PublicKeyCredentialDescriptor(Base64Url.Decode("lz6_hw1jzaRNhhu9dt_M1Q=")),
                new PublicKeyCredentialDescriptor(Base64Url.Decode("Zod6YhgNV2dQeT3v8ekjRpU0nVlEkPlpXF5Vx6f4P9g=")),
                new PublicKeyCredentialDescriptor(Base64Url.Decode("sx2P4XkPO6TUoSf0pMEm3zi5gdwVrIRjiYvuTFRAkNMe_jVsntSgkyG5aV8er5GCA_G1X2idph-8lhhMFX3aaAyBCQIAAA="))
            };

            // TODO: Test extensions
            //var extensions = new WinExtensionsIn()
            //{
            //    HmacSecret = true,
            //    CredProtect = UserVerification.Optional
            //};

            var options = CredentialCreateOptions.Create(
                config,
                challenge,
                user,
                authenticator,
                AttestationConveyancePreference.Direct,
                excludedCredentials,
                null
            );

            options.PubKeyCredParams = new List<PubKeyCredParam>()
            {
                new PubKeyCredParam() { Alg = Fido2NetLib.Objects.COSE.Algorithm.ES256, Type = PublicKeyCredentialType.PublicKey },
                new PubKeyCredParam() { Alg = Fido2NetLib.Objects.COSE.Algorithm.RS256, Type = PublicKeyCredentialType.PublicKey }
            };

            var webauthn = new WebAuthnApiAdapter();
            var response = webauthn.AuthenticatorMakeCredential(options);

            // Validate
            var fido2 = new Fido2(config);
            var result = fido2.MakeNewCredentialAsync(response, options, _ => Task.FromResult(true)).GetAwaiter().GetResult();
            Assert.AreEqual(user.Name, result.Result.User.Name);
        }

        [TestMethod]
        public void WebAuthN_GetAssertion_MSAccount_CredentialList()
        {
            var config = new Fido2Configuration()
            {
                Origin = "login.microsoft.com",
                ServerDomain = "login.microsoft.com",
                ServerName = "Microsoft",
                Timeout = 60000
            };

            byte[] challenge = Encoding.ASCII.GetBytes("O.eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImtnMkxZczJUMENUaklmajRydDZKSXluZW4zOCJ9.eyJhdWQiOiJ1cm46bWljcm9zb2Z0OmZpZG86Y2hhbGxlbmdlIiwiaXNzIjoiaHR0cHM6Ly9sb2dpbi5taWNyb3NvZnQuY29tIiwiaWF0IjoxNjAyNDEzMzIxLCJuYmYiOjE2MDI0MTMzMjEsImV4cCI6MTYwMjQxMzYyMX0.ogwFJG0w6TX4QAGmwh-0aizApfdxtpQ_Kra9Bjk7LCuHxglV1rU1C5U9nPx4cKoNh09zucnTx9HpbvytgQtenOHiErZswGY_oi53EGL_ftfICm80agFcwMYLzHPH-yoIE9B5uORnLFOZmz98aZAFXcofZcK1E3-A1wZVcES9mjBN34G6iwG7-pcmBJge0Xc8nkRO-dei4RlFWnnYNc6iSPuJEknojGAvstmkfqSGnNgTpQXvn5eBVSHltY1C8jkk-qUzNxf1mfypcdXxfZxAIQnc50JRDz1QsD6bKV97crqhvv1ROpQ3L1V6dtYYiIQAUKQgm6FhYbS-3uj3ziK_fA");

            var allowedCredentials = new List<PublicKeyCredentialDescriptor>()
            {
                new PublicKeyCredentialDescriptor(Base64Url.Decode("sx2P4XkPO6TUoSf0pMEm3zi5gdwVrIRjiYvuTFRAkNMe_jVsntSgkyG5aV8er5GCA_G1X2idph-8lhhMFX3aaAyBCQIAAA")),
                new PublicKeyCredentialDescriptor(Base64Url.Decode("JmLEadmzHpm5K3i5gVFO-MJz43GukTKYkcRR8qO6Bp0")),
                new PublicKeyCredentialDescriptor(Base64Url.Decode("Zod6YhgNV2dQeT3v8ekjRpU0nVlEkPlpXF5Vx6f4P9g")),
                new PublicKeyCredentialDescriptor(Base64Url.Decode("lz6_hw1jzaRNhhu9dt_M1Q")),
                new PublicKeyCredentialDescriptor(Base64Url.Decode("j37zD-1LtCU2H2v0w72N8qR-Zelt6U5hqOAx-dUKdKw")),
                new PublicKeyCredentialDescriptor(Base64Url.Decode("p9NO19fDeDXHrHGm9yX66AXtHaXXpHB58xsK-d_wSFY")),
                new PublicKeyCredentialDescriptor(Base64Url.Decode("u967rd7wTTQd8lp8NLxsNG8BSpvRKAO24Qy5izqhqcE")),
                new PublicKeyCredentialDescriptor(Base64Url.Decode("lc_oOBQhFX0jZob_D0PHA5EssRQ3fTQhktgYQlFwwWE")),
                new PublicKeyCredentialDescriptor(Base64Url.Decode("sZJOM5fErnFrfdWHWG9U4B5K6dlWjVPs3OF53CLdAj4")),
                new PublicKeyCredentialDescriptor(Base64Url.Decode("PJO9rMaY2zkrjWuDy682AB_uEoTqYNBZiwZMzQwbvC0")),
                new PublicKeyCredentialDescriptor(Base64Url.Decode("f5okakXYucsZ9b7y9afAIZPQbn_WNhZBcWCwmg5P290")),
                new PublicKeyCredentialDescriptor(Base64Url.Decode("bx1_lrIkU5QLmhsvIqkLK505GTHp0dIq4BmscZiZsXo")),
                new PublicKeyCredentialDescriptor(Base64Url.Decode("bl3koxWkLmNagO54CL3hTR5gZw-oiaBtwDczQu3BBz0")),
                new PublicKeyCredentialDescriptor(Base64Url.Decode("NMIWbuqahv94x7MgQRB1Cay7T8BGjfAgY-cwe_a-5b8")),
                new PublicKeyCredentialDescriptor(Base64Url.Decode("W7oQBUjStY8lK-t_ALpLTYiFfLKM8T51c58xFt7XEA8")),
                new PublicKeyCredentialDescriptor(Base64Url.Decode("GCSUWxviqWdV0nzU7Ugz0SUzZYX8I1wWfqMROZFDUx4"))
            };

            var options = AssertionOptions.Create(
                config,
                challenge,
                allowedCredentials,
                Fido2NetLib.Objects.UserVerificationRequirement.Required,
                null
            );

            var webauthn = new WebAuthnApiAdapter();
            var response = webauthn.AuthenticatorGetAssertion(options);

            // Validate
            var fido2 = new Fido2(config);
            var pubKey = new byte[] { 0, 0, 0 };
            uint counter = 25;
            var result = fido2.MakeAssertionAsync(response, options, pubKey, counter, _ => Task.FromResult(true)).GetAwaiter().GetResult();
        }

        [TestMethod]
        public void WebAuthN_GetAssertion_MSAccount_Usernameless()
        {
            var config = new Fido2Configuration()
            {
                Origin = "login.microsoft.com",
                ServerDomain = "login.microsoft.com",
                ServerName = "Microsoft",
                Timeout = 60000
            };

            byte[] challenge = Encoding.ASCII.GetBytes("O.eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImtnMkxZczJUMENUaklmajRydDZKSXluZW4zOCJ9.eyJhdWQiOiJ1cm46bWljcm9zb2Z0OmZpZG86Y2hhbGxlbmdlIiwiaXNzIjoiaHR0cHM6Ly9sb2dpbi5taWNyb3NvZnQuY29tIiwiaWF0IjoxNjAyNDQ1NDI0LCJuYmYiOjE2MDI0NDU0MjQsImV4cCI6MTYwMjQ0NTcyNH0.KMarezIE6LW3NG99LemMpYHLeiiFJe2NLz04z2Q5hsJLx6hbHpXzhoEfElZi4NaNgvBvv37vv7BqdWbUa6iYWaKI73eXhPuj42IddNE2nqdLzjSX-PCiAeBSi7xMNapW9feAj8zjg9pfCluS8ly_mgYlhBUVMlulL5bR40JQU2bQ6m9PMt1-eFO9A55KbJ_BFSwrsFSJxS612wxceM4cjepEZEGj7QO1bZYLOROnmTk8wcojqWjVpvXnCRocbhFp8CEqM4ucFVsTXiQ4X66aykHbUdKOxCmvB_a20N1fPKhKrS-Hh50jCOPNpwbNVJN7AOhMRyrMiTW2qQb8suihRw");

            var options = AssertionOptions.Create(
                config,
                challenge,
                null,
                Fido2NetLib.Objects.UserVerificationRequirement.Required,
                null
                );

            var webauthn = new WebAuthnApiAdapter();
            var response = webauthn.AuthenticatorGetAssertion(options, AuthenticatorAttachment.Platform);

            // Validate
            var fido2 = new Fido2(config);
            var pubKey = new byte[] { 0, 0, 0 };
            uint counter = 25;
            var result = fido2.MakeAssertionAsync(response, options, pubKey, counter, _ => Task.FromResult(true)).GetAwaiter().GetResult();
        }

        [TestMethod]
        [ExpectedException(typeof(OperationCanceledException))]
        public void WebAuthN_GetAssertion_Cancel()
        {
            var config = new Fido2Configuration()
            {
                Origin = "login.microsoft.com",
                ServerDomain = "login.microsoft.com",
                ServerName = "Microsoft"
            };

            byte[] challenge = Encoding.ASCII.GetBytes("O.eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6ImtnMkxZczJUMENUaklmajRydDZKSXluZW4zOCJ9.eyJhdWQiOiJ1cm46bWljcm9zb2Z0OmZpZG86Y2hhbGxlbmdlIiwiaXNzIjoiaHR0cHM6Ly9sb2dpbi5taWNyb3NvZnQuY29tIiwiaWF0IjoxNjAyNDQ1NDI0LCJuYmYiOjE2MDI0NDU0MjQsImV4cCI6MTYwMjQ0NTcyNH0.KMarezIE6LW3NG99LemMpYHLeiiFJe2NLz04z2Q5hsJLx6hbHpXzhoEfElZi4NaNgvBvv37vv7BqdWbUa6iYWaKI73eXhPuj42IddNE2nqdLzjSX-PCiAeBSi7xMNapW9feAj8zjg9pfCluS8ly_mgYlhBUVMlulL5bR40JQU2bQ6m9PMt1-eFO9A55KbJ_BFSwrsFSJxS612wxceM4cjepEZEGj7QO1bZYLOROnmTk8wcojqWjVpvXnCRocbhFp8CEqM4ucFVsTXiQ4X66aykHbUdKOxCmvB_a20N1fPKhKrS-Hh50jCOPNpwbNVJN7AOhMRyrMiTW2qQb8suihRw");

            var options = AssertionOptions.Create(
                config,
                challenge,
                null,
                Fido2NetLib.Objects.UserVerificationRequirement.Required,
                null
                );

            var webauthn = new WebAuthnApiAdapter();

            var source = new CancellationTokenSource(5000);
            webauthn.AuthenticatorGetAssertionAsync(options, null, source.Token).GetAwaiter().GetResult();
        }
    }
}
