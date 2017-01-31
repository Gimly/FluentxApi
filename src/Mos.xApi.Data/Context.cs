using Mos.xApi.Data.Actors;
using Mos.xApi.Data.Objects;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Mos.xApi.Data
{
    public class Context
    {
        public Context(
            Guid? registration = null,
            Actor instructor = null,
            Group team = null,
            ContextActivities contextActivities = null,
            string revision = null,
            string platform = null,
            string language = null,
            StatementReference statement = null,
            Extension extensions = null)
        {
            Registration = registration;
            Instructor = instructor;
            Team = team;
            ContextActivities = contextActivities;
            Revision = revision;
            Platform = platform;
            Language = language;
            Statement = statement;

            if (extensions != null && extensions.Any())
            {
                Extensions = extensions;
            }
        }

        [JsonProperty("registration", Order = 0, NullValueHandling = NullValueHandling.Ignore)]
        public Guid? Registration { get; }

        [JsonProperty("instructor", Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        public Actor Instructor { get; }

        [JsonProperty("team", Order = 2, NullValueHandling = NullValueHandling.Ignore)]
        public Group Team { get; }

        [JsonProperty("contextActivities", Order = 3, NullValueHandling = NullValueHandling.Ignore)]
        public ContextActivities ContextActivities { get; }

        [JsonProperty("revision", Order = 4, NullValueHandling = NullValueHandling.Ignore)]
        public string Revision { get; }

        [JsonProperty("platform", Order = 5, NullValueHandling = NullValueHandling.Ignore)]
        public string Platform { get; }

        [JsonProperty("language", Order = 6, NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; }

        [JsonProperty("statement", Order = 7, NullValueHandling = NullValueHandling.Ignore)]
        public StatementReference Statement { get; }

        [JsonProperty("extensions", Order = 8, NullValueHandling = NullValueHandling.Ignore)]
        public Extension Extensions { get; }

        public static IContextBuilder Create() => new ContextBuilder();
    }
}