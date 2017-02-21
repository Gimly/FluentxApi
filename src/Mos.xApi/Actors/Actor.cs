using Mos.xApi.InverseFunctionalIdentifiers;
using Newtonsoft.Json;

namespace Mos.xApi.Actors
{
    /// <summary>
    /// This abstract class is the base class for both Agent and Group.
    /// The Actor defines who performed the action.
    /// </summary>
    public abstract class Actor
    {
        /// <summary>
        /// Initializes a new instance of the Actor class.
        /// </summary>
        /// <param name="identifier">An Inverse Functional Identifier unique to the Actor.</param>
        /// <param name="name">Gets the name of the Actor (Full name for Agent, team name for Group).</param>
        protected Actor(IInverseFunctionalIdentifier identifier, string name = null)
        {
            Identifier = identifier;
            Name = name;
        }

        /// <summary>
        /// Gets the Inverse Functional Identifier unique to the Actor.
        /// </summary>
        [JsonProperty(Order = 2)]
        public IInverseFunctionalIdentifier Identifier { get; }

        /// <summary>
        /// Gets the name of the Actor (Full name for Agent, team name for Group).
        /// </summary>
        [JsonProperty("name", Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; }

        /// <summary>
        /// Gets the type of Actor
        /// </summary>
        [JsonProperty("objectType", Order = 0)]
        public abstract string ObjectType { get; }

        /// <summary>
        /// Starts the creation of a new Agent. An Agent (an individual) is a persona or system.
        /// </summary>
        /// <param name="name">Full name of the Agent.</param>
        /// <returns>The builder class that allows to fluently create an Agent.</returns>
        public static IAgentBuilder CreateAgent(string name = null)
        {
            return new AgentBuilder(name);
        }

        /// <summary>
        /// Starts the creation of a new Group. A Group represents a collection of Agents and can be used in most of the same situations an Agent can be used. 
        /// <para>There are two types of Groups: Anonymous Groups and Identified Groups.</para>
        /// </summary>
        /// <param name="name">Name of the Group.</param>
        /// <returns>The builder class that allows to fluently create a Group.</returns>
        public static IGroupBuilder CreateGroup(string name = null)
        {
            return new GroupBuilder(name);
        }

        /// <summary>
        /// Creates a JSON representation of the Actor based on the Experience API
        /// specification.
        /// </summary>
        /// <param name="prettyPrint">If set to true, the json will have indentation for easier readability.</param>
        /// <returns>The JSON representation of the Actor</returns>
        public string ToJson(bool prettyPrint = false)
        {
            return JsonConvert.SerializeObject(this, prettyPrint ? Formatting.Indented : Formatting.None, JsonSerializerSettingsFactory.CreateSettings());
        }
    }
}