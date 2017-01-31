using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mos.xApi.Objects.InteractionActivities
{
    public class MatchingPair : IMatchingPair
    {
        public MatchingPair(string sourceId, string targetId)
        {
            SourceId = sourceId;
            TargetId = targetId;
        }

        public string SourceId { get; }

        public string TargetId { get; }
    }
}
