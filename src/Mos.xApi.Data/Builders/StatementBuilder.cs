using Mos.xApi.Actors;
using Mos.xApi.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mos.xApi.Builders
{
    internal class StatementBuilder : IStatementBuilder
    {
        private readonly List<Attachment> _attachments;
        private readonly Guid? _id;
        private Actor _actor;
        private Actor _authority;
        private Context _context;
        private Result _result;
        private StatementObject _statementObject;
        private DateTime? _timestamp;
        private Verb _verb;

        public StatementBuilder(Actor actor, Verb verb, StatementObject statementObject)
        {
            _actor = actor;
            _verb = verb;
            _statementObject = statementObject;

            _attachments = new List<Attachment>();
        }

        public StatementBuilder(Guid id, Actor actor, Verb verb, StatementObject statementObject) : this(actor, verb, statementObject)
        {
            _id = id;
        }

        public IStatementBuilder AddAttachment(Attachment attachment)
        {
            _attachments.Add(attachment);
            return this;
        }

        public IStatementBuilder AddAttachments(IEnumerable<Attachment> attachment)
        {
            _attachments.AddRange(attachment);
            return this;
        }

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

        public IStatementBuilder WithAuthority(Actor authority)
        {
            _authority = authority;
            return this;
        }

        public IStatementBuilder WithContext(Context context)
        {
            _context = context;
            return this;
        }

        public IStatementBuilder WithContext(IContextBuilder contextBuilder) => WithContext(contextBuilder.Build());

        public IStatementBuilder WithResult(Result result)
        {
            _result = result;
            return this;
        }

        public IStatementBuilder WithResult(IResultBuilder resultBuilder) => WithResult(resultBuilder.Build());

        public IStatementBuilder WithTimeStamp(DateTime timeStamp)
        {
            _timestamp = timeStamp;
            return this;
        }
    }
}