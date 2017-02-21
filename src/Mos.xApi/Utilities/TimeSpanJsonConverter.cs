using Newtonsoft.Json;
using System;
using System.Xml;

namespace Mos.xApi.Utilities
{
    /// <summary>
    /// Json converter that allows the conversion of a TimeSpan to its ISO 8601 representation.
    /// </summary>
    internal class TimeSpanJsonConverter : JsonConverter
    {
        /// <summary>
        /// Checks whether the type can be converted by this converter. Type should be a TimeSpan.
        /// </summary>
        /// <param name="objectType">The type of object trying to be converted.</param>
        /// <returns>True if the converter can convert the type, otherwise false.</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(TimeSpan) || objectType == typeof(TimeSpan?);
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
            var iso8601Representation = (string)reader.Value;
            return XmlConvert.ToTimeSpan(iso8601Representation);
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The JsonWriter to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
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
