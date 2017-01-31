using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mos.xApi.Objects.InteractionActivities
{
    public class NumericPerformanceInteractionActivity : IInteractionActivity<IEnumerable<INumericPerformanceInteractionStepResponse>>
    {
        public NumericPerformanceInteractionActivity(IEnumerable<INumericPerformanceInteractionStepResponse> correctResponse, IEnumerable<InteractionComponent> steps, bool orderMatters = true)
        {
            CorrectResponse = correctResponse;

            Steps = steps;
            OrderMatters = orderMatters;
        }

        public IEnumerable<INumericPerformanceInteractionStepResponse> CorrectResponse { get; }
        public bool OrderMatters { get; }
        public IEnumerable<InteractionComponent> Steps { get; }
    }
}
