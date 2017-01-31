using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mos.xApi.Data.Objects.InteractionActivities
{
    public class FillInResponseText : IFillInResponseText
    {
        public FillInResponseText(string text, string languageCode = null)
        {
            LanguageCode = languageCode;
            Text = text;
        }

        public string LanguageCode { get; }

        public string Text { get; }
    }
}
