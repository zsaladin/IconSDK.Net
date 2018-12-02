using System;
using System.Threading.Tasks;
using System.Linq;
using NUnit.Framework;

namespace IconSDK.Tests
{
    using Types;

    public class TestTypes
    {
        [Test]
        public void Test_AddressCasting()
        {
            Address address = "hx0000000000000000000000000000000000000000";
            Assert.AreEqual(address.GetType(), typeof(ExternalAddress));
            Assert.AreEqual(address.ToString(), "hx0000000000000000000000000000000000000000");

            ExternalAddress externalAddress = "hx0000000000000000000000000000000000000000";
            Assert.AreEqual(externalAddress.ToHexhx(), address.ToString());

            address = "cx0000000000000000000000000000000000000000";
            Assert.AreEqual(address.GetType(), typeof(ContractAddress));
            Assert.AreEqual(address.ToString(), "cx0000000000000000000000000000000000000000");

            ContractAddress contractAddress = "cx0000000000000000000000000000000000000000";
            Assert.AreEqual(contractAddress.ToHexcx(), address.ToString());

            Assert.Throws(typeof(FormatException), () =>
            {
                Address addr = "0000000000000000000000000000000000000000";
            });

            Assert.Throws(typeof(FormatException), () =>
            {
                ExternalAddress addr = "0000000000000000000000000000000000000000";
            });

            Assert.Throws(typeof(FormatException), () =>
            {
                ContractAddress addr = "0000000000000000000000000000000000000000";
            });
        }

        [Test]
        public void Test_AddressEquality()
        {
            Address address = "hx0000000000000000000000000000000000000000";
            Bytes bytes = "0000000000000000000000000000000000000000";

            Assert.IsTrue(Enumerable.SequenceEqual(address.Binary, bytes.Binary));
            Assert.IsTrue(address != bytes);
            Assert.IsFalse(address == bytes);
            Assert.AreNotEqual(address, bytes);

            Address addressCloned = "hx0000000000000000000000000000000000000000";
            Assert.IsTrue(address == addressCloned);
            Assert.AreEqual(address, addressCloned);

            string addressHex = "hx0000000000000000000000000000000000000000";
            Assert.IsTrue(address == addressHex);
            Assert.AreEqual(address, addressHex);

            addressHex = "cx0000000000000000000000000000000000000000";
            Assert.IsFalse(address == addressHex);
            Assert.AreNotEqual(address, addressHex);

            addressHex = "0000000000000000000000000000000000000000";
            Assert.Throws(typeof(FormatException), () =>
            {
                bool result = address == addressHex;
            });
        }
    }
}