using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Formats.Asn1;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using DSInternals.Win32.WebAuthn.EntraID;
using DSInternals.Win32.WebAuthn.FIDO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeterO.Cbor;
using DSInternals.Win32.WebAuthn.Okta;

namespace DSInternals.Win32.WebAuthn.Tests
{
    [TestClass]
    public class PasskeyFactory
    {
        internal WebauthnCredentialCreationOptions _options;
        internal CBORObject _attestationObject => CBORObject.NewMap().
            Add("fmt", "packed").
            Add("authData", _authData).
            Add("attStmt", CBORObject.NewMap().
                Add("alg", _alg).
                Add("sig", _sig).
                Add("x5c", _x5c)
            );

        internal COSE.Algorithm _alg;
        internal COSE.KeyType _kty
        {
            get
            {
                return _alg switch
                {
                    COSE.Algorithm.RS1 or COSE.Algorithm.RS256 or COSE.Algorithm.RS384 or COSE.Algorithm.RS512 or COSE.Algorithm.PS256 or COSE.Algorithm.PS384 or COSE.Algorithm.PS512 => COSE.KeyType.RSA,
                    COSE.Algorithm.ES256 or COSE.Algorithm.ES384 or COSE.Algorithm.ES512 => COSE.KeyType.EC2,
                    COSE.Algorithm.EdDSA => COSE.KeyType.OKP,
                    _ => throw new ArgumentOutOfRangeException(nameof(_alg)),
                };
            }
        }
        internal COSE.EllipticCurve _crv
        {
            get
            {
                return _alg switch
                {
                    COSE.Algorithm.ES256 => COSE.EllipticCurve.P256,
                    COSE.Algorithm.ES384 => COSE.EllipticCurve.P384,
                    COSE.Algorithm.ES512 => COSE.EllipticCurve.P521,
                    _ => throw new ArgumentOutOfRangeException(nameof(_alg)),
                };
            }
        }

        internal RSASignaturePadding _padding
        {
            get
            {
                return _alg switch
                {
                    COSE.Algorithm.RS1 or COSE.Algorithm.RS256 or COSE.Algorithm.RS384 or COSE.Algorithm.RS512 => RSASignaturePadding.Pkcs1,
                    COSE.Algorithm.PS256 or COSE.Algorithm.PS384 or COSE.Algorithm.PS512 => RSASignaturePadding.Pss,
                    _ => throw new ArgumentOutOfRangeException(nameof(_alg)),
                };
            }
        }

        internal CredentialPublicKey _credentialPublicKey;

        internal string _rp => _options.PublicKeyOptions.RelyingParty.Id;
        internal string _origin => new UriBuilder(Uri.UriSchemeHttps, _options.PublicKeyOptions.RelyingParty.Id).ToString();
        internal byte[] _challenge => _options.PublicKeyOptions.Challenge;
        internal CertificateRequest _certReq;
        internal static X500DistinguishedName _rootDN = new X500DistinguishedName("CN=Testing, O=DSInternals, OU=Passkeys, C=US");
        internal static byte[] _asnEncodedAaguid = [0x04, 0x10, 0x44, 0x53, 0x49, 0x6E, 0x74, 0x65, 0x72, 0x6E, 0x61, 0x6C, 0x73, 0x00, 0x00, 0x00, 0x00, 0x00,];
        internal byte[] _sig;
        internal CBORObject _x5c
        {
            get
            {
                _certReq.CertificateExtensions.Add(new X509BasicConstraintsExtension(true, true, 2, false));
                _certReq.CertificateExtensions.Add(new X509Extension(new Oid("1.3.6.1.4.1.45724.1.1.4"), _asnEncodedAaguid, true));
                using X509Certificate2 root = _certReq.CreateSelfSigned(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddDays(2));
                return CBORObject.NewArray().Add(root.RawData);
            }
        }

        internal byte[] _rpIdHash => SHA256.HashData(Encoding.UTF8.GetBytes(_rp));
        internal byte[] _clientDataJson
        {
            get
            {
                return JsonSerializer.SerializeToUtf8Bytes(new CollectedClientData()
                {
                    Type = "webauthn.create",
                    Challenge = _challenge,
                    Origin = _origin
                });
            }
        }

        internal byte[] _clientDataHash => SHA256.HashData(_clientDataJson);

        internal byte[] _attToBeSigned {
            get
            {
                byte[] toBeSigned = new byte[_authData.Length + _clientDataHash.Length];
                _authData.CopyTo(toBeSigned, 0);
                _clientDataHash.CopyTo(toBeSigned, _authData.Length);
                return toBeSigned;
            }
        }

        internal static byte[] HashData(HashAlgorithmName hashName, ReadOnlySpan<byte> data)
        {
            return hashName.Name switch
            {
                "SHA1" => SHA1.HashData(data),
                "SHA256" or "HS256" or "RS256" or "ES256" or "PS256" => SHA256.HashData(data),
                "SHA384" or "HS384" or "RS384" or "ES384" or "PS384" => SHA384.HashData(data),
                "SHA512" or "HS512" or "RS512" or "ES512" or "PS512" => SHA512.HashData(data),
                _ => throw new ArgumentOutOfRangeException(nameof(hashName)),
            };
        }

        internal byte[] _attToBeSignedHash(HashAlgorithmName alg)
        {
            return HashData(alg, _attToBeSigned);
        }

        internal byte[] _credentialID;
        internal const AuthenticatorFlags _flags = AuthenticatorFlags.AttestationData | AuthenticatorFlags.ExtensionData | AuthenticatorFlags.UserPresent | AuthenticatorFlags.UserVerified;
        internal ushort _signCount;
        internal static byte[] _aaguid = [0x44, 0x53, 0x49, 0x6E, 0x74, 0x65, 0x72, 0x6E, 0x61, 0x6C, 0x73, 0x00, 0x00, 0x00, 0x00, 0x00,];

        internal byte[] _authData
        {
            get
            {
                var writer = new ArrayBufferWriter<byte>(512);
                writer.Write(_rpIdHash);
                writer.Write(stackalloc byte[1] { (byte)_flags });
                var buffer = writer.GetSpan(4);
                BinaryPrimitives.WriteUInt32BigEndian(buffer, _signCount);
                writer.Advance(4);
                writer.Write(_acd);
                CBORObject exts = CBORObject.NewMap().Add("testing", true);
                writer.Write(exts.EncodeToBytes());
                return writer.WrittenSpan.ToArray();
            }
        }
        internal byte[] _acd
        {
            get
            {
                var writer = new ArrayBufferWriter<byte>(16 + 2 + _credentialID.Length + _credentialPublicKey.GetBytes().Length);

                writer.Write(_aaguid);

                // Write the length of credential ID, as big endian bytes of a 16-bit unsigned integer
                var credentialIDLen = (ushort)_credentialID.Length;
                var credentialIDLenBytes = BitConverter.GetBytes(credentialIDLen);
                if (BitConverter.IsLittleEndian)
                {
                    Array.Reverse(credentialIDLenBytes);
                }

                writer.Write(credentialIDLenBytes);
                // Write CredentialID bytes
                writer.Write(_credentialID);

                // Write credential public key bytes
                writer.Write(_credentialPublicKey.GetBytes());
                return writer.WrittenSpan.ToArray();
            }
        }

        internal static HashAlgorithmName HashAlgFromCOSEAlg(COSE.Algorithm alg)
        {
            return alg switch
            {
                COSE.Algorithm.RS1 => HashAlgorithmName.SHA1,
                COSE.Algorithm.ES256 => HashAlgorithmName.SHA256,
                COSE.Algorithm.ES384 => HashAlgorithmName.SHA384,
                COSE.Algorithm.ES512 => HashAlgorithmName.SHA512,
                COSE.Algorithm.PS256 => HashAlgorithmName.SHA256,
                COSE.Algorithm.PS384 => HashAlgorithmName.SHA384,
                COSE.Algorithm.PS512 => HashAlgorithmName.SHA512,
                COSE.Algorithm.RS256 => HashAlgorithmName.SHA256,
                COSE.Algorithm.RS384 => HashAlgorithmName.SHA384,
                COSE.Algorithm.RS512 => HashAlgorithmName.SHA512,
                (COSE.Algorithm)4 => HashAlgorithmName.SHA1,
                (COSE.Algorithm)11 => HashAlgorithmName.SHA256,
                (COSE.Algorithm)12 => HashAlgorithmName.SHA384,
                (COSE.Algorithm)13 => HashAlgorithmName.SHA512,
                COSE.Algorithm.EdDSA => HashAlgorithmName.SHA512,
                _ => throw new ArgumentOutOfRangeException(nameof(alg)),
            };
        }

        internal void MakeCredentialPublicKey()
        {
            var cpk = CBORObject.NewMap().
                Add(COSE.KeyCommonParameter.KeyType, _kty).
                Add(COSE.KeyCommonParameter.Alg, _alg);

            switch (_kty)
            {
                case COSE.KeyType.EC2:
                    ECCurve curve = _crv switch
                    {
                        COSE.EllipticCurve.P256 => ECCurve.NamedCurves.nistP256,
                        COSE.EllipticCurve.P384 => ECCurve.NamedCurves.nistP384,
                        COSE.EllipticCurve.P521 => ECCurve.NamedCurves.nistP521,
                        _ => throw new ArgumentOutOfRangeException(nameof(_crv)),
                    };

                    var ecdsa = ECDsa.Create(curve);
                    _certReq = new CertificateRequest(_rootDN, ecdsa, HashAlgorithmName.SHA256);
                    var ecparams = ecdsa.ExportParameters(true);

                    cpk.Add(COSE.KeyTypeParameter.X, ecparams.Q.X);
                    cpk.Add(COSE.KeyTypeParameter.Y, ecparams.Q.Y);
                    cpk.Add((int)COSE.KeyTypeParameter.Crv, (int)_crv);
                    _credentialPublicKey = new CredentialPublicKey(cpk);
                    var sig = ecdsa.SignData(_attToBeSigned, HashAlgFromCOSEAlg(_alg));
                    var coefficientSize = (int)Math.Ceiling((decimal)ecdsa.KeySize / 8);
                    var r = sig[0..coefficientSize];
                    var s = sig[(sig.Length - coefficientSize)..sig.Length];

                    var asnwriter = new AsnWriter(AsnEncodingRules.BER);
                    ReadOnlySpan<byte> zero = new byte[1] { 0 };
                    using (asnwriter.PushSequence())
                    {
                        asnwriter.WriteIntegerUnsigned(r);
                        asnwriter.WriteIntegerUnsigned(s);
                    }
                    _sig = asnwriter.Encode();
                    break;
                case COSE.KeyType.RSA:
                    var rsa = RSA.Create();

                    var padding = _alg switch // https://www.iana.org/assignments/cose/cose.xhtml#algorithms
                    {
                        COSE.Algorithm.RS1 or COSE.Algorithm.RS256 or COSE.Algorithm.RS384 or COSE.Algorithm.RS512 => RSASignaturePadding.Pkcs1,
                        COSE.Algorithm.PS256 or COSE.Algorithm.PS384 or COSE.Algorithm.PS512 => RSASignaturePadding.Pss,
                        _ => throw new ArgumentOutOfRangeException(nameof(_alg)),
                    };
                    _certReq = new CertificateRequest(_rootDN, rsa, HashAlgorithmName.SHA256, padding);
                    var rsaparams = rsa.ExportParameters(true);
                    cpk.Add(COSE.KeyTypeParameter.N, rsaparams.Modulus);
                    cpk.Add(COSE.KeyTypeParameter.E, rsaparams.Exponent);
                    _credentialPublicKey = new CredentialPublicKey(cpk);
                    _sig = rsa.SignData(_attToBeSigned, HashAlgFromCOSEAlg(_alg), _padding);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_kty), _kty, "Invalid COSE key type");
            }
        }

        public WebauthnAttestationResponse MakePasskey(WebauthnCredentialCreationOptions options, int algIndex = 0)
        {
            WebauthnAttestationResponse response;
            _credentialID = RandomNumberGenerator.GetBytes(32);
            _options = options;
            _alg = _options.PublicKeyOptions.PublicKeyCredentialParameters[algIndex].Algorithm;
            MakeCredentialPublicKey();

            PublicKeyCredential pkc = new()
            {
                AuthenticatorResponse = new AuthenticatorAttestationResponse()
                {
                    AttestationObject = _attestationObject.EncodeToBytes(),
                    ClientDataJson = _clientDataJson
                },
                ClientExtensionResults = new()
                {
                    HmacSecret = true
                },
                Id = _credentialID
            };

            response = options.GetType().Name switch
            {
                nameof(MicrosoftGraphWebauthnCredentialCreationOptions) => new MicrosoftGraphWebauthnAttestationResponse(pkc, $"DSInternals.Passkeys {_alg}"),
                nameof(OktaWebauthnCredentialCreationOptions) => new OktaWebauthnAttestationResponse(pkc, options.PublicKeyOptions.User.Id, (options as OktaWebauthnCredentialCreationOptions).Id),
                _ => throw new ArgumentOutOfRangeException(nameof(options)),
            };

            return response;
        }
    }

    [TestClass]
    public class PublicKeyCredentialCreationOptionsTester
    {
        [TestMethod]
        public void EntraIdPublicKeyCredentialCreationOptions_Deserialize()
        {
            var options = JsonSerializer.Deserialize<PublicKeyCredentialCreationOptions>(@"{
                ""rp"": {
                    ""id"": ""login.microsoft.com"",
                    ""name"": ""Microsoft""
                },
                ""user"": {
                    ""id"": ""T0Y6mho3vwFprE6aslBq9b2Qjz7Ixbn0PntEPkHirHimyfAtmz4xoilu6sNoiPFkmvxW"",
                    ""name"": ""john@contoso.com"",
                    ""displayName"": ""John Doe""
                },
                ""challenge"": ""ZXlKaGJHY2lPaUpTVTBFdFQwRkZVQzB5TlRZaUxDSmxibU1pT2lKQk1qVTJSME5OSWl3aWVEVmpJanBiSWsxSlNVUmhSRU5EUVd4RFowRjNTVUpCWjBsUmJ6bHBiMVZwVVhadk4wcFFPVmhCUWxCUFJsQTFha0ZPUW1kcmNXaHJhVWM1ZHpCQ1FWRnpSa0ZFUWpSTldGbDNSVkZaUzBOYVNXMXBXbEI1VEVkUlFrZFNXVVJpYlZZd1RVSlZSME5uYlZOS2IyMVVPR2w0YTBGU2ExZENNMlJ3WW0xU2RtUXpUWGRJVVZsRVZsRlJSRVY0V2s1VmVURlFZMjFrYUdKdGJEWlpXRkp3WWpJMGRGRlhUbXBhV0U1NlRVTnpSMEV4VlVWRGVFMXJUMFJLYTFsdFJtcFpWRkYwVFRKVk5FMVRNREJPYlU1b1RGUnNhazU2VFhSTlJHc3hUVWROZUZwWFJtcFpWR3N6VFVJMFdFUlVSVEJOUkZGNFQwUkJNRTFVUVRGTmJHOVlSRlJKTUUxRVVYaE9WRUV3VFZSVk1VMXNiM2RNZWtWMFRVTnpSMEV4VlVWQmVFMXJXVzFLYUU1cVdUVk5WMVYwVG1wck1rOVRNREJQVkd4clRGZEthMDlIUlhSTmFsRXlXbFJCZDAxdFZtcE5lbWN3VFVsSlFrbHFRVTVDWjJ0eGFHdHBSemwzTUVKQlVVVkdRVUZQUTBGUk9FRk5TVWxDUTJkTFEwRlJSVUZyUVRkWmVFSkdPV1pwV0VWaWN6UkVaM00yTlV0WWFTdHBjbEUxT0VsMVJrazJWbTR5VWs0eGMzZHRkMGRpTjJwWk1uTXhjVk13TkV0blUyTmpWSFZDVkdoWVUwaEZORFJpYzJGQmIwUlNPRFV5U1ZOdE1sbFhjVkIyUVZwUmFTdFhTbEpTYWxsWU1tazFSWFZUWm1SRGRHTXZZMVk0VGxSV2NuaFhNMVpRWVZScmEzRm9WR1ZaUWxGVFJHWjRjbGd4ZEZGM09VWTJRbUZIU3preWIwRXZWU3RPTmxGMmRVNXNlRmRhVWpOSVUyOW9ZM2w1WmtWSk1FWlhiemgxVkVsaWRqbHpUbWszYVVGT09FbENTMloxVnk4NFJtcE5ZbmhaWjFaWFJFZ3daSFZNUjBKMk5rczVhRll2WW1NMlluQjNWMlpKZDJaR2FFZFVOMWxNYUZaMk5rVjJkbkE0YmpGWlVIUTVTR2gzYlRnM2FYUlpSRzFHTlZGVk4ySjFaV0phVFhKaVUybE9NR2RUVkhCSFIyWkxNRGx4VUV4TWVFODVVbUZ2Ym5BeGMxQk9RV3h2Tkhod1ltSk5jV1puWTJGTk16RlFTRkZKUkVGUlFVSnZlbU4zVGxSQlRVSm5UbFpJVWsxQ1FXWTRSVUZxUVVGTlFtZEhRVEZWWkVwUlJVSXZkMUZQVFVGM1IwTnBjMGRCVVZGQ1oycGpWVUZuUlhkRGQxbEVWbEl3VUVKQlVVUkJaMDAwVFVFd1IwTlRjVWRUU1dJelJGRkZRa04zVlVGQk5FbENRVkZDTXpjcllWcGlWMUJGUmxKNmFYQlBSVU0yZGtsdVVXa3lSbmgyWTFGb1duaFNNWEZhUmtwSFNtRldSM2wxU1dseGVFNXJjbmwzU0RReVQwZHdlR3RFTTNKdEszcDNaRVY1ZVU5UFYwazJXSEJzVlU1NWFGTjZWM0ZXSzBSM2RIcGlkWEJVVDJaQ1NERmhLMnRSVkhnMWMwYzJabXBWYTBJeWFtbElZVEIxWVZvMFl6SXdNV0pzUmtsdWVuTnlVMkpoWldsemFscDNOM1pxT0dSa1JGcG5jazB4Tm5GWWFrTkJOelZuWTJSRGNWcE5XVTk0Y205YVpuQkVURTl5Tm5sc1kwVkJWbVJGUlV4RVoxRkVUM1l4YVVOQ2VEazFVMmRrWXpOUU55OWhXWHA2VFZvd1ZWQXJjMEZHY0Zvd09GSkRZWFZIVVhKaE5sTjRNbVJRV0dnM1ZWUTJObGhYVVZKNVJUVkVWV2RuZW5KcFRIRTJiRGhOVFdoUmIxZzBkMkpuV1ROTFJXWnpTWHBMVDNkdEswZ3hWaTh6V1RkdFJ6Sk9VU3RQV1ZwbkwxQk5PVkpJVUVSSlNWRlVVemh4TjB4ek1HOUZXVWNpWFN3aWVEVjBJam9pTTBNMFEwSTVOVVV4TWpJMU5VUkVRVGt6TmpNMk1qQTROa0pFTVRNNU16WTJRak5GT1RaQlFTSjkuTFBWTFpJeGdqc2hXRVQyd1dueG40bmZremJxZXdiN3VSamlfOXotMzNLWnpBTEF0QXdhNE40NnRoLVVEdXpOVlp4c0RtYkpjOWFJaUU3ZDRXdlpOYjg0LUtDbVJfTWJFYlFCYjlINU5ldXFycnktaWhoNzctNWxRVEgxSDNCVjFMMl8yUTEweUgyQlpkUVhuZTQ5UlM2QVY4SVBIYUUyT1ZFS2lxcjZSY0Z3eGUzSG5ZdFBpQUlST0dzSkstVnBkQ21RTm9fWWttTS1Dd2dPNVZtMnhDM2FnS1RkYWQxVmEwb0s4djZVQlZaSDJqSW5za21DWUhtYkplbmhqcGRFVjhGOHM3UGZ0VWxEQWlycHpxdnNqdkpIX2x3bkxjaW9aa0Z6V052ZFJYMk81ZzBPTGhMSkUwbXZmSDB1ZHVKMi1OdGtGQzFvRTZ1Vi0xMjY0MUNGWFVnLkN0NkdtbWtGS1BxR1lELVBSV04yd2cuVC10OEpibEtocmtRUDRRSHVrSlc2NDRmbFFKVjc1c1VIYXQweDlPSjVQNERXZTRHUU95US03bWMzcVBESWJQX2hyb25jMXhCbFBsSmt1ay1xV1pONUIxOXNJUWhPWVo3S0JfbktkQktDb0xIY21idmZpc0hSLXowRkdySlFUUkRzSmhVQVVSNHNKYm5vODBjU240NTYwN2FNOGxBTFJSNkJERkIzVkxxaW1Yd19ROGJiWHo1UTNzS1R5QTFaa0UtRGxuUFBGbm5pOTNuekhCZlQ4clVFcmctb1lPeTNMTWtiUk1qYlVDWDk5eW9jYmc5RG9Ba3hNQ04zcmZac2hyUVdvTC1FOUVTd2w2YkVlZlByb3pTNXozOENTTVJVZlIzUXNtMkxyVi1QUl9HOTFLUGpIeUtreG52UTNGdWpPUDJMOFR3bkpRWk9lUEtjVV9GSUYxWjFFZjV2TVBVTF94QzgzRWNvenE2b2JWRERDZl9aVlpueEh6bnJndy1YSjlpTC0tVVBiOHFjVE55eFl1bkhsR3ZoV2c3V2NuV09rZkJzbGlWZ0MxM01JeEZWRFQwTm9ta2pWb3R1cjdPenZtMDFienpicS0wMG1vdlJvUnlTSDFKcmJDcUdfN2tVOElGcG02Q3Q3ZGVZUlE1QjgySVRQZ0RMZllEOTNtUHdvQjh5eGhPSklMeThCa2NvbkZCd3VkZWJwWmdQaGJwQ1U3UDI3NVo5R0xDTkEtMjRSd190Sl8xb3NUcm5LSFB3bHpmR3VRVTE0RXpUaHotRVNjamZHLWVERXFDeGtINXk2WjV6ZFg4MW5WQzliLWppN3FHdGc4SVdLaklQamU0LWhueTM0ZWNXUndVVjM5NWEwZzVUWi1sNlNVSG1hVmNDdl9GQWNWUmlRaWxBMXFibl80UlotZW92NEpySUlOZE44a2NOZTJHV1hBMEVjU0xRVkdYUl9sbDBDNFlsdmZaeE53NEpyZ0pFU192VE1vQWZWM2JDdGVCcC00cTVZTm1tRm5qSUtlVUhwOWUxdi1tYnBZNXp1WnVxYTVhcWlOQU5sbnozUWdKVS1HbUlmeS1zQ01PdzJSTVJpYld4T0NmN0JmNGtDTUxHR29Rdl9HLVVPTEtyZjBxWHE3NzRJamV5bmpyYVdLQjRndXV3Q1oxWnpKUUwzRXZ5dlZTNF85NVhEWmtsaWh6N20zaGtNeUVMbHRCMDA2SUh0YnB1MmQ4MXI3VkYyQWUyWThPRTRsdHVtUlVxSUhSakZYQi16cTRWd0VoeXB1a2xWVExwVVJyT09FQkhKWWVMdUU2SnBXOFF6SHhFTl9YWWUycnh1VllFRU5KeklNMUh3bVhLcmN0cGZlZ19DeE9fZmJGbjlmN0FJVTlCMFFqaWdwUEVOMXpNbWJERG1rV01WV2NTbXEtNklVYkRkNEQ2OEt4LW5weEJrVEo4YnRLYlV3dUlJY25hSEliRVk0VkE2bjVpWE1OX2tHM3FlaHhtRklEVUNTdEhkT1l1dWF0ZlhSUjZYWmVJLU5YelBld205N2NLN2JLYmJGdE9RUUlLeFJxUzZSR2xMN0tjVGYyWHdaUkx1bGpWZ1gtdVpQNzJDVXloNG10TnU4allvWmVwaThGQnpTQWtlZS11SmV2UHMyMDVPd3I3d29wWnJnS3hpNVp3UjNOVWltVmVmZEE1U3dsal9LSWVydEpPYTZRbTRiSElMQW81dU5hNEtYcDNxQU96ekJaSVpSVWFPSEpyYnB5aUFBU24zOWFGRE91bExsM3BsNkkzZ2Rxc0lIcFNkczROUXFyNlJ6WVB0NzBxQ1hnWGpjRnlFaE91NDdES2NMenVBeTgtcWotQ2paWVBCUWpfcUNHUUJiYTh2eWtzMDBHMU12RlJpam1wRzFaT0d4UDhyZWxvRXdqLS13TEtLQnlkdFhkYUw4RndrLTZGUkRBU21EUGZLQUl5SGppS3owaHN6NERueUR0X0RKaFd5RnNXVEFjTVpIdE03R2hnU0dKa1A3SVVRcFUyN3JYdGlDTm11ajlKN0JDcEpPdmtNdHhuTmgzejc5QklneHRxTkJnOXV6MDJnUllWVDdUTHN4c1pjaEZmOFZQNExhQTZpbDNvanpBSFhWLUUzd3lrWTEzcmo2OURUTTg3bUxzY1dlTnV1MkpUNkl5MEplRGdNU25WVzM3ZkRPMEtldV9Fa1Uwa0hRSU1TU0NpdnlyeFNLMlJnRmVBelFNNndLOHEyYXl2ZVRNamZzcXRib3ZubUlmRjh6TkJESWM0VGhvdGFGRjYwTmx5NkRQMjAyRktFd2VYV0k2dnFtUHVVcElFQzlJS2pHSU5kR0dCRjVuOHRUMVNIRTdKR2l4MFdMc1ZlN0NSQkdwMU1DZFZ1QkFkalRxSFZybXZKWUgtbkZfUWFuZS1wQ181ZW5tcmszdHhoZVp5WXJha3BwRTZrNEhWaThPQXRkYmc2S3lGUDVTX0Y4YWdEUmFkek5IdGE0MVRXb3ZKSjZwbFZyZW5CLTcya0h0ME0zc2dqUXlOQU8zMkhUQnNPZXlOR1F6WkNfRkdaN3EwYWRhWU51Nm5DMHNnMHNzWVdxVS1ieGV1ME1RWHlkUzBtZFFMUmRTUFdnM0ZXYmJhM3pmUkRrQ295UXZhdkE2dXdlTXFfQUJwMnJxVWM3RmlNREV6VGVFcjFVOERRTWNLam8wb0paRDYtMUM0NlN5LWlDckhCcFlYS2txUmg5TW8tVWs3Nzl2TlF4YWc2TmtwMWRfa3k3WTRPbG5ncm1TenFoM0tqUjZ3OTByRUt1LVlQM1ZkR3hlUnkzd2RCNU1LUUFmMlZfUGJjNlpZc2JkZVUydDkzUkpsYmlaQ2hWa3NCNzc3b2hVWDk2cnUzOGhscjZGSU9WMk5YMHA2X3FQb19HNnZxNW5VQkh3MU9DOWpLQUlFRXNzaGcwdVdKMEpVUGhfTlNQMlQ2YnFRZWZKNTYyRlZoVlFIMkVIbUFvMWlrbFRDRFk3M0hKcUE5S2NZNW9sQXEyT2Q5eVBPcXk5SU40ajRReWRvTmhKTzVVY1FaYmNPZUF4N2kzT3pBWlVzYTRuNDlmNlh3bFRyVWRBRXNwT3dmU0NwMHR2Sl9FSG8zOGtpLWlKWnpkVzhiajVnT1Z2NVpBRHlLeWtCZ3lXZUFJN0FiaGdkWkJhYjRMaFRiY0c5dEdUTzBhbjhuVDkxUHViTENjbjltQ05DM01EdWpmYmktMmR3WFBnSGdCTmtWd3JJcTFsUGFLZlpuUXhaWGh0aDFGYkxDU1A1UklkTmh3eF85N05XdVUtSEozR29rNllRQnFpQnJ3NGRFbjBhc2pRLVplYVZFY2pyY3NuNURRRFd2a19QRXFLTUpBcjN5QURxOEFibHJQelRZVGFGdy1TczZ2TXdKQ1hoTTRoRXJYOTB0Mm84RGQ1dVN6TTFCNExUTUhyTVBiaktjY2Q0UHcwOVNMQW5uNGJIRm5CU0dzYVVMMVlMZ3VkdllQTEpFc3RIanlpQXcyTXk5NHJTbUg4Q2NmdC1UVllDQjZxZGEtNGtqTTFFbnQzNG5BSjREYlM4SklCTXlkWUZndF93WHBwUTBkNFUwTmtZdFBUTlV1dnlZMkJrVUVvOWs0aTRZRXlUMGFJRlY4SGd4SGRxU1ZEbW9JRGFkRlN6emcxQ28xVVh3MkpRQllRWE1WY05pcU1VRWVuS0EtR214TzlWSHVFNWp3aTgzWk1OYmN2a1JxdTF5cHpJYktoMXZIbDVfdXhEN0lZaGsxRGR5bG91bWRkT0FBX3Jjei1GMDAydF9sXzJ1SFFVOVcxNUFwVDl0X25oZmwxZmxRalRidGZhd0hEQWxCZjFLRFFiZV93QVd0eUNoXzNtZjRoUGdpQmlqbTZKdTh5SnFKcHpfUEgzX1c0LWlRMXZnQUoxTEV5VTZaMXZJYkJPelBTUXVmQ1lrTU1UV0ppZ3pZbm9raS1SeXlKWWMwRTg1ZU1pUDFmVWtDTTdWbkJiYnU2SU5ERV9OOFo3cV9CRVlwaC1xNmtENm9uanhRUnpKcEJfQS0yOGVNLVFqSkYwTl9jLUZzZHRTcDBXaDM3OGhoSkhkMnRyUlZOWnNWbXB3VGZXWHhjbHl3SkpnclFpVGNhSEtvRXNJUmtFdHl0LlB6NktCcDV0Y0VwNC12LWUxemt4c1E"",
                ""pubKeyCredParams"": [
                    {
                        ""type"": ""public-key"",
                        ""alg"": -7
                    },
                    {
                        ""type"": ""public-key"",
                        ""alg"": -257
                    },
                    {
                        ""type"": ""public-key"",
                        ""alg"": -8
                    }
                ],
                ""timeout"": 60000,
                ""excludeCredentials"": [
                    {
                        ""type"": ""public-key"",
                        ""id"": ""TZ2KrDEawbrIGQsnheGZ/BG9Nfnb7blSGwQYMGyJIhM="",
                        ""transports"":[]
                    },
                    {
                        ""type"": ""public-key"",
                        ""id"": ""g/V3JMewxqZaKD8mRpB2pTTl3SBnOR6gZEw/URDlB4o="",
                        ""transports"":[ ""usb"" ]
                    },
                    {
                        ""type"": ""public-key"",
                        ""id"": ""GshyINLMaOOwqt1LNUi0gQ=="",
                        ""transports"":[ ""usb"", ""nfc"" ]
                    },
                    {
                        ""type"": ""public-key"",
                        ""id"": ""9ccVagkzE02vPo+qoonYDv8FoREd9QYOn8RFSsbLr8Y="",
                        ""transports"":[]
                    },
                    {
                        ""type"": ""public-key"",
                        ""id"": ""prXptqCfm_ohEAcE2Jw6CUtUb4uWpb2vaS-cFB82ruMhcIRAE17S0g1bkPjtsC6S0"",
                        ""transports"":[]
                    },
                    {
                        ""type"": ""public-key"",
                        ""id"": ""MMeD8wrNCZ-0hbfdYvvAIzrsrTGd_uXdt2iXbYlGJSs1"",
                        ""transports"":[]
                    },
                    {
                        ""type"": ""public-key"",
                        ""id"": ""owBYhYk51YPpOdLDJ-mcaIFdZVcCZ63YFogBU0VyJ42UkiptLxN5l7ZkRu4KtEo4XFJfJcahJ5C5uTj4YKxTmeISUiuOSgtBHlUh_RndhweyQJ8UY3HWwqvN57T3cvqXy_nG2VsR-Gw2upzNUW4LHGFjfSXj3rEe3B6KltliX1c68aDqDh3dquwBTAK9roaQzq3XZBZT_AJQKX2qop1j1Fh2-xOdeFR7aQ2"",
                        ""transports"":[]
                    }
                ],
                ""authenticatorSelection"": {
                    ""authenticatorAttachment"": ""cross-platform"",
                    ""requireResidentKey"": true,
                    ""userVerification"": ""required""
                },
                ""attestation"": ""direct"",
                ""extensions"": {
                    ""hmacCreateSecret"":true,
                    ""enforceCredentialProtectionPolicy"":true,
                    ""credentialProtectionPolicy"":""userVerificationOptional""
                }
            }");

            Assert.AreEqual("login.microsoft.com", options.RelyingParty.Id);
            Assert.AreEqual("john@contoso.com", options.User.Name);
            Assert.AreEqual(AttestationConveyancePreference.Direct, options.Attestation);
            Assert.IsNotNull(options.ExcludeCredentials);
            Assert.IsTrue(options.AuthenticatorSelection.RequireResidentKey);
            Assert.AreEqual(AuthenticatorAttachment.CrossPlatform, options.AuthenticatorSelection.AuthenticatorAttachment);
            Assert.AreEqual(UserVerificationRequirement.Required, options.AuthenticatorSelection.UserVerificationRequirement);
            Assert.AreEqual(3, options.PublicKeyCredentialParameters.Count);
            Assert.AreEqual(COSE.Algorithm.ES256, options.PublicKeyCredentialParameters[0].Algorithm);
            Assert.AreEqual(COSE.Algorithm.RS256, options.PublicKeyCredentialParameters[1].Algorithm);
            Assert.AreEqual(COSE.Algorithm.EdDSA, options.PublicKeyCredentialParameters[2].Algorithm);
        }

        [TestMethod]
        public void OktaPublicKeyCredentialCreationOptions_Deserialize()
        {
            var options = JsonSerializer.Deserialize<PublicKeyCredentialCreationOptions>(@"{
                ""rp"": {
                    ""name"": ""Okta Tenant Name -- Environment"",
                    ""id"": ""example.okta.com""
                },
                ""user"": {
                    ""displayName"": ""Okta Doe"",
                    ""name"": ""okta@contoso.com"",
                    ""id"": ""00eDuihq64pgP1gVD0x7""
                },
                ""pubKeyCredParams"": [
                    {
                        ""type"": ""public-key"",
                        ""alg"": -7
                    },
                    {
                        ""type"": ""public-key"",
                        ""alg"": -257
                    }
                ],
                ""challenge"": ""AVun-poGmJKZOAT0r-KBSs-94BPqMf3j"",
                ""attestation"": ""direct"",
                ""authenticatorSelection"": {
                    ""userVerification"": ""required"",
                    ""requireResidentKey"": false
                },
                ""u2fParams"": {
                    ""appid"": ""https://example.okta.com""
                },
                ""excludeCredentials"": [
                    {
                        ""type"": ""public-key"",
                        ""id"": ""VX_AlCL9qUx2ox_Ekth4NYngvwpUswaBqcfb4XsHglI""
                    },
                    {
                        ""type"": ""public-key"",
                        ""id"": ""kFPT5CL3-I30e22QQ0WYo4C9EFCTcbWM0-G-wTBslVNzKzf-FsJ1CBVrgN2k5RJH2dTFJxyzgI06XxIbrcbpAA""
                    }
                ]
            }");

            Assert.AreEqual("example.okta.com", options.RelyingParty.Id);
            Assert.AreEqual("okta@contoso.com", options.User.Name);
            Assert.AreEqual(AttestationConveyancePreference.Direct, options.Attestation);
            Assert.IsNotNull(options.ExcludeCredentials);
            Assert.IsFalse(options.AuthenticatorSelection.RequireResidentKey);
            Assert.AreEqual(AuthenticatorAttachment.Any, options.AuthenticatorSelection.AuthenticatorAttachment);
            Assert.AreEqual(UserVerificationRequirement.Required, options.AuthenticatorSelection.UserVerificationRequirement);
            Assert.AreEqual(2, options.PublicKeyCredentialParameters.Count);
            Assert.AreEqual(COSE.Algorithm.ES256, options.PublicKeyCredentialParameters[0].Algorithm);
            Assert.AreEqual(COSE.Algorithm.RS256, options.PublicKeyCredentialParameters[1].Algorithm);
        }
    }
}
