using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Mos.xApi.Utilities
{
    /// <summary>
    /// Json converter that allows the conversion of an Extension to its JSON representation as defined in the Experience API.
    /// </summary>
    internal class ExtensionJsonConverter : JsonConverter
    {
        /// <summary>
        /// Checks whether the type can be converted by this converter. Type should be an Extension.
        /// </summary>
        /// <param name="objectType">The type of object trying to be converted.</param>
        /// <returns>True if the converter can convert the type, otherwise false.</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Extension);
        }

        /// <summary>
        /// Gets a value indicating whether this JsonConverter can read JSON.
        /// </summary>
        public override bool CanRead => true;

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The JsonReader to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
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

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The JsonWriter to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
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