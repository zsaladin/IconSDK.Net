using System;
using System.Numerics;
using Newtonsoft.Json;

namespace IconSDK.Extensions
{
    public class BigIntegerConverter : JsonConverter<BigInteger?>
    {
        public override BigInteger? ReadJson(JsonReader reader, Type objectType, BigInteger? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string s = (string)reader.Value;
            return s.ToBigInteger();
        }

        public override void WriteJson(JsonWriter writer, BigInteger? value, JsonSerializer serializer)
        {
            writer.WriteValue(value.Value.ToHex0x());
        }
    }
}