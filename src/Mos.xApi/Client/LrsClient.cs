using Mos.xApi.Actors;
using Mos.xApi.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Mos.xApi.Client
{
    public class LrsClient : ILrsClient
    {
        private static readonly HttpClient HttpClient;
        private readonly string _statementEndPoint;

        static LrsClient()
        {
            HttpClient = new HttpClient();
        }

        public LrsClient(Uri lrsBaseUrl, string statementEndPoint = "statements", string xApiVersion = "1.0.0")
        {
            HttpClient.BaseAddress = lrsBaseUrl;
            _statementEndPoint = statementEndPoint;

            if (HttpClient.DefaultRequestHeaders.Any(x => x.Key == "X-Experience-API-Version"))
            {
                HttpClient.DefaultRequestHeaders.Remove("X-Experience-API-Version");
            }

            HttpClient.DefaultRequestHeaders.Add("X-Experience-API-Version", xApiVersion);
        }

        /// <summary>
        /// Configures the LRS client to use basic authentication using the passed username and password.
        /// <para>
        /// Both information will be encoded to base 64 and added to the authentication HTTP header.
        /// </para>
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
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
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return null;
                    }

                    throw new InvalidOperationException($"The statement with id {statementId} could not be found. {response.ReasonPhrase}");
                }

                var data = await response.Content.ReadAsStringAsync();
                return Statement.FromJson(data);
            }
        }

        /// <summary>
        /// Retrieves more statements from the same query that returned the given
        /// more statement Uri.
        /// </summary>
        /// <param name="moreStatementsUri">The URI returned by the LRS to retrieve more statements for the same query.</param>
        /// <returns>The resulting statements.</returns>
        public async Task<StatementResult> FindMoreStatementsAsync(Uri moreStatementsUri)
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

        /// <summary>
        /// Fetches all the statements corresponsing to the specified query.
        /// </summary>
        /// <param name="query">The query defining the statements to retrieve</param>
        /// <returns>The list of all statements corresponding to the query.</returns>
        public async Task<StatementResult> FindStatementsAsync(StatementQuery query)
        {
            using (var response = await HttpClient.GetAsync($"{_statementEndPoint}{query.ToQueryString()}"))
            {
                return await ParseStatementResultAsync(response);
            }
        }

        /// <summary>
        /// Stores a statement to the LRS
        /// </summary>
        /// <param name="statement">The statement to store</param>
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

        /// <summary>
        /// Stores a list of statements to the LRS
        /// </summary>
        /// <param name="statements">The list of statements to store</param>
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
        
        /// <summary>
        /// Voids a statement by sending a statement containing the void verb.
        /// </summary>
        /// <param name="statementId">The id of the statement to void</param>
        /// <param name="agent">The agent that voids the statement</param>
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
        public async Task<IEnumerable<string>> FindStateIdsAsync(Uri activityId, Agent agent, Guid? registration = default(Guid?), DateTime? since = default(DateTime?))
        {
            var activityUrlEncoded = WebUtility.UrlEncode(activityId.ToString());
            var agentUrlEncoded = WebUtility.UrlEncode(agent.ToJson());

            var stateQuery = $"activities/state?activityId={activityUrlEncoded}&agent={agentUrlEncoded}";
            if (registration.HasValue)
            {
                var registrationEncoded = WebUtility.UrlEncode(registration.Value.ToString());
                stateQuery += $"&registration={registrationEncoded}";
            }
            if (since.HasValue)
            {
                var sinceEncoded = WebUtility.UrlEncode(since.Value.ToString("o"));
                stateQuery += $"&since={sinceEncoded}";
            }

            var response = await HttpClient.GetAsync(stateQuery);

            var content = await response.Content.ReadAsStringAsync();
            var data = JToken.Parse(content);

            return data.Select(x => x.Value<string>()).ToArray();
        }

        /// <summary>
        /// Stores the document specified by the given "stateId" that exists in the context of the specified Activity, Agent, 
        /// and registration (if specified).
        /// </summary>
        /// <param name="stateId">The id for this state, within the given context.</param>
        /// <param name="activityId">The Activity id associated with this state.</param>
        /// <param name="agent">The Agent associated with this state.</param>
        /// <param name="document">The document to be stored for this state.</param>
        /// <param name="registration">The registration associated with this state.</param>
        public async Task SaveStateAsync(string stateId, Uri activityId, Agent agent, byte[] document, Guid? registration = null)
        {
            var stateQuery = CreateStateQuery(stateId, activityId, agent, registration);

            using (var byteArrayContent = new ByteArrayContent(document))
            {
                var response = await HttpClient.PutAsync(stateQuery, byteArrayContent);
                response.EnsureSuccessStatusCode();
            }
        }

        /// <summary>
        /// Stores the document specified by the given "stateId" that exists in the context of the specified Activity, Agent, 
        /// and registration (if specified). The document is stored in ASCII.
        /// </summary>
        /// <param name="stateId">The id for this state, within the given context.</param>
        /// <param name="activityId">The Activity id associated with this state.</param>
        /// <param name="agent">The Agent associated with this state.</param>
        /// <param name="document">The document to be stored for this state.</param>
        /// <param name="registration">The registration associated with this state.</param>
        public async Task SaveStateAsync(string stateId, Uri activityId, Agent agent, string document, Guid? registration = null)
        {
            var byteArray = Encoding.ASCII.GetBytes(document);
            await SaveStateAsync(stateId, activityId, agent, byteArray, registration);
        }

        /// <summary>
        /// Fetches the document specified by the given "stateId" that exists in the context of the specified Activity,
        /// Agent and registration (if specified).
        /// </summary>
        /// <param name="stateId">The id for this state, within the given context.</param>
        /// <param name="activityId">The Activity id associated with this state.</param>
        /// <param name="agent">The Agent associated with this state.</param>
        /// <param name="registration">The registration associated with this state.</param>
        /// <returns>The state retrieved from the LRS</returns>
        public async Task<State> GetStateAsync(string stateId, Uri activityId, Agent agent, Guid? registration = null)
        {
            var stateQuery = CreateStateQuery(stateId, activityId, agent, registration);

            var response = await HttpClient.GetAsync(stateQuery);
            response.EnsureSuccessStatusCode();

            var updated = DateTime.Parse(response.Content.Headers.GetValues("Last-Modified").Single());

            var content = await response.Content.ReadAsByteArrayAsync();

            return new State(stateId, updated, agent, registration, content);
        }

        /// <summary>
        /// Deletes the document specified by the given "stateId" that exists in the context of the specified Activity, Agent, 
        /// and registration (if specified).
        /// </summary>
        /// <param name="stateId">The id for this state, within the given context.</param>
        /// <param name="activityId">The Activity id associated with this state.</param>
        /// <param name="agent">The Agent associated with this state.</param>
        /// <param name="registration">The registration associated with this state.</param>
        public async Task DeleteStateAsync(string stateId, Uri activityId, Agent agent, Guid? registration = null)
        {
            var stateQuery = CreateStateQuery(stateId, activityId, agent, registration);

            var response = await HttpClient.DeleteAsync(stateQuery);
            response.EnsureSuccessStatusCode();
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

        private static string CreateStateQuery(string stateId, Uri activityId, Agent agent, Guid? registration)
        {
            var activityIdUrlEncoded = WebUtility.UrlEncode(activityId.ToString());
            var agentUrlEncoded = WebUtility.UrlEncode(agent.ToJson());
            var stateIdUrlEncoded = WebUtility.UrlEncode(stateId);

            var stateQuery = $"activities/state?activityId={activityIdUrlEncoded}&agent={agentUrlEncoded}&stateId={stateIdUrlEncoded}";

            if (registration.HasValue)
            {
                var registrationEncoded = WebUtility.UrlEncode(registration.Value.ToString());
                stateQuery += $"&registration={registrationEncoded}";
            }

            return stateQuery;
        }
    }
}
