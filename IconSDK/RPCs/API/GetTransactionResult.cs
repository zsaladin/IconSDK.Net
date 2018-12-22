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

    public class GetTransactionResultResponseMessage : RPCResponseMessage<GetTransactionResultResponseMessage.Receipt>
    {
        public class Receipt
        {
            [JsonProperty]
            public readonly Hash32 BlockHash;
            [JsonProperty]
            public readonly BigInteger BlockHeight;
            [JsonProperty]
            public readonly Hash32 TxHash;
            [JsonProperty]
            public readonly BigInteger TxIndex;
            [JsonProperty]
            public readonly Address To;
            [JsonProperty]
            public readonly BigInteger StepUsed;
            [JsonProperty]
            public readonly BigInteger StepPrice;
            [JsonProperty]
            public readonly BigInteger CumulativeStepUsed;
            [JsonProperty]
            public readonly ContractAddress ScoreAddress;
            [JsonProperty]
            public readonly EventLog[] EventLogs;
            [JsonProperty]
            public readonly string LogsBloom;
            [JsonProperty]
            public readonly bool Status;
            [JsonProperty]
            public readonly Failure Failure;
        }

        public class EventLog
        {
            [JsonProperty]
            public readonly ContractAddress ScoreAddress;
            [JsonProperty]
            public readonly string[] Indexed;
            [JsonProperty]
            public readonly string[] Data;
        }

        public class Failure
        {
            [JsonProperty]
            public readonly BigInteger Code;
            [JsonProperty]
            public readonly string Message;
        }
    }

    public class GetTransactionResult : RPC<GetTransactionResultRequestMessage, GetTransactionResultResponseMessage>
    {
        public GetTransactionResult(string url) : base(url)
        {

        }

        public async Task<GetTransactionResultResponseMessage.Receipt> Invoke(Hash32 hash)
        {
            var request = new GetTransactionResultRequestMessage(hash);
            var response = await Invoke(request);
            return response.Result;
        }

        public static new Func<Hash32, Task<GetTransactionResultResponseMessage.Receipt>> Create(string url)
        {
            return new GetTransactionResult(url).Invoke;
        }
    }
}
