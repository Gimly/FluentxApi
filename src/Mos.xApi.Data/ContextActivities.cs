using Mos.xApi.Data.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mos.xApi.Data
{
    public class ContextActivities
    {
        public ContextActivities(
            IEnumerable<Activity> parents = null,
            IEnumerable<Activity> groupings = null,
            IEnumerable<Activity> categories = null,
            IEnumerable<Activity> others = null)
        {
            if (parents != null && parents.Any())
            {
                Parents = parents;
            }

            if (groupings != null && groupings.Any())
            {
                Groupings = groupings;
            }

            if (categories != null && categories.Any())
            {
                Categories = categories;
            }

            if (others != null && others.Any())
            {
                Others = others;
            }
        }

        [JsonProperty("category", Order = 2, NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Activity> Categories { get; }

        [JsonProperty("grouping", Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Activity> Groupings { get; }

        [JsonProperty("other", Order = 3, NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Activity> Others { get; }

        [JsonProperty("parent", Order = 0, NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Activity> Parents { get; }
    }
}