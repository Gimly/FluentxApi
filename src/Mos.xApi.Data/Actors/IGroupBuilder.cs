using System;
using System.Collections.Generic;

namespace Mos.xApi.Data.Actors
{
    public interface IGroupBuilder : IActorBuilder<Group>
    {
        IGroupBuilder Add(Agent agent);

        IGroupBuilder AddRange(IEnumerable<Agent> agents);

        Group AsAnonymous();
    }
}