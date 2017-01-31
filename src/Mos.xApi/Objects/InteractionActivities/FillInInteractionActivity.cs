using System.Collections.Generic;

namespace Mos.xApi.Objects.InteractionActivities
{
    /// <summary>
    /// <para>
    ///     Represents an interaction which requires the learner to supply a response in the form of one
    ///     or more strings of characters.
    /// </para>
    /// <para>
    ///     This class is used for both short and long fill-in types, with a flag defining whether it's a short or long fill-in.
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    ///     "Short" means that the correct responses pattern and learner response strings will normally be 250 characters or less.
    ///     Typically, the correct response consists of part of a word, one word or a few words.
    /// </para>
    /// <para>
    ///     "Long" means that the correct responses pattern and learner response strings will normally be more than 250 characters.
    /// </para>
    /// </remarks>
    public class FillInInteractionActivity : IInteractionActivity<IEnumerable<IFillInResponseText>>
    {
        public FillInInteractionActivity(bool isLong, IEnumerable<IFillInResponseText> correctResponse, bool caseMatters = false, bool orderMatters = true)
        {
            IsLong = isLong;

            //TODO validate that if the flag isLong is not set, the values are not more than 250 characters
            CorrectResponse = correctResponse;

            CaseMatters = caseMatters;
            OrderMatters = orderMatters;
        }

        public bool CaseMatters { get; }

        public IEnumerable<IFillInResponseText> CorrectResponse { get; }

        public bool IsLong { get; }

        public string LanguageCode { get; }

        public bool OrderMatters { get; }
    }
}