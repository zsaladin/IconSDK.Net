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

            // case 0: Successfull calls
            // Get balance from EOA or score successfully
            var balance = await GetBalance.Invoke("hx0000000000000000000000000000000000000000");
            Assert.IsInstanceOf<System.Numerics.BigInteger>(balance);
            // No prefix
            var balance2 = await GetBalance.Invoke("0000000000000000000000000000000000000000");
            Assert.True(balance == balance2);

            // case 1: when a param is wrong.
            // Wrong hex format
            Assert.ThrowsAsync<FormatException>(() => GetBalance.Invoke("hxWRONG"));
            // Wrong prefix
            Assert.ThrowsAsync<FormatException>(() => GetBalance.Invoke("0x0000000000000000000000000000000000000000"));
            // Wrong number of bytes
            Assert.ThrowsAsync<Exception>(() => GetBalance.Invoke("hx0000000000000000000"));
        }
    }
}