using Mos.xApi.Actors;
using Mos.xApi.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mos.xApi.Builders
{
    /// <summary>
    /// Class that defines a builder used to simplify the creation of a Statement, 
    /// in a fluent interface manner.
    /// </summary>
    internal class StatementBuilder : IStatementBuilder
    {
        /// <summary>
        /// Headers for Attachments to the Statement
        /// </summary>
        private readonly List<Attachment> _attachments;

        /// <summary>
        /// Unique identifier for the Statement.
        /// </summary>
        private readonly Guid? _id;

        /// <summary>
        /// Whom the Statement is about, as an Agent or Group Object.
        /// </summary>
        private readonly Actor _actor;

        /// <summary>
        /// Agent or Group who is asserting this Statement is true. 
        /// </summary>
        private Actor _authority;

        /// <summary>
        /// Context that gives the Statement more meaning.
        /// </summary>
        private Context _context;

        /// <summary>
        /// Result Object, further details representing a measured outcome.	
        /// </summary>
        private Result _result;

        /// <summary>
        /// Activity, Agent, or another Statement that is the Object of the Statement.
        /// </summary>
        private readonly StatementObject _statementObject;

        /// <summary>
        /// Timestamp of when the events described within this Statement occurred.
        /// </summary>
        private DateTime? _timestamp;

        /// <summary>
        /// Action taken by the Actor.
        /// </summary>
        private readonly Verb _verb;

        /// <summary>
        /// Initializes a new instance of a StatementBuilder class.
        /// </summary>
        /// <param name="actor">Whom the Statement is about, as an Agent or Group Object.</param>
        /// <param name="verb">Action taken by the Actor.</param>
        /// <param name="statementObject">Activity, Agent, or another Statement that is the Object of the Statement.</param>
        public StatementBuilder(Actor actor, Verb verb, StatementObject statementObject)
        {
            _actor = actor;
            _verb = verb;
            _statementObject = statementObject;

            _attachments = new List<Attachment>();
        }

        /// <summary>
        /// Initializes a new instance of a StatementBuilder class.
        /// </summary>
        /// <param name="id">The Statement unique identifier.</param>
        /// <param name="actor">Whom the Statement is about, as an Agent or Group Object.</param>
        /// <param name="verb">Action taken by the Actor.</param>
        /// <param name="statementObject">Activity, Agent, or another Statement that is the Object of the Statement.</param>
        public StatementBuilder(Guid id, Actor actor, Verb verb, StatementObject statementObject) : 
            this(actor, verb, statementObject)
        {
            _id = id;
        }

        /// <summary>
        /// Adds an Attachment to the Statement
        /// </summary>
        /// <param name="attachment">The attachment to add</param>
        /// <returns>The statement builder, to continue the fluent configuration.</returns>
        public IStatementBuilder AddAttachment(Attachment attachment)
        {
            _attachments.Add(attachment);
            return this;
        }

        /// <summary>
        /// Adds a list of Attachment to the Statement
        /// </summary>
        /// <param name="attachments">The list of attachments to add</param>
        /// <returns>The statement builder, to continue the fluent configuration.</returns>
        public IStatementBuilder AddAttachments(IEnumerable<Attachment> attachments)
        {
            _attachments.AddRange(attachments);
            return this;
        }

        /// <summary>
        /// Creates the Statement object constructed from the
        /// fluent configuration.
        /// </summary>
        /// <returns>The Statement object constructed.</returns>
        public Statement Build()
        {
            if (_id.HasValue)
            {
                return new Statement(
                        _id.Value,
                        _actor,
                        _verb,
                        _statementObject,
                        _result,
                        _context,
                        _timestamp,
                        _attachments.Any() ? _attachments : null,
                        _authority);
            }
            return new Statement(
                        _actor,
                        _verb,
                        _statementObject,
                        _result,
                        _context,
                        _timestamp,
                        _attachments.Any() ? _attachments : null,
                        _authority);
        }

        /// <summary>
        /// Sets an Agent or Group who is asserting this Statement is true. Verified by the LRS based on authentication. 
        /// <para>Set by LRS if not provided or if a strong trust relationship between the Learning Record Provider and LRS has not been established.</para>
        /// </summary>
        /// <param name="authority">The Agent or Group who is asserting the Statement is true.</param>
        /// <returns>The statement builder, to continue the fluent configuration.</returns>
        public IStatementBuilder WithAuthority(Actor authority)
        {
            _authority = authority;
            return this;
        }

        /// <summary>
        /// Sets the Context that gives the Statement more meaning. 
        /// <para>Examples: a team the Actor is working with, altitude at which a scenario was attempted in a flight simulator.</para>
        /// </summary>
        /// <param name="context">The context of the Statement.</param>
        /// <returns>The statement builder, to continue the fluent configuration.</returns>
        public IStatementBuilder WithContext(Context context)
        {
            _context = context;
            return this;
        }

        /// <summary>
        /// Sets the Context that gives the Statement more meaning. 
        /// <para>Examples: a team the Actor is working with, altitude at which a scenario was attempted in a flight simulator.</para>
        /// </summary>
        /// <param name="contextBuilder">A context builder.</param>
        /// <returns>The statement builder, to continue the fluent configuration.</returns>
        public IStatementBuilder WithContext(IContextBuilder contextBuilder) => WithContext(contextBuilder.Build());

        /// <summary>
        /// Sets the Result Object, further details representing a measured outcome.
        /// </summary>
        /// <param name="result">The result object</param>
        /// <returns>The statement builder, to continue the fluent configuration.</returns>
        public IStatementBuilder WithResult(Result result)
        {
            _result = result;
            return this;
        }

        /// <summary>
        /// Sets the Result Object, further details representing a measured outcome.
        /// </summary>
        /// <param name="resultBuilder">A result builder.</param>
        /// <returns>The statement builder, to continue the fluent configuration.</returns>
        public IStatementBuilder WithResult(IResultBuilder resultBuilder) => WithResult(resultBuilder.Build());

        /// <summary>
        /// Sets the Timestamp of when the events described within this Statement occurred. Set by the LRS if not provided.
        /// </summary>
        /// <param name="timeStamp">The timestamp of when the events occured.</param>
        /// <returns>The statement builder, to continue the fluent configuration.</returns>
        public IStatementBuilder WithTimeStamp(DateTime timeStamp)
        {
            _timestamp = timeStamp;
            return this;
        }
    }
}