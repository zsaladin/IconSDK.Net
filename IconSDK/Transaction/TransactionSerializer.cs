using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace IconSDK.Transaction
{
	using Types;

	public class TransactionSerializer
	{
		public Dictionary<string, object> Serialize(Transaction tx)
		{
			var param = new Dictionary<string, object>()
			{
				["version"] = "0x3",
				["from"] = tx.From.ToString(),
				["to"] = tx.To.ToString(),
				["stepLimit"] = $"0x{tx.StepLimit.Value.ToString("x")}",
				["timestamp"] = $"0x{tx.Timestamp.Value.ToString("x")}",
				["nid"] = $"0x{tx.NID.Value.ToString("x")}",
				["signature"] = tx.Signature.ToBase64(),
			};

			if (tx.Value.HasValue)
				param["value"] = $"0x{tx.Value.Value.ToString("x")}";

			if (tx.Nonce.HasValue)
				param["nonce"] = $"0x{tx.Nonce.Value.ToString("x")}";

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