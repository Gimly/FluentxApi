using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Mos.xApi.Utilities
{
    /// <summary>
    /// Json converter that allows the conversion of a LanguageMap to its JSON representation
    /// as defined in the Experience API specification.
    /// </summary>
    internal class LanguageMapConverter : JsonConverter
    {
        /// <summary>
        /// Checks whether the type can be converted by this converter. Type should implement ILanguageMap.
        /// </summary>
        /// <param name="objectType">The type of object trying to be converted.</param>
        /// <returns>True if the converter can convert the type, otherwise false.</returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(ILanguageMap).IsAssignableFrom(objectType);
        }

        /// <summary>
        /// Gets a value indicating whether this JsonConverter can write JSON.
        /// In this case, no need to override the default write behavior, it's working
        /// fine as a dictionary.
        /// </summary>
        public override bool CanWrite => false;

        /// <summary>
        /// Reads the JSON representation of the object. Json.Net doesn't know
        /// what to do with the interface, overriding here allows to read as
        /// a standard string, string dictionary and then to construct the language map
        /// from this.
        /// </summary>
        /// <param name="reader">The JsonReader to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var item = JObject.Load(reader);
            return new LanguageMap(item.ToObject<Dictionary<string, string>>());
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The JsonWriter to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
