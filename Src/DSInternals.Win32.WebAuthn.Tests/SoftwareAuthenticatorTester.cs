#if NET8_0_OR_GREATER

using System;
using System.Buffers.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Formats.Cbor;
using DSInternals.Win32.WebAuthn.COSE;
using DSInternals.Win32.WebAuthn.FIDO;
using DSInternals.Win32.WebAuthn.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Win32.WebAuthn.Tests
{
    [TestClass]
    public class SoftwareAuthenticatorTester
    {
        private const string TestRpId = "example.com";
        private static readonly byte[] TestChallenge = Encoding.UTF8.GetBytes("test-challenge-value");
        private static readonly byte[] TestCredentialId = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        private static readonly byte[] TestUserHandle = new byte[] { 0xAA, 0xBB, 0xCC };
        private static readonly Guid TestAaGuid = new("01020304-0506-0708-090a-0b0c0d0e0f10");

        /// <summary>
        /// Parses a CBOR-encoded attestation object and extracts fmt, authData, and attStmt fields.
        /// </summary>
        private static (string fmt, byte[] authData, int alg, byte[] sig) ParseAttestationObject(byte[] attestationObjectBytes)
        {
            string? fmt = null;
            byte[]? authData = null;
            int alg = 0;
            byte[]? sig = null;

            var reader = new CborReader(attestationObjectBytes, CborConformanceMode.Lax);
            int? mapLength = reader.ReadStartMap();
            int count = mapLength ?? int.MaxValue;

            for (int i = 0; i < count; i++)
            {
                if (reader.PeekState() == CborReaderState.EndMap)
                    break;

                string key = reader.ReadTextString();
                switch (key)
                {
                    case "fmt":
                        fmt = reader.ReadTextString();
                        break;
                    case "authData":
                        authData = reader.ReadByteString();
                        break;
                    case "attStmt":
                        int? attStmtLength = reader.ReadStartMap();
                        int attStmtCount = attStmtLength ?? int.MaxValue;
                        for (int j = 0; j < attStmtCount; j++)
                        {
                            if (reader.PeekState() == CborReaderState.EndMap)
                                break;

                            string attKey = reader.ReadTextString();
                            switch (attKey)
                            {
                                case "alg":
                                    alg = reader.ReadInt32();
                                    break;
                                case "sig":
                                    sig = reader.ReadByteString();
                                    break;
                                default:
                                    reader.SkipValue();
                                    break;
                            }
                        }
                        reader.ReadEndMap();
                        break;
                    default:
                        reader.SkipValue();
                        break;
                }
            }

            reader.ReadEndMap();
            return (fmt!, authData!, alg, sig!);
        }

        #region MakeCredential Tests

        [TestMethod]
        public void MakeCredential_ES256_ReturnsValidResponse()
        {
            using var key = ECDsa.Create(ECCurve.NamedCurves.nistP256);
            var rp = new RelyingPartyInformation { Id = TestRpId, Name = "Example" };
            var user = new UserInformation { Id = TestUserHandle, Name = "testuser", DisplayName = "Test User" };
            var flags = AuthenticatorFlags.UserPresent | AuthenticatorFlags.UserVerified;

            var credential = SoftwareAuthenticator.MakeCredential(rp, user, TestChallenge, Algorithm.ES256, TestAaGuid, 1, flags, key);

            Assert.IsNotNull(credential);
            Assert.AreEqual(ApiConstants.PublicKeyCredentialType, credential.Type);
            Assert.IsNotNull(credential.Id);
            Assert.IsNotEmpty(credential.Id);
            CollectionAssert.AreEqual(credential.Id, credential.RawId);

            var response = credential.Response as AuthenticatorAttestationResponse;
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.ClientDataJson);
            Assert.IsNotNull(response.AttestationObject);
        }

        [TestMethod]
        public void MakeCredential_ES256_ClientDataJsonIsValid()
        {
            using var key = ECDsa.Create(ECCurve.NamedCurves.nistP256);
            var rp = new RelyingPartyInformation { Id = TestRpId, Name = "Example" };
            var user = new UserInformation { Id = TestUserHandle, Name = "testuser", DisplayName = "Test User" };

            var credential = SoftwareAuthenticator.MakeCredential(rp, user, TestChallenge, Algorithm.ES256, TestAaGuid, 0, AuthenticatorFlags.UserPresent, key);
            var response = (AuthenticatorAttestationResponse)credential.Response;

            var clientData = JsonSerializer.Deserialize(response.ClientDataJson, WebAuthnJsonContext.Default.CollectedClientData);
            Assert.IsNotNull(clientData);
            var parsedClientData = (CollectedClientData)clientData;

            Assert.AreEqual(ApiConstants.ClientDataCredentialCreate, parsedClientData.Type);
            Assert.AreEqual($"https://{TestRpId}", parsedClientData.Origin);
            Assert.IsFalse(parsedClientData.CrossOrigin);
            CollectionAssert.AreEqual(TestChallenge, parsedClientData.Challenge);
        }

        [TestMethod]
        public void MakeCredential_ES256_AttestationObjectContainsValidAuthData()
        {
            using var key = ECDsa.Create(ECCurve.NamedCurves.nistP256);
            var rp = new RelyingPartyInformation { Id = TestRpId, Name = "Example" };
            var user = new UserInformation { Id = TestUserHandle, Name = "testuser", DisplayName = "Test User" };
            uint counter = 42;
            var flags = AuthenticatorFlags.UserPresent | AuthenticatorFlags.UserVerified;

            var credential = SoftwareAuthenticator.MakeCredential(rp, user, TestChallenge, Algorithm.ES256, TestAaGuid, counter, flags, key);
            var response = (AuthenticatorAttestationResponse)credential.Response;

            // Decode CBOR attestation object
            var (fmt, authData, _, _) = ParseAttestationObject(response.AttestationObject);
            Assert.AreEqual("packed", fmt);

            // Verify RP ID hash (first 32 bytes)
            byte[] expectedRpIdHash = SHA256.HashData(Encoding.UTF8.GetBytes(TestRpId));
            byte[] actualRpIdHash = authData[..32];
            CollectionAssert.AreEqual(expectedRpIdHash, actualRpIdHash);

            // Verify flags byte
            byte flagsByte = authData[32];
            Assert.AreNotEqual(0, flagsByte & 0x01, "UP flag should be set");
            Assert.AreNotEqual(0, flagsByte & 0x04, "UV flag should be set");
            Assert.AreNotEqual(0, flagsByte & 0x40, "AT flag should be set for attestation");

            // Verify signature counter (bytes 33-36, big-endian)
            uint actualCounter = BinaryPrimitives.ReadUInt32BigEndian(authData.AsSpan(33, 4));
            Assert.AreEqual(counter, actualCounter);
        }

        [TestMethod]
        public void MakeCredential_ES256_SignatureIsVerifiable()
        {
            using var key = ECDsa.Create(ECCurve.NamedCurves.nistP256);
            var rp = new RelyingPartyInformation { Id = TestRpId, Name = "Example" };
            var user = new UserInformation { Id = TestUserHandle, Name = "testuser", DisplayName = "Test User" };

            var credential = SoftwareAuthenticator.MakeCredential(rp, user, TestChallenge, Algorithm.ES256, TestAaGuid, 0, AuthenticatorFlags.UserPresent, key);
            var response = (AuthenticatorAttestationResponse)credential.Response;

            var (_, authData, _, signature) = ParseAttestationObject(response.AttestationObject);

            // Reconstruct signed data: authData || SHA-256(clientDataJSON)
            byte[] clientDataHash = SHA256.HashData(response.ClientDataJson);
            byte[] signedData = new byte[authData.Length + clientDataHash.Length];
            Buffer.BlockCopy(authData, 0, signedData, 0, authData.Length);
            Buffer.BlockCopy(clientDataHash, 0, signedData, authData.Length, clientDataHash.Length);

            // Verify signature using the original public key
            bool isValid = key.VerifyData(signedData, signature, HashAlgorithmName.SHA256, DSASignatureFormat.Rfc3279DerSequence);
            Assert.IsTrue(isValid, "Attestation signature should be verifiable with the signing key");
        }

        [TestMethod]
        public void MakeCredential_RS256_SignatureIsVerifiable()
        {
            using var key = RSA.Create(2048);
            var rp = new RelyingPartyInformation { Id = TestRpId, Name = "Example" };
            var user = new UserInformation { Id = TestUserHandle, Name = "testuser", DisplayName = "Test User" };

            var credential = SoftwareAuthenticator.MakeCredential(rp, user, TestChallenge, Algorithm.RS256, TestAaGuid, 0, AuthenticatorFlags.UserPresent, key);
            var response = (AuthenticatorAttestationResponse)credential.Response;

            var (_, authData, _, signature) = ParseAttestationObject(response.AttestationObject);

            byte[] clientDataHash = SHA256.HashData(response.ClientDataJson);
            byte[] signedData = new byte[authData.Length + clientDataHash.Length];
            Buffer.BlockCopy(authData, 0, signedData, 0, authData.Length);
            Buffer.BlockCopy(clientDataHash, 0, signedData, authData.Length, clientDataHash.Length);

            bool isValid = key.VerifyData(signedData, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            Assert.IsTrue(isValid, "RS256 attestation signature should be verifiable with the signing key");
        }

        [TestMethod]
        public void MakeCredential_NullRelyingParty_Throws()
        {
            using var key = ECDsa.Create(ECCurve.NamedCurves.nistP256);
            var user = new UserInformation { Id = TestUserHandle, Name = "testuser", DisplayName = "Test User" };

            Assert.ThrowsExactly<ArgumentNullException>(() =>
                SoftwareAuthenticator.MakeCredential(null!, user, TestChallenge, Algorithm.ES256, TestAaGuid, 0, AuthenticatorFlags.UserPresent, key));
        }

        [TestMethod]
        public void MakeCredential_NullChallenge_Throws()
        {
            using var key = ECDsa.Create(ECCurve.NamedCurves.nistP256);
            var rp = new RelyingPartyInformation { Id = TestRpId, Name = "Example" };
            var user = new UserInformation { Id = TestUserHandle, Name = "testuser", DisplayName = "Test User" };

            Assert.ThrowsExactly<ArgumentNullException>(() =>
                SoftwareAuthenticator.MakeCredential(rp, user, null!, Algorithm.ES256, TestAaGuid, 0, AuthenticatorFlags.UserPresent, key));
        }

        #endregion

        #region GetAssertion Tests

        [TestMethod]
        public void GetAssertion_ES256_ReturnsValidResponse()
        {
            using var key = ECDsa.Create(ECCurve.NamedCurves.nistP256);
            var flags = AuthenticatorFlags.UserPresent | AuthenticatorFlags.UserVerified;

            var credential = SoftwareAuthenticator.GetAssertion(TestRpId, TestChallenge, Algorithm.ES256, 5, flags, TestCredentialId, TestUserHandle, key);

            Assert.IsNotNull(credential);
            Assert.AreEqual(ApiConstants.PublicKeyCredentialType, credential.Type);
            CollectionAssert.AreEqual(TestCredentialId, credential.Id);
            CollectionAssert.AreEqual(TestCredentialId, credential.RawId);

            var response = credential.Response as AuthenticatorAssertionResponse;
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.ClientDataJson);
            Assert.IsNotNull(response.AuthenticatorData);
            Assert.IsNotNull(response.Signature);
            CollectionAssert.AreEqual(TestUserHandle, response.UserHandle);
        }

        [TestMethod]
        public void GetAssertion_ES256_ClientDataJsonIsValid()
        {
            using var key = ECDsa.Create(ECCurve.NamedCurves.nistP256);

            var credential = SoftwareAuthenticator.GetAssertion(TestRpId, TestChallenge, Algorithm.ES256, 0, AuthenticatorFlags.UserPresent, TestCredentialId, null!, key);
            var response = (AuthenticatorAssertionResponse)credential.Response;

            var clientData = JsonSerializer.Deserialize(response.ClientDataJson, WebAuthnJsonContext.Default.CollectedClientData);
            Assert.IsNotNull(clientData);
            var parsedClientData = (CollectedClientData)clientData;

            Assert.AreEqual(ApiConstants.ClientDataCredentialGet, parsedClientData.Type);
            Assert.AreEqual($"https://{TestRpId}", parsedClientData.Origin);
            Assert.IsFalse(parsedClientData.CrossOrigin);
            CollectionAssert.AreEqual(TestChallenge, parsedClientData.Challenge);
        }

        [TestMethod]
        public void GetAssertion_ES256_AuthenticatorDataIsValid()
        {
            using var key = ECDsa.Create(ECCurve.NamedCurves.nistP256);
            uint counter = 100;
            var flags = AuthenticatorFlags.UserPresent | AuthenticatorFlags.UserVerified;

            var credential = SoftwareAuthenticator.GetAssertion(TestRpId, TestChallenge, Algorithm.ES256, counter, flags, TestCredentialId, null!, key);
            var response = (AuthenticatorAssertionResponse)credential.Response;

            byte[] authData = response.AuthenticatorData;

            // Assertion authenticator data: 32 (rpIdHash) + 1 (flags) + 4 (counter) = 37 bytes
            Assert.HasCount(37, authData);

            // Verify RP ID hash
            byte[] expectedRpIdHash = SHA256.HashData(Encoding.UTF8.GetBytes(TestRpId));
            CollectionAssert.AreEqual(expectedRpIdHash, authData[..32]);

            // Verify flags
            byte flagsByte = authData[32];
            Assert.AreNotEqual(0, flagsByte & 0x01, "UP flag should be set");
            Assert.AreNotEqual(0, flagsByte & 0x04, "UV flag should be set");
            Assert.AreEqual(0, flagsByte & 0x40, "AT flag should NOT be set for assertion");

            // Verify counter
            uint actualCounter = BinaryPrimitives.ReadUInt32BigEndian(authData.AsSpan(33, 4));
            Assert.AreEqual(counter, actualCounter);
        }

        [TestMethod]
        public void GetAssertion_ES256_SignatureIsVerifiable()
        {
            using var key = ECDsa.Create(ECCurve.NamedCurves.nistP256);

            var credential = SoftwareAuthenticator.GetAssertion(TestRpId, TestChallenge, Algorithm.ES256, 0, AuthenticatorFlags.UserPresent, TestCredentialId, null!, key);
            var response = (AuthenticatorAssertionResponse)credential.Response;

            // Reconstruct signed data: authenticatorData || SHA-256(clientDataJSON)
            byte[] clientDataHash = SHA256.HashData(response.ClientDataJson);
            byte[] signedData = new byte[response.AuthenticatorData.Length + clientDataHash.Length];
            Buffer.BlockCopy(response.AuthenticatorData, 0, signedData, 0, response.AuthenticatorData.Length);
            Buffer.BlockCopy(clientDataHash, 0, signedData, response.AuthenticatorData.Length, clientDataHash.Length);

            bool isValid = key.VerifyData(signedData, response.Signature, HashAlgorithmName.SHA256, DSASignatureFormat.Rfc3279DerSequence);
            Assert.IsTrue(isValid, "Assertion signature should be verifiable with the signing key");
        }

        [TestMethod]
        public void GetAssertion_RS256_SignatureIsVerifiable()
        {
            using var key = RSA.Create(2048);

            var credential = SoftwareAuthenticator.GetAssertion(TestRpId, TestChallenge, Algorithm.RS256, 0, AuthenticatorFlags.UserPresent, TestCredentialId, null!, key);
            var response = (AuthenticatorAssertionResponse)credential.Response;

            byte[] clientDataHash = SHA256.HashData(response.ClientDataJson);
            byte[] signedData = new byte[response.AuthenticatorData.Length + clientDataHash.Length];
            Buffer.BlockCopy(response.AuthenticatorData, 0, signedData, 0, response.AuthenticatorData.Length);
            Buffer.BlockCopy(clientDataHash, 0, signedData, response.AuthenticatorData.Length, clientDataHash.Length);

            bool isValid = key.VerifyData(signedData, response.Signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            Assert.IsTrue(isValid, "RS256 assertion signature should be verifiable with the signing key");
        }

        [TestMethod]
        public void GetAssertion_NullUserHandle_IsAccepted()
        {
            using var key = ECDsa.Create(ECCurve.NamedCurves.nistP256);

            var credential = SoftwareAuthenticator.GetAssertion(TestRpId, TestChallenge, Algorithm.ES256, 0, AuthenticatorFlags.UserPresent, TestCredentialId, null!, key);
            var response = (AuthenticatorAssertionResponse)credential.Response;

            Assert.IsNull(response.UserHandle);
        }

        [TestMethod]
        public void GetAssertion_NullRelyingPartyId_Throws()
        {
            using var key = ECDsa.Create(ECCurve.NamedCurves.nistP256);

            Assert.ThrowsExactly<ArgumentNullException>(() =>
                SoftwareAuthenticator.GetAssertion(null!, TestChallenge, Algorithm.ES256, 0, AuthenticatorFlags.UserPresent, TestCredentialId, null!, key));
        }

        [TestMethod]
        public void GetAssertion_NullCredentialId_Throws()
        {
            using var key = ECDsa.Create(ECCurve.NamedCurves.nistP256);

            Assert.ThrowsExactly<ArgumentNullException>(() =>
                SoftwareAuthenticator.GetAssertion(TestRpId, TestChallenge, Algorithm.ES256, 0, AuthenticatorFlags.UserPresent, null!, null!, key));
        }

        #endregion

        #region Key Management Tests

        [TestMethod]
        public void GenerateKeyPairPem_ES256_ProducesValidPem()
        {
            string pem = SoftwareAuthenticator.GenerateKeyPairPem(Algorithm.ES256);

            Assert.Contains("BEGIN PRIVATE KEY", pem);
            using var importedKey = SoftwareAuthenticator.ImportPrivateKeyFromPem(pem);
            Assert.IsInstanceOfType<ECDsa>(importedKey);
            Assert.AreEqual(256, ((ECDsa)importedKey).KeySize);
        }

        [TestMethod]
        public void GenerateKeyPairPem_RS256_ProducesValidPem()
        {
            string pem = SoftwareAuthenticator.GenerateKeyPairPem(Algorithm.RS256);

            Assert.Contains("BEGIN PRIVATE KEY", pem);
            using var importedKey = SoftwareAuthenticator.ImportPrivateKeyFromPem(pem);
            Assert.IsInstanceOfType<RSA>(importedKey);
        }

        [TestMethod]
        public void DetectAlgorithm_EC256_ReturnsES256()
        {
            using var key = ECDsa.Create(ECCurve.NamedCurves.nistP256);
            Assert.AreEqual(Algorithm.ES256, SoftwareAuthenticator.DetectAlgorithm(key));
        }

        [TestMethod]
        public void DetectAlgorithm_EC384_ReturnsES384()
        {
            using var key = ECDsa.Create(ECCurve.NamedCurves.nistP384);
            Assert.AreEqual(Algorithm.ES384, SoftwareAuthenticator.DetectAlgorithm(key));
        }

        [TestMethod]
        public void DetectAlgorithm_RSA_ReturnsRS256()
        {
            using var key = RSA.Create(2048);
            Assert.AreEqual(Algorithm.RS256, SoftwareAuthenticator.DetectAlgorithm(key));
        }

        [TestMethod]
        public void ValidateKeyForAlgorithm_MatchingECKey_ReturnsNull()
        {
            using var key = ECDsa.Create(ECCurve.NamedCurves.nistP256);
            Assert.IsNull(SoftwareAuthenticator.ValidateKeyForAlgorithm(key, Algorithm.ES256));
        }

        [TestMethod]
        public void ValidateKeyForAlgorithm_MismatchedECKey_ReturnsError()
        {
            using var key = ECDsa.Create(ECCurve.NamedCurves.nistP384);
            string? error = SoftwareAuthenticator.ValidateKeyForAlgorithm(key, Algorithm.ES256);
            Assert.IsNotNull(error);
        }

        [TestMethod]
        public void ValidateKeyForAlgorithm_RSAKeyWithECAlgorithm_ReturnsError()
        {
            using var key = RSA.Create(2048);
            string? error = SoftwareAuthenticator.ValidateKeyForAlgorithm(key, Algorithm.ES256);
            Assert.IsNotNull(error);
        }

        [TestMethod]
        public void ValidateKeyForAlgorithm_ECKeyWithRSAAlgorithm_ReturnsError()
        {
            using var key = ECDsa.Create(ECCurve.NamedCurves.nistP256);
            string? error = SoftwareAuthenticator.ValidateKeyForAlgorithm(key, Algorithm.RS256);
            Assert.IsNotNull(error);
        }

        [TestMethod]
        public void ImportPrivateKeyFromPem_InvalidPem_Throws()
        {
            Assert.ThrowsExactly<CryptographicException>(() =>
                SoftwareAuthenticator.ImportPrivateKeyFromPem("not a valid PEM"));
        }

        #endregion

        #region Round-Trip Tests

        [TestMethod]
        public void RoundTrip_ES256_MakeCredentialThenGetAssertion()
        {
            using var key = ECDsa.Create(ECCurve.NamedCurves.nistP256);
            var rp = new RelyingPartyInformation { Id = TestRpId, Name = "Example" };
            var user = new UserInformation { Id = TestUserHandle, Name = "testuser", DisplayName = "Test User" };
            var flags = AuthenticatorFlags.UserPresent | AuthenticatorFlags.UserVerified;

            // Registration
            var regCredential = SoftwareAuthenticator.MakeCredential(rp, user, TestChallenge, Algorithm.ES256, TestAaGuid, 0, flags, key);
            byte[] credentialId = regCredential.Id;

            // Authentication with a different challenge
            byte[] authChallenge = Encoding.UTF8.GetBytes("auth-challenge");
            var authCredential = SoftwareAuthenticator.GetAssertion(TestRpId, authChallenge, Algorithm.ES256, 1, flags, credentialId, TestUserHandle, key);

            // Verify the assertion credential ID matches the registration
            CollectionAssert.AreEqual(credentialId, authCredential.Id);

            // Verify assertion signature
            var assertionResponse = (AuthenticatorAssertionResponse)authCredential.Response;
            byte[] clientDataHash = SHA256.HashData(assertionResponse.ClientDataJson);
            byte[] signedData = new byte[assertionResponse.AuthenticatorData.Length + clientDataHash.Length];
            Buffer.BlockCopy(assertionResponse.AuthenticatorData, 0, signedData, 0, assertionResponse.AuthenticatorData.Length);
            Buffer.BlockCopy(clientDataHash, 0, signedData, assertionResponse.AuthenticatorData.Length, clientDataHash.Length);

            bool isValid = key.VerifyData(signedData, assertionResponse.Signature, HashAlgorithmName.SHA256, DSASignatureFormat.Rfc3279DerSequence);
            Assert.IsTrue(isValid, "Assertion signature should be valid after round-trip");
        }

        [TestMethod]
        public void RoundTrip_GenerateKeyPemThenSign()
        {
            // Generate a key, export to PEM, reimport, and sign
            string pem = SoftwareAuthenticator.GenerateKeyPairPem(Algorithm.ES256);
            using var key = SoftwareAuthenticator.ImportPrivateKeyFromPem(pem);

            var credential = SoftwareAuthenticator.GetAssertion(TestRpId, TestChallenge, Algorithm.ES256, 0, AuthenticatorFlags.UserPresent, TestCredentialId, null!, key);

            var response = (AuthenticatorAssertionResponse)credential.Response;
            Assert.IsNotNull(response.Signature);
            Assert.IsNotEmpty(response.Signature);

            // Verify the signature
            byte[] clientDataHash = SHA256.HashData(response.ClientDataJson);
            byte[] signedData = new byte[response.AuthenticatorData.Length + clientDataHash.Length];
            Buffer.BlockCopy(response.AuthenticatorData, 0, signedData, 0, response.AuthenticatorData.Length);
            Buffer.BlockCopy(clientDataHash, 0, signedData, response.AuthenticatorData.Length, clientDataHash.Length);

            bool isValid = ((ECDsa)key).VerifyData(signedData, response.Signature, HashAlgorithmName.SHA256, DSASignatureFormat.Rfc3279DerSequence);
            Assert.IsTrue(isValid, "Signature from reimported PEM key should be valid");
        }

        #endregion
    }
}

#endif
