using System.Collections.Generic;

namespace Mos.xApi.Objects.InteractionActivities
{
    public class MatchingInteractionActivity : IInteractionActivity<IEnumerable<IMatchingPair>>
    {
        public MatchingInteractionActivity(
            IEnumerable<IMatchingPair> correctResponse,
            IEnumerable<InteractionComponent> sources,
            IEnumerable<InteractionComponent> targets)
        {
            Sources = sources;
            Targets = targets;
            CorrectResponse = correctResponse;
        }

        public IEnumerable<IMatchingPair> CorrectResponse { get; }
        public IEnumerable<InteractionComponent> Sources { get; }
        public IEnumerable<InteractionComponent> Targets { get; }
    }
}