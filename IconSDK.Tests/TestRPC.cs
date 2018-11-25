using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace IconSDK.Tests
{
    using RPC;
    public class TestRPC
    {
        private const string API_URL = "https://testwallet.icon.foundation/api/v3";

        [Test]
        public async Task Test_GetBalance()
        {
            var GetBalance = new GetBalance(API_URL);
            var balance = await GetBalance.Invoke("hx0000000000000000000000000000000000000000");
            Console.WriteLine($"Treasury : {balance.ToString()}");
        }
    }
}