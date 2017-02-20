using Newtonsoft.Json;
using System;
using System.Linq;

namespace Mos.xApi.Objects
{
    /// <summary>
    /// This class contains all properties used to define an Activity.
    /// </summary>
    public class ActivityDefinition
    {
        /// <summary>
        /// Initializes a new instance of the ActivityDefinition class.
        /// </summary>
        /// <param name="name">The human readable/visual name of the Activity</param>
        /// <param name="type">The type of Activity.</param>
        /// <param name="description">A description of the Activity</param>
        /// <param name="moreInfo">Resolves to a document with human-readable information about the Activity, which could include a way to launch the activity.</param>
        /// <param name="extensions">A map of other properties as needed</param>
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

        /// <summary>
        /// Gets a description of the Activity
        /// </summary>
        [JsonProperty("description", Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        public ILanguageMap Description { get; }

        /// <summary>
        /// Gets an IRI that resolves to a document with human-readable information about the Activity, which could include a way to launch the activity.
        /// </summary>
        [JsonProperty("moreInfo", Order = 3, NullValueHandling = NullValueHandling.Ignore)]
        public Uri MoreInfo { get; }

        /// <summary>
        /// Gets the human readable/visual name of the Activity
        /// </summary>
        [JsonProperty("name", Order = 0, NullValueHandling = NullValueHandling.Ignore)]
        public ILanguageMap Name { get; }

        /// <summary>
        /// Gets the type of Activity.
        /// </summary>
        [JsonProperty("type", Order = 2, NullValueHandling = NullValueHandling.Ignore)]
        public Uri Type { get; }

        /// <summary>
        /// Gets a map of other properties as needed
        /// </summary>
        [JsonProperty("extensions", Order = 4, NullValueHandling = NullValueHandling.Ignore)]
        public Extension Extensions { get; }
    }
}