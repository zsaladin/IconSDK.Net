using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using Newtonsoft.Json;

namespace IconSDK.RPCs
{
    using Types;
    public class CallRequestMessage : RPCRequestMessage<CallRequestMessage.Parameter>
    {
        public class Parameter
        {
            [JsonProperty]
            public readonly string From;
            [JsonProperty]
            public readonly string To;
            [JsonProperty]
            public readonly string DataType;
            [JsonProperty]
            public readonly IDictionary<string, object> Data;

            public Parameter(Address from, Address to, string dataType, IDictionary<string, object> data)
            {
                From = from.ToString();
                To = to.ToString();
                DataType = dataType;
                Data = data;
            }
        }

        public CallRequestMessage(Address from, Address to, string dataType, IDictionary<string, object> data)
        : base("icx_call", new Parameter(from, to, dataType, data))
        {

        }
    }

    public class CallResponseMessage : RPCResponseMessage<string>
    {

    }

    public class Call : RPC<CallRequestMessage, CallResponseMessage>
    {
        public Call(string url) : base(url)
        {

        }

        public async Task<string> Invoke(Address from, Address to, string dataType, IDictionary<string, object> data)
        {
            var request = new CallRequestMessage(from, to, dataType, data);
            var response = await Invoke(request);
            return response.Result;
        }

        public static new Func<Address, Address, string, IDictionary<string, object>, Task<string>> Create(string url)
        {
            return new Call(url).Invoke;
        }
    }
}
