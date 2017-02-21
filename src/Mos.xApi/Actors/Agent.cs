using Mos.xApi.InverseFunctionalIdentifiers;
using Newtonsoft.Json;

namespace Mos.xApi.Actors
{
    /// <summary>
    /// An Agent(an individual) is a persona or system.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Agent : Actor
    {
        /// <summary>
        /// Initializes a new instance of an Agent class.
        /// </summary>
        /// <param name="identifier">An Inverse Functional Identifier unique to the Agent.</param>
        /// <param name="name">The full name of the Agent.</param>
        public Agent(IInverseFunctionalIdentifier identifier, string name = null) : base(identifier, name)
        {
        }

        /// <summary>
        /// Gets the type of Actor (in this case, returns "Agent").
        /// </summary>
        public override string ObjectType => nameof(Agent);
    }
}