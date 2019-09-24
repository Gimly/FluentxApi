using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mos.xApi.Objects.InteractionActivities
{
    public class NumericRange : INumericRange
    {
        public NumericRange(int? minimum, int? maximum)
        {
            if (!(minimum.HasValue || maximum.HasValue))
            {
                throw new ArgumentNullException(nameof(minimum), "Minimum and Maximum value cannot be both null.");
            }

            if (minimum.HasValue && maximum.HasValue && minimum.Value > maximum.Value)
            {
                throw new ArgumentOutOfRangeException(nameof(maximum), "The value for maximum must be greater than minimum.");
            }

            Minimum = minimum;
            Maximum = maximum;
        }

        public int? Minimum { get; }
        public int? Maximum { get; }
    }
}
