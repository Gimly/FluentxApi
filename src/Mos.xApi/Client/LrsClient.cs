using Mos.xApi;
using Mos.xApi.Actors;
using Mos.xApi.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Mos.xApi.Client
{
    public class LrsClient
    {
        private static readonly HttpClient HttpClient;
        private readonly string _statementEndPoint;

        static LrsClient()
        {
            HttpClient = new HttpClient();
        }

        public LrsClient(Uri lrsBaseUrl, string statementEndPoint = "statements")
        {
            HttpClient.BaseAddress = lrsBaseUrl;
            _statementEndPoint = statementEndPoint;
        }

        public void SetBasicAuthentication(string username, string password)
        {
            HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}")));
        }

        /// <summary>
        /// Returns the statement identified by the statement id passed in parameter.
        /// If there is no statement linked to that identifier, the method will return null.
        /// </summary>
        /// <param name="statementId">A unique identifier linked to the statement to retrieve</param>
        /// <returns>The statement linked to the passed identifier if found, otherwise null</returns>
        public async Task<Statement> GetStatementAsync(Guid statementId)
        {
            using (var response = await HttpClient.GetAsync($"{_statementEndPoint}?statementId={statementId}"))
            {
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        return null;
                    }

                    throw new InvalidOperationException($"The statement with id {statementId} could not be found. {response.ReasonPhrase}");
                }

                var data = await response.Content.ReadAsStringAsync();
                return Statement.FromJson(data);
            }
        }

        public async Task<StatementResult> FindMoreStatements(Uri moreStatementsUri)
        {
            using (var response = await HttpClient.GetAsync(moreStatementsUri))
            {
                return await ParseStatementResultAsync(response);
            }
        }

        /// <summary>
        /// Returns the voided statement identified by the statement id passed in parameter. If there is no voided statement linked
        /// to that identifier, the method will return null.
        /// </summary>
        /// <param name="statementId">A unique identifier linked to the voided statement to retrieve</param>
        /// <returns>The voided statement linked to the passed identifier if found, otherwise null</returns>
        public async Task<Statement> GetVoidedStatementAsync(Guid statementId)
        {
            using (var response = await HttpClient.GetAsync($"{_statementEndPoint}?voidedStatementId={statementId}"))
            {
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        return null;
                    }

                    throw new InvalidOperationException($"The statement with id {statementId} could not be found. {response.ReasonPhrase}");
                }

                var data = await response.Content.ReadAsStringAsync();
                return Statement.FromJson(data);
            }
        }

        public async Task<StatementResult> FindStatementsAsync(StatementQuery query)
        {
            using (var response = await HttpClient.GetAsync($"{_statementEndPoint}{query.ToQueryString()}"))
            {
                return await ParseStatementResultAsync(response);
            }
        }

        private async Task<StatementResult> ParseStatementResultAsync(HttpResponseMessage response)
        {
            var data = await response.Content.ReadAsStringAsync();
            var result = JObject.Parse(data);

            var statementResult = new StatementResult();

            var statementsJson = result["statements"];
            if (statementsJson != null)
            {
                foreach (var statement in statementsJson)
                {
                    statementResult.AddStatement(Statement.FromJson(statement.ToString()));
                }
            }

            if (result["more"] != null)
            {
                statementResult.More = new Uri(result["more"].Value<string>());
            }

            return statementResult;
        }

        public async Task SendStatementAsync(Statement statement)
        {
            using (var content = new StringContent(statement.ToJson(), Encoding.UTF8, "application/json"))
            {
                using (var response = await HttpClient.PutAsync($"{_statementEndPoint}?statementId={statement.Id}", content))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        throw new InvalidOperationException($"Could not send the statement.\r\nLRS responded with: \r\n{(int)response.StatusCode} {response.ReasonPhrase}\r\n{responseContent}");
                    }
                }
            }
        }

        public async Task SendStatementAsync(IEnumerable<Statement> statements)
        {
            using (var content = new StringContent(JsonConvert.SerializeObject(statements.ToList(), JsonSerializerSettingsFactory.CreateSettings()), Encoding.UTF8, "application/json"))
            {
                using (var response = await HttpClient.PostAsync($"{_statementEndPoint}", content))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        throw new InvalidOperationException($"Could not send statements.\r\nLRS responded with: \r\n{(int)response.StatusCode} {response.ReasonPhrase}\r\n{responseContent}");
                    }
                }
            }
        }

        public async Task VoidStatementAsync(Guid statementId, Agent agent)
        {
            var voidingStatement = 
                Statement.Create(
                    agent, 
                    Verb.Create("http://adlnet.gov/expapi/verbs/voided")
                        .AddDisplay("en-US", "voided"), 
                    StatementObject.CreateStatementReference(statementId)).Build();
            await SendStatementAsync(voidingStatement);
        }
    }
}
