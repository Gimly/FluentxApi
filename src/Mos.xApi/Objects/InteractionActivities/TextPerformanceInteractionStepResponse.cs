using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mos.xApi.Objects.InteractionActivities
{
    public class TextPerformanceInteractionStepResponse : ITextPerformanceInteractionStepResponse
    {
        public TextPerformanceInteractionStepResponse(string id, string response, string languageCode = null)
        {
            Id = id;
            Response = response;
            LanguageCode = languageCode;
        }

        public string Id { get; }

        public string Response { get; }

        public string LanguageCode { get; }
    }
}
