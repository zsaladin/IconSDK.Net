using System.Collections.Generic;

namespace IconSDK.Blockchain
{
    using Types;

    public class DeployTransactionBuilder : TransactionBuilder<IDictionary<string, object>>
    {
        public Bytes Content;
        public string ContentType;

        public Dictionary<string, object> Params = new Dictionary<string, object>();

        protected override string RawDataType
        {
            get { return "deploy"; }
        }

        protected override IDictionary<string, object> RawData
        {
            get
            {
                var data = new Dictionary<string, object>()
                {
                    ["content"] = Content,
                    ["contentType"] = ContentType,
                };

                if (Params.Count > 0)
                    data["params"] = Params;

                return data;
            }
        }
    }
}