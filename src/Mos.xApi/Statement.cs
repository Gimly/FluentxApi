using Mos.xApi.Actors;
using Mos.xApi.Builders;
using Mos.xApi.Objects;
using Mos.xApi.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mos.xApi
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Statement
    {
        private Statement() { }

        public Statement(
            Actor actor,
            Verb verb,
            StatementObject statementObject,
            Result result = null,
            Context context = null,
            DateTime? timestamp = null,
            IEnumerable<Attachment> attachments = null,
            Actor authority = null) : this(Guid.NewGuid(), actor, verb, statementObject, result, context, timestamp, attachments, authority)
        {

        }

        public Statement(
            Guid id,
            Actor actor,
            Verb verb,
            StatementObject statementObject,
            Result result = null,
            Context context = null,
            DateTime? timestamp = null,
            IEnumerable<Attachment> attachments = null,
            Actor authority = null)
        {
            if (statementObject == null)
                throw new ArgumentNullException(nameof(statementObject));
            if (verb == null)
                throw new ArgumentNullException(nameof(verb));
            if (actor == null)
                throw new ArgumentNullException(nameof(actor));

            Id = id;
            Context = context;
            Result = result;
            Actor = actor;
            Verb = verb;
            StatementObject = statementObject;

            Timestamp = timestamp;
            Authority = authority;

            if (attachments != null && attachments.Any())
            {
                Attachments = attachments;
            }
        }

        public static Statement FromJson(string jsonString) => 
            JsonConvert.DeserializeObject<Statement>(jsonString, JsonSerializerSettingsFactory.CreateSettings());

        [JsonProperty("actor", Order = 2)]
        public Actor Actor { get; private set; }

        [JsonProperty("attachments", Order = 10, NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Attachment> Attachments { get; private set; }

        [JsonProperty("authority", Order = 9, NullValueHandling = NullValueHandling.Ignore)]
        public Actor Authority { get; private set; }

        [JsonProperty("context", Order = 6, NullValueHandling = NullValueHandling.Ignore)]
        public Context Context { get; private set; }

        [JsonProperty("id", Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        public Guid Id { get; private set; }

        [JsonProperty("result", Order = 5, NullValueHandling = NullValueHandling.Ignore)]
        public Result Result { get; private set; }

        [JsonProperty("object", Order = 4)]
        public StatementObject StatementObject { get; private set; }

        [JsonProperty("stored", Order = 8, NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Stored { get; private set; }

        [JsonProperty("timestamp", Order = 7, NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Timestamp { get; private set; }

        [JsonProperty("verb", Order = 3)]
        public Verb Verb { get; private set; }

        public string ToJson(bool prettyPrint = false)
        {
            return JsonConvert.SerializeObject(this, prettyPrint ? Formatting.Indented : Formatting.None, JsonSerializerSettingsFactory.CreateSettings());
        }

        public void SetStored(DateTime stored)
        {
            Stored = stored;
        }

        public static IStatementBuilder Create(Actor actor, Verb verb, StatementObject statementObject)
        {
            return new StatementBuilder(actor, verb, statementObject);
        }

        public static IStatementBuilder Create(Guid id, Actor actor, Verb verb, StatementObject statementObject)
        {
            return new StatementBuilder(id, actor, verb, statementObject);
        }

        public static IStatementBuilder Create(Actor agent, IVerbBuilder verbBuilder, StatementObject statementObject)
        {
            return Create(agent, verbBuilder.Build(), statementObject);
        }

        public static IStatementBuilder Create(Actor agent, IVerbBuilder verbBuilder, ISubStatementBuilder subStatementBuilder)
        {
            return Create(agent, verbBuilder.Build(), subStatementBuilder.Build());
        }

        public static IStatementBuilder Create(Actor agent, IVerbBuilder verbBuilder, IActivityBuilder activityBuilder)
        {
            return Create(agent, verbBuilder.Build(), activityBuilder.Build());
        }

        public static IStatementBuilder Create(Guid id, Actor agent, IVerbBuilder verbBuilder, StatementObject statementObject)
        {
            return Create(id, agent, verbBuilder.Build(), statementObject);
        }

        public static IStatementBuilder Create(Guid id, Actor agent, IVerbBuilder verbBuilder, ISubStatementBuilder subStatementBuilder)
        {
            return Create(id, agent, verbBuilder.Build(), subStatementBuilder.Build());
        }

        public static IStatementBuilder Create(Guid id, Actor agent, IVerbBuilder verbBuilder, IActivityBuilder activityBuilder)
        {
            return Create(id, agent, verbBuilder.Build(), activityBuilder.Build());
        }
    }
}