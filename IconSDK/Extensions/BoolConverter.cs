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
            if (value)
                writer.WriteValue("0x1");
            else
                writer.WriteValue("0x0");
        }
    }

    public class BoolNullableConverter : JsonConverter<bool?>
    {
        public override bool? ReadJson(JsonReader reader, Type objectType, bool? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string s = (string)reader.Value;
            if (s == "0x0")
                return false;
            if (s == "0x1")
                return true;

            throw new FormatException($"Cannot convert to boolean type. {s}");
        }

        public override void WriteJson(JsonWriter writer, bool? value, JsonSerializer serializer)
        {
            if (value.HasValue)
                if (value.Value)
                    writer.WriteValue("0x1");
                else
                    writer.WriteValue("0x0");
        }
    }
}