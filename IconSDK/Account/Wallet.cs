using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;

namespace IconSDK.Account
{
    using Crypto;
    using Types;
    using RPCs;
    using Blockchain;

    public class Wallet
    {
        public readonly PrivateKey PrivateKey;
        public readonly ExternalAddress Address;

        public string ApiUrl { get; set; } = Consts.ApiUrl.TestNet;

        private Wallet(PrivateKey privateKey, ExternalAddress address)
        {
            PrivateKey = privateKey;
            Address = address;
        }

        public static Wallet Create()
        {
            var privateKey = PrivateKey.Random();
            return Create(privateKey);
        }

        public static Wallet Create(PrivateKey privateKey)
        {
            return new Wallet(privateKey, Addresser.Create(privateKey));
        }

        public static Wallet Create(KeyStore keyStore)
        {
            return new Wallet(keyStore.PrivateKey, keyStore.Address);
        }

        public static Wallet Load(string password, string keyStoreFilePath)
        {
            return Create(KeyStore.Load(password, keyStoreFilePath));
        }

        public string Store(string password, string keyStoreFilePath = null)
        {
            return KeyStore.Create(PrivateKey, Address).Store(password, keyStoreFilePath);
        }

        public async Task<BigInteger> GetBalance()
        {
            var getBalance = new GetBalance(ApiUrl);
            return await getBalance.Invoke(Address);
        }

        public async Task<Hash32> Transfer(Address to, BigInteger amount, BigInteger stepLimit, int? networkID = null)
        {
            var builder = new TransferTransactionBuilder();
            builder.PrivateKey = PrivateKey;
            builder.To = to;
            builder.Value = amount;
            builder.StepLimit = stepLimit;
            builder.NID = networkID ?? Consts.ApiUrl.GetNetworkID(ApiUrl);

            var tx = builder.Build();
            var sendTransaction = new SendTransaction(ApiUrl);
            return await sendTransaction.Invoke(tx);
        }
    }
}
