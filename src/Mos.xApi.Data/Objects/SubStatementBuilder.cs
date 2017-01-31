using System;
using System.Collections.Generic;
using Mos.xApi.Data.Actors;
using System.Linq;

namespace Mos.xApi.Data.Objects
{
    internal class SubStatementBuilder : ISubStatementBuilder
    {
        private Actor _actor;
        private readonly List<Attachment> _attachments;
        private Context _context;
        private Result _result;
        private StatementObject _statementObject;
        private DateTime? _timeStamp;
        private Verb _verb;

        public SubStatementBuilder(Actor actor, Verb verb, StatementObject statementObject)
        {
            _actor = actor;
            _verb = verb;
            _statementObject = statementObject;

            _attachments = new List<Attachment>();
        }

        public ISubStatementBuilder AddAttachment(Attachment attachment)
        {
            _attachments.Add(attachment);
            return this;
        }

        public ISubStatementBuilder AddAttachments(IEnumerable<Attachment> attachment)
        {
            _attachments.AddRange(attachment);
            return this;
        }

        public SubStatement Build() =>
            new SubStatement(
                _actor,
                _verb,
                _statementObject,
                _result,
                _context,
                _timeStamp,
                _attachments.Any() ? _attachments : null);

        public ISubStatementBuilder WithContext(Context context)
        {
            _context = context;
            return this;
        }

        public ISubStatementBuilder WithResult(Result result)
        {
            _result = result;
            return this;
        }

        public ISubStatementBuilder WithTimeStamp(DateTime timeStamp)
        {
            _timeStamp = timeStamp;
            return this;
        }
    }
}