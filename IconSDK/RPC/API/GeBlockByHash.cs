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

    public class GetBlockByHashRequestMessage : RPCRequestMessage<GetBlockByHashRequestMessage.Parameter>
    {
        public class Parameter
        {
            [JsonProperty]
            public readonly string Hash;

            public Parameter(Hash32 hash)
            {
                Hash = hash.ToString();
            }
        }

        public GetBlockByHashRequestMessage(Hash32 hash)
        : base("icx_getBlockByHash", new Parameter(hash))
        {

        }
    }

    public class GetBlockByHashResponseMessage : RPCResponseMessage<Dictionary<string, object>>
    {

    }

    public class GetBlockByHash : RPC<GetBlockByHashRequestMessage, GetBlockByHashResponseMessage>
    {
        public GetBlockByHash(string url) : base(url)
        {

        }

        public async Task<Block> Invoke(Hash32 hash)
        {
            var request = new GetBlockByHashRequestMessage(hash);
            var response = await Invoke(request);
            var bs = new BlockSerializer();
            return  bs.Deserialize(response.Result);
        }

        public static new Func<Hash32, Task<Block>> Create(string url)
        {
            return new GetBlockByHash(url).Invoke;
        }
    }
}
