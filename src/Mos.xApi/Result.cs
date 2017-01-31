using Mos.xApi.Builders;
using Mos.xApi.Objects;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Mos.xApi
{
    public class Result
    {
        public Result(
            Score score = null,
            bool? success = null,
            bool? completion = null,
            string response = null,
            TimeSpan? duration = null,
            Extension extensions = null)
        {
            Score = score;
            Completion = completion;
            Success = success;
            Response = response;
            Duration = duration;

            if (extensions != null && extensions.Any())
            {
                Extensions = extensions;
            }
        }

        [JsonProperty("completion", Order = 2, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Completion { get; }

        [JsonProperty("duration", Order = 4, NullValueHandling = NullValueHandling.Ignore)]
        public TimeSpan? Duration { get; }

        [JsonProperty("extensions", Order = 5, NullValueHandling = NullValueHandling.Ignore)]
        public Extension Extensions { get; }

        [JsonProperty("response", Order = 3, NullValueHandling = NullValueHandling.Ignore)]
        public string Response { get; }

        [JsonProperty("score", Order = 0, NullValueHandling = NullValueHandling.Ignore)]
        public Score Score { get; }

        [JsonProperty("success", Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Success { get; }

        public static IResultBuilder Create()
        {
            return new ResultBuilder();
        }
    }
}