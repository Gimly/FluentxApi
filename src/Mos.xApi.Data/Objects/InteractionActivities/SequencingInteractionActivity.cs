using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mos.xApi.Data.Objects.InteractionActivities
{
    public class SequencingInteractionActivity : IInteractionActivity<IEnumerable<string>>
    {
        public SequencingInteractionActivity(IOrderedEnumerable<string> correctResponse, IEnumerable<InteractionComponent> choices)
        {
            CorrectResponse = correctResponse;
            Choices = choices;
        }

        public IEnumerable<InteractionComponent> Choices { get; private set; }
        public IEnumerable<string> CorrectResponse { get; }
    }
}
