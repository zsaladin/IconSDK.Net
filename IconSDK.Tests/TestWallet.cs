using System;
using System.IO;
using NUnit.Framework;

namespace IconSDK.Tests
{
    using Wallet;
    using Types;
    using Crypto;

    public class TestWallet
    {
        [Test]
        public void Test_KeyStore()
        {
            Random random = new Random();

            byte[] passwordBytes = new byte[random.Next() % 256];
            random.NextBytes(passwordBytes);
            string password = Convert.ToBase64String(passwordBytes);

            PrivateKey privateKey = PrivateKey.Random();
            ExternalAddress address = Addresser.Create(privateKey);

            KeyStore keyStore = new KeyStore(privateKey, address);
            string fileName = keyStore.Store(password);
            keyStore = KeyStore.Load(password, fileName);

            File.Delete(fileName);

            Assert.AreEqual(privateKey, keyStore.PrivateKey);
            Assert.AreEqual(address, keyStore.Address);
        }
    }
}