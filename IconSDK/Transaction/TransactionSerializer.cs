using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace IconSDK.Transaction
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
	}
}