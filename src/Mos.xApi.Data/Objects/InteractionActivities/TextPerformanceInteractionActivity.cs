using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mos.xApi.Objects.InteractionActivities
{
    public class TextPerformanceInteractionActivity : IInteractionActivity<IEnumerable<ITextPerformanceInteractionStepResponse>>
    {
        public TextPerformanceInteractionActivity(IEnumerable<ITextPerformanceInteractionStepResponse> responses, IEnumerable<InteractionComponent> steps, bool orderMatters = true)
        {
            CorrectResponse = responses;
            Steps = steps;
            OrderMatters = orderMatters;
        }

        public IEnumerable<ITextPerformanceInteractionStepResponse> CorrectResponse { get; }
        public bool OrderMatters { get; }
        public IEnumerable<InteractionComponent> Steps { get; }
    }
}
