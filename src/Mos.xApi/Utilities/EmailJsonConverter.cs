using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Mos.xApi.Utilities
{
    public class EmailJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(string) == objectType;
        }

        public override bool CanRead => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var rawValue = (string)value;

            var emailAddressValidator = new EmailAddressAttribute();
            if (!emailAddressValidator.IsValid(rawValue))
            {
                throw new NotSupportedException($"{rawValue} is not a valid e-mail address.");
            }

            writer.WriteValue($"mailto:{rawValue}");
        }
    }
}
