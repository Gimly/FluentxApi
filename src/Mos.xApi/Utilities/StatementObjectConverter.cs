using Mos.xApi.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Mos.xApi.Utilities
{
    /// <summary>
    /// Json converter that allows the conversion of a StatementObject to the JSON representation defined in the 
    /// Experience API specification.
    /// </summary>
    internal class StatementObjectConverter : JsonConverter
    {
        /// <summary>
        /// Checks whether the type can be converted by this converter. Type should be a StatementObject.
        /// </summary>
        /// <param name="objectType">The type of object trying to be converted.</param>
        /// <returns>True if the converter can convert the type, otherwise false.</returns>
        public override bool CanConvert(Type objectType) => typeof(StatementObject) == objectType;

        /// <summary>
        /// Gets a value indicating whether this JsonConverter can write JSON.
        /// In the specific case of this JsonConverter, it's only used for reading, because the
        /// class deserialized to is abstract and Json.Net has to be helped finding the type.
        /// </summary>
        public override bool CanWrite => false;

        /// <summary>
        /// Reads the JSON representation of the object. Checks the objectType from the
        /// json and deserializes using the specific type corresponding.
        /// </summary>
        /// <param name="reader">The JsonReader to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var item = JToken.ReadFrom(reader);

            var statementObjectType = item["objectType"].Value<string>();
            if (statementObjectType == "Activity")
            {
                return item.ToObject<Activity>(serializer);
            }

            if (statementObjectType == "StatementRef")
            {
                return item.ToObject<StatementReference>(serializer);
            }

            if (statementObjectType == "SubStatement")
            {
                return item.ToObject<SubStatement>(serializer);
            }

            throw new NotSupportedException($"Cannot read statement object of type {statementObjectType}");
        }

        /// <summary>
        /// Writes the JSON representation of the object. This is not supported by this Converter.
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
