using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mos.xApi.Objects.InteractionActivities
{
    public class OtherInteractionActivity : IInteractionActivity<string>
    {
        public OtherInteractionActivity(string correctResponse)
        {
            CorrectResponse = correctResponse;
        }

        public string CorrectResponse { get; }
    }
}
