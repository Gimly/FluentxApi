using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mos.xApi.Data.Objects.InteractionActivities
{
    public class NumericRange : INumericRange
    {
        public NumericRange(int? minimum, int? maximum)
        {
            if (!(minimum.HasValue || maximum.HasValue))
            {
                throw new ArgumentNullException(nameof(minimum), "Minimum and Maximum value cannot be both null.");
            }

            Minimum = minimum;
            Maximum = maximum;
        }

        int? Minimum { get; }
        int? Maximum { get; }
    }
}
