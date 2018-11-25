using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Digests;

namespace IconSDK.Crypto
{
    using Types;

    public static class Hasher
    {
        public static Hash32 Digest(byte[] bytes)
        {
            var sha3 = new Sha3Digest();
            sha3.BlockUpdate(bytes, 0, bytes.Length);

            bytes = new byte[32];
            sha3.DoFinal(bytes, 0);
            return new Hash32(bytes);
        }

        public static Hash32 Digest(string origin)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(origin);
            return Digest(bytes);
        }

        public static Hash32 Digest(IDictionary<string, object> source)
        {
            var origin = GenerateOrigin(source);
            return Digest(origin);
        }

        public static string GenerateOrigin(IDictionary<string, object> source)
        {
            return "icx_sendTransaction." + string.Join(".", _Encode(source));
        }

        private static string Encode(object source)
        {
            var dict = source as IDictionary<string, object>;
            if (dict != null)
                return Encode(dict);

            var array = source as ICollection<object>;
            if (array != null)
                return Encode(array);

            return Escape((string)source);
        }

        private static string Encode(IDictionary<string, object> dict)
        {
            var result = string.Join(".", _Encode(dict));
            return "{" + result + "}";
        }

        private static IEnumerable<string> _Encode(IDictionary<string, object> dict)
        {
            foreach (var pair in new SortedDictionary<string, object>(dict))
            {
                yield return pair.Key;
                yield return Encode(pair.Value);
            }
        }

        private static string Encode(ICollection<object> array)
        {
            var result = string.Join(".", _Encode(array));
            return "[" + result + "]";
        }

        private static IEnumerable<string> _Encode(ICollection<object> array)
        {
            foreach (var item in array)
                yield return Encode(item);
        }

        private static string Escape(string source)
        {
            if (source == null)
                return @"\0";

            return source
                .Replace(@"\", @"\\")
                .Replace(@"{", @"\{")
                .Replace(@"}", @"\}")
                .Replace(@"[", @"\[")
                .Replace(@"]", @"\]")
                .Replace(@".", @"\.");
        }
    }
}
