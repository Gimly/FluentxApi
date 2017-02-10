using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        /// <summary>
        /// Stores the document specified by the given "stateId" that exists in the context of the specified Activity, Agent, 
        /// and registration (if specified).
        /// </summary>
        /// <param name="stateId">The id for this state, within the given context.</param>
        /// <param name="activityId">The Activity id associated with this state.</param>
        /// <param name="agent">The Agent associated with this state.</param>
        /// <param name="document">The document to be stored for this state.</param>
        /// <param name="registration">The registration associated with this state.</param>
        /// <returns></returns>
        Task SaveStateAsync(string stateId, Uri activityId, Agent agent, byte[] document, Guid? registration = null);

        /// <summary>
        /// Stores the document specified by the given "stateId" that exists in the context of the specified Activity, Agent, 
        /// and registration (if specified).
        /// </summary>
        /// <param name="stateId">The id for this state, within the given context.</param>
        /// <param name="activityId">The Activity id associated with this state.</param>
        /// <param name="agent">The Agent associated with this state.</param>
        /// <param name="document">The document to be stored for this state.</param>
        /// <param name="registration">The registration associated with this state.</param>
        /// <returns></returns>
        Task SaveStateAsync(string stateId, Uri activityId, Agent agent, string document, Guid? registration = null);

        /// <summary>
        /// Fetches the document specified by the given "stateId" that exists in the context of the specified Activity,
        /// Agent and registration (if specified).
        /// </summary>
        /// <param name="stateId">The id for this state, within the given context.</param>
        /// <param name="activityId">The Activity id associated with this state.</param>
        /// <param name="agent">The Agent associated with this state.</param>
        /// <param name="registration">The registration associated with this state.</param>
        /// <returns></returns>
        Task<State> GetStateAsync(string stateId, Uri activityId, Agent agent, Guid? registration = null);
    }
}