using System;
using System.Threading.Tasks;
using System.Globalization;
using System.Numerics;
using Newtonsoft.Json;

namespace IconSDK.RPC
{
    using Types;
    public class GetBalanceRequestMessage : RPCRequestMessage<GetBalanceRequestMessage.Parameter>
    {
        public class Parameter
        {
            [JsonProperty(PropertyName="address")]
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
            if (response.IsSuccess)
                return BigInteger.Parse(response.result.Replace("0x", "00"), NumberStyles.HexNumber);

            throw new Exception(response.Error.Message);
        }
    }
}
