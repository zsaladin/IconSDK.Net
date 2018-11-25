using System.Numerics;

namespace IconSDK.Extensions
{
    public static class BigIntegerExtention
    {
        public static string ToHex(this BigInteger bigInteger)
        {
            return bigInteger.ToString("x");
        }

        public static string ToHex0x(this BigInteger bigInteger)
        {
            return $"0x{bigInteger.ToHex()}";
        }
    }
}