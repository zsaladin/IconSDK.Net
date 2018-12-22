namespace IconSDK.Blockchain
{
    public class TransferTransactionBuilder : TransactionBuilder<object>
    {
        protected override string RawDataType
        {
            get { return null; }
        }

        protected override object RawData
        {
            get { return null; }
        }
    }
}