using Mos.xApi.Actors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mos.xApi.Objects
{
    /// <summary>
    /// A SubStatement is like a StatementRef in that it is included as part of a containing Statement, but unlike a StatementRef, it does not represent an event that has occurred. 
    /// <para>It can be used to describe, for example, a predication of a potential future Statement or the behavior a teacher looked for when evaluating a student (without representing the student actually doing that behavior).</para>
    /// </summary>
    public class SubStatement : StatementObject
    {
        /// <summary>
        /// Initializes a new instance of the SubStatement class.
        /// </summary>
        /// <param name="actor">Whom the SubStatement is about, as an Agent or Group Object.</param>
        /// <param name="verb">Action taken by the Actor.</param>
        /// <param name="statementObject">Activity or Agent that is the Object of the SubStatement.</param>
        /// <param name="result">Result Object, further details representing a measured outcome.</param>
        /// <param name="context">Context that gives the SubStatement more meaning. Examples: a team the Actor is working with, altitude at which a scenario was attempted in a flight simulator.</param>
        /// <param name="timestamp">Timestamp of when the events described within this SubStatement occurred. Set by the LRS if not provided.</param>
        /// <param name="attachments">Headers for Attachments to the SubStatement</param>
        public SubStatement(
            Actor actor, 
            Verb verb, 
            StatementObject statementObject, 
            Result result = null, 
            Context context = null, 
            DateTime? timestamp = null, 
            IEnumerable<Attachment> attachments = null)
        {
            if (statementObject == null)
                throw new ArgumentNullException(nameof(statementObject));
            if (statementObject is SubStatement)
                throw new ArgumentException("A substatement cannot contain a substatement as object", nameof(statementObject));
            if (verb == null)
                throw new ArgumentNullException(nameof(verb));
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            Actor = actor;
            Verb = verb;
            StatementObject = statementObject;

            Result = result;
            Context = context;
            Timestamp = timestamp;

            if (attachments != null && attachments.Any())
            {
                Attachments = attachments;
            }
        }

        /// <summary>
        /// The type of the object, used by the JSON serializer.
        /// </summary>
        [JsonProperty("objectType", Order = 0)]
        public const string ObjectType = nameof(SubStatement);

        /// <summary>
        /// Gets whom the SubStatement is about, as an Agent or Group Object.
        /// </summary>
        [JsonProperty("actor", Order = 1)]
        public Actor Actor { get; }

        /// <summary>
        /// Gets headers for Attachments to the SubStatement
        /// </summary>
        [JsonProperty("attachments", Order = 7, NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Attachment> Attachments { get; }

        /// <summary>
        /// Gets the context that gives the SubStatement more meaning. Examples: a team the Actor is working with, altitude at which a scenario was attempted in a flight simulator.
        /// </summary>
        [JsonProperty("context", Order = 5, NullValueHandling = NullValueHandling.Ignore)]
        public Context Context { get; }

        /// <summary>
        /// Gets the result Object, further details representing a measured outcome.
        /// </summary>
        [JsonProperty("result", Order = 4, NullValueHandling = NullValueHandling.Ignore)]
        public Result Result { get; }

        /// <summary>
        /// Gets the activity or Agent that is the Object of the SubStatement.
        /// </summary>
        [JsonProperty("object", Order = 3)]
        public StatementObject StatementObject { get; }

        /// <summary>
        /// Gets the timestamp of when the events described within this SubStatement occurred. Set by the LRS if not provided.
        /// </summary>
        [JsonProperty("timestamp", Order = 6, NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Timestamp { get; }

        /// <summary>
        /// Gets the action taken by the Actor.
        /// </summary>
        [JsonProperty("verb", Order = 2)]
        public Verb Verb { get; }
    }
}