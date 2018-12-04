using System;
using System.Threading.Tasks;
using System.Globalization;
using System.Numerics;
using Newtonsoft.Json;

namespace IconSDK.RPCs
{
    using Types;
    using Extensions;

    public class GetBalanceRequestMessage : RPCRequestMessage<GetBalanceRequestMessage.Parameter>
    {
        public class Parameter
        {
            [JsonProperty]
            public readonly string Address;

            public Parameter(Address address)
            {
                Address = address.ToString();
            }
        }

        public GetBalanceRequestMessage(Address address)
        : base("icx_getBalance", new Parameter(address))
        {

        }
    }

    public class GetBalanceResponseMessage : RPCResponseMessage<string>
    {

    }

    public class GetBalance : RPC<GetBalanceRequestMessage, GetBalanceResponseMessage>
    {
        public GetBalance(string url) : base(url)
        {

        }

        public async Task<BigInteger> Invoke(Address address)
        {
            var request = new GetBalanceRequestMessage(address);
            var response = await Invoke(request);
            return response.Result.ToBigInteger();
        }

        public static new Func<Address, Task<BigInteger>> Create(string url)
        {
            return new GetBalance(url).Invoke;
        }
    }
}
