using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mos.xApi.Objects.InteractionActivities
{
    public class NumericPerformanceInteractionStepResponse : INumericPerformanceInteractionStepResponse
    {
        public NumericPerformanceInteractionStepResponse(string id, INumericRange range)
        {
            Id = id;
            Range = range;
        }

        public string Id { get; }

        public INumericRange Range { get; }
    }
}
