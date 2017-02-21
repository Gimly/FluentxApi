using Mos.xApi.Actors;
using Mos.xApi.Builders;
using Mos.xApi.Objects;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Mos.xApi
{
    /// <summary>
    /// Defines an optional property that provides a place to add contextual information to a Statement. All "context" properties are optional.
    /// </summary>
    public class Context
    {
        /// <summary>
        /// Initializes a new Context instance.
        /// </summary>
        /// <param name="registration">The registration that the Statement is associated with.</param>
        /// <param name="instructor">Instructor that the Statement relates to, if not included as the Actor of the Statement.</param>
        /// <param name="team">Team that this Statement relates to, if not included as the Actor of the Statement.</param>
        /// <param name="contextActivities">A map of the types of learning activity context that this Statement is related to. Valid context types are: "parent", "grouping", "category" and "other".</param>
        /// <param name="revision">Revision of the learning activity associated with this Statement. Format is free.</param>
        /// <param name="platform">Platform used in the experience of this learning activity.</param>
        /// <param name="language">Code representing the language in which the experience being recorded in this Statement (mainly) occurred in, if applicable and known.</param>
        /// <param name="statement">Another Statement to be considered as context for this Statement.</param>
        /// <param name="extensions">A map of any other domain-specific context relevant to this Statement. <para>For example, in a flight simulator altitude, airspeed, wind, attitude, GPS coordinates might all be relevant.</para></param>
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

        /// <summary>
        /// Gets the registration that the Statement is associated with.
        /// </summary>
        [JsonProperty("registration", Order = 0, NullValueHandling = NullValueHandling.Ignore)]
        public Guid? Registration { get; }

        /// <summary>
        /// Gets the instructor that the Statement relates to, if not included as the Actor of the Statement.
        /// </summary>
        [JsonProperty("instructor", Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        public Actor Instructor { get; }

        /// <summary>
        /// Gets the team that this Statement relates to, if not included as the Actor of the Statement.
        /// </summary>
        [JsonProperty("team", Order = 2, NullValueHandling = NullValueHandling.Ignore)]
        public Group Team { get; }

        /// <summary>
        /// Gets a map of the types of learning activity context that this Statement is related to. Valid context types are: "parent", "grouping", "category" and "other".
        /// </summary>
        [JsonProperty("contextActivities", Order = 3, NullValueHandling = NullValueHandling.Ignore)]
        public ContextActivities ContextActivities { get; }

        /// <summary>
        /// Gets a revision of the learning activity associated with this Statement. Format is free.
        /// </summary>
        [JsonProperty("revision", Order = 4, NullValueHandling = NullValueHandling.Ignore)]
        public string Revision { get; }

        /// <summary>
        /// Gets the platform used in the experience of this learning activity.
        /// </summary>
        [JsonProperty("platform", Order = 5, NullValueHandling = NullValueHandling.Ignore)]
        public string Platform { get; }

        /// <summary>
        /// Gets a code representing the language in which the experience being recorded in this Statement (mainly) occurred in, if applicable and known.
        /// </summary>
        [JsonProperty("language", Order = 6, NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; }
        
        /// <summary>
        /// Gets another Statement to be considered as context for this Statement.
        /// </summary>
        [JsonProperty("statement", Order = 7, NullValueHandling = NullValueHandling.Ignore)]
        public StatementReference Statement { get; }

        /// <summary>
        /// Gets a map of any other domain-specific context relevant to this Statement. <para>For example, in a flight simulator altitude, airspeed, wind, attitude, GPS coordinates might all be relevant.</para>
        /// </summary>
        [JsonProperty("extensions", Order = 8, NullValueHandling = NullValueHandling.Ignore)]
        public Extension Extensions { get; }

        /// <summary>
        /// Starts the creation of a new Context, using the fluent interface.
        /// </summary>
        /// <returns>A builder that allows to fluently create a Context.</returns>
        public static IContextBuilder Create() => new ContextBuilder();
    }
}