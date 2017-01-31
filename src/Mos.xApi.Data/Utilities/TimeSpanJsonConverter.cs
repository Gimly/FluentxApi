using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Mos.xApi.Data.Utilities
{
    public class TimeSpanJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TimeSpan) || objectType == typeof(TimeSpan?);
        }

        public override bool CanRead => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var iso8601Representation = (string)reader.Value;
            return XmlConvert.ToTimeSpan(iso8601Representation);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!CanConvert(value.GetType()))
            {
                throw new ArgumentException("Value is not of the correct type, expected TimeSpan or TimeSpan?.", nameof(value));
            }

            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var timeSpanValue = (TimeSpan)value;
            var iso8601Representation = XmlConvert.ToString(timeSpanValue);

            writer.WriteValue(iso8601Representation);
        }
    }
}
