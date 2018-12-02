using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IconSDK.Types
{
    public class Signature : Bytes, IEquatable<Signature>
    {
        public Signature(IEnumerable<byte> bytes)
            : base(bytes)
        {

        }

        public Signature(string base64)
            : base(Convert.FromBase64String(base64))
        {

        }

        public string ToBase64()
        {
            return Convert.ToBase64String(Binary.ToArray());
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool Equals(Signature signature)
        {
            return base.Equals(signature);
        }

        public override bool Equals(object obj)
        {
            if (obj is Signature signature)
                return Equals(signature);

            if (obj is string hex)
                return Equals(hex);

            return false;
        }

        public static implicit operator Signature(string base64)
        {
            return new Signature(base64);
        }

        public static bool operator ==(Signature x, Signature y)
        {
            return (Bytes)x == (Bytes)y;
        }

        public static bool operator !=(Signature x, Signature y)
        {
            return !(x == y);
        }
    }
}
