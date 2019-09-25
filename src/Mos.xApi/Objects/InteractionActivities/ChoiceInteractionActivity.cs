using System.Collections.Generic;

namespace Mos.xApi.Objects.InteractionActivities
{
    public class ChoiceInteractionActivity : IInteractionActivity<IEnumerable<string>>
    {

        public ChoiceInteractionActivity(IEnumerable<InteractionComponent> possibleAnswers, IEnumerable<string> correctResponse)
        {
            PossibleAnswers = possibleAnswers;
            CorrectResponse = correctResponse;
        }

        public ChoiceInteractionActivity(IEnumerable<InteractionComponent> possibleAnswers, string correctResponse)
            : this(possibleAnswers, new[] { correctResponse })
        {
        }

        public IEnumerable<string> CorrectResponse { get; }

        public IEnumerable<InteractionComponent> PossibleAnswers { get; }
    }
}