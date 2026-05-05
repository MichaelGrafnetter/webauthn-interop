using System;
using System.Collections.Generic;
using System.Formats.Cbor;
using System.Security.Cryptography;
using DSInternals.Win32.WebAuthn.FIDO;

namespace DSInternals.Win32.WebAuthn.COSE;

/// <summary>
/// Represents a parsed COSE <c>credentialPublicKey</c> structure.
/// </summary>
public class CredentialPublicKey
{
    private readonly byte[] _rawCborBytes;
    private readonly Dictionary<int, ReadOnlyMemory<byte>> _parameters;

    /// <summary>
    /// Gets the COSE key type.
    /// </summary>
    public KeyType Type
    {
        get;
        private set;
    }

    /// <summary>
    /// Gets the COSE signature algorithm identifier.
    /// </summary>
    public Algorithm Algorithm
    {
        get;
        private set;
    }

    /// <summary>
    /// Gets the RSA public key when the key type is RSA.
    /// </summary>
    public RSACng? RSA
    {
        get
        {
            if (Type == KeyType.RSA)
            {
                var rsa = new RSACng();
                rsa.ImportParameters(
                    new RSAParameters()
                    {
                        Modulus = CborHelper.DecodeByteString(_parameters[(int)KeyTypeParameter.N]),
                        Exponent = CborHelper.DecodeByteString(_parameters[(int)KeyTypeParameter.E])
                    }
                );
                return rsa;
            }
            return null;
        }
    }

    /// <summary>
    /// Gets the ECDSA public key when the key type is EC2.
    /// </summary>
    public ECDsa? ECDsa
    {
        get
        {
            if (Type == KeyType.EC2)
            {
                var point = new ECPoint
                {
                    X = CborHelper.DecodeByteString(_parameters[(int)KeyTypeParameter.X]),
                    Y = CborHelper.DecodeByteString(_parameters[(int)KeyTypeParameter.Y]),
                };
                ECCurve curve;
                var crv = (EllipticCurve)CborHelper.DecodeInt32(_parameters[(int)KeyTypeParameter.Crv]);
                switch (Algorithm)
                {
                    case Algorithm.ES256:
                        switch (crv)
                        {
                            case COSE.EllipticCurve.P256:
                            case COSE.EllipticCurve.P256K:
                                curve = ECCurve.NamedCurves.nistP256;
                                break;
                            default:
                                throw new FormatException($"Missing or unknown crv {crv}");
                        }
                        break;
                    case Algorithm.ES384:
                        switch (crv) // https://www.iana.org/assignments/cose/cose.xhtml#elliptic-curves
                        {
                            case EllipticCurve.P384:
                                curve = ECCurve.NamedCurves.nistP384;
                                break;
                            default:
                                throw new FormatException($"Missing or unknown crv {crv}");
                        }
                        break;
                    case Algorithm.ES512:
                        switch (crv) // https://www.iana.org/assignments/cose/cose.xhtml#elliptic-curves
                        {
                            case EllipticCurve.P521:
                                curve = ECCurve.NamedCurves.nistP521;
                                break;
                            default:
                                throw new FormatException($"Missing or unknown crv {crv}");
                        }
                        break;
                    default:
                        throw new FormatException($"Missing or unknown alg {Algorithm}");
                }
                return ECDsa.Create(new ECParameters
                {
                    Q = point,
                    Curve = curve
                });
            }
            return null;
        }
    }

    /// <summary>
    /// Gets the RSA signature padding implied by the selected algorithm.
    /// </summary>
    public RSASignaturePadding? Padding
    {
        get
        {
            if (Type == KeyType.RSA)
            {
                switch (Algorithm) // https://www.iana.org/assignments/cose/cose.xhtml#algorithms
                {
                    case Algorithm.PS256:
                    case Algorithm.PS384:
                    case Algorithm.PS512:
                        return RSASignaturePadding.Pss;

                    case Algorithm.RS1:
                    case Algorithm.RS256:
                    case Algorithm.RS384:
                    case Algorithm.RS512:
                        return RSASignaturePadding.Pkcs1;
                    default:
                        throw new FormatException($"Missing or unknown alg {Algorithm}");
                }
            }

            // This is not a RSA key, so there is no padding.
            return null;
        }
    }

    /// <summary>
    /// Gets the EdDSA public key bytes when the key type is OKP and algorithm is EdDSA.
    /// </summary>
    public byte[]? EdDSAPublicKey
    {
        get
        {
            if (Type == KeyType.OKP)
            {
                switch (Algorithm) // https://www.iana.org/assignments/cose/cose.xhtml#algorithms
                {
                    case COSE.Algorithm.EdDSA:
                        var crv = (COSE.EllipticCurve)CborHelper.DecodeInt32(_parameters[(int)KeyTypeParameter.Crv]);
                        switch (crv) // https://www.iana.org/assignments/cose/cose.xhtml#elliptic-curves
                        {
                            case COSE.EllipticCurve.Ed25519:
                                return CborHelper.DecodeByteString(_parameters[(int)KeyTypeParameter.X]);
                            default:
                                throw new FormatException($"Missing or unknown crv {crv}");
                        }
                    default:
                        throw new FormatException($"Missing or unknown alg {Algorithm}");
                }
            }
            return null;
        }
    }

    /// <summary>
    /// Initializes a new instance from CBOR-encoded credential public key bytes.
    /// </summary>
    /// <param name="cborBytes">CBOR-encoded <c>credentialPublicKey</c> (COSE_Key).</param>
    public CredentialPublicKey(byte[] cborBytes)
    {
        _rawCborBytes = cborBytes ?? throw new ArgumentNullException(nameof(cborBytes));
        _parameters = CborHelper.ReadIntKeyedMap(cborBytes);
        this.Type = (KeyType)CborHelper.DecodeInt32(_parameters[(int)KeyCommonParameter.KeyType]);
        this.Algorithm = (Algorithm)CborHelper.DecodeInt32(_parameters[(int)KeyCommonParameter.Alg]);
    }

    /// <summary>
    /// Returns a textual representation of the credential public key.
    /// </summary>
    public override string ToString()
    {
        return $"Type: {Type}, Algorithm: {Algorithm}";
    }

    /// <summary>
    /// Encodes the credential public key to raw CBOR bytes.
    /// </summary>
    /// <returns>CBOR-encoded key bytes.</returns>
    public byte[] GetBytes()
    {
        return _rawCborBytes;
    }

    public static byte[] Build(AsymmetricAlgorithm key, Algorithm algorithm)
    {
        ArgumentNullException.ThrowIfNull(key);

        var writer = new CborWriter(CborConformanceMode.Lax);

        if (key is ECDsa ecdsa)
        {
            var parameters = ecdsa.ExportParameters(false);

            if (!algorithm.IsECDSA)
            {
                throw new NotSupportedException($"Algorithm {algorithm} is not compatible with EC keys.");
            }

            writer.WriteStartMap(5);

            writer.WriteInt32((int)KeyCommonParameter.KeyType);
            writer.WriteInt32((int)KeyType.EC2);

            writer.WriteInt32((int)KeyCommonParameter.Alg);
            writer.WriteInt32((int)algorithm);

            writer.WriteInt32((int)KeyTypeParameter.Crv);
            writer.WriteInt32((int)algorithm.Curve!);

            writer.WriteInt32((int)KeyTypeParameter.X);
            writer.WriteByteString(parameters.Q.X!);

            writer.WriteInt32((int)KeyTypeParameter.Y);
            writer.WriteByteString(parameters.Q.Y!);

            writer.WriteEndMap();
            return writer.Encode();
        }

        if (key is RSA rsa)
        {
            // Validate that the specified algorithm is compatible with RSA keys
            if (!algorithm.IsRSA)
            {
                throw new NotSupportedException($"Algorithm {algorithm} is not compatible with RSA keys.");
            }

            var parameters = rsa.ExportParameters(false);
            writer.WriteStartMap(4);

            writer.WriteInt32((int)KeyCommonParameter.KeyType);
            writer.WriteInt32((int)KeyType.RSA);

            writer.WriteInt32((int)KeyCommonParameter.Alg);
            writer.WriteInt32((int)algorithm);

            writer.WriteInt32((int)KeyTypeParameter.N);
            writer.WriteByteString(parameters.Modulus!);

            writer.WriteInt32((int)KeyTypeParameter.E);
            writer.WriteByteString(parameters.Exponent!);

            writer.WriteEndMap();
            return writer.Encode();
        }

        // TODO: Add support for EdDSA keys.
        throw new NotSupportedException($"Unsupported key type: {key.GetType().Name}");
    }
}
