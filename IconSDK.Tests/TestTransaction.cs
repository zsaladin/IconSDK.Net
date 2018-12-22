using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;
using NUnit.Framework;

namespace IconSDK.Tests
{
    using Types;
    using Crypto;
    using Blockchain;
    using RPCs;

    public class TestTransaction
    {
        [Test]
        public void TestTransferTransactionBuilder()
        {
            var builder = new TransferTransactionBuilder();
            builder.NID = 2;
            builder.PrivateKey = PrivateKey.Random();
            builder.To = "hx54f7853dc6481b670caf69c5a27c7c8fe5be8269";
            builder.StepLimit = BigInteger.Pow(10, 17);
            builder.Value = 1 * Consts.Loop2ICX;
            builder.Timestamp = 100000000000;

            var tx = builder.Build();

            Assert.Null(tx.Data);
            Assert.Null(tx.DataType);
            Assert.True(Signer.Verify(tx.Signature, tx.Hash, tx.From));

            var hashSource = new Dictionary<string, object>()
            {
                ["version"] = "0x3",
                ["nid"] = "0x2",
                ["from"] = Addresser.Create(builder.PrivateKey).ToString(),
                ["to"] = "hx54f7853dc6481b670caf69c5a27c7c8fe5be8269",
                ["stepLimit"] = "0x16345785d8a0000",
                ["value"] = "0xde0b6b3a7640000",
                ["timestamp"] = "0x174876e800"
            };

            var hash = Hasher.Digest(hashSource);
            Assert.AreEqual(tx.Hash, hash);
        }

        [Test]
        public void TestMessageTransactionBuilder()
        {
            var builder = new MessageTransactionBuilder();
            builder.NID = 2;
            builder.PrivateKey = PrivateKey.Random();
            builder.To = "hx54f7853dc6481b670caf69c5a27c7c8fe5be8269";
            builder.StepLimit = BigInteger.Pow(10, 17);
            builder.Value = 1 * Consts.Loop2ICX;
            builder.Timestamp = 100000000000;
            builder.Message = "testMessage";

            var tx = builder.Build();

            Assert.True(Signer.Verify(tx.Signature, tx.Hash, tx.From));

            var hashSource = new Dictionary<string, object>()
            {
                ["version"] = "0x3",
                ["nid"] = "0x2",
                ["from"] = Addresser.Create(builder.PrivateKey).ToString(),
                ["to"] = "hx54f7853dc6481b670caf69c5a27c7c8fe5be8269",
                ["stepLimit"] = "0x16345785d8a0000",
                ["value"] = "0xde0b6b3a7640000",
                ["timestamp"] = "0x174876e800",
                ["dataType"] = "message",
                ["data"] = new Bytes(Encoding.UTF8.GetBytes(builder.Message)).ToString()
            };

            var hash = Hasher.Digest(hashSource);
            Assert.AreEqual(tx.Hash, hash);
        }

        [Test]
        public void TestCallTransactionBuilder()
        {
            var builder = new CallTransactionBuilder();
            builder.NID = 2;
            builder.PrivateKey = PrivateKey.Random();
            builder.To = "cx54f7853dc6481b670caf69c5a27c7c8fe5be8269";
            builder.StepLimit = BigInteger.Pow(10, 17);
            builder.Value = 1 * Consts.Loop2ICX;
            builder.Timestamp = 100000000000;
            builder.Method = "transfer";
            builder.Params["to"] = new ExternalAddress("hx54f7853dc6481b670caf69c5a27c7c8fe5be8269");
            builder.Params["value"] = new BigInteger(10);

            var tx = builder.Build();

            Assert.True(Signer.Verify(tx.Signature, tx.Hash, tx.From));

            var hashSource = new Dictionary<string, object>()
            {
                ["version"] = "0x3",
                ["nid"] = "0x2",
                ["from"] = Addresser.Create(builder.PrivateKey).ToString(),
                ["to"] = "cx54f7853dc6481b670caf69c5a27c7c8fe5be8269",
                ["stepLimit"] = "0x16345785d8a0000",
                ["value"] = "0xde0b6b3a7640000",
                ["timestamp"] = "0x174876e800",
                ["dataType"] = "call",
                ["data"] = new Dictionary<string, object>()
                {
                    ["method"] = "transfer",
                    ["params"] = new Dictionary<string, object>()
                    {
                        ["to"] = "hx54f7853dc6481b670caf69c5a27c7c8fe5be8269",
                        ["value"] = "0xa"
                    }
                }
            };

            var hash = Hasher.Digest(hashSource);
            Assert.AreEqual(tx.Hash, hash);
        }

        [Test]
        public void TestDelopyTransaction()
        {
            var builder = new DeployTransactionBuilder();
            builder.NID = 2;
            builder.PrivateKey = PrivateKey.Random();
            builder.To = "cx0000000000000000000000000000000000000000";
            builder.StepLimit = BigInteger.Pow(10, 17);
            builder.Timestamp = 100000000000;
            builder.ContentType = "application/zip";
            builder.Content = new Bytes("0x1212121212");
            builder.Params["to"] = new ExternalAddress("hx54f7853dc6481b670caf69c5a27c7c8fe5be8269");
            builder.Params["value"] = new BigInteger(10);

            var tx = builder.Build();

            Assert.True(Signer.Verify(tx.Signature, tx.Hash, tx.From));

            var hashSource = new Dictionary<string, object>()
            {
                ["version"] = "0x3",
                ["nid"] = "0x2",
                ["from"] = Addresser.Create(builder.PrivateKey).ToString(),
                ["to"] = "cx0000000000000000000000000000000000000000",
                ["stepLimit"] = "0x16345785d8a0000",
                ["timestamp"] = "0x174876e800",
                ["dataType"] = "deploy",
                ["data"] = new Dictionary<string, object>()
                {
                    ["contentType"] = "application/zip",
                    ["content"] = "0x1212121212",
                    ["params"] = new Dictionary<string, object>()
                    {
                        ["to"] = "hx54f7853dc6481b670caf69c5a27c7c8fe5be8269",
                        ["value"] = "0xa"
                    }
                }
            };

            var hash = Hasher.Digest(hashSource);
            Assert.AreEqual(tx.Hash, hash);
        }
    }
}