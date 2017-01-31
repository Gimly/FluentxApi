using Mos.xApi.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Mos.xApi.Utilities
{
    public class StatementObjectConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => typeof(StatementObject) == objectType;

        public override bool CanWrite => false;

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

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
