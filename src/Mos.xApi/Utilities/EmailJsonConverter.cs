using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Mos.xApi.Utilities
{
    /// <summary>
    /// Json converter that allows the conversion of an Email to the JSON representation
    /// mandated by the Experience API specification. Namely, the e-mail has to start with
    /// a "mailto:".
    /// </summary>
    internal class EmailJsonConverter : JsonConverter
    {
        internal const string MailToPrefix = "mailto:";

        /// <summary>
        /// Checks whether the type can be converted by this converter. Type should be a string.
        /// </summary>
        /// <param name="objectType">The type of object trying to be converted.</param>
        /// <returns>True if the converter can convert the type, otherwise false.</returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(string) == objectType;
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
            var emailRead = (string)reader.Value;
            return emailRead.Remove(0, 7);
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The JsonWriter to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var rawValue = (string)value;

            var emailAddressValidator = new EmailAddressAttribute();
            if (!emailAddressValidator.IsValid(rawValue))
            {
                throw new NotSupportedException($"{rawValue} is not a valid e-mail address.");
            }

            writer.WriteValue($"{MailToPrefix}{rawValue}");
        }
    }
}
