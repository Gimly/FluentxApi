using Newtonsoft.Json;
using System;

namespace Mos.xApi.Objects
{
    public class StatementReference : StatementObject
    {
        public StatementReference(Guid id)
        {
            Id = id;
        }

        [JsonProperty("objectType")]
        public const string ObjectType = "StatementRef";

        [JsonProperty("id")]
        public Guid Id { get; }
    }
}
