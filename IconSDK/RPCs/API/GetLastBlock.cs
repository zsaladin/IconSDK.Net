using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Globalization;
using System.Numerics;
using Newtonsoft.Json;

namespace IconSDK.RPCs
{
    using Types;
    using Extensions;
    using Blockchain;

    public class GetLastBlockRequestMessage : RPCRequestMessage
    {
        public GetLastBlockRequestMessage()
        : base("icx_getLastBlock")
        {

        }
    }

    public class GetLastBlockResponseMessage : RPCResponseMessage<Dictionary<string, object>>
    {

    }

    public class GetLastBlock : RPC<GetLastBlockRequestMessage, GetLastBlockResponseMessage>
    {
        public GetLastBlock(string url) : base(url)
        {

        }

        public async Task<Block> Invoke()
        {
            var request = new GetLastBlockRequestMessage();
            var response = await Invoke(request);
            var bs = new BlockSerializer();
            return  bs.Deserialize(response.Result);
        }

        public static new Func<Task<Block>> Create(string url)
        {
            return new GetLastBlock(url).Invoke;
        }
    }
}
