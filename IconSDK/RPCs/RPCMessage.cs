using System;
using Newtonsoft.Json;

namespace IconSDK.RPCs
{
    public class RPCMessage
    {
        private static Random _random = new Random();

        [JsonProperty(PropertyName="jsonrpc")]
        public readonly string Version = "2.0";

        [JsonProperty]
        public readonly object ID;

        public RPCMessage()
        {
            ID = _random.Next();
        }
    }

    public class RPCRequestMessage : RPCMessage
    {
        [JsonProperty]
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
            [JsonProperty]
            public readonly int Code;
            [JsonProperty]
            public readonly string Message;
        }

        [JsonProperty]
        public readonly RPCError Error;

        public virtual bool IsSuccess
        {
            get { return Error == null; }
        }
    }

    public class RPCResponseMessage<TResult> : RPCResponseMessage
    {
        [JsonProperty]
        public readonly TResult Result;

        public override bool IsSuccess
        {
            get { return base.IsSuccess && Result != null; }
        }
    }
}