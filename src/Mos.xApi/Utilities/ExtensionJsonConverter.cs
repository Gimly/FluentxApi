using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Mos.xApi.Utilities
{
    public class ExtensionJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Extension);
        }

        public override bool CanRead => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var item = JObject.Load(reader);

            var result = new Dictionary<Uri, string>();

            foreach (var element in item)
            {
                result.Add(new Uri(element.Key), element.Value.ToString());
            }

            return new Extension(result);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var extensions = (Extension)value;

            var jo = new JObject();

            foreach (var item in extensions)
            {
                JToken token;
                if (item.Value.Trim().StartsWith("{", StringComparison.Ordinal))
                {
                    token = JToken.Parse(item.Value);
                }
                else
                {
                    token = JToken.FromObject(item.Value);
                }

                jo.Add(item.Key.ToString(), token);
            }

            jo.WriteTo(writer);
        }
    }
}