using Mos.xApi.Actors;
using System;

namespace Mos.xApi
{
    public class State : Document
    {
        internal State(string stateId, DateTime updated, Agent agent, Guid? registration, byte[] content) : base(stateId, updated, content)
        {
            Agent = agent;
            Registration = registration;
        }

        public Agent Agent { get; }

        public Guid? Registration { get; }
    }
}
