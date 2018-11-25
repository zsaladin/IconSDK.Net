using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;

namespace IconSDK.Crypto
{
    using Types;

    public static class Addresser
    {
        public static ExternalAddress Create(PrivateKey privateKey)
        {
            X9ECParameters ec = SecNamedCurves.GetByName("secp256k1");
            ECDomainParameters domainParams = new ECDomainParameters(ec.Curve, ec.G, ec.N, ec.H);

            BigInteger pk = new BigInteger(1, privateKey.Binary.ToArray());
            ECPoint q = domainParams.G.Multiply(pk);

            var publicParams = new ECPublicKeyParameters(q, domainParams);
            byte[] publicBytes = publicParams.Q.GetEncoded();
            return Create(new PublicKey(publicBytes));
        }

        public static ExternalAddress Create(PublicKey publicKey)
        {
            byte[] bytes = publicKey.Binary.Skip(1).ToArray();

            var sha3 = new Sha3Digest();
            sha3.BlockUpdate(bytes, 0, bytes.Length);

            bytes = new byte[32];
            sha3.DoFinal(bytes, 0);

            return new ExternalAddress(bytes.Skip(12));
        }
    }
}
