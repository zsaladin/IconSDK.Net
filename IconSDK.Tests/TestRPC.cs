using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;
using NUnit.Framework;

namespace IconSDK.Tests
{
    using RPCs;
    using Transaction;
    using Types;
    using Extensions;

    public class TestRPC
    {
        [Test]
        public async Task Test_GetBalance1()
        {
            var getBalance = new GetBalance(Consts.ApiUrl.TestNet);
            var balance = await getBalance.Invoke("hx0000000000000000000000000000000000000000");
            Console.WriteLine($"Treasury : {balance.ToString()}");
        }

        [Test]
        public async Task Test_GetBalance2()
        {
            var getBalance = GetBalance.Create(Consts.ApiUrl.TestNet);
            var balance = await getBalance("hx0000000000000000000000000000000000000000");
            Console.WriteLine($"Treasury : {balance.ToString()}");
        }

        [Test]
        public async Task Test_GetTransactionByHashVersion3Tx1()
        {
            // Link : https://trackerdev.icon.foundation/transaction/0x1dd7f6ec8f6de0b454c827d7f845fcc8056dd32f0ed1fecb37be860148081263

            var GetTransactionByHash = new GetTransactionByHash(Consts.ApiUrl.TestNet);
            var result = await GetTransactionByHash.Invoke("0x1dd7f6ec8f6de0b454c827d7f845fcc8056dd32f0ed1fecb37be860148081263");

            Assert.AreEqual(result.Version, "0x3");
            Assert.AreEqual(result.From, "hx889fd3476171ccaf650bdba0778ddafe7a5efc3e");
            Assert.AreEqual(result.To, "hx1589eda4474e6fb213ee0cd68e7768b3ded5bb34");
            Assert.AreEqual(result.StepLimit.Value.ToHex0x(), "0x186a0");
            Assert.AreEqual(result.Timestamp.Value.ToHex0x(), "0x57bde4189e6fa");
            Assert.AreEqual(result.NID.Value.ToHex0x(), "0x2");
            Assert.AreEqual(result.Value.Value.ToHex0x(), "0x1a5a3afa64f38000");
            Assert.AreEqual(result.Nonce.Value.ToHex0x(), "0x2");
            Assert.AreEqual(result.Data, null);
            Assert.AreEqual(result.DataType, null);
            Assert.AreEqual(result.Signature, "rSzwJxRMl0wRTPq98RMKFFaGpJLmb03fcKSEjqozwhV8upGrK31iguB6e9HvnOXnbaTqAnB/ofmvebclxKaa2gA=");
            Assert.AreEqual(result.TxIndex.Value.ToHex0x(), "0x0");
            Assert.AreEqual(result.TxHash, "0x1dd7f6ec8f6de0b454c827d7f845fcc8056dd32f0ed1fecb37be860148081263");
            Assert.AreEqual(result.BlockHash, "0x2213ebbdd4e6ec569ca002f2da1c228f4a6e3e1c2d346c4890bc8c0da454dee3");
            Assert.AreEqual(result.BlockHeight.Value.ToHex0x(), "0xa056");
        }

        [Test]
        public async Task Test_GetTransactionByHashVersion3Tx2()
        {
            // Link : https://trackerdev.icon.foundation/transaction/0x5aff7a465532e0262c9ca7d8ea783855675ad5ed1a4aa2e1b40c96291c84dd9c

            var GetTransactionByHash = new GetTransactionByHash(Consts.ApiUrl.TestNet);
            var result = await GetTransactionByHash.Invoke("0x5aff7a465532e0262c9ca7d8ea783855675ad5ed1a4aa2e1b40c96291c84dd9c");

            Assert.AreEqual(result.Version, "0x3");
            Assert.AreEqual(result.From, "hxcc345473807f9fa3c4d147433708e85bb106885b");
            Assert.AreEqual(result.To, "cx0dd9315a1d34f547fca030381c1cd3a803dba22c");
            Assert.AreEqual(result.StepLimit.Value.ToHex0x(), "0x87000000");
            Assert.AreEqual(result.Timestamp.Value.ToHex0x(), "0x57c65b3f9adb8");
            Assert.AreEqual(result.NID.Value.ToHex0x(), "0x2");
            Assert.AreEqual(result.Value, null);
            Assert.AreEqual(result.Nonce, null);
            Assert.AreEqual(result.DataType, "call");
            Assert.AreEqual(result.Signature, "v5xCYupHC0fXWV/FWV9TaDT0ykqbFYH9olRxoO/rZN574N97niadjmQIwSpKsufjqhTeYDhCRy0CbSZ22cQA4gA=");
            Assert.AreEqual(result.TxIndex.Value.ToHex0x(), "0x0");
            Assert.AreEqual(result.TxHash, "0x5aff7a465532e0262c9ca7d8ea783855675ad5ed1a4aa2e1b40c96291c84dd9c");
            Assert.AreEqual(result.BlockHash, "0xbcff5484e5faf559401bdcc2d4a2f843e6659a69e22cea23074d75c6119f146e");
            Assert.AreEqual(result.BlockHeight.Value.ToHex0x(), "0xa190");
            Assert.AreEqual(result.Data, new Dictionary<string, object>()
            {
                ["method"] = "dispossess_broker_tokens",
                ["params"] = new Dictionary<string, object>()
                {
                    ["amount"] = "0xf6b75ab2bc2bfc0"
                }
            });
        }

        [Test]
        public void Test_RPCMethodNotFoundException()
        {
            var GetBalance = new GetBalance(Consts.ApiUrl.TestNet);

            GetBalanceRequestMessage requestMessage = new GetBalanceRequestMessage("hx0000000000000000000000000000000000000000");
            FieldInfo methodFieldInfo = typeof(GetBalanceRequestMessage).GetField("Method");
            methodFieldInfo.SetValue(requestMessage, "icx_GetBalance");  // 'icx_getBalance' is correct

            Assert.ThrowsAsync(typeof(RPCMethodNotFoundException), async () => await GetBalance.Invoke(requestMessage));
        }

        [Test]
        public void Test_RPCInvalidParamsException()
        {
            var GetBalance = new GetBalance(Consts.ApiUrl.TestNet);

            GetBalanceRequestMessage requestMessage = new GetBalanceRequestMessage("hx0000000000000000000000000000000000000000");
            FieldInfo addressFieldInfo = requestMessage.Parameters.GetType().GetField("Address");
            addressFieldInfo.SetValue(requestMessage.Parameters, "hxz000000000000000000000000000000000000000");  // 'hx0000000000000000000000000000000000000000' is correct

            Assert.ThrowsAsync(typeof(RPCInvalidParamsException), async () => await GetBalance.Invoke(requestMessage));
        }

        [Test]
        public void Test_RPCInvalidRequestException()
        {
            var txBuilder = new TransactionBuilder();
            txBuilder.PrivateKey = PrivateKey.Random();
            txBuilder.To = "hx0000000000000000000000000000000000000000";
            txBuilder.StepLimit = 100000000;
            txBuilder.NID = 2;
            txBuilder.Value = 0;

            var tx = txBuilder.Build();

            var SendTransactin = new SendTransaction(Consts.ApiUrl.TestNet);
            Assert.ThrowsAsync(typeof(RPCInvalidRequestException), async () => await SendTransactin.Invoke(tx));
        }
    }
}