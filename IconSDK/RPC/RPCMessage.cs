using System;
using Newtonsoft.Json;

namespace IconSDK.RPC
{
    public class RPCMessage
    {
        private static Random _random = new Random();

        [JsonProperty(PropertyName="jsonrpc")]
        public readonly string Version = "2.0";

        [JsonProperty(PropertyName="id")]
        public readonly object ID;

        public RPCMessage()
        {
            ID = _random.Next();
        }
    }

    public class RPCRequestMessage : RPCMessage
    {
        [JsonProperty(PropertyName="method")]
        public readonly string Method;

        public RPCRequestMessage(string method) : base()
        {
            Method = method;
        }
    }

    public class RPCRequestMessage<TParameter> : RPCRequestMessage
    {
        [JsonProperty(PropertyName="params")]
        public readonly TParameter Parameters;

        public RPCRequestMessage(string method, TParameter parameters) : base(method)
        {
            Parameters = parameters;
        }
    }

    public class RPCResponseMessage : RPCMessage
    {
        public class RPCError
        {
            [JsonProperty(PropertyName="code")]
            public readonly int Code;

            [JsonProperty(PropertyName="message")]
            public readonly string Message;
        }

        [JsonProperty(PropertyName="error")]
        public readonly RPCError Error;

        public bool IsSuccess
        {
            get { return Error == null; }
        }
    }

    public class RPCResponseMessage<TResult> : RPCResponseMessage
    {
        [JsonProperty(PropertyName="result")]
        public readonly TResult result;
    }
}