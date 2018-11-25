using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace IconSDK.Transaction
{
    using Crypto;
    using Types;

    public class TransactionBuilder
    {
        public string Version;
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
                ["version"] = Version ?? "0x3",
                ["from"] = from.ToString(),
                ["to"] = To.ToString(),
                ["stepLimit"] = $"0x{StepLimit.Value.ToString("x")}",
                ["nid"] = $"0x{NID.Value.ToString("x")}",
                ["timestamp"] = $"0x{Timestamp.Value.ToString("x")}",
            };

            if (Value.HasValue)
                param["value"] = $"0x{Value.Value.ToString("x")}";

            if (Nonce.HasValue)
                param["nonce"] = $"0x{Nonce.Value.ToString("x")}";

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
