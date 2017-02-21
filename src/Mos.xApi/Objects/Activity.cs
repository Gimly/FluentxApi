using Newtonsoft.Json;
using System;

namespace Mos.xApi.Objects
{
    /// <summary>
    /// Defines a type of statement object that is an Activity.
    /// </summary>
    public class Activity : StatementObject
    {
        /// <summary>
        /// Initializes a new instance of an Activity class.
        /// </summary>
        /// <param name="id">An identifier for a single unique Activity</param>
        /// <param name="activityDefinition">All the metadata associated with this Activity</param>
        public Activity(Uri id, ActivityDefinition activityDefinition = null)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            Id = id;
            ActivityDefinition = activityDefinition;
        }

        /// <summary>
        /// The type of object. In that case, Activity.
        /// </summary>
        [JsonProperty("objectType", Order = 0)]
        public const string ObjectType = nameof(Activity);

        /// <summary>
        /// Gets all the metadata defining the Activity.
        /// </summary>
        [JsonProperty("definition", Order = 2, NullValueHandling = NullValueHandling.Ignore)]
        public ActivityDefinition ActivityDefinition { get; }

        /// <summary>
        /// Gets an identifier for a single unique Activity
        /// </summary>
        [JsonProperty("id", Order = 1)]
        public Uri Id { get; }
    }
}