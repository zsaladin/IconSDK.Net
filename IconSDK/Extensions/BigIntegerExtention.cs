using System;
using System.Numerics;
using System.Globalization;
using Newtonsoft.Json;

namespace IconSDK.Extensions
{
    public static class BigIntegerExtention
    {
        public static string ToHex(this BigInteger bigInteger)
        {
            if (bigInteger > 0)
            {
                var result = bigInteger.ToString("x");
                if (result[0] == '0')
                    result = result.Substring(1);
                return result;
            }

            if (bigInteger < 0)
            {
                bigInteger = -bigInteger;
                return $"-{bigInteger.ToString("x")}";
            }

            return "0";
        }

        public static string ToHex0x(this BigInteger bigInteger)
        {
            if (bigInteger > 0)
            {
                return $"0x{bigInteger.ToHex()}";
            }

            if (bigInteger < 0)
            {
                bigInteger = -bigInteger;
                return $"-0x{bigInteger.ToString("x")}";
            }

            return "0x0";
        }

        public static BigInteger ToBigInteger(this string hex)
        {
            hex = hex.Replace("0x", "00");
            bool isNegative = (hex[0] == '-');
            if (isNegative)
                hex = hex.Replace("-", string.Empty);

            var result = BigInteger.Parse(hex, NumberStyles.AllowHexSpecifier);
            return isNegative ? -result : result;
        }
    }
}