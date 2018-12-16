using System;
using System.Numerics;
using Newtonsoft.Json;

namespace IconSDK.Extensions
{
    using Types;
    public class BytesConverter<TBytes> : JsonConverter<TBytes> where TBytes : Bytes
    {
        public override TBytes ReadJson(JsonReader reader, Type objectType, TBytes existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string s = (string)reader.Value;
            return (TBytes)Activator.CreateInstance(typeof(TBytes), s);
        }

        public override void WriteJson(JsonWriter writer, TBytes value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}