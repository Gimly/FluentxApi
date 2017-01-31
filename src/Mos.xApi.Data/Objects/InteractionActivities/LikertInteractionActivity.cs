using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mos.xApi.Data.Objects.InteractionActivities
{
    public class LikertInteractionActivity : IInteractionActivity<string>
    {
        public LikertInteractionActivity(string correctResponse, IOrderedEnumerable<InteractionComponent> scale)
        {
            CorrectResponse = correctResponse;
            Scale = scale;
        }

        public string CorrectResponse { get; }
        public IOrderedEnumerable<InteractionComponent> Scale { get; }
    }
}
