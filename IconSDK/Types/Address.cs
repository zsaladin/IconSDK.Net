using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace IconSDK.Types
{
    public abstract class Address : Bytes, IEquatable<Address>
    {
        public Address(IEnumerable<byte> bytes)
            : base(bytes)
        {

        }

        public Address(string hex)
            : base(hex)
        {

        }

        public Address(BigInteger value)
            : base(value)
        {

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool Equals(Address address)
        {
            return base.Equals(address);
        }

        public override bool Equals(object obj)
        {
            if (obj is Address address)
                return Equals(address);

            if (obj is string hex)
                return Equals(hex);

            return false;
        }

        public static implicit operator Address(string hex)
        {
            if (hex.Substring(0, 2) == "cx")
                return new ContractAddress(hex);

            return new ExternalAddress(hex);
        }

        public static bool operator ==(Address x, Address y)
        {
            return (Bytes)x == (Bytes)y;
        }

        public static bool operator !=(Address x, Address y)
        {
            return !(x == y);
        }
    }

    public class ExternalAddress : Address
    {
        public ExternalAddress(IEnumerable<byte> bytes)
            : base(bytes)
        {

        }

        public ExternalAddress(string hex)
            : base(hex)
        {

        }

        public ExternalAddress(BigInteger value)
            : base(value)
        {

        }

        public string ToHexhx()
        {
            return ToString();
        }

        public static implicit operator ExternalAddress(string hex)
        {
            return new ExternalAddress(hex);
        }
    }

    public class ContractAddress : Address
    {
        public ContractAddress(IEnumerable<byte> bytes)
            : base(bytes)
        {

        }

        public ContractAddress(string hex)
            : base(hex)
        {

        }

        public ContractAddress(BigInteger value)
            : base(value)
        {

        }

        public string ToHexcx()
        {
            return ToString();
        }

        public static implicit operator ContractAddress(string hex)
        {
            return new ContractAddress(hex);
        }
    }
}
