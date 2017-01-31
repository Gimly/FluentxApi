using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mos.xApi.Objects.InteractionActivities
{
    public class NumericInteractionActivity : IInteractionActivity<IEnumerable<INumericRange>>
    {
        public NumericInteractionActivity(IEnumerable<INumericRange> correctResponse)
        {
            CorrectResponse = correctResponse;
        }

        public IEnumerable<INumericRange> CorrectResponse { get; }
    }
}
