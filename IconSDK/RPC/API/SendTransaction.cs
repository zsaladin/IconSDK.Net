using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Globalization;
using System.Numerics;
using Newtonsoft.Json;

namespace IconSDK.RPCs
{
    using Types;
    using Blockchain;

    public class SendTransactionRequestMessage : RPCRequestMessage<IDictionary<string, object>>
    {
        public SendTransactionRequestMessage(IDictionary<string, object> param)
        : base("icx_sendTransaction", param)
        {

        }
    }

    public class SendTransactionResponseMessage : RPCResponseMessage<Hash32>
    {

    }

    public class SendTransaction : RPC<SendTransactionRequestMessage, SendTransactionResponseMessage>
    {
        public SendTransaction(string url) : base(url)
        {

        }

        public async Task<Hash32> Invoke(Transaction tx)
        {
            var ts = new TransactionSerializer();
            var request = new SendTransactionRequestMessage(ts.Serialize(tx));
            var response = await Invoke(request);
            return response.Result;
        }

        public static new Func<Transaction, Task<Hash32>> Create(string url)
        {
            return new SendTransaction(url).Invoke;
        }
    }
}
