using System;
using System.Threading.Tasks;
using System.Globalization;
using System.Numerics;
using Newtonsoft.Json;

namespace IconSDK.RPCs
{
    using Types;
    using Extensions;

    public class GetScoreApiRequestMessage : RPCRequestMessage<GetScoreApiRequestMessage.Parameter>
    {
        public class Parameter
        {
            [JsonProperty]
            public readonly string Address;

            public Parameter(ContractAddress address)
            {
                Address = address.ToString();
            }
        }

        public GetScoreApiRequestMessage(ContractAddress address)
        : base("icx_getScoreApi", new Parameter(address))
        {

        }
    }

    public class GetScoreApiResponseMessage : RPCResponseMessage<GetScoreApiResponseMessage.ScoreApi[]>
    {
        public class ScoreApi
        {
            [JsonProperty]
            public readonly string Type;
            [JsonProperty]
            public readonly string Name;
            [JsonProperty]
            public readonly ScoreApiValue[] Inputs;
            [JsonProperty]
            public readonly ScoreApiValue[] Outputs;
            [JsonProperty]
            public readonly bool? Readonly;
        }

        public class ScoreApiValue
        {
            [JsonProperty]
            public readonly string Type;
            [JsonProperty]
            public readonly string Name;
            [JsonProperty]
            public readonly BigInteger? Indexed;
        }
    }

    public class GetScoreApi : RPC<GetScoreApiRequestMessage, GetScoreApiResponseMessage>
    {
        public GetScoreApi(string url) : base(url)
        {

        }

        public async Task<GetScoreApiResponseMessage.ScoreApi[]> Invoke(ContractAddress address)
        {
            var request = new GetScoreApiRequestMessage(address);
            var response = await Invoke(request);
            return response.Result;
        }

        public static new Func<ContractAddress, Task<GetScoreApiResponseMessage.ScoreApi[]>> Create(string url)
        {
            return new GetScoreApi(url).Invoke;
        }
    }
}
