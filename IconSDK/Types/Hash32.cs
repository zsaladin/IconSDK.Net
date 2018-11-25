using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace IconSDK.Types
{
    public class Hash32 : Bytes
    {
        public override uint Size => 32;
        public override string Prefix => "0x";

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

        public static implicit operator Hash32(string hex)
        {
            return new Hash32(hex);
        }
    }
}
