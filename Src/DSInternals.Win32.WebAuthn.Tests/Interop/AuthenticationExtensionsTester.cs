using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using DSInternals.Win32.WebAuthn;
using DSInternals.Win32.WebAuthn.FIDO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Win32.WebAuthn.Interop.Tests
{
    [TestClass]
    public class AuthenticationExtensionsTester
    {
        [TestMethod]
        public void AuthenticationExtensionsClientAttestationInputs_Parse_Vector1()
        {
            string jsonRequest = @"{""hmacCreateSecret"":true,""credentialProtectionPolicy"":""userVerificationOptional""}";

            var request = JsonSerializer.Deserialize(jsonRequest, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationInputs);
            Assert.IsNotNull(request);
            Assert.IsTrue(request.HmacCreateSecret);
            Assert.AreEqual(UserVerification.Optional, request.CredProtect);
            Assert.IsFalse(request.EnforceCredProtect);

            string jsonRequest2 = JsonSerializer.Serialize(request, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationInputs);
            Assert.AreEqual(jsonRequest, jsonRequest2);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientAttestationInputs_Parse_Vector2()
        {
            string jsonRequest = @"{}";

            var request = JsonSerializer.Deserialize(jsonRequest, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationInputs);
            Assert.IsNotNull(request);
            Assert.IsFalse(request.HmacCreateSecret);
            Assert.AreEqual(UserVerification.Any, request.CredProtect);
            Assert.IsFalse(request.EnforceCredProtect);

            string jsonRequest2 = JsonSerializer.Serialize(request, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationInputs);
            Assert.AreEqual(jsonRequest, jsonRequest2);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientAttestationOutputs_Parse_Vector1()
        {
            string jsonResponse = @"{""hmacCreateSecret"":true,""credentialProtectionPolicy"":""userVerificationOptional""}";

            var response = JsonSerializer.Deserialize(jsonResponse, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationOutputs);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.HmacSecret);
            Assert.AreEqual(UserVerification.Optional, response.CredProtect);

            string jsonResponse2 = JsonSerializer.Serialize(response, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationOutputs);
            Assert.AreEqual(jsonResponse, jsonResponse2);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientAttestationOutputs_Parse_Vector2()
        {
            string jsonResponse = @"{}";

            var response = JsonSerializer.Deserialize(jsonResponse, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationOutputs);
            Assert.IsNotNull(response);
            Assert.IsFalse(response.HmacSecret);
            Assert.AreEqual(UserVerification.Any, response.CredProtect);

            string jsonResponse2 = JsonSerializer.Serialize(response, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationOutputs);
            Assert.AreEqual(jsonResponse, jsonResponse2);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientAttestationInputs_Parse_W3CExtensions()
        {
            string jsonRequest = @"{""credProps"":true,""minPinLength"":true,""credBlob"":""AQID-_8"",""largeBlob"":{""support"":""preferred""},""prf"":{""eval"":{""first"":""AQID"",""second"":""BAU""}}}";

            var request = JsonSerializer.Deserialize(jsonRequest, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationInputs);

            Assert.IsNotNull(request);
            Assert.IsTrue(request.CredentialProperties);
            Assert.IsTrue(request.MinimumPinLength);
            CollectionAssert.AreEqual(new byte[] { 1, 2, 3, 251, 255 }, request.CredentialBlob);
            Assert.IsNotNull(request.LargeBlob);
            Assert.AreEqual(LargeBlobSupport.Preferred, request.LargeBlob.Support);
            Assert.IsNotNull(request.Prf);
            Assert.IsNotNull(request.Prf.Eval);
            CollectionAssert.AreEqual(new byte[] { 1, 2, 3 }, request.Prf.Eval.First);
            CollectionAssert.AreEqual(new byte[] { 4, 5 }, request.Prf.Eval.Second);

            string jsonRequest2 = JsonSerializer.Serialize(request, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationInputs);
            Assert.AreEqual(jsonRequest, jsonRequest2);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientAttestationInputs_Parse_MixedBrowserExtensions()
        {
            string jsonRequest = @"{""prf"":{""eval"":{""first"":""UlAgcHJvdmlkZWQgY29uc3RhbnQ""}},""uvm"":true,""largeBlob"":{""support"":""preferred""},""payment"":{""isPayment"":true},""credProps"":true}";

            var request = JsonSerializer.Deserialize(jsonRequest, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationInputs);

            Assert.IsNotNull(request);
            Assert.IsTrue(request.CredentialProperties);
            Assert.IsTrue(request.Uvm);
            Assert.IsNotNull(request.LargeBlob);
            Assert.AreEqual(LargeBlobSupport.Preferred, request.LargeBlob.Support);
            Assert.IsNotNull(request.Prf);
            Assert.IsNotNull(request.Prf.Eval);
            CollectionAssert.AreEqual(Encoding.UTF8.GetBytes("RP provided constant"), request.Prf.Eval.First);
            Assert.IsNull(request.Prf.Eval.Second);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientAttestationInputs_Parse_Uvm()
        {
            string jsonRequest = @"{""uvm"":true}";

            var request = JsonSerializer.Deserialize(jsonRequest, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationInputs);

            Assert.IsNotNull(request);
            Assert.IsTrue(request.Uvm);

            string jsonRequest2 = JsonSerializer.Serialize(request, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationInputs);
            Assert.AreEqual(jsonRequest, jsonRequest2);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientAssertionInputs_Parse_Uvm()
        {
            string jsonRequest = @"{""uvm"":true}";

            var request = JsonSerializer.Deserialize(jsonRequest, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAssertionInputs);

            Assert.IsNotNull(request);
            Assert.IsTrue(request.Uvm);

            string jsonRequest2 = JsonSerializer.Serialize(request, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAssertionInputs);
            Assert.AreEqual(jsonRequest, jsonRequest2);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientAttestationOutputs_Parse_Uvm()
        {
            // Spec example: two factors, fingerprint+TEE+TEE and passcode+software+software.
            string jsonResponse = @"{""uvm"":[[2,4,2],[4,1,1]]}";

            var response = JsonSerializer.Deserialize(jsonResponse, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationOutputs);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Uvm);
            Assert.HasCount(2, response.Uvm);

            Assert.AreEqual(UserVerificationMethod.Fingerprint, response.Uvm[0].UserVerificationMethod);
            Assert.AreEqual(KeyProtectionType.TEE, response.Uvm[0].KeyProtectionType);
            Assert.AreEqual(MatcherProtectionType.TEE, response.Uvm[0].MatcherProtectionType);

            Assert.AreEqual(UserVerificationMethod.Passcode, response.Uvm[1].UserVerificationMethod);
            Assert.AreEqual(KeyProtectionType.Software, response.Uvm[1].KeyProtectionType);
            Assert.AreEqual(MatcherProtectionType.Software, response.Uvm[1].MatcherProtectionType);

            string jsonResponse2 = JsonSerializer.Serialize(response, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationOutputs);
            Assert.AreEqual(jsonResponse, jsonResponse2);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientAssertionOutputs_Parse_Uvm()
        {
            string jsonResponse = @"{""uvm"":[[2,4,2]]}";

            var response = JsonSerializer.Deserialize(jsonResponse, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAssertionOutputs);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Uvm);
            Assert.HasCount(1, response.Uvm);
            Assert.AreEqual(UserVerificationMethod.Fingerprint, response.Uvm[0].UserVerificationMethod);
            Assert.AreEqual(KeyProtectionType.TEE, response.Uvm[0].KeyProtectionType);
            Assert.AreEqual(MatcherProtectionType.TEE, response.Uvm[0].MatcherProtectionType);

            string jsonResponse2 = JsonSerializer.Serialize(response, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAssertionOutputs);
            Assert.AreEqual(jsonResponse, jsonResponse2);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientAssertionOutputs_Serialize_Uvm()
        {
            var response = new AuthenticationExtensionsClientAssertionOutputs
            {
                Uvm =
                [
                    new UvmEntry(UserVerificationMethod.Fingerprint, KeyProtectionType.TEE, MatcherProtectionType.TEE),
                    new UvmEntry(UserVerificationMethod.Passcode, KeyProtectionType.Software, MatcherProtectionType.Software)
                ]
            };

            string jsonResponse = JsonSerializer.Serialize(response, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAssertionOutputs);

            Assert.AreEqual(@"{""uvm"":[[2,4,2],[4,1,1]]}", jsonResponse);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientAttestationInputs_Parse_PaymentRegistration()
        {
            // Per W3C SPC §5, only isPayment (and optionally browserBoundPubKeyCredParams) is meaningful at registration.
            string jsonRequest = @"{""payment"":{""isPayment"":true}}";

            var request = JsonSerializer.Deserialize(jsonRequest, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationInputs);

            Assert.IsNotNull(request);
            Assert.IsNotNull(request.Payment);
            Assert.IsTrue(request.Payment.IsPayment);

            string jsonRequest2 = JsonSerializer.Serialize(request, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationInputs);
            Assert.AreEqual(jsonRequest, jsonRequest2);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientAssertionInputs_Parse_PaymentAuthentication()
        {
            string jsonRequest = @"{""payment"":{""isPayment"":true,""rpId"":""opotonniee.github.io"",""topOrigin"":""opotonniee.github.io"",""payeeName"":""Merchant Shop"",""payeeOrigin"":""https://merchant.com"",""total"":{""currency"":""USD"",""value"":""5.00""},""instrument"":{""displayName"":""Fancy Card ****1234"",""icon"":""https://merchant.com/icon.png""}}}";

            var request = JsonSerializer.Deserialize(jsonRequest, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAssertionInputs);

            Assert.IsNotNull(request);
            Assert.IsNotNull(request.Payment);
            Assert.IsTrue(request.Payment.IsPayment);
            Assert.AreEqual("opotonniee.github.io", request.Payment.RpId);
            Assert.AreEqual("opotonniee.github.io", request.Payment.TopOrigin);
            Assert.AreEqual("Merchant Shop", request.Payment.PayeeName);
            Assert.AreEqual("https://merchant.com", request.Payment.PayeeOrigin);
            Assert.IsNotNull(request.Payment.Total);
            Assert.AreEqual("USD", request.Payment.Total.Currency);
            Assert.AreEqual("5.00", request.Payment.Total.Value);
            Assert.IsNotNull(request.Payment.Instrument);
            Assert.AreEqual("Fancy Card ****1234", request.Payment.Instrument.DisplayName);
            Assert.AreEqual("https://merchant.com/icon.png", request.Payment.Instrument.Icon);
            Assert.IsNull(request.Payment.Instrument.IconMustBeShown);
            Assert.IsNull(request.Payment.Instrument.Details);

            string jsonRequest2 = JsonSerializer.Serialize(request, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAssertionInputs);
            Assert.AreEqual(jsonRequest, jsonRequest2);
        }

        [TestMethod]
        public void PaymentCredentialInstrument_Parse_OptionalFields()
        {
            string json = @"{""displayName"":""Card"",""icon"":""https://example.com/i.png"",""details"":""ending in 1234"",""iconMustBeShown"":false}";

            var instrument = JsonSerializer.Deserialize(json, WebAuthnJsonContext.Default.PaymentCredentialInstrument);

            Assert.IsNotNull(instrument);
            Assert.AreEqual("Card", instrument.DisplayName);
            Assert.AreEqual("https://example.com/i.png", instrument.Icon);
            Assert.AreEqual("ending in 1234", instrument.Details);
            Assert.IsFalse(instrument.IconMustBeShown);

            string json2 = JsonSerializer.Serialize(instrument, WebAuthnJsonContext.Default.PaymentCredentialInstrument);
            Assert.AreEqual(json, json2);
        }

        [TestMethod]
        public void PaymentCurrencyAmount_Parse_RequiresCurrencyAndValue()
        {
            AssertInvalidJson(@"{}", WebAuthnJsonContext.Default.PaymentCurrencyAmount);
            AssertInvalidJson(@"{""currency"":""USD""}", WebAuthnJsonContext.Default.PaymentCurrencyAmount);
            AssertInvalidJson(@"{""value"":""1.00""}", WebAuthnJsonContext.Default.PaymentCurrencyAmount);
        }

        [TestMethod]
        public void PaymentCredentialInstrument_Parse_RequiresDisplayNameAndIcon()
        {
            AssertInvalidJson(@"{}", WebAuthnJsonContext.Default.PaymentCredentialInstrument);
            AssertInvalidJson(@"{""displayName"":""Card""}", WebAuthnJsonContext.Default.PaymentCredentialInstrument);
            AssertInvalidJson(@"{""icon"":""https://example.com/i.png""}", WebAuthnJsonContext.Default.PaymentCredentialInstrument);
        }

        [TestMethod]
        public void UvmEntry_Parse_RejectsInvalidLength()
        {
            AssertInvalidJson(
                @"{""uvm"":[[2,4]]}",
                WebAuthnJsonContext.Default.AuthenticationExtensionsClientAssertionOutputs);

            AssertInvalidJson(
                @"{""uvm"":[[2,4,2,1]]}",
                WebAuthnJsonContext.Default.AuthenticationExtensionsClientAssertionOutputs);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientAssertionInputs_Parse_W3CExtensions()
        {
            string jsonRequest = @"{""appid"":""https://example.com/appid"",""getCredBlob"":true,""largeBlob"":{""write"":""BAU""},""prf"":{""evalByCredential"":{""BgcI"":{""first"":""CQo""}}}}";

            var request = JsonSerializer.Deserialize(jsonRequest, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAssertionInputs);

            Assert.IsNotNull(request);
            Assert.AreEqual("https://example.com/appid", request.AppID);
            Assert.IsTrue(request.GetCredentialBlob);
            Assert.IsNotNull(request.LargeBlob);
            CollectionAssert.AreEqual(new byte[] { 4, 5 }, request.LargeBlob.Write);
            Assert.AreEqual(CredentialLargeBlobOperation.Set, request.LargeBlob.Operation);
            Assert.IsNotNull(request.Prf);
            Assert.IsNotNull(request.Prf.EvalByCredential);
            CollectionAssert.AreEqual(new byte[] { 9, 10 }, request.Prf.EvalByCredential["BgcI"].First);

            string jsonRequest2 = JsonSerializer.Serialize(request, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAssertionInputs);
            Assert.AreEqual(jsonRequest, jsonRequest2);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientAttestationInputs_Parse_SpecOptionalExtensionInputs()
        {
            string jsonRequest = @"{""largeBlob"":{},""prf"":{}}";

            var request = JsonSerializer.Deserialize(jsonRequest, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationInputs);

            Assert.IsNotNull(request);
            Assert.IsNotNull(request.LargeBlob);
            Assert.AreEqual(LargeBlobSupport.Preferred, request.LargeBlob.Support);
            Assert.IsNotNull(request.Prf);
            Assert.IsNull(request.Prf.Eval);

            string jsonRequest2 = JsonSerializer.Serialize(request, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationInputs);
            Assert.AreEqual(@"{""largeBlob"":{""support"":""preferred""},""prf"":{}}", jsonRequest2);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientAttestationInputs_Parse_IgnoresAssertionOnlyPrfInputs()
        {
            var request = JsonSerializer.Deserialize(
                @"{""prf"":{""evalByCredential"":{}}}",
                WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationInputs);

            Assert.IsNotNull(request);
            Assert.IsNotNull(request.Prf);
            Assert.IsNull(request.Prf.Eval);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientAssertionInputs_Parse_RejectsMissingOrConflictingLargeBlobOperation()
        {
            AssertInvalidJson(
                @"{""largeBlob"":{}}",
                WebAuthnJsonContext.Default.AuthenticationExtensionsClientAssertionInputs);

            AssertInvalidJson(
                @"{""largeBlob"":{""read"":true,""write"":""BAU""}}",
                WebAuthnJsonContext.Default.AuthenticationExtensionsClientAssertionInputs);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientAssertionInputs_Parse_PrfGlobalEval()
        {
            string jsonRequest = @"{""prf"":{""eval"":{""first"":""AQID""}}}";

            var request = JsonSerializer.Deserialize(jsonRequest, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAssertionInputs);

            Assert.IsNotNull(request);
            Assert.IsNotNull(request.Prf);
            Assert.IsNotNull(request.Prf.Eval);
            CollectionAssert.AreEqual(new byte[] { 1, 2, 3 }, request.Prf.Eval.First);

            string jsonRequest2 = JsonSerializer.Serialize(request, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAssertionInputs);
            Assert.AreEqual(jsonRequest, jsonRequest2);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientAssertionInputs_Parse_RemoteDesktopClientOverride()
        {
            string jsonRequest = @"{""remoteDesktopClientOverride"":{""origin"":""https://accounts.example.com"",""sameOriginWithAncestors"":false}}";

            var request = JsonSerializer.Deserialize(jsonRequest, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAssertionInputs);

            Assert.IsNotNull(request);
            Assert.IsNotNull(request.RemoteDesktopClientOverride);
            Assert.AreEqual("https://accounts.example.com", request.RemoteDesktopClientOverride.Origin);
            Assert.IsFalse(request.RemoteDesktopClientOverride.SameOriginWithAncestors);

            string jsonRequest2 = JsonSerializer.Serialize(request, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAssertionInputs);
            Assert.AreEqual(jsonRequest, jsonRequest2);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientAssertionInputs_Parse_RemoteDesktopClientOverrideSingularCompatibility()
        {
            string jsonRequest = @"{""remoteDesktopClientOverride"":{""origin"":""https://accounts.example.com"",""sameOriginWithAncestor"":true}}";

            var request = JsonSerializer.Deserialize(jsonRequest, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAssertionInputs);

            Assert.IsNotNull(request);
            Assert.IsNotNull(request.RemoteDesktopClientOverride);
            Assert.IsTrue(request.RemoteDesktopClientOverride.SameOriginWithAncestors);

            string jsonRequest2 = JsonSerializer.Serialize(request, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAssertionInputs);
            Assert.AreEqual(@"{""remoteDesktopClientOverride"":{""origin"":""https://accounts.example.com"",""sameOriginWithAncestors"":true}}", jsonRequest2);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientInputs_Parse_RemoteClientDataJson()
        {
            string remoteClientDataJson = @"{""type"":""webauthn.get"",""challenge"":""AQID"",""origin"":""https://accounts.example.com""}";
            string escapedRemoteClientDataJson = '"' + remoteClientDataJson.Replace("\"", "\\\"") + '"';

            string attestationJson = $@"{{""remoteClientDataJSON"":{escapedRemoteClientDataJson}}}";
            var attestationRequest = JsonSerializer.Deserialize(
                attestationJson,
                WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationInputs);

            Assert.IsNotNull(attestationRequest);
            Assert.AreEqual(remoteClientDataJson, attestationRequest.RemoteClientDataJson);
            string serializedAttestation = JsonSerializer.Serialize(
                attestationRequest,
                WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationInputs);
            StringAssert.Contains(serializedAttestation, @"""remoteClientDataJSON"":");
            Assert.AreEqual(
                remoteClientDataJson,
                JsonSerializer.Deserialize(
                    serializedAttestation,
                    WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationInputs)?.RemoteClientDataJson);

            string assertionJson = $@"{{""remoteClientDataJSON"":{escapedRemoteClientDataJson}}}";
            var assertionRequest = JsonSerializer.Deserialize(
                assertionJson,
                WebAuthnJsonContext.Default.AuthenticationExtensionsClientAssertionInputs);

            Assert.IsNotNull(assertionRequest);
            Assert.AreEqual(remoteClientDataJson, assertionRequest.RemoteClientDataJson);
            string serializedAssertion = JsonSerializer.Serialize(
                assertionRequest,
                WebAuthnJsonContext.Default.AuthenticationExtensionsClientAssertionInputs);
            StringAssert.Contains(serializedAssertion, @"""remoteClientDataJSON"":");
            Assert.AreEqual(
                remoteClientDataJson,
                JsonSerializer.Deserialize(
                    serializedAssertion,
                    WebAuthnJsonContext.Default.AuthenticationExtensionsClientAssertionInputs)?.RemoteClientDataJson);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientAttestationOutputs_Parse_CredBlobRegistrationResult()
        {
            string jsonResponse = @"{""credBlob"":true}";

            var response = JsonSerializer.Deserialize(jsonResponse, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationOutputs);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.CredentialBlobCreated);

            string jsonResponse2 = JsonSerializer.Serialize(response, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationOutputs);
            Assert.AreEqual(jsonResponse, jsonResponse2);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientAssertionOutputs_Parse_CredBlobAssertionResult()
        {
            string jsonResponse = @"{""credBlob"":""AQID-_8""}";

            var response = JsonSerializer.Deserialize(jsonResponse, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAssertionOutputs);

            Assert.IsNotNull(response);
            CollectionAssert.AreEqual(new byte[] { 1, 2, 3, 251, 255 }, response.CredentialBlob);

            string jsonResponse2 = JsonSerializer.Serialize(response, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAssertionOutputs);
            Assert.AreEqual(jsonResponse, jsonResponse2);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientAssertionOutputs_Serialize_W3CFalseExtensionResults()
        {
            var response = new AuthenticationExtensionsClientAssertionOutputs
            {
                AppID = false,
                LargeBlob = new LargeBlobAssertionOutputs
                {
                    Written = false
                }
            };

            string jsonResponse = JsonSerializer.Serialize(response, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAssertionOutputs);

            Assert.AreEqual(@"{""appid"":false,""largeBlob"":{""written"":false}}", jsonResponse);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientAttestationOutputs_Serialize_W3CFalseExtensionResults()
        {
            var response = new AuthenticationExtensionsClientAttestationOutputs
            {
                CredentialProperties = new CredentialPropertiesOutputs
                {
                    ResidentKey = false
                },
                LargeBlob = new LargeBlobAttestationOutputs
                {
                    Supported = false
                },
                Prf = new PRFAttestationOutputs
                {
                    Enabled = false
                }
            };

            string jsonResponse = JsonSerializer.Serialize(response, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationOutputs);

            Assert.AreEqual(@"{""credProps"":{""rk"":false},""largeBlob"":{""supported"":false},""prf"":{""enabled"":false}}", jsonResponse);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientOutputs_Serialize_RemoteClientDataJson()
        {
            var attestationResponse = new AuthenticationExtensionsClientAttestationOutputs
            {
                RemoteClientDataJson = true
            };

            Assert.AreEqual(
                @"{""remoteClientDataJson"":true}",
                JsonSerializer.Serialize(attestationResponse, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationOutputs));

            var assertionResponse = new AuthenticationExtensionsClientAssertionOutputs
            {
                RemoteClientDataJson = true
            };

            Assert.AreEqual(
                @"{""remoteClientDataJson"":true}",
                JsonSerializer.Serialize(assertionResponse, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAssertionOutputs));
        }

        [TestMethod]
        public void AuthenticationExtensionsClientAttestationOutputs_Parse_RejectsIncompleteExtensionOutputs()
        {
            AssertInvalidJson(
                @"{""largeBlob"":{}}",
                WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationOutputs);

            AssertInvalidJson(
                @"{""prf"":{}}",
                WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationOutputs);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientAttestationOutputs_Parse_MinPinLength()
        {
            string jsonResponse = @"{""minPinLength"":6}";

            var response = JsonSerializer.Deserialize(jsonResponse, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationOutputs);

            Assert.IsNotNull(response);
            Assert.AreEqual((uint?)6, response.MinimumPinLength);

            string jsonResponse2 = JsonSerializer.Serialize(response, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationOutputs);
            Assert.AreEqual(jsonResponse, jsonResponse2);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientAttestationInputs_Setters()
        {
            var extensions = new AuthenticationExtensionsClientAttestationInputs()
            {
                HmacCreateSecret = false,
                CredProtect = UserVerification.Any
            };

            string jsonExtensions = JsonSerializer.Serialize(extensions, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationInputs);
            Assert.AreEqual("{}", jsonExtensions);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientAttestationOutputs_Setters()
        {
            var extensions = new AuthenticationExtensionsClientAttestationOutputs()
            {
                HmacSecret = false,
                CredProtect = UserVerification.Any
            };

            string jsonExtensions = JsonSerializer.Serialize(extensions, WebAuthnJsonContext.Default.AuthenticationExtensionsClientAttestationOutputs);
            Assert.AreEqual("{}", jsonExtensions);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientAttestationInputs_Conversion_Vector1()
        {
            var extensions = new AuthenticationExtensionsClientAttestationInputs()
            {
                HmacCreateSecret = true,
                CredProtect = UserVerification.Optional,
                MinimumPinLength = true
            };

            using (var nativeExtensionList = ApiHelper.Translate(extensions))
            using (var nativeExtensions = new ExtensionsIn(nativeExtensionList.ToArray()))
            {
                Assert.HasCount(3, nativeExtensionList);
                var hmacSecret = nativeExtensionList.Find(extension => extension.Identifier == ApiConstants.ExtensionIdentifierHmacSecret);
                var credProtect = nativeExtensionList.Find(extension => extension.Identifier == ApiConstants.ExtensionIdentifierCredProtect);
                var minPinLength = nativeExtensionList.Find(extension => extension.Identifier == ApiConstants.ExtensionIdentifierMinPinLength);

                Assert.IsNotNull(hmacSecret);
                Assert.IsNotNull(credProtect);
                Assert.IsNotNull(minPinLength);
            }
        }

        [TestMethod]
        public void AuthenticationExtensionsClientAssertionInputs_Conversion_Vector1()
        {
            var extensions = new AuthenticationExtensionsClientAssertionInputs()
            {
                GetCredentialBlob = true
            };

            using (var nativeExtensionList = ApiHelper.Translate(extensions))
            using (var nativeExtensions = new ExtensionsIn(nativeExtensionList.ToArray()))
            {
                Assert.HasCount(1, nativeExtensionList);
                var credBlob = nativeExtensionList.Find(extension => extension.Identifier == ApiConstants.ExtensionIdentifierCredBlob);

                Assert.IsNotNull(credBlob);
            }
        }

        [TestMethod]
        public void AuthenticationExtensionsClientInputs_ValidateRemoteClientDataJson()
        {
            string remoteClientDataJson = @"{""type"":""webauthn.get"",""challenge"":""AQID"",""origin"":""https://accounts.example.com""}";
            var clientData = CollectedClientData.Create(
                ApiConstants.ClientDataCredentialGet,
                [1, 2, 3],
                "example.com",
                remoteClientDataJson: remoteClientDataJson);

            Assert.AreEqual("https://accounts.example.com", clientData.Origin);

            using (var nativeClientData = new ClientData(clientData))
            {
                Assert.IsNotNull(nativeClientData.ClientDataRaw);
                var nativeParsedClientData = CollectedClientData.FromJson(Encoding.UTF8.GetString(nativeClientData.ClientDataRaw));
                Assert.AreEqual(clientData.Type, nativeParsedClientData.Type);
                CollectionAssert.AreEqual(clientData.Challenge, nativeParsedClientData.Challenge);
                Assert.AreEqual(clientData.Origin, nativeParsedClientData.Origin);
            }

            Assert.ThrowsExactly<ArgumentException>(() =>
                CollectedClientData.Create(
                    ApiConstants.ClientDataCredentialGet,
                    [1, 2, 3],
                    "example.com",
                    remoteClientDataJson: remoteClientDataJson,
                    remoteDesktopClientOverride: new RemoteDesktopClientOverride { Origin = "https://accounts.example.com" }));

            Assert.ThrowsExactly<ArgumentException>(() =>
                CollectedClientData.Create(
                    ApiConstants.ClientDataCredentialGet,
                    [1, 2, 3],
                    "example.com",
                    remoteClientDataJson: @"{""type"":""webauthn.get""}"));
        }

        private static void AssertInvalidJson<T>(string json, JsonTypeInfo<T> jsonTypeInfo)
        {
            try
            {
                _ = JsonSerializer.Deserialize(json, jsonTypeInfo);
            }
            catch (Exception ex) when (ex is ArgumentException or JsonException)
            {
                return;
            }

            Assert.Fail("Expected invalid extension JSON to be rejected.");
        }
    }
}
