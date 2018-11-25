using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Crypto.Digests;

namespace IconSDK.Crypto
{
    using Types;

    public class Signer
    {
        public static Signature Sign(Hash32 hash, PrivateKey privateKey)
        {
            BigInteger pk = new BigInteger(1, privateKey.Binary.ToArray());

            X9ECParameters ec = SecNamedCurves.GetByName("secp256k1");
            ECDomainParameters domainParams = new ECDomainParameters(ec.Curve, ec.G, ec.N, ec.H);
            ECPrivateKeyParameters param = new ECPrivateKeyParameters(pk, domainParams);
            ECDsaSigner signer = new ECDsaSigner(new HMacDsaKCalculator(new Sha256Digest()));
            signer.Init(true, param);

            var hashArray = hash.Binary.ToArray();
            BigInteger[] sig = signer.GenerateSignature(hashArray);

            BigInteger r = sig[0];
            BigInteger s = sig[1];
            if (s.CompareTo(domainParams.N.ShiftRight(1)) > 0) {
                s = domainParams.N.Subtract(s);
            }

            ECPoint q = domainParams.G.Multiply(pk);
            var publicParams = new ECPublicKeyParameters(q, domainParams);
            byte[] publicBytes = publicParams.Q.GetEncoded();

            int recid = -1;
            for (int rec = 0; rec < 4; rec++)
            {
                try
                {
                    ECPoint Q = Recover(sig, hashArray, rec, true);
                    if (Enumerable.SequenceEqual(publicBytes, Q.GetEncoded()))
                    {
                        recid = rec;
                        break;
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
            if (recid < 0)
                throw new Exception("Did not find proper recid");

            byte[] fullSigBytes = new byte[65];
            Buffer.BlockCopy(sig[0].ToByteArrayUnsigned(), 0, fullSigBytes, 0, 32);
            Buffer.BlockCopy(sig[1].ToByteArrayUnsigned(), 0, fullSigBytes, 32, 32);
            fullSigBytes[64] = (byte)recid;

            return new Signature(fullSigBytes);
        }

        public static bool Verify(Signature signature, Hash32 hash, Address address)
        {
            BigInteger[] sig = new BigInteger[]
            {
                new BigInteger(1, signature.Binary.Take(32).ToArray()),
                new BigInteger(1, signature.Binary.Skip(32).Take(32).ToArray())
            };
            try
            {
                ECPoint q = Recover(sig, hash.Binary.ToArray(), (int)signature.Binary.Last(), true);
                return Addresser.Create(new PublicKey(q.GetEncoded())) == address;
            }
            catch
            {
                return false;
            }
        }

        private static ECPoint Recover(BigInteger[] sig, byte[] hash, int recid, bool check)
        {
            X9ECParameters ecParams = Org.BouncyCastle.Asn1.Sec.SecNamedCurves.GetByName("secp256k1");
            int i = recid / 2;

            BigInteger order = ecParams.N;
            BigInteger field = (ecParams.Curve as FpCurve).Q;
            BigInteger x = order.Multiply(new BigInteger(i.ToString())).Add(sig[0]);
            if (x.CompareTo(field) >= 0)
                throw new Exception("X too large");

            byte[] compressedPoint = new Byte[x.ToByteArrayUnsigned().Length + 1];
            compressedPoint[0] = (byte)(0x02 + (recid % 2));
            Buffer.BlockCopy(x.ToByteArrayUnsigned(), 0, compressedPoint, 1, compressedPoint.Length - 1);
            ECPoint R = ecParams.Curve.DecodePoint(compressedPoint);

            if (check)
            {
                ECPoint O = R.Multiply(order);
                if (!O.IsInfinity) throw new Exception("Check failed");
            }

            int n = (ecParams.Curve as FpCurve).Q.ToByteArrayUnsigned().Length * 8;
            BigInteger e = new BigInteger(1, hash);
            if (8 * hash.Length > n)
            {
                e = e.ShiftRight(8 - (n & 7));
            }
            e = BigInteger.Zero.Subtract(e).Mod(order);
            BigInteger rr = sig[0].ModInverse(order);
            BigInteger sor = sig[1].Multiply(rr).Mod(order);
            BigInteger eor = e.Multiply(rr).Mod(order);
            ECPoint Q = ecParams.G.Multiply(eor).Add(R.Multiply(sor));

            return Q;
        }
    }
}
