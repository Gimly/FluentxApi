using Mos.xApi.Actors;
using Mos.xApi.Builders;
using Mos.xApi.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mos.xApi
{
    /// <summary>
    /// Statements are the evidence for any sort of experience or event which is to be tracked in xAPI. This class defines 
    /// an object-oriented representation of the experience API's JSON statement.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Statement
    {
        /// <summary>
        /// Private constructor used by Json.Net's deserializing process
        /// </summary>
        private Statement() { }

        /// <summary>
        /// Creates a new instance of a Statement. The statement's ID is automatically
        /// generated using a GUID.
        /// </summary>
        /// <param name="actor">Whom the Statement is about, as an Agent or Group Object.</param>
        /// <param name="verb">Action taken by the Actor.</param>
        /// <param name="statementObject">Activity, Agent, or another Statement that is the Object of the Statement.</param>
        /// <param name="result">Result Object, further details representing a measured outcome.</param>
        /// <param name="context">Context that gives the Statement more meaning. Examples: a team the Actor is working with, altitude at which a scenario was attempted in a flight simulator.</param>
        /// <param name="timestamp">Timestamp of when the events described within this Statement occurred. Set by the LRS if not provided.</param>
        /// <param name="attachments">Headers for Attachments to the Statement</param>
        /// <param name="authority">Agent or Group who is asserting this Statement is true. Verified by the LRS based on authentication. Set by LRS if not provided or if a strong trust relationship between the Learning Record Provider and LRS has not been established.</param>
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

        /// <summary>
        /// Creates a new instance of a Statement.
        /// </summary>
        /// <param name="id">Unique identifier for the statement.</param>
        /// <param name="actor">Whom the Statement is about, as an Agent or Group Object.</param>
        /// <param name="verb">Action taken by the Actor.</param>
        /// <param name="statementObject">Activity, Agent, or another Statement that is the Object of the Statement.</param>
        /// <param name="result">Result Object, further details representing a measured outcome.</param>
        /// <param name="context">Context that gives the Statement more meaning. Examples: a team the Actor is working with, altitude at which a scenario was attempted in a flight simulator.</param>
        /// <param name="timestamp">Timestamp of when the events described within this Statement occurred. Set by the LRS if not provided.</param>
        /// <param name="attachments">Headers for Attachments to the Statement</param>
        /// <param name="authority">Agent or Group who is asserting this Statement is true. Verified by the LRS based on authentication. Set by LRS if not provided or if a strong trust relationship between the Learning Record Provider and LRS has not been established.</param>
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
            Actor = actor ?? throw new ArgumentNullException(nameof(actor));
            Verb = verb ?? throw new ArgumentNullException(nameof(verb));
            StatementObject = statementObject ?? throw new ArgumentNullException(nameof(statementObject));

            Id = id;
            Context = context;
            Result = result;

            Timestamp = timestamp;
            Authority = authority;

            if (attachments != null && attachments.Any())
            {
                Attachments = attachments;
            }
        }

        /// <summary>
        /// Deserializes a Statement from a json string.
        /// </summary>
        /// <param name="jsonString">The json representation of a statement.</param>
        /// <returns>The deserialized statement</returns>
        public static Statement FromJson(string jsonString)
        {
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                throw new ArgumentNullException(nameof(jsonString));
            }

            try
            {
                return JsonConvert.DeserializeObject<Statement>(jsonString, JsonSerializerSettingsFactory.CreateSettings());
            }
            catch (JsonReaderException ex)
            {
                throw new ArgumentException(
                    "Unable to deserialize passed string, check inner exception for more details",
                    nameof(jsonString),
                    ex);
            }
            catch (JsonSerializationException ex)
            {
                throw new ArgumentException(
                    "The passed Json string is not a valid Statement. Please check inner exception for more details",
                    nameof(jsonString),
                    ex);
            }
        }

        /// <summary>
        /// Gets whom the Statement is about, as an Agent or Group Object.
        /// </summary>
        [JsonProperty("actor", Order = 2, Required = Required.Always)]
        public Actor Actor { get; private set; }

        /// <summary>
        /// Gets headers for Attachments to the Statement
        /// </summary>
        [JsonProperty("attachments", Order = 10, NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Attachment> Attachments { get; private set; }

        /// <summary>
        /// Gets an agent or Group who is asserting this Statement is true. Verified by the LRS based on authentication. 
        /// <para>Set by LRS if not provided or if a strong trust relationship between the Learning Record Provider and LRS has not been established.</para>
        /// </summary>
        [JsonProperty("authority", Order = 9, NullValueHandling = NullValueHandling.Ignore)]
        public Actor Authority { get; private set; }

        /// <summary>
        /// Gets context that gives the Statement more meaning. 
        /// <para>Examples: a team the Actor is working with, altitude at which a scenario was attempted in a flight simulator.</para>
        /// </summary>
        [JsonProperty("context", Order = 6, NullValueHandling = NullValueHandling.Ignore)]
        public Context Context { get; private set; }

        /// <summary>
        /// Gets the unique identifier representing the Statement.
        /// </summary>
        [JsonProperty("id", Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets further details representing a measured outcome.
        /// </summary>
        [JsonProperty("result", Order = 5, NullValueHandling = NullValueHandling.Ignore)]
        public Result Result { get; private set; }

        /// <summary>
        /// Gets an Activity, Agent, or another Statement that is the Object of the Statement.
        /// </summary>
        [JsonProperty("object", Order = 4, Required = Required.Always)]
        public StatementObject StatementObject { get; private set; }

        /// <summary>
        /// Gets the timestamp of when this Statement was recorded. Set by LRS.
        /// </summary>
        [JsonProperty("stored", Order = 8, NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Stored { get; private set; }

        /// <summary>
        /// Gets the timestamp of when the events described within this Statement occurred. Set by the LRS if not provided.
        /// </summary>
        [JsonProperty("timestamp", Order = 7, NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Timestamp { get; private set; }

        /// <summary>
        /// Gets the action taken by the Actor.
        /// </summary>
        [JsonProperty("verb", Order = 3, Required = Required.Always)]
        public Verb Verb { get; private set; }

        /// <summary>
        /// Returns the json representation of the Statement object.
        /// </summary>
        /// <param name="prettyPrint">If set to true, the json will be with an indented formatting.</param>
        /// <returns>The json representation of the statement.</returns>
        public string ToJson(bool prettyPrint = false)
        {
            return JsonConvert.SerializeObject(this, prettyPrint ? Formatting.Indented : Formatting.None, JsonSerializerSettingsFactory.CreateSettings());
        }

        /// <summary>
        /// Sets the stored date to the passed DateTime.
        /// <para>This should normally only be used by a LRS.</para>
        /// </summary>
        /// <param name="stored">The date and time where the statement was stored in the LRS</param>
        public void SetStored(DateTime stored)
        {
            Stored = stored;
        }

        /// <summary>
        /// Starts the creation of a Statement object with the mandatory objects passed. The Statement ID is created
        /// automatically through the creation of a new Guid.
        /// <para>Actor, Verb and StatementObject are mandatory, the other optional properties are
        /// passed through the returned Statement builder.</para>
        /// </summary>
        /// <param name="actor">Whom the Statement is about, as an Agent or Group Object.</param>
        /// <param name="verb">Action taken by the Actor.</param>
        /// <param name="statementObject">Activity, Agent, or another Statement that is the Object of the Statement.</param>
        /// <returns>A builder class that allows to continue to set optional properties to the Statement.</returns>
        public static IStatementBuilder Create(Actor actor, Verb verb, StatementObject statementObject)
        {
            return new StatementBuilder(actor, verb, statementObject);
        }

        /// <summary>
        /// Starts the creation of a Statement object with the mandatory objects passed as well as a specific Guid
        /// for the statement.
        /// <para>Actor, Verb and StatementObject are mandatory, the other optional properties are
        /// passed through the returned Statement builder.</para>
        /// </summary>
        /// <param name="id">The unique identifier for the Statement.</param>
        /// <param name="actor">Whom the Statement is about, as an Agent or Group Object.</param>
        /// <param name="verb">Action taken by the Actor.</param>
        /// <param name="statementObject">Activity, Agent, or another Statement that is the Object of the Statement.</param>
        /// <returns>A builder class that allows to continue to set optional properties to the Statement.</returns>
        public static IStatementBuilder Create(Guid id, Actor actor, Verb verb, StatementObject statementObject)
        {
            return new StatementBuilder(id, actor, verb, statementObject);
        }

        /// <summary>
        /// Starts the creation of a Statement object with the mandatory objects passed. The Statement ID is created
        /// automatically through the creation of a new Guid.
        /// <para>Actor, Verb and StatementObject are mandatory, the other optional properties are
        /// passed through the returned Statement builder.</para>
        /// </summary>
        /// <param name="actor">Whom the Statement is about, as an Agent or Group Object.</param>
        /// <param name="verbBuilder">An instance of a verb builder class.</param>
        /// <param name="statementObject">Activity, Agent, or another Statement that is the Object of the Statement.</param>
        /// <returns>A builder class that allows to continue to set optional properties to the Statement.</returns>
        public static IStatementBuilder Create(Actor actor, IVerbBuilder verbBuilder, StatementObject statementObject)
        {
            return Create(actor, verbBuilder.Build(), statementObject);
        }

        /// <summary>
        /// Starts the creation of a Statement object with the mandatory objects passed. The Statement ID is created
        /// automatically through the creation of a new Guid.
        /// <para>Actor, Verb and StatementObject are mandatory, the other optional properties are
        /// passed through the returned Statement builder.</para>
        /// </summary>
        /// <param name="actor">Whom the Statement is about, as an Agent or Group Object.</param>
        /// <param name="verbBuilder">An instance of a verb builder class.</param>
        /// <param name="subStatementBuilder">An instance of a sub-statement builder class to create a statement who's object is a substatement.</param>
        /// <returns>A builder class that allows to continue to set optional properties to the Statement.</returns>
        public static IStatementBuilder Create(Actor actor, IVerbBuilder verbBuilder, ISubStatementBuilder subStatementBuilder)
        {
            return Create(actor, verbBuilder.Build(), subStatementBuilder.Build());
        }

        /// <summary>
        /// Starts the creation of a Statement object with the mandatory objects passed. The Statement ID is created
        /// automatically through the creation of a new Guid.
        /// <para>Actor, Verb and StatementObject are mandatory, the other optional properties are
        /// passed through the returned Statement builder.</para>
        /// </summary>
        /// <param name="actor">Whom the Statement is about, as an Agent or Group Object.</param>
        /// <param name="verbBuilder">An instance of a verb builder class.</param>
        /// <param name="activityBuilder">An instance of an activity builder class to create a statement who's object is an activity.</param>
        /// <returns>A builder class that allows to continue to set optional properties to the Statement.</returns>
        public static IStatementBuilder Create(Actor actor, IVerbBuilder verbBuilder, IActivityBuilder activityBuilder)
        {
            return Create(actor, verbBuilder.Build(), activityBuilder.Build());
        }

        /// <summary>
        /// Starts the creation of a Statement object with the mandatory objects passed. The Statement ID is defined
        /// as a parameter passed to the method.
        /// <para>Actor, Verb and StatementObject are mandatory, the other optional properties are
        /// passed through the returned Statement builder.</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="actor">Whom the Statement is about, as an Agent or Group Object.</param>
        /// <param name="verbBuilder">An instance of a verb builder class.</param>
        /// <param name="statementObject">Activity, Agent, or another Statement that is the Object of the Statement.</param>
        /// <returns>A builder class that allows to continue to set optional properties to the Statement.</returns>
        public static IStatementBuilder Create(Guid id, Actor actor, IVerbBuilder verbBuilder, StatementObject statementObject)
        {
            return Create(id, actor, verbBuilder.Build(), statementObject);
        }

        /// <summary>
        /// Starts the creation of a Statement object with the mandatory objects passed. The Statement ID is defined
        /// as a parameter passed to the method.
        /// <para>Actor, Verb and StatementObject are mandatory, the other optional properties are
        /// passed through the returned Statement builder.</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="actor">Whom the Statement is about, as an Agent or Group Object.</param>
        /// <param name="verbBuilder">An instance of a verb builder class.</param>
        /// <param name="subStatementBuilder">An instance of a sub-statement builder class to create a statement who's object is a substatement.</param>
        /// <returns>A builder class that allows to continue to set optional properties to the Statement.</returns>
        public static IStatementBuilder Create(Guid id, Actor actor, IVerbBuilder verbBuilder, ISubStatementBuilder subStatementBuilder)
        {
            return Create(id, actor, verbBuilder.Build(), subStatementBuilder.Build());
        }

        /// <summary>
        /// Starts the creation of a Statement object with the mandatory objects passed. The Statement ID is defined
        /// as a parameter passed to the method.
        /// <para>Actor, Verb and StatementObject are mandatory, the other optional properties are
        /// passed through the returned Statement builder.</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="actor">Whom the Statement is about, as an Agent or Group Object.</param>
        /// <param name="verbBuilder">An instance of a verb builder class.</param>
        /// <param name="activityBuilder">An instance of an activity builder class to create a statement who's object is an activity.</param>
        /// <returns>A builder class that allows to continue to set optional properties to the Statement.</returns>
        public static IStatementBuilder Create(Guid id, Actor actor, IVerbBuilder verbBuilder, IActivityBuilder activityBuilder)
        {
            return Create(id, actor, verbBuilder.Build(), activityBuilder.Build());
        }
    }
}