using Mos.xApi.Actors;
using Mos.xApi.InverseFunctionalIdentifiers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace Mos.xApi.Utilities
{
    /// <summary>
    /// Json converter that allows the conversion of an Actor to its JSON representation
    /// as defined in the Experience API specification.
    /// </summary>
    internal class ActorJsonConverter : JsonConverter
    {
        /// <summary>
        /// Checks whether the type can be converted by this converter. Type should be derived from Actor.
        /// </summary>
        /// <param name="objectType">The type of object trying to be converted.</param>
        /// <returns>True if the converter can convert the type, otherwise false.</returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(Actor).IsAssignableFrom(objectType);
        }

        /// <summary>
        /// Gets a value indicating whether this JsonConverter can read JSON.
        /// </summary>
        public override bool CanRead => true;

        /// <summary>
        /// Reads the JSON representation of the object. The tricky part here is that
        /// the type can be either Agent or Group and that they contain vastly different
        /// properties in each of the different definition of an Agent.
        /// </summary>
        /// <param name="reader">The JsonReader to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
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

            throw new InvalidOperationException($"Cannot parse Actor, unrecognized objectType.\r\n{item}");
        }

        /// <summary>
        /// Parses all the different types of Agent (mbox, mbos-sha1, openid, account)
        /// </summary>
        /// <param name="item">The Json object read from the source</param>
        /// <returns>The parsed representation of the Agent</returns>
        private Agent ParseAgent(JToken item)
        {
            var builder = Actor.CreateAgent(item["name"]?.Value<string>());
            if (item["mbox"] != null)
            {
                var value = item["mbox"].Value<string>();
                if (!value.StartsWith(EmailJsonConverter.MailToPrefix))
                {
                    throw new NotSupportedException($"mbox value must start with '{EmailJsonConverter.MailToPrefix}' prefix.");
                }

                var email = value.Substring(EmailJsonConverter.MailToPrefix.Length);
                return builder.WithMailBox(email);
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

        /// <summary>
        /// Parses the group, same issue as with the Agent, but can also contain
        /// a list of Agents.
        /// </summary>
        /// <param name="item">The Json object read from the source</param>
        /// <returns>The parsed representation of the Group</returns>
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

        /// <summary>
        /// Writes the JSON representation of the object. Here the difficulty
        /// is with the identifier. Since there are different kind of IFI, I
        /// decided to define them as different objects to have the object
        /// representation cleaner. This means that JSON.Net would like
        /// to put them under a property in the JSON, which is not how
        /// it has been defined by the xApi specs. Therefore the use of this
        /// converter is needed.
        /// </summary>
        /// <param name="writer">The JsonWriter to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
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

        /// <summary>
        /// Checks if the read property has a JsonConverterAttribute and uses it
        /// if it does. Otherwise calls the standard serializer.
        /// </summary>
        /// <param name="jo">The Json object to parse.</param>
        /// <param name="prop">The property info where to the object will be saved.</param>
        /// <param name="serializer">The serializer used to serialize.</param>
        /// <param name="propertyValue">The value of the property</param>
        /// <param name="propertyName">The name of the property.</param>
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