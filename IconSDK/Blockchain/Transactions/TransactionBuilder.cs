using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace IconSDK.Blockchain
{
    using Crypto;
    using Types;
    using Extensions;

    public abstract class TransactionBuilder<TData>
    {
        public string Version = "0x3";
        public PrivateKey PrivateKey;
        public Address To;
        public BigInteger? Value;
        public BigInteger? StepLimit;
        public BigInteger? Nonce;
        public BigInteger? NID;
        public BigInteger? Timestamp;

        protected abstract string RawDataType { get; }
        protected abstract TData RawData { get; }

        public Transaction Build()
        {
           Address from = Addresser.Create(PrivateKey);
           Hash32 hash = BuildHash(from);
           Signature signature = Signer.Sign(hash, PrivateKey);

           return new Transaction(
               Version, from, To, Value, StepLimit, Nonce, NID, Timestamp, RawDataType, RawData, hash, signature
           );
        }

        public Hash32 BuildHash(Address from)
        {
            Timestamp = Timestamp ?? (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).Ticks / 10;

            var param = new Dictionary<string, object>()
            {
                ["version"] = Version,
                ["from"] = from.ToString(),
                ["to"] = To.ToString(),
                ["stepLimit"] = StepLimit.Value.ToHex0x(),
                ["nid"] = NID.Value.ToHex0x(),
                ["timestamp"] = Timestamp.Value.ToHex0x(),
            };

            if (Value.HasValue)
                param["value"] = Value.Value.ToHex0x();

            if (Nonce.HasValue)
                param["nonce"] = Nonce.Value.ToHex0x();

            var rawDataType = RawDataType;
            if (rawDataType != null)
                param["dataType"] = rawDataType;

            var rawData = RawData;
            if (rawData != null)
            {
                param["data"] = ConvertToString(rawData);
            }

            return Hasher.Digest(param);
        }

        private object ConvertToString(object rawDataValue)
        {
            var stringValue = rawDataValue as string;
            if (stringValue != null)
                return stringValue;

            var bytesValue = rawDataValue as Bytes;
            if (bytesValue != null)
                return bytesValue.ToString();

            if (rawDataValue.GetType() == typeof(BigInteger))
            {
                var bigInteger = (BigInteger)rawDataValue;
                return bigInteger.ToHex0x();
            }

            var dictValue = rawDataValue as IDictionary<string, object>;
            if (dictValue != null)
                return ConvertToString(dictValue);

            string msg = $"Not supported value, type: {rawDataValue.GetType()}, value: {rawDataValue}";
            throw new FormatException(msg);
        }

        private IDictionary<string, object> ConvertToString(IDictionary<string, object> rawDataDict)
        {
            Dictionary<string, object> newDict = new Dictionary<string, object>();
            foreach (var pair in rawDataDict)
            {
                newDict[pair.Key] = ConvertToString(pair.Value);
            }
            return newDict;
        }
    }

    public class TransactionBuilder : TransactionBuilder<object>
    {
        public string DataType { get; set; }
        public object Data { get; set; }

        protected override string RawDataType
        {
            get { return DataType; }
        }

        protected override object RawData
        {
            get { return Data; }
        }
    }
}
