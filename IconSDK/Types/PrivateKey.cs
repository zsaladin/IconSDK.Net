using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Security.Cryptography;

namespace IconSDK.Types
{
    public class PrivateKey : Bytes, IEquatable<PrivateKey>
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

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool Equals(PrivateKey privateKey)
        {
            return base.Equals(privateKey);
        }

        public override bool Equals(object obj)
        {
            if (obj is PrivateKey privateKey)
                return Equals(privateKey);

            if (obj is string hex)
                return Equals(hex);

            return false;
        }

        public static implicit operator PrivateKey(string hex)
        {
            return new PrivateKey(hex);
        }

        public static bool operator ==(PrivateKey x, PrivateKey y)
        {
            return (Bytes)x == (Bytes)y;
        }

        public static bool operator !=(PrivateKey x, PrivateKey y)
        {
            return !(x == y);
        }
    }
}
