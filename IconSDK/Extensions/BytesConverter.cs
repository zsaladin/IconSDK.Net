using System;
using System.Numerics;
using Newtonsoft.Json;

namespace IconSDK.Extensions
{
    using Types;
    public class BytesConverter<TBytes> : JsonConverter where TBytes : Bytes
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(TBytes) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string s = (string)reader.Value;
            return Activator.CreateInstance(typeof(TBytes), s);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}