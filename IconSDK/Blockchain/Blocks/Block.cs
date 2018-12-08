using System.Numerics;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace IconSDK.Blockchain
{
    using Types;

    public class Block
    {
        public readonly string Version;
        public readonly Hash32 Hash;
        public readonly Hash32 PrevHash;
        public readonly Hash32 MerkleTreeRootHash;
        public readonly BigInteger? Height;
        public readonly BigInteger? Timestamp;
        public readonly ExternalAddress PeerID;
        public readonly Signature Signature;
        public readonly ImmutableArray<Transaction> Transactions;

        public Block(
            string version,
            Hash32 hash,
            Hash32 prevHash,
            Hash32 merkleTreeRootHash,
            BigInteger? height,
            BigInteger? timestamp,
            ExternalAddress peerID,
            Signature signature,
            ImmutableArray<Transaction> transactions
        )
        {
            Version = version;
            Hash = hash;
            PrevHash = prevHash;
            MerkleTreeRootHash = merkleTreeRootHash;
            Height= height;
            Timestamp= timestamp;
            PeerID = peerID;
            Signature = signature;
            Transactions = transactions;
        }
    }
}