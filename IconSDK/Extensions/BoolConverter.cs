using System;
using System.Numerics;
using Newtonsoft.Json;

namespace IconSDK.Extensions
{
    using Types;
    public class BoolConverter : JsonConverter<bool>
    {
        public override bool ReadJson(JsonReader reader, Type objectType, bool existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string s = (string)reader.Value;
            if (s == "0x0")
                return false;
            if (s == "0x1")
                return true;

            throw new FormatException($"Cannot convert to boolean type. {s}");
        }

        public override void WriteJson(JsonWriter writer, bool value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}