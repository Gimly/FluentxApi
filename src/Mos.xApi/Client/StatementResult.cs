using System;
using System.Collections.Generic;

namespace Mos.xApi.Client
{
    public class StatementResult
    {
        private List<Statement> _statements;

        internal StatementResult()
        {
            _statements = new List<Statement>();
        }

        public IEnumerable<Statement> Statements => _statements;

        public Uri More { get; internal set; }

        internal void AddStatement(Statement statement)
        {
            _statements.Add(statement);
        }
    }
}