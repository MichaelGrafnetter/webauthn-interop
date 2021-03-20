using System;
using PeterO.Cbor;
using System.Security.Cryptography;
using System.Globalization;
using DSInternals.Win32.WebAuthn.COSE;

namespace DSInternals.Win32.WebAuthn.FIDO
{
    public class CredentialPublicKey
    {
        private CBORObject _cpk;

        public KeyType Type
        {
            get;
            private set;
        }

        public Algorithm Algorithm
        {
            get;
            private set;
        }

        public RSACng RSA
        {
            get
            {
                if (Type == COSE.KeyType.RSA)
                {
                    var rsa = new RSACng();
                    rsa.ImportParameters(
                        new RSAParameters()
                        {
                            Modulus = _cpk[CBORObject.FromObject(KeyTypeParameter.N)].GetByteString(),
                            Exponent = _cpk[CBORObject.FromObject(KeyTypeParameter.E)].GetByteString()
                        }
                    );
                    return rsa;
                }
                return null;
            }
        }

        public ECDsa ECDsa
        {
            get
            {
                if (Type == KeyType.EC2)
                {
                    var point = new ECPoint
                    {
                        X = _cpk[CBORObject.FromObject(KeyTypeParameter.X)].GetByteString(),
                        Y = _cpk[CBORObject.FromObject(KeyTypeParameter.Y)].GetByteString(),
                    };
                    ECCurve curve;
                    var crv = (EllipticCurve)_cpk[CBORObject.FromObject(KeyTypeParameter.Crv)].AsInt32();
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
                                    throw new FormatException(String.Format(CultureInfo.InvariantCulture, "Missing or unknown crv {0}", crv.ToString()));
                            }
                            break;
                        case Algorithm.ES384:
                            switch (crv) // https://www.iana.org/assignments/cose/cose.xhtml#elliptic-curves
                            {
                                case EllipticCurve.P384:
                                    curve = ECCurve.NamedCurves.nistP384;
                                    break;
                                default:
                                    throw new FormatException(String.Format(CultureInfo.InvariantCulture, "Missing or unknown crv {0}", crv.ToString()));
                            }
                            break;
                        case Algorithm.ES512:
                            switch (crv) // https://www.iana.org/assignments/cose/cose.xhtml#elliptic-curves
                            {
                                case EllipticCurve.P521:
                                    curve = ECCurve.NamedCurves.nistP521;
                                    break;
                                default:
                                    throw new FormatException(String.Format(CultureInfo.InvariantCulture, "Missing or unknown crv {0}", crv.ToString()));
                            }
                            break;
                        default:
                            throw new FormatException(String.Format(CultureInfo.InvariantCulture, "Missing or unknown alg {0}", Algorithm.ToString()));
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

        public RSASignaturePadding Padding
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
                            throw new FormatException(String.Format(CultureInfo.InvariantCulture, "Missing or unknown alg {0}", Algorithm.ToString()));
                    }
                }

                // This is not a RSA key, so there is no padding.
                return null;
            }
        }

        public byte[] EdDSAPublicKey
        {
            get
            {
                if (Type == KeyType.OKP)
                {
                    switch (Algorithm) // https://www.iana.org/assignments/cose/cose.xhtml#algorithms
                    {
                        case COSE.Algorithm.EdDSA:
                            var crv = (COSE.EllipticCurve)_cpk[CBORObject.FromObject(KeyTypeParameter.Crv)].AsInt32();
                            switch (crv) // https://www.iana.org/assignments/cose/cose.xhtml#elliptic-curves
                            {
                                case COSE.EllipticCurve.Ed25519:
                                    var publicKey = _cpk[CBORObject.FromObject(KeyTypeParameter.X)].GetByteString();
                                    return publicKey;
                                default:
                                    throw new FormatException(String.Format(CultureInfo.InvariantCulture, "Missing or unknown crv {0}", crv.ToString()));
                            }
                        default:
                            throw new FormatException(String.Format(CultureInfo.InvariantCulture, "Missing or unknown alg {0}", Algorithm.ToString()));
                    }
                }
                return null;
            }
        }

        public CredentialPublicKey(CBORObject cpk)
        {
            _cpk = cpk ?? throw new ArgumentNullException(nameof(cpk));
            this.Type = (KeyType) cpk[CBORObject.FromObject(KeyCommonParameter.KeyType)].AsInt32();
            this.Algorithm = (Algorithm) cpk[CBORObject.FromObject(KeyCommonParameter.Alg)].AsInt32();
        }

        public override string ToString()
        {
            return _cpk.ToString();
        }

        public byte[] GetBytes()
        {
            return _cpk.EncodeToBytes();
        }
    }
}
