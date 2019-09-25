using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mos.xApi.Actors;

namespace Mos.xApi.Client
{
    /// <summary>
    /// Interface that defines all actions that can be made to a LRS client.
    /// </summary>
    public interface ILrsClient
    {
        /// <summary>
        /// Retrieves more statements from the same query that returned the given
        /// more statement Uri.
        /// </summary>
        /// <param name="moreStatementsUri">The URI returned by the LRS to retrieve more statements for the same query.</param>
        /// <returns>The resulting statements.</returns>
        Task<StatementResult> FindMoreStatementsAsync(Uri moreStatementsUri);

        /// <summary>
        /// Fetches all the statements corresponding to the specified query.
        /// </summary>
        /// <param name="query">The query defining the statements to retrieve</param>
        /// <returns>The list of all statements corresponding to the query.</returns>
        Task<StatementResult> FindStatementsAsync(StatementQuery query);

        /// <summary>
        /// Returns the statement identified by the statement id passed in parameter.
        /// If there is no statement linked to that identifier, the method will return null.
        /// </summary>
        /// <param name="statementId">A unique identifier linked to the statement to retrieve</param>
        /// <returns>The statement linked to the passed identifier if found, otherwise null</returns>
        Task<Statement> GetStatementAsync(Guid statementId);

        /// <summary>
        /// Returns the voided statement identified by the statement id passed in parameter. If there is no voided statement linked
        /// to that identifier, the method will return null.
        /// </summary>
        /// <param name="statementId">A unique identifier linked to the voided statement to retrieve</param>
        /// <returns>The voided statement linked to the passed identifier if found, otherwise null</returns>
        Task<Statement> GetVoidedStatementAsync(Guid statementId);

        /// <summary>
        /// Stores a statement to the LRS
        /// </summary>
        /// <param name="statement">The statement to store</param>
        Task SendStatementAsync(Statement statement);

        /// <summary>
        /// Stores a list of statements to the LRS
        /// </summary>
        /// <param name="statements">The list of statements to store</param>
        Task SendStatementAsync(IEnumerable<Statement> statements);

        /// <summary>
        /// Configures the LRS client to use basic authentication using the passed username and password.
        /// <para>
        /// Both information will be encoded to base 64 and added to the authentication HTTP header.
        /// </para>
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        void SetBasicAuthentication(string username, string password);

        /// <summary>
        /// Voids a statement by sending a statement containing the void verb.
        /// </summary>
        /// <param name="statementId">The id of the statement to void</param>
        /// <param name="agent">The agent that voids the statement</param>
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
        /// <param name="contentType">The content type that defines the type of the passed document</param>
        /// <param name="registration">The registration associated with this state.</param>
        Task SaveStateAsync(string stateId, Uri activityId, Agent agent, byte[] document, string contentType, Guid? registration = null);

        /// <summary>
        /// Stores the document specified by the given "stateId" that exists in the context of the specified Activity, Agent, 
        /// and registration (if specified).
        /// </summary>
        /// <param name="stateId">The id for this state, within the given context.</param>
        /// <param name="activityId">The Activity id associated with this state.</param>
        /// <param name="agent">The Agent associated with this state.</param>
        /// <param name="document">The document to be stored for this state.</param>
        /// <param name="contentType">The content type that defines the type of the passed document</param>
        /// <param name="registration">The registration associated with this state.</param>
        Task SaveStateAsync(string stateId, Uri activityId, Agent agent, string document, string contentType, Guid? registration = null);

        /// <summary>
        /// Fetches the document specified by the given "stateId" that exists in the context of the specified Activity,
        /// Agent and registration (if specified).
        /// </summary>
        /// <param name="stateId">The id for this state, within the given context.</param>
        /// <param name="activityId">The Activity id associated with this state.</param>
        /// <param name="agent">The Agent associated with this state.</param>
        /// <param name="registration">The registration associated with this state.</param>
        /// <returns>The state retrieved from the LRS</returns>
        Task<State> GetStateAsync(string stateId, Uri activityId, Agent agent, Guid? registration = null);

        /// <summary>
        /// Deletes the document specified by the given "stateId" that exists in the context of the specified Activity, Agent, 
        /// and registration (if specified).
        /// </summary>
        /// <param name="stateId">The id for this state, within the given context.</param>
        /// <param name="activityId">The Activity id associated with this state.</param>
        /// <param name="agent">The Agent associated with this state.</param>
        /// <param name="registration">The registration associated with this state.</param>
        Task DeleteStateAsync(string stateId, Uri activityId, Agent agent, Guid? registration = null);
    }
}