using System;
using System.Collections.Generic;

namespace Mos.xApi.Client
{
    /// <summary>
    /// This class represents the results of a query run against
    /// a LRS.
    /// </summary>
    public class StatementResult
    {
        /// <summary>
        /// The list of statements returned from the query.
        /// </summary>
        private readonly List<Statement> _statements;

        /// <summary>
        /// Initializes a new instance of a StatementResult class.
        /// </summary>
        internal StatementResult()
        {
            _statements = new List<Statement>();
        }

        /// <summary>
        /// Gets the list of all statements corresponding to the query.
        /// </summary>
        public IEnumerable<Statement> Statements => _statements;

        /// <summary>
        /// Gets the URI that allows to retrieve more statements corresponding
        /// to the same query.
        /// </summary>
        public Uri More { get; internal set; }

        /// <summary>
        /// Adds a statement to the result.
        /// </summary>
        /// <param name="statement">The statement that must be added to the result.</param>
        internal void AddStatement(Statement statement)
        {
            _statements.Add(statement);
        }
    }
}