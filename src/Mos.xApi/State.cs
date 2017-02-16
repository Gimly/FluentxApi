using Mos.xApi.Actors;
using System;

namespace Mos.xApi
{
    /// <summary>
    /// A State is a Document stored in the LRS linked to an activity, an agent and, optionally, a registration. 
    /// <para>Generally, this is a scratch area for Learning Record Providers that do not have their own internal storage,
    /// or need to persist state across devices.</para>
    /// </summary>
    public class State : Document
    {
        /// <summary>
        /// Creates a new instance of a State.
        /// </summary>
        /// <param name="stateId">The identifier for this state, within the given context.</param>
        /// <param name="activityId">The Activity identifier associated with this state.</param>
        /// <param name="updated">The date and time when the State was last updated.</param>
        /// <param name="agent">The Agent associated with this state.</param>
        /// <param name="registration">The registration associated with this state.</param>
        /// <param name="content">The content stored in this state.</param>
        internal State(string stateId, Uri activityId, DateTime updated, Agent agent, Guid? registration, byte[] content)
            : base(stateId, updated, content)
        {
            ActivityId = activityId;
            Agent = agent;
            Registration = registration;
        }

        /// <summary>
        /// Gets the Activity identifier associated with this state.
        /// </summary>
        public Uri ActivityId { get; }

        /// <summary>
        /// Gets the Agent associated with this state.
        /// </summary>
        public Agent Agent { get; }

        /// <summary>
        /// Gets the registration associated with this state.
        /// </summary>
        public Guid? Registration { get; }
    }
}
