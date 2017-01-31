using Newtonsoft.Json;
using System;

namespace Mos.xApi.Objects
{
    public class Activity : StatementObject
    {
        public Activity(Uri id, ActivityDefinition activityDefinition = null)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            Id = id;
            ActivityDefinition = activityDefinition;
        }

        [JsonProperty("objectType", Order = 0)]
        public const string ObjectType = nameof(Activity);

        [JsonProperty("definition", Order = 2, NullValueHandling = NullValueHandling.Ignore)]
        public ActivityDefinition ActivityDefinition { get; }

        [JsonProperty("id", Order = 1)]
        public Uri Id { get; }
    }
}