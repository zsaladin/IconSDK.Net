using System;
using System.Threading.Tasks;
using System.Globalization;
using System.Numerics;
using Newtonsoft.Json;

namespace IconSDK.RPC
{
    using Types;
    public class GetTransactionByHashRequestMessage : RPCRequestMessage<GetTransactionByHashRequestMessage.Parameter>
    {
        public class Parameter
        {
            [JsonProperty(PropertyName="txHash")]
            public readonly string TxHash;

            public Parameter(Hash32 hash)
            {
                TxHash = hash.ToHex0x();
            }
        }

        public GetTransactionByHashRequestMessage(Hash32 hash)
        : base("icx_getTransactionByHash", new Parameter(hash))
        {

        }
    }

    public class GetTransactionByHashResponseMessage : RPCResponseMessage<object>
    {

    }

    public class GetTransactionByHash : RPC<GetTransactionByHashRequestMessage, GetTransactionByHashResponseMessage>
    {
        public GetTransactionByHash(string url) : base(url)
        {

        }

        public async Task<bool> Invoke(Hash32 hash)
        {
            var request = new GetTransactionByHashRequestMessage(hash);
            var response = await Invoke(request);
            return response.IsSuccess;
        }
    }
}
