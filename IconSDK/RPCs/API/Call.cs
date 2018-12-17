using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Linq;
using Newtonsoft.Json;

namespace IconSDK.RPCs
{
    using Types;
    public class CallRequestMessage : CallRequestMessage<IDictionary<string, object>>
    {
        public CallRequestMessage(Address from, Address to, string method, IDictionary<string, object> param)
        : base(from, to, method, param)
        {

        }
    }

    public class Call<TResponseParam> : RPC<CallRequestMessage, CallResponseMessage<TResponseParam>>
    {
        public Call(string url) : base(url)
        {

        }

        public async Task<TResponseParam> Invoke(Address from, Address to, string method, IDictionary<string, object> param)
        {
            var request = new CallRequestMessage(from, to, method, param);
            var response = await Invoke(request);
            return response.Result;
        }

        public async Task<TResponseParam> Invoke(Address from, Address to, string method, params ValueTuple<string, object>[] param)
        {
            return await Invoke(from, to, method, param.ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2));
        }

        public static new Func<Address, Address, string, IDictionary<string, object>, Task<TResponseParam>> Create(string url)
        {
            return new Call<TResponseParam>(url).Invoke;
        }
    }

    public class Call : Call<string>
    {
        public Call(string url) : base(url)
        {

        }

        public static new Func<Address, Address, string, IDictionary<string, object>, Task<string>> Create(string url)
        {
            return new Call(url).Invoke;
        }
    }
}
