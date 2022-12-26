using System;
using System.Collections.Generic;
using DSInternals.Win32.WebAuthn.COSE;
using Fido2NetLib;
using Fido2NetLib.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Win32.WebAuthn.Adapter.Tests
{
    [TestClass]
    public class ApiMapperTester
    {
        [TestMethod]
        public void ApiMapper_Translate_CredentialType_CorrectInput()
        {
            string result = ApiMapper.Translate(PublicKeyCredentialType.PublicKey);
            Assert.AreEqual("public-key", result);
        }

        [TestMethod]
        public void ApiMapper_Translate_CredentialType_NullInput()
        {
            string result = ApiMapper.Translate((PublicKeyCredentialType?)null);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ApiMapper_Translate_PubKeyCredParams_Input1()
        {
            var input = new List<PubKeyCredParam>()
            {
                new PubKeyCredParam() { Alg = Fido2NetLib.Objects.COSE.Algorithm.ES256, Type = PublicKeyCredentialType.PublicKey },
                new PubKeyCredParam() { Alg = Fido2NetLib.Objects.COSE.Algorithm.RS256, Type = PublicKeyCredentialType.PublicKey }
            };

            var expected = new[] { Algorithm.ES256, Algorithm.RS256 };
            var result = ApiMapper.Translate(input);

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ApiMapper_Translate_PubKeyCredParams_Input2()
        {
            var input = new List<PubKeyCredParam>()
            {
                new PubKeyCredParam() { Alg = Fido2NetLib.Objects.COSE.Algorithm.ES256, Type = PublicKeyCredentialType.PublicKey }
            };

            var expected = new[] { Algorithm.ES256 };
            var result = ApiMapper.Translate(input);

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ApiMapper_Translate_PubKeyCredParams_EmptyInput()
        {
            var input = new List<PubKeyCredParam>();

            var result = ApiMapper.Translate(input);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ApiMapper_Translate_PubKeyCredParams_NullInput()
        {
            ApiMapper.Translate((IList<PubKeyCredParam>)null);
        }
    }
}
