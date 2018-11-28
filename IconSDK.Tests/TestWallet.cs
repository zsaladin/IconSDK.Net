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
            byte[] fileNameBytes = new byte[random.Next() % 32];
            random.NextBytes(fileNameBytes);
            string fileName = Convert.ToBase64String(fileNameBytes);

            byte[] passwordBytes = new byte[random.Next() % 256];
            random.NextBytes(passwordBytes);
            string password = Convert.ToBase64String(passwordBytes);

            PrivateKey privateKey = PrivateKey.Random();
            ExternalAddress address = Addresser.Create(privateKey);

            KeyStore keyStore = new KeyStore(privateKey, address);
            keyStore.Store(fileName, password);

            keyStore = KeyStore.Load(fileName, password);

            File.Delete(fileName);

            Assert.AreEqual(privateKey, keyStore.PrivateKey);
            Assert.AreEqual(address, keyStore.Address);
        }
    }
}