using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace IconSDK.Types
{
    public class PublicKey : Bytes, IEquatable<PublicKey>
    {
        public PublicKey(IEnumerable<byte> bytes)
            : base(bytes)
        {

        }

        public PublicKey(string hex)
           : base(hex)
        {

        }

        public PublicKey(BigInteger value)
            : base(value)
        {

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool Equals(PublicKey publicKey)
        {
            return base.Equals(publicKey);
        }

        public override bool Equals(object obj)
        {
            if (obj is PublicKey publicKey)
                return Equals(publicKey);

            if (obj is string hex)
                return Equals(hex);

            return false;
        }


        public static implicit operator PublicKey(string hex)
        {
            return new PublicKey(hex);
        }

        public static bool operator ==(PublicKey x, PublicKey y)
        {
            return (Bytes)x == (Bytes)y;
        }

        public static bool operator !=(PublicKey x, PublicKey y)
        {
            return !(x == y);
        }
    }
}
