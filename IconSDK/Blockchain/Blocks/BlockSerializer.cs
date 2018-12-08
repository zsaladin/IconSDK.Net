using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Numerics;
using Newtonsoft.Json.Linq;

namespace IconSDK.Blockchain
{
    using Types;
    using Extensions;

    public class BlockSerializer
    {
        public Block Deserialize(Dictionary<string, object> blockSerialized)
        {
            string version = (string)blockSerialized["version"];
            Hash32 hash = "0x" + (string)blockSerialized["block_hash"];
            Hash32 prevHash =  "0x" + (string)blockSerialized["prev_block_hash"];
            Hash32 merkleTreeRootHash = "0x" + (string)blockSerialized["merkle_tree_root_hash"];
            BigInteger height = (long)blockSerialized["height"];
            BigInteger timestamp = (long)blockSerialized["time_stamp"];
            ExternalAddress peerID = (string)blockSerialized["peer_id"];
            Signature signature = (string)blockSerialized["signature"];

            TransactionSerializer ts = new TransactionSerializer();
            ImmutableArray<Transaction> transactions =
            ((JArray)blockSerialized["confirmed_transaction_list"]).Select(txSerialized =>
            {
                return ts.Deserialize(txSerialized.ToObject<Dictionary<string, object>>());
            }).ToImmutableArray();

            return new Block(
                version,
                hash,
                prevHash,
                merkleTreeRootHash,
                height,
                timestamp,
                peerID,
                signature,
                transactions
            );
        }
    }
}