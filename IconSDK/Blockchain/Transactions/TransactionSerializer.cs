using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace IconSDK.Blockchain
{
	using Types;
	using Extensions;

	public class TransactionSerializer
	{
		public Dictionary<string, object> Serialize(Transaction tx)
		{
			var param = new Dictionary<string, object>()
			{
				["version"] = tx.Version,
				["from"] = tx.From.ToString(),
				["to"] = tx.To.ToString(),
				["stepLimit"] = tx.StepLimit.Value.ToHex0x(),
				["timestamp"] = tx.Timestamp.Value.ToHex0x(),
				["nid"] = tx.NID.Value.ToHex0x(),
				["signature"] = tx.Signature.ToBase64(),
			};

			if (tx.Value.HasValue)
				param["value"] = tx.Value.Value.ToHex0x();

			if (tx.Nonce.HasValue)
				param["nonce"] = tx.Nonce.Value.ToHex0x();

			if (tx.Data != null)
			{
				var message = tx.Data as string;
				if (message != null)
					param["data"] = new Bytes(Encoding.UTF8.GetBytes(message)).ToHex();
				else
					param["data"] = tx.Data;
				param["dataType"] = tx.DataType;
			}
			return param;
		}

		public Transaction Deserialize(Dictionary<string, object> txSerialized)
		{
			string version = (string)txSerialized["version"];
			Address from = (string)txSerialized["from"];
			Address to = (string)txSerialized["to"];

			BigInteger stepLimit = ((string)txSerialized["stepLimit"]).ToBigInteger();
			BigInteger nid = ((string)txSerialized["nid"]).ToBigInteger();
			BigInteger timestamp = ((string)txSerialized["timestamp"]).ToBigInteger();

			BigInteger? value = null;
			if (txSerialized.ContainsKey("value"))
				value = ((string)txSerialized["value"]).ToBigInteger();

			BigInteger? nonce = null;
			if (txSerialized.ContainsKey("nonce"))
				nonce = ((string)txSerialized["nonce"]).ToBigInteger();

			string dataType = null;
			if (txSerialized.ContainsKey("dataType"))
				dataType = (string)txSerialized["dataType"];

			object data = null;
			if (txSerialized.ContainsKey("data"))
				data = txSerialized["data"];

			Hash32 hash = (string)txSerialized["txHash"];
			Signature signature = (string)txSerialized["signature"];

			return new Transaction(
				version, from, to, value, stepLimit, nonce, nid, timestamp, dataType, data, hash, signature
			);
		}
	}
}