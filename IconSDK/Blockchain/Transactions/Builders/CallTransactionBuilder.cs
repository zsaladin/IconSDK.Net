using System.Collections.Generic;

namespace IconSDK.Blockchain
{
    public class CallTransactionBuilder : TransactionBuilder<IDictionary<string, object>>
    {
        public string Method;
        public Dictionary<string, object> Params = new Dictionary<string, object>();

        protected override string RawDataType
        {
            get { return "call"; }
        }

        protected override IDictionary<string, object> RawData
        {
            get
            {
                var data = new Dictionary<string, object>()
                {
                    ["method"] = Method,
                };

                if (Params.Count > 0)
                    data["params"] = Params;

                return data;
            }
        }
    }
}