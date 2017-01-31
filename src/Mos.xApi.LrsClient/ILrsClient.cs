using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mos.xApi.Data;
using Mos.xApi.Data.Actors;

namespace Mos.xApi.LrsClient
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
    }
}