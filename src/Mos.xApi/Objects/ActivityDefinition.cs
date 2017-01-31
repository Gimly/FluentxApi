using Newtonsoft.Json;
using System;
using System.Linq;

namespace Mos.xApi.Objects
{
    public class ActivityDefinition
    {
        public ActivityDefinition(ILanguageMap name, Uri type, ILanguageMap description = null, Uri moreInfo = null, Extension extensions = null)
        {
            if (name != null && name.Any())
            {
                Name = name;
            }

            if (description != null && description.Any())
            {
                Description = description;
            }

            if (extensions != null && extensions.Any())
            {
                Extensions = extensions;
            }
            
            Type = type;
            MoreInfo = moreInfo;
            
        }

        [JsonProperty("description", Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        public ILanguageMap Description { get; }

        [JsonProperty("moreInfo", Order = 3, NullValueHandling = NullValueHandling.Ignore)]
        public Uri MoreInfo { get; }

        [JsonProperty("name", Order = 0, NullValueHandling = NullValueHandling.Ignore)]
        public ILanguageMap Name { get; }

        [JsonProperty("type", Order = 2, NullValueHandling = NullValueHandling.Ignore)]
        public Uri Type { get; }

        [JsonProperty("extensions", Order = 4, NullValueHandling = NullValueHandling.Ignore)]
        public Extension Extensions { get; }
    }
}