using Mos.xApi.Actors;
using Mos.xApi.InverseFunctionalIdentifiers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace Mos.xApi.Utilities
{
    public class ActorJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Actor).IsAssignableFrom(objectType);
        }

        public override bool CanRead => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var item = JObject.Load(reader);

            var xApiobjectType = item["objectType"]?.Value<string>();

            if (xApiobjectType == "Agent")
            {
                return ParseAgent(item);
            }

            if (xApiobjectType == "Group")
            {
                return ParseGroup(item);
            }

            throw new InvalidOperationException($"Cannot parse Actor, unrecognized objectType.\r\n{item.ToString()}");
        }

        private Agent ParseAgent(JToken item)
        {
            var builder = Actor.CreateAgent(item["name"]?.Value<string>());
            if (item["mbox"] != null)
            {
                // First 7 characters are the "mailto:", no need to keep it
                return builder.WithMailBox(item["mbox"].Value<string>().Remove(0, 7));
            }

            if (item["openid"] != null)
            {
                return builder.WithOpenId(item["openid"].Value<string>());
            }

            if (item["account"] != null)
            {
                return builder.WithAccount(item["account"]["name"].Value<string>(), item["account"]["homePage"].Value<string>());
            }

            if (item["mbox_sha1sum"] != null)
            {
                return builder.WithHashedMailBox(item["mbox_sha1sum"].Value<string>());
            }

            throw new NotSupportedException($"Cannot deserialize Actor, missing Inverse Functional Identifier. {item}");
        }

        private Group ParseGroup(JToken item)
        {
            var builder = Actor.CreateGroup(item["name"]?.Value<string>());

            var members = item["member"];
            if (members != null)
            {
                foreach (var member in members)
                {
                    builder.Add(ParseAgent(member));
                }
            }

            if (item["mbox"] != null)
            {
                // First 7 characters are the "mailto:", no need to keep it
                return builder.WithMailBox(item["mbox"].Value<string>().Remove(0, 7));
            }

            if (item["openid"] != null)
            {
                return builder.WithOpenId(item["openid"].Value<string>());
            }

            if (item["account"] != null)
            {
                return builder.WithAccount(item["account"]["name"].Value<string>(), item["account"]["homePage"].Value<string>());
            }

            if (item["mbox_sha1sum"] != null)
            {
                return builder.WithHashedMailBox(item["mbox_sha1sum"].Value<string>());
            }

            return builder.AsAnonymous();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var jo = new JObject();
            var type = value.GetType();

            foreach (var prop in type.GetProperties())
            {
                if (prop.CanRead)
                {
                    var propVal = prop.GetValue(value, null);
                    if (prop.Name == "Identifier")
                    {
                        var identifierType = propVal.GetType();

                        if (identifierType == typeof(Account))
                        {
                            AddPropertyToObject(jo, prop, serializer, propVal, "account");
                        }
                        else
                        {
                            foreach (var identifierProp in identifierType.GetProperties())
                            {
                                var identifierPropVal = identifierProp.GetValue(propVal, null);
                                AddPropertyToObject(jo, identifierProp, serializer, identifierPropVal);
                            }
                        }
                    }
                    else
                    {
                        AddPropertyToObject(jo, prop, serializer, propVal);
                    }
                }
            }

            jo.WriteTo(writer);
        }

        private static void AddPropertyToObject(JObject jo, PropertyInfo prop, JsonSerializer serializer, object propertyValue, string propertyName = null)
        {
            var jsonConvertAttribute = prop.GetCustomAttribute<JsonConverterAttribute>(true);

            JToken token;
            if (jsonConvertAttribute != null)
            {
                var converter = (JsonConverter)Activator.CreateInstance(jsonConvertAttribute.ConverterType, jsonConvertAttribute.ConverterParameters);
                var json = JsonConvert.SerializeObject(propertyValue, converter);
                token = JToken.Parse(json);
            }
            else
            {
                token = propertyValue != null ? JToken.FromObject(propertyValue, serializer) : null;
            }

            if (token != null)
            {
                if (string.IsNullOrEmpty(propertyName))
                {
                    var jsonAttribute = prop.GetCustomAttribute<JsonPropertyAttribute>(true);
                    jo.Add(jsonAttribute?.PropertyName ?? prop.Name, token);
                }
                else
                {
                    jo.Add(propertyName, token);
                }
            }
        }
    }
}