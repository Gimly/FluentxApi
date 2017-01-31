using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mos.xApi.Data.Objects.InteractionActivities
{
    /// <summary>
    /// Represents an interaction with two possible response: true or false.
    /// </summary>
    public class TrueFalseInteractionActivity : IInteractionActivity<bool?>
    {
        public TrueFalseInteractionActivity(bool? correctResponse)
        {
            CorrectResponse = correctResponse;
        }

        public bool? CorrectResponse { get; }
    }
}
