using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IconSDK.Types
{
    public class Signature : Bytes
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

        public static implicit operator Signature(string base64)
        {
            return new Signature(base64);
        }
    }
}
