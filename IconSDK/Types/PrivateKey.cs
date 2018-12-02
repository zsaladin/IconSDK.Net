using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Security.Cryptography;

namespace IconSDK.Types
{
    public class PrivateKey : Bytes
    {
        public PrivateKey(IEnumerable<byte> bytes)
            : base(bytes)
        {

        }

        public PrivateKey(string hex)
            : base(hex)
        {

        }

        public PrivateKey(BigInteger value)
            : base(value)
        {

        }

        public static PrivateKey Random()
        {
            byte[] bytes = new byte[32];

            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);

            return new PrivateKey(bytes);
        }

        public static implicit operator PrivateKey(string hex)
        {
            return new PrivateKey(hex);
        }
    }
}
