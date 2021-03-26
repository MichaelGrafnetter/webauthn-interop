using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Win32.WebAuthn.FIDO.Tests
{
    [TestClass]
    public class Base64UrlConverterTester
    {
        [TestMethod]
        public void Base64UrlConverter_RFC4648Vectors()
        {
            CollectionAssert.AreEqual(new byte[] { }, Base64UrlConverter.FromBase64UrlString(string.Empty));
            CollectionAssert.AreEqual(new byte[] { 102 }, Base64UrlConverter.FromBase64UrlString("Zg"));
            CollectionAssert.AreEqual(new byte[] { 102, 111 }, Base64UrlConverter.FromBase64UrlString("Zm8"));
            CollectionAssert.AreEqual(new byte[] { 102, 111, 111 }, Base64UrlConverter.FromBase64UrlString("Zm9v"));
            CollectionAssert.AreEqual(new byte[] { 102, 111, 111, 98 }, Base64UrlConverter.FromBase64UrlString("Zm9vYg"));
            CollectionAssert.AreEqual(new byte[] { 102, 111, 111, 98, 97 }, Base64UrlConverter.FromBase64UrlString("Zm9vYmE"));
            CollectionAssert.AreEqual(new byte[] { 102, 111, 111, 98, 97, 114 }, Base64UrlConverter.FromBase64UrlString("Zm9vYmFy"));
        }

        [TestMethod]
        public void Base64UrlConverter_CharReplacement()
        {
            throw new AssertInconclusiveException();
        }
    }
}
