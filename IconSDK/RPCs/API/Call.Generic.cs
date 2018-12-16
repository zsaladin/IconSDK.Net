using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using Newtonsoft.Json;

namespace IconSDK.RPCs
{
    using Types;
    public class CallRequestMessage<TRequestParam>
        : RPCRequestMessage<CallRequestMessage<TRequestParam>.Parameter>
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

            public Parameter(Address from, Address to, string method, TRequestParam param)
            {
                From = from.ToString();
                To = to.ToString();
                DataType = "call";
                Data = new Dictionary<string, object>()
                {
                    ["method"] = method,
                    ["params"] = param
                };
            }
        }

        public CallRequestMessage(Address from, Address to, string method, TRequestParam param)
        : base("icx_call", new Parameter(from, to, method, param))
        {

        }
    }

    public class CallResponseMessage<TResponseParam> : RPCResponseMessage<TResponseParam>
    {

    }

    public class Call<TRequestParam, TResponseParam>
        : RPC<CallRequestMessage<TRequestParam>, CallResponseMessage<TResponseParam>>
    {
        public Call(string url) : base(url)
        {

        }

        public async Task<TResponseParam> Invoke(Address from, Address to, string method, TRequestParam param)
        {
            var request = new CallRequestMessage<TRequestParam>(from, to, method, param);
            var response = await Invoke(request);
            return response.Result;
        }

        public static new Func<Address, Address, string, TRequestParam, Task<TResponseParam>> Create(string url)
        {
            return new Call<TRequestParam, TResponseParam>(url).Invoke;
        }
    }
}
