using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mos.xApi;
using Mos.xApi.Actors;

namespace Mos.xApi.Client
{
    public interface ILrsClient
    {
        Task<StatementResult> FindMoreStatements(Uri moreStatementsUri);
        Task<StatementResult> FindStatementsAsync(StatementQuery query);
        Task<Statement> GetStatementAsync(Guid statementId);
        Task<Statement> GetVoidedStatementAsync(Guid statementId);
        Task SendStatementAsync(Statement statement);
        Task SendStatementAsync(IEnumerable<Statement> statements);
        void SetBasicAuthentication(string username, string password);
        Task VoidStatementAsync(Guid statementId, Agent agent);

        /// <summary>
        /// Fetches State ids of all state data for this context (Activity + Agent (+ registration if specified).
        /// <para>
        /// If since parameter is specified, this is limited to entries that have been stored or updated since the
        /// specified timestamp (exclusive).
        /// </para>
        /// </summary>
        /// <param name="activityId">The Activity id associated with these states.</param>
        /// <param name="agent">The Agent associated with these states.</param>
        /// <param name="registration">The Registration associated with these states.</param>
        /// <param name="since">Only ids of states stored since the specified DateTime (exclusive) are returned.</param>
        /// <returns>The list of State ids for all state data for this context.</returns>
        Task<IEnumerable<string>> FindStateIdsAsync(Uri activityId, Agent agent, Guid? registration = null, DateTime? since = null);

    }
}