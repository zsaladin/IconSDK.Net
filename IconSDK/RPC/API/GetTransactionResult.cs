using System;
using System.Threading.Tasks;
using System.Globalization;
using System.Numerics;
using Newtonsoft.Json;

namespace IconSDK.RPCs
{
    using Types;
    public class GetTransactionResultRequestMessage : RPCRequestMessage<GetTransactionResultRequestMessage.Parameter>
    {
        public class Parameter
        {
            [JsonProperty]
            public readonly string TxHash;

            public Parameter(Hash32 hash)
            {
                TxHash = hash.ToHex0x();
            }
        }

        public GetTransactionResultRequestMessage(Hash32 hash)
        : base("icx_getTransactionResult", new Parameter(hash))
        {

        }
    }

    public class GetTransactionResultResponseMessage : RPCResponseMessage<object>
    {

    }

    public class GetTransactionResult : RPC<GetTransactionResultRequestMessage, GetTransactionResultResponseMessage>
    {
        public GetTransactionResult(string url) : base(url)
        {

        }

        public async Task<bool> Invoke(Hash32 hash)
        {
            var request = new GetTransactionResultRequestMessage(hash);
            var response = await Invoke(request);
            return response.IsSuccess;
        }

        public static new Func<Hash32, Task<bool>> Create(string url)
        {
            return new GetTransactionResult(url).Invoke;
        }
    }
}
