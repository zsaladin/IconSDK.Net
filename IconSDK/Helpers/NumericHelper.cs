using System;
using System.Linq;
using System.Numerics;

namespace IconSDK.Helpers
{
    public static class NumericsHelper
    {
        public static BigInteger ICX2Loop(string icx)
        {
            if (icx.Count(c => c == '.') >= 2)
                throw new FormatException($"Not a number. {icx}");

            int index = icx.Length - icx.IndexOf(".") - 1;
            if (index == icx.Length)
                index = 0;

            int repeat = 18 - index;
            if (repeat < 0)
                throw new FormatException($"Not supported precision. {icx}");

            icx = icx.Replace(".", string.Empty);
            return BigInteger.Parse(icx + string.Concat(Enumerable.Repeat("0", repeat)));
        }
    }
}