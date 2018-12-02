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
            [JsonProperty]
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

    public class GetTransactionByHashResponseMessage : RPCResponseMessage<GetTransactionByHashResponseMessage.TransactionInfo>
    {
        public class TransactionInfo
        {
            [JsonProperty]
            public readonly string Version;
            [JsonProperty]
            public readonly string Method;
            [JsonProperty]
            public readonly Address From;
            [JsonProperty]
            public readonly Address To;
            [JsonProperty]
            public readonly BigInteger Value;
            [JsonProperty]
            public readonly BigInteger Fee;
            [JsonProperty]
            public readonly BigInteger StepLimit;
            [JsonProperty]
            public readonly BigInteger Timestamp;
            [JsonProperty]
            public readonly BigInteger NID;
            [JsonProperty]
            public readonly BigInteger Nonce;
            [JsonProperty]
            public readonly Signature Signature;
            [JsonProperty]
            public readonly Hash32 TxHash;
            [JsonProperty]
            public readonly BigInteger TxIndex;
            [JsonProperty]
            public readonly BigInteger BlockHeight;
            [JsonProperty]
            public readonly Hash32 BlockHash;
        }
    }

    public class GetTransactionByHash : RPC<GetTransactionByHashRequestMessage, GetTransactionByHashResponseMessage>
    {
        public GetTransactionByHash(string url) : base(url)
        {

        }

        public async Task<GetTransactionByHashResponseMessage.TransactionInfo> Invoke(Hash32 hash)
        {
            var request = new GetTransactionByHashRequestMessage(hash);
            var response = await Invoke(request);
            return response.Result;
        }
    }
}
