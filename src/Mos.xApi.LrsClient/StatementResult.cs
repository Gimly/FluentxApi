using Mos.xApi.Data;
using System;
using System.Collections.Generic;

namespace Mos.xApi.LrsClient
{
    public class StatementResult
    {
        private List<Statement> _statements;

        internal StatementResult()
        {
            _statements = new List<Statement>();
        }

        public IEnumerable<Statement> Statement => _statements;

        public Uri More { get; internal set; }

        internal void AddStatement(Statement statement)
        {
            _statements.Add(statement);
        }
    }
}