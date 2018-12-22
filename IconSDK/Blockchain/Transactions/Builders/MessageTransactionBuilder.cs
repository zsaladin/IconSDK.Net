using System.Text;

namespace IconSDK.Blockchain
{
    using Types;

    public class MessageTransactionBuilder : TransactionBuilder<Bytes>
    {
        public string Message;

        protected override string RawDataType
        {
            get { return "message"; }
        }

        protected override Bytes RawData
        {
            get
            {
                return new Bytes(Encoding.UTF8.GetBytes(Message));
            }
        }
    }
}