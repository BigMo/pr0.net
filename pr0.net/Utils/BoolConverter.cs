using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pr0.net.Utils
{
    public class BoolConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteValue(((bool)value) ? 1 : 0);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            return reader.Value.ToString() == "1";
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool);
        }
    }
}
