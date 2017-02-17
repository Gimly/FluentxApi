using System;
using System.Collections.Generic;
using Mos.xApi.Actors;
using System.Linq;

namespace Mos.xApi.Objects
{
    /// <summary>
    /// Builder class used to fluently create a SubStatement instance.
    /// </summary>
    internal class SubStatementBuilder : ISubStatementBuilder
    {
        /// <summary>
        /// Whom the SubStatement is about, as an Agent or Group Object.
        /// </summary>
        private Actor _actor;

        /// <summary>
        /// Headers for Attachments to the SubStatement
        /// </summary>
        private readonly List<Attachment> _attachments;

        /// <summary>
        /// Context that gives the SubStatement more meaning. Examples: a team the Actor is working with, altitude at which a scenario was attempted in a flight simulator.
        /// </summary>
        private Context _context;

        /// <summary>
        /// Result Object, further details representing a measured outcome.
        /// </summary>
        private Result _result;

        /// <summary>
        /// Activity or Agent that is the Object of the SubStatement.
        /// </summary>
        private StatementObject _statementObject;

        /// <summary>
        /// Timestamp of when the events described within this SubStatement occurred. Set by the LRS if not provided.
        /// </summary>
        private DateTime? _timeStamp;

        /// <summary>
        /// Action taken by the Actor.
        /// </summary>
        private Verb _verb;

        /// <summary>
        /// Initializes a new instance of the SubStatementBuilder class, passing the
        /// mandatory objects.
        /// </summary>
        /// <param name="actor">Whom the Statement is about, as an Agent or Group Object.</param>
        /// <param name="verb">Action taken by the Actor.</param>
        /// <param name="statementObject"> Activity or Agent that is the Object of the SubStatement.</param>
        public SubStatementBuilder(Actor actor, Verb verb, StatementObject statementObject)
        {
            _actor = actor;
            _verb = verb;
            _statementObject = statementObject;

            _attachments = new List<Attachment>();
        }

        /// <summary>
        /// Adds an attachment to the SubStatement
        /// </summary>
        /// <param name="attachment">The attachment to be added</param>
        /// <returns>The substatement builder, to continue the fluent configuration.</returns>
        public ISubStatementBuilder AddAttachment(Attachment attachment)
        {
            _attachments.Add(attachment);
            return this;
        }

        /// <summary>
        /// Adds a list of attachments to the SubStatement
        /// </summary>
        /// <param name="attachment">The list of attachments to be added</param>
        /// <returns>The substatement builder, to continue the fluent configuration.</returns>
        public ISubStatementBuilder AddAttachments(IEnumerable<Attachment> attachment)
        {
            _attachments.AddRange(attachment);
            return this;
        }

        /// <summary>
        /// Creates the SubStatement object constructed from the
        /// fluent configuration.
        /// </summary>
        /// <returns>The SubStatement object constructed.</returns>
        public SubStatement Build() =>
            new SubStatement(
                _actor,
                _verb,
                _statementObject,
                _result,
                _context,
                _timeStamp,
                _attachments.Any() ? _attachments : null);

        /// <summary>
        /// Specifies a context to the SubStatement.
        /// </summary>
        /// <param name="context">Context that gives the Statement more meaning. Examples: a team the Actor is working with, altitude at which a scenario was attempted in a flight simulator.</param>
        /// <returns>The substatement builder, to continue the fluent configuration.</returns>
        public ISubStatementBuilder WithContext(Context context)
        {
            _context = context;
            return this;
        }

        /// <summary>
        /// Specifies a result to the SubStatement.
        /// </summary>
        /// <param name="result">Result Object, further details representing a measured outcome.</param>
        /// <returns>The substatement builder, to continue the fluent configuration.</returns>
        public ISubStatementBuilder WithResult(Result result)
        {
            _result = result;
            return this;
        }

        /// <summary>
        /// Adds a time stamp to the SubStatement.
        /// </summary>
        /// <param name="timeStamp">Timestamp of when the events described within this Statement occurred. Set by the LRS if not provided.</param>
        /// <returns>The substatement builder, to continue the fluent configuration.</returns>
        public ISubStatementBuilder WithTimeStamp(DateTime timeStamp)
        {
            _timeStamp = timeStamp;
            return this;
        }
    }
}