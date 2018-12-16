using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;
using System.Numerics;
using NUnit.Framework;
using Newtonsoft.Json;

namespace IconSDK.Tests
{
    using RPCs;
    using Blockchain;
    using Types;
    using Extensions;
    using Crypto;

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

            var getTransactionByHash = new GetTransactionByHash(Consts.ApiUrl.TestNet);
            var result = await getTransactionByHash.Invoke("0x1dd7f6ec8f6de0b454c827d7f845fcc8056dd32f0ed1fecb37be860148081263");

            Assert.AreEqual(result.Transaction.Version, "0x3");
            Assert.AreEqual(result.Transaction.From, "hx889fd3476171ccaf650bdba0778ddafe7a5efc3e");
            Assert.AreEqual(result.Transaction.To, "hx1589eda4474e6fb213ee0cd68e7768b3ded5bb34");
            Assert.AreEqual(result.Transaction.StepLimit.Value.ToHex0x(), "0x186a0");
            Assert.AreEqual(result.Transaction.Timestamp.Value.ToHex0x(), "0x57bde4189e6fa");
            Assert.AreEqual(result.Transaction.NID.Value.ToHex0x(), "0x2");
            Assert.AreEqual(result.Transaction.Value.Value.ToHex0x(), "0x1a5a3afa64f38000");
            Assert.AreEqual(result.Transaction.Nonce.Value.ToHex0x(), "0x2");
            Assert.AreEqual(result.Transaction.Data, null);
            Assert.AreEqual(result.Transaction.DataType, null);
            Assert.AreEqual(result.Transaction.Signature, "rSzwJxRMl0wRTPq98RMKFFaGpJLmb03fcKSEjqozwhV8upGrK31iguB6e9HvnOXnbaTqAnB/ofmvebclxKaa2gA=");
            Assert.AreEqual(result.Transaction.Hash, "0x1dd7f6ec8f6de0b454c827d7f845fcc8056dd32f0ed1fecb37be860148081263");
            Assert.AreEqual(result.TxIndex.ToHex0x(), "0x0");
            Assert.AreEqual(result.BlockHash, "0x2213ebbdd4e6ec569ca002f2da1c228f4a6e3e1c2d346c4890bc8c0da454dee3");
            Assert.AreEqual(result.BlockHeight.ToHex0x(), "0xa056");
        }

        [Test]
        public async Task Test_GetTransactionByHashVersion3Tx2()
        {
            // Link : https://trackerdev.icon.foundation/transaction/0x5aff7a465532e0262c9ca7d8ea783855675ad5ed1a4aa2e1b40c96291c84dd9c

            var getTransactionByHash = new GetTransactionByHash(Consts.ApiUrl.TestNet);
            var result = await getTransactionByHash.Invoke("0x5aff7a465532e0262c9ca7d8ea783855675ad5ed1a4aa2e1b40c96291c84dd9c");

            Assert.AreEqual(result.Transaction.Version, "0x3");
            Assert.AreEqual(result.Transaction.From, "hxcc345473807f9fa3c4d147433708e85bb106885b");
            Assert.AreEqual(result.Transaction.To, "cx0dd9315a1d34f547fca030381c1cd3a803dba22c");
            Assert.AreEqual(result.Transaction.StepLimit.Value.ToHex0x(), "0x87000000");
            Assert.AreEqual(result.Transaction.Timestamp.Value.ToHex0x(), "0x57c65b3f9adb8");
            Assert.AreEqual(result.Transaction.NID.Value.ToHex0x(), "0x2");
            Assert.AreEqual(result.Transaction.Value, null);
            Assert.AreEqual(result.Transaction.Nonce, null);
            Assert.AreEqual(result.Transaction.DataType, "call");
            Assert.AreEqual(result.Transaction.Hash, "0x5aff7a465532e0262c9ca7d8ea783855675ad5ed1a4aa2e1b40c96291c84dd9c");
            Assert.AreEqual(result.Transaction.Signature, "v5xCYupHC0fXWV/FWV9TaDT0ykqbFYH9olRxoO/rZN574N97niadjmQIwSpKsufjqhTeYDhCRy0CbSZ22cQA4gA=");
            Assert.AreEqual(result.TxIndex.ToHex0x(), "0x0");
            Assert.AreEqual(result.BlockHash, "0xbcff5484e5faf559401bdcc2d4a2f843e6659a69e22cea23074d75c6119f146e");
            Assert.AreEqual(result.BlockHeight.ToHex0x(), "0xa190");
            Assert.AreEqual(result.Transaction.Data, new Dictionary<string, object>()
            {
                ["method"] = "dispossess_broker_tokens",
                ["params"] = new Dictionary<string, object>()
                {
                    ["amount"] = "0xf6b75ab2bc2bfc0"
                }
            });
        }

        [Test]
        public async Task Test_GetLastBlock()
        {
            var getLastBlock = GetLastBlock.Create(Consts.ApiUrl.TestNet);
            var lastBlock = await getLastBlock();

            var getBlockByHeight = GetBlockByHeight.Create(Consts.ApiUrl.TestNet);
            var blockByHeight = await getBlockByHeight(lastBlock.Height.Value);

            Assert.AreEqual(lastBlock.Height, blockByHeight.Height);
            Assert.AreEqual(lastBlock.Hash, blockByHeight.Hash);
            Assert.AreEqual(lastBlock.Signature, blockByHeight.Signature);

            var getBlockByHash = GetBlockByHash.Create(Consts.ApiUrl.TestNet);
            var blockByHash = await getBlockByHash(lastBlock.Hash);

            Assert.AreEqual(lastBlock.Height, blockByHash.Height);
            Assert.AreEqual(lastBlock.Hash, blockByHash.Hash);
            Assert.AreEqual(lastBlock.Signature, blockByHash.Signature);
        }

        [Test]
        public async Task Test_GetTotalSupply()
        {
            var getTotalSupply = new GetTotalSupply(Consts.ApiUrl.TestNet);
            var totalSupply = await getTotalSupply.Invoke();

            Assert.AreEqual(totalSupply.ToHex0x(), "0x2961fff8ca4a62327800000");
        }

        [Test]
        public async Task Test_GetScoreApi()
        {
            var getScoreApi = new GetScoreApi(Consts.ApiUrl.TestNet);
            var scoreApi = await getScoreApi.Invoke("cx0000000000000000000000000000000000000001");

            Assert.Greater(scoreApi.Length, 0);
        }

        [Test]
        public async Task Test_GetTransactionResult()
        {
            // Link : https://trackerdev.icon.foundation/transaction/0x380066add28d677954ac67596041ae6babdb405399fb37e070459fb2b92f60ce
            var getTransactionResult = GetTransactionResult.Create(Consts.ApiUrl.TestNet);
            var transactionResult = await getTransactionResult("0x380066add28d677954ac67596041ae6babdb405399fb37e070459fb2b92f60ce");

            Assert.AreEqual(transactionResult.BlockHeight.ToHex0x(), "0x9104");
            Assert.AreEqual(transactionResult.BlockHash.ToHex0x(), "0x96dd9702e443d7fd5f2cdb42dc9ac3398ca445e04085b2f7a8270b9f275e0bdd");
            Assert.AreEqual(transactionResult.TxHash, "0x380066add28d677954ac67596041ae6babdb405399fb37e070459fb2b92f60ce");
            Assert.AreEqual(transactionResult.TxIndex.ToHex0x(), "0x0");
            Assert.AreEqual(transactionResult.LogsBloom, "0x00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000008000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000400000000000040000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000");
            Assert.AreEqual(transactionResult.StepPrice.ToHex0x(), "0x2540be400");
            Assert.AreEqual(transactionResult.StepUsed.ToHex0x(), "0x21cb4");
            Assert.AreEqual(transactionResult.CumulativeStepUsed.ToHex0x(), "0x21cb4");
            Assert.AreEqual(transactionResult.To.ToString(), "cx0000000000000000000000000000000000000001");
            Assert.AreEqual(transactionResult.Status.ToHex0x(), "0x1");
            Assert.AreEqual(transactionResult.EventLogs[0].ScoreAddress, "cx0000000000000000000000000000000000000001");
            Assert.AreEqual(transactionResult.EventLogs[0].Indexed[0], "AddImportWhiteListLog(str,int)");
            Assert.AreEqual(transactionResult.EventLogs[0].Data[0], "[('struct', ['pack', 'unpack'])]");
            Assert.AreEqual(transactionResult.EventLogs[0].Data[1], "0x1");
        }

        [Test]
        public async Task Test_Call()
        {
            var privateKey = PrivateKey.Random();
            var address = Addresser.Create(privateKey);

            var call = new Call<bool>(Consts.ApiUrl.TestNet);
            var result = await call.Invoke(
                address,
                "cx0000000000000000000000000000000000000001",
                "isDeployer",
                ("address", address)
             );

            Console.WriteLine(result);

            var call1 = new Call<IsDeployerRequestParam, bool>(Consts.ApiUrl.TestNet);
            var result1 = await call1.Invoke(
                address,
                "cx0000000000000000000000000000000000000001",
                "isDeployer",
                new IsDeployerRequestParam() { Address = address }
             );

            Console.WriteLine(result1);

            var call2 = new Call<BigInteger>(Consts.ApiUrl.TestNet);
            var result2 = await call2.Invoke(
                address,
                "cx0000000000000000000000000000000000000001",
                "getStepPrice"
            );

            Console.WriteLine(result2);

            var call3 = new Call<GetRevisionResponseParam>(Consts.ApiUrl.TestNet);
            var result3 = await call3.Invoke(
                address,
                "cx0000000000000000000000000000000000000001",
                "getRevision"
            );

            Console.WriteLine(result3.Code);
            Console.WriteLine(result3.Name);

            var call4 = new Call<Dictionary<string, BigInteger>>(Consts.ApiUrl.TestNet);
            var result4 = await call4.Invoke(
                address,
                "cx0000000000000000000000000000000000000001",
                "getStepCosts"
            );

            Console.WriteLine(JsonConvert.SerializeObject(result4));
        }

        [Test]
        public void Test_RPCMethodNotFoundException()
        {
            var getBalance = new GetBalance(Consts.ApiUrl.TestNet);

            GetBalanceRequestMessage requestMessage = new GetBalanceRequestMessage("hx0000000000000000000000000000000000000000");
            FieldInfo methodFieldInfo = typeof(GetBalanceRequestMessage).GetField("Method");
            methodFieldInfo.SetValue(requestMessage, "icx_GetBalance");  // 'icx_getBalance' is correct

            Assert.ThrowsAsync(typeof(RPCMethodNotFoundException), async () => await getBalance.Invoke(requestMessage));
        }

        [Test]
        public void Test_RPCInvalidParamsException()
        {
            var getBalance = new GetBalance(Consts.ApiUrl.TestNet);

            GetBalanceRequestMessage requestMessage = new GetBalanceRequestMessage("hx0000000000000000000000000000000000000000");
            FieldInfo addressFieldInfo = requestMessage.Parameters.GetType().GetField("Address");
            addressFieldInfo.SetValue(requestMessage.Parameters, "hxz000000000000000000000000000000000000000");  // 'hx0000000000000000000000000000000000000000' is correct

            Assert.ThrowsAsync(typeof(RPCInvalidParamsException), async () => await getBalance.Invoke(requestMessage));
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

            var gendTransactin = new SendTransaction(Consts.ApiUrl.TestNet);
            Assert.ThrowsAsync(typeof(RPCInvalidRequestException), async () => await gendTransactin.Invoke(tx));
        }

        class IsDeployerRequestParam
        {
            public Address Address;
        }

        class GetRevisionResponseParam
        {
            public BigInteger Code;
            public string Name;
        }
    }
}