using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Numerics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IconSDK.Blockchain
{
    using Types;

    public class Transaction
    {
        public readonly string Version;
        public readonly Address From;
        public readonly Address To;
        public readonly BigInteger? Value;
        public readonly BigInteger? StepLimit;
        public readonly BigInteger? Nonce;
        public readonly BigInteger? NID;
        public readonly BigInteger? Timestamp;
        public readonly string DataType;
        public readonly object Data;

        public readonly Hash32 Hash;
        public readonly Signature Signature;

        public Transaction(
            string version,
            Address from,
            Address to,
            BigInteger? value,
            BigInteger? stepLimit,
            BigInteger? nonce,
            BigInteger? nid,
            BigInteger? timestamp,
            string dataType,
            object data,
            Hash32 hash,
            Signature signature)
        {
            Version = version;
            From = from;
            To = to;
            Value = value;
            StepLimit = stepLimit;
            Nonce = nonce;
            NID = nid;
            Timestamp = timestamp;
            DataType = dataType;
            Data = data;

            Hash = hash;
            Signature = signature;
        }
    }
}
