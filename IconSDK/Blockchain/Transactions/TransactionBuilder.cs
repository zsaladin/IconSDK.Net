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

    public class TransactionBuilder
    {
        public string Version = "0x3";
        public PrivateKey PrivateKey;
        public Address To;
        public BigInteger? Value;
        public BigInteger? StepLimit;
        public BigInteger? Nonce;
        public BigInteger? NID;
        public BigInteger? Timestamp;

        public string DataType;
        public object Data;

        public Transaction Build()
        {
           Address from = Addresser.Create(PrivateKey);
           Hash32 hash = BuildHash(from);
           Signature signature = Signer.Sign(hash, PrivateKey);

           return new Transaction(
               Version, from, To, Value, StepLimit, Nonce, NID, Timestamp, DataType, Data, hash, signature
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

            if (DataType != null && Data != null)
            {
                var message = Data as string;
                if (message != null)
                    param["data"] = new Bytes(Encoding.UTF8.GetBytes(message)).ToHex();
                else
                    param["data"] = Data;
                param["dataType"] = DataType;
            }

            return Hasher.Digest(param);
        }
    }
}
