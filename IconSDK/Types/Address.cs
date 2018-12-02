using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace IconSDK.Types
{
    public abstract class Address : Bytes
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

        public static implicit operator Address(string hex)
        {
            if (hex.Substring(0, 2) == "cx")
                return new ContractAddress(hex);

            return new ExternalAddress(hex);
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
