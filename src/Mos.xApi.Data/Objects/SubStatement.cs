using Mos.xApi.Data.Actors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mos.xApi.Data.Objects
{
    public class SubStatement : StatementObject
    {
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

        [JsonProperty("objectType", Order = 0)]
        public const string ObjectType = nameof(SubStatement);

        [JsonProperty("actor", Order = 1)]
        public Actor Actor { get; }

        [JsonProperty("attachments", Order = 7, NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Attachment> Attachments { get; }

        [JsonProperty("context", Order = 5, NullValueHandling = NullValueHandling.Ignore)]
        public Context Context { get; }

        [JsonProperty("result", Order = 4, NullValueHandling = NullValueHandling.Ignore)]
        public Result Result { get; }

        [JsonProperty("object", Order = 3)]
        public StatementObject StatementObject { get; }

        [JsonProperty("timestamp", Order = 6, NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Timestamp { get; }

        [JsonProperty("verb", Order = 2)]
        public Verb Verb { get; }
    }
}