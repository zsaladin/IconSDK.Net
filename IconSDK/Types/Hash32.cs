using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace IconSDK.Types
{
    public class Hash32 : Bytes, IEquatable<Hash32>
    {
        public Hash32(IEnumerable<byte> bytes)
            : base(bytes)
        {

        }

        public Hash32(string hex)
            : base(hex)
        {

        }

        public Hash32(BigInteger value)
            : base(value)
        {

        }

        public string ToHex0x()
        {
            return Prefix + ToHex();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool Equals(Hash32 hash32)
        {
            return base.Equals(hash32);
        }

        public override bool Equals(object obj)
        {
            if (obj is Hash32 hash32)
                return Equals(hash32);

            if (obj is string hex)
                return Equals(hex);

            return false;
        }

        public static implicit operator Hash32(string hex)
        {
            return new Hash32(hex);
        }

        public static bool operator ==(Hash32 x, Hash32 y)
        {
            return (Bytes)x == (Bytes)y;
        }

        public static bool operator !=(Hash32 x, Hash32 y)
        {
            return !(x == y);
        }
    }
}
