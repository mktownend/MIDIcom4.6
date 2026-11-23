using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MIDIcom4._6.Tests
{
    [TestClass]
    public class KeyTests
    {
        [TestMethod]
        public void EncryptDecrypt_RoundTripsNonEmptyInput()
        {
            Key.SetUpEncryption();
            var plainText = "user/password";

            var cipher = Key.Encrypt(plainText);
            var output = Key.Decrypt(cipher);

            Assert.AreEqual(plainText, output);
        }

        [TestMethod]
        public void Encrypt_EmptyInput_ReturnsNull()
        {
            Key.SetUpEncryption();

            var cipher = Key.Encrypt(string.Empty);

            Assert.IsNull(cipher);
        }

        [TestMethod]
        public void Decrypt_InvalidInput_ReturnsEmptyString()
        {
            var output = Key.Decrypt("not-base64");

            Assert.AreEqual(string.Empty, output);
        }
    }
}
