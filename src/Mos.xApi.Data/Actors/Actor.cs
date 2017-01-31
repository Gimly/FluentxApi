using Mos.xApi.InverseFunctionalIdentifiers;
using Newtonsoft.Json;
using System;

namespace Mos.xApi.Actors
{
    public abstract class Actor
    {
        protected Actor(IInverseFunctionalIdentifier identifier, string name = null)
        {
            Identifier = identifier;
            Name = name;
        }

        [JsonProperty(Order = 2)]
        public IInverseFunctionalIdentifier Identifier { get; }

        [JsonProperty("name", Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; }

        [JsonProperty("objectType", Order = 0)]
        public abstract string ObjectType { get; }

        public static IAgentBuilder CreateAgent(string name = null)
        {
            return new AgentBuilder(name);
        }

        public static IGroupBuilder CreateGroup(string name = null)
        {
            return new GroupBuilder(name);
        }

        public string ToJson(bool prettyPrint = false)
        {
            return JsonConvert.SerializeObject(this, prettyPrint ? Formatting.Indented : Formatting.None, JsonSerializerSettingsFactory.CreateSettings());
        }
    }
}