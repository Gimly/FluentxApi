using System.Collections.Generic;

namespace Mos.xApi.Actors
{
    /// <summary>
    /// Interface that defines a builder used to simplify the creation of a Group, 
    /// in a fluent interface manner.
    /// </summary>
    public interface IGroupBuilder : IActorBuilder<Group>
    {
        /// <summary>
        /// Add an Agent to the group.
        /// </summary>
        /// <param name="agent">The agent to be added.</param>
        /// <returns>The Group builder, to continue the fluent configuration.</returns>
        IGroupBuilder Add(Agent agent);

        /// <summary>
        /// Adds a list of Agents to the group.
        /// </summary>
        /// <param name="agents">The list of agents to be added.</param>
        /// <returns>The Group builder, to continue the fluent configuration.</returns>
        IGroupBuilder AddRange(IEnumerable<Agent> agents);

        /// <summary>
        /// Creates a Group as an anonymous group, with no Inverse Functional Identifier.
        /// <para>An Anonymous Group is used describe a cluster of people where there is no ready identifier for this cluster, e.g. an ad hoc team.</para>
        /// </summary>
        /// <returns>The created group.</returns>
        Group AsAnonymous();
    }
}