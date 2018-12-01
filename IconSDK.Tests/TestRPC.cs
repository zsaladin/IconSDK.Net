using System;
using System.Threading.Tasks;
using System.Reflection;
using NUnit.Framework;

namespace IconSDK.Tests
{
    using RPC;
    using Transaction;
    using Types;

    public class TestRPC
    {
        [Test]
        public async Task Test_GetBalance()
        {
            var GetBalance = new GetBalance(Consts.ApiUrl.TestNet);
            var balance = await GetBalance.Invoke("hx0000000000000000000000000000000000000000");
            Console.WriteLine($"Treasury : {balance.ToString()}");
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