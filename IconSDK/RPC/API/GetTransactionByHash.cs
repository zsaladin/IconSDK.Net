using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using Newtonsoft.Json;

namespace IconSDK.RPCs
{
    using Types;
    using Transaction;
    using Extensions;
    
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

    public class GetTransactionByHashResponseMessage : RPCResponseMessage<Dictionary<string, object>>
    {
        public class TransactionInfo
        {
            public readonly Transaction Transaction;
            public readonly BigInteger TxIndex;
            public readonly BigInteger BlockHeight;
            public readonly Hash32 BlockHash;

            public TransactionInfo(Transaction transaction, BigInteger txIndex, BigInteger blockHeight, Hash32 blockHash)
            {
                Transaction = transaction;
                TxIndex = txIndex;
                BlockHeight = blockHeight;
                BlockHash = blockHash;
            }
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
            var tx = new TransactionSerializer().Deserialize(response.Result);
            return new GetTransactionByHashResponseMessage.TransactionInfo(
                tx,
                ((string)response.Result["txIndex"]).ToBigInteger(),
                ((string)response.Result["blockHeight"]).ToBigInteger(),
                (string)response.Result["blockHash"]
            );
        }

        public static new Func<Hash32, Task<GetTransactionByHashResponseMessage.TransactionInfo>> Create(string url)
        {
            return new GetTransactionByHash(url).Invoke;
        }
    }
}
