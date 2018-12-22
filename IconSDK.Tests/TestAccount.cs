using System;
using System.IO;
using System.Threading.Tasks;
using System.Numerics;
using NUnit.Framework;

namespace IconSDK.Tests
{
    using Account;
    using Types;
    using Crypto;

    public class TestWallet
    {
        [Test]
        public async Task Test_Wallet()
        {
            var wallet = Wallet.Create();
            var balance = await wallet.GetBalance();

            Assert.AreEqual(balance, new BigInteger(0));

            Task task = wallet.Transfer("hxffffffffffffffffffffffffffffffffffffffff", 1 * Consts.ICX2Loop, 1000000000);
            Assert.CatchAsync(async () => await task);
        }

        [Test]
        public void Test_KeyStore()
        {
            Random random = new Random();

            byte[] passwordBytes = new byte[random.Next() % 256];
            random.NextBytes(passwordBytes);
            string password = Convert.ToBase64String(passwordBytes);

            PrivateKey privateKey = PrivateKey.Random();
            ExternalAddress address = Addresser.Create(privateKey);

            KeyStore keyStore = KeyStore.Create(privateKey, address);
            string fileName = keyStore.Store(password);
            keyStore = KeyStore.Load(password, fileName);

            File.Delete(fileName);

            Assert.AreEqual(privateKey, keyStore.PrivateKey);
            Assert.AreEqual(address, keyStore.Address);
        }
    }
}