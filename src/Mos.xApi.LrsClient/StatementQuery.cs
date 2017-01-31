using Mos.xApi.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Mos.xApi.LrsClient
{
    /// <summary>
    /// This class is used to define a query that allows to retrieve Statements from the Statement API of an LRS.
    /// <para>If a property is left null, the filter will not be passed to the LRS and it will be ignored or the default
    /// will be used.</para>
    /// </summary>
    public class StatementQuery
    {
        private int? _limit;

        /// <summary>
        /// Only returns Statements for which the specified Agent or Group
        /// is the Actor or Object of the Statement.
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// Agents or Identified Groups are equal when the same Inverse Functional Identifier
        /// is used in each Object compared and those Inverse Functional Identifiers have equal values.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// For the purposes of this filter, Groups that have members which match the specified Agent based
        /// on their Inverse Functional Identifier as described above are considered a match.
        /// </description>
        /// </item>
        /// </list>
        /// </remarks>
        public Actor Agent { get; set; }

        /// <summary>
        /// Only return Statements matching the specified Verb id.
        /// </summary>
        public Uri VerbId { get; set; }

        /// <summary>
        /// Only return Statements for which the Object of the Statement is an Activity with the specified id.
        /// </summary>
        public Uri ActivityId { get; set; }

        /// <summary>
        /// Only return Statements matching the specified registration id.
        /// </summary>
        /// <remarks>
        /// Note that although frequently a unique registration will be used for one Actor assigned to one Activity, this cannot be assumed.
        /// If only Statements for a certain Actor or Activity are required, those parameters also need to be specified.
        /// </remarks>
        public Guid? Registration { get; set; }

        /// <summary>
        /// Apply the Activity filter broadly. Include Statements for which the Object, any of the context Activities,
        /// or any of those properties in a contained SubStatement match the Activity parameter, instead of that parameter's
        /// normal behavior. Matching is defined in the same way it is for the "activity" parameter.
        /// <para>Default is false.</para>
        /// </summary>
        public bool? RelatedActivities { get; set; }

        /// <summary>
        /// Apply the Agent filter broadly. Include Statements for which the Actor, Object, Authority, Instructor, Team,
        /// or any of these properties in a contained SubStatement match the Agent parameter, instead of that parameter's normal behavior.
        /// Matching is defined in the same way it is for the "agent" parameter.
        /// <para>Default is false.</para>
        /// </summary>
        public bool? RelatedAgents { get; set; }

        /// <summary>
        /// Only Statements stored since the specified DateTime (exclusive) are returned.
        /// </summary>
        public DateTime? Since { get; set; }

        internal string ToQueryString()
        {
            var dictionary = new Dictionary<string, string>();

            if (Agent != null)
            {
                dictionary.Add("agent", Agent.ToJson());
            }

            if (VerbId != null)
            {
                dictionary.Add("verb", VerbId.ToString());
            }

            if (ActivityId != null)
            {
                dictionary.Add("activity", ActivityId.ToString());
            }

            if (Registration.HasValue)
            {
                dictionary.Add("registration", Registration.Value.ToString());
            }

            if (RelatedActivities.HasValue)
            {
                dictionary.Add("related_activities", RelatedActivities.Value ? "true" : "false");
            }

            if (RelatedAgents.HasValue)
            {
                dictionary.Add("related_agents", RelatedAgents.Value ? "true" : "false");
            }

            if (Since.HasValue)
            {
                dictionary.Add("since", Since.Value.ToString("o"));
            }

            if (Until.HasValue)
            {
                dictionary.Add("until", Until.Value.ToString("o"));
            }

            if (Limit.HasValue)
            {
                dictionary.Add("limit", Limit.Value.ToString());
            }

            if (Format.HasValue)
            {
                dictionary.Add("format", Format.ToString().ToLower());
            }

            if (Attachments.HasValue)
            {
                dictionary.Add("attachments", Attachments.Value ? "true" : "false");
            }

            if (Ascending.HasValue)
            {
                dictionary.Add("ascending", Ascending.Value ? "true" : "false");
            }

            if (!dictionary.Any())
            {
                return string.Empty;
            }

            return string.Concat("?", string.Join("&", dictionary.Select(x => $"{WebUtility.UrlEncode(x.Key)}={WebUtility.UrlEncode(x.Value)}")));
        }

        /// <summary>
        /// Only Statements stored at or before the specified DateTime are returned.
        /// </summary>
        public DateTime? Until { get; set; }

        /// <summary>
        /// Maximum number of Statements to return. 0 indicates return the maximum the server will allow.
        /// <para>Must be greater or equal to 0. Default is 0</para>
        /// </summary>
        public int? Limit
        {
            get
            {
                return _limit;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Value must be greater or equal to 0");
                }
                _limit = value;
            }
        }

        /// <summary>
        /// Defines the format in which the Agent, Activity, Verb and Group Objects will be returned.
        /// </summary>
        public StatementQueryFormat? Format { get; set; }

        /// <summary>
        /// If true, the LRS uses the multipart response format and includes all attachments.
        /// <para>If false, the LRS sends the prescribed response with Content-Type application/json and does not send attachment data.</para>
        /// <para>Default is false.</para>
        /// </summary>
        public bool? Attachments { get; set; }

        /// <summary>
        /// If true, return results in ascending order of stored time
        /// </summary>
        public bool? Ascending { get; set; }
    }
}
