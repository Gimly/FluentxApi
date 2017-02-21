using Mos.xApi.Builders;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Mos.xApi
{
    /// <summary>
    /// This class defines a result, which is an optional property that represents a measured outcome 
    /// related to the Statement in which it is included.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Initializes a new instance of a Result class.
        /// </summary>
        /// <param name="score">The score of the Agent in relation to the success or quality of the experience.</param>
        /// <param name="success">Indicates whether or not the attempt on the Activity was successful.</param>
        /// <param name="completion">Indicates whether or not the Activity was completed.</param>
        /// <param name="response">A response appropriately formatted for the given Activity.</param>
        /// <param name="duration">Period of time over which the Statement occurred.</param>
        /// <param name="extensions">A map of other properties as needed.</param>
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

        /// <summary>
        /// Gets an indicator whether or not the Activity was completed.
        /// </summary>
        [JsonProperty("completion", Order = 2, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Completion { get; }

        /// <summary>
        /// Gets the period of time over which the Statement occurred.
        /// </summary>
        [JsonProperty("duration", Order = 4, NullValueHandling = NullValueHandling.Ignore)]
        public TimeSpan? Duration { get; }

        /// <summary>
        /// Gets a map of other properties as needed.
        /// </summary>
        [JsonProperty("extensions", Order = 5, NullValueHandling = NullValueHandling.Ignore)]
        public Extension Extensions { get; }

        /// <summary>
        /// Gets a response appropriately formatted for the given Activity.
        /// </summary>
        [JsonProperty("response", Order = 3, NullValueHandling = NullValueHandling.Ignore)]
        public string Response { get; }

        /// <summary>
        /// Gets the score of the Agent in relation to the success or quality of the experience.
        /// </summary>
        [JsonProperty("score", Order = 0, NullValueHandling = NullValueHandling.Ignore)]
        public Score Score { get; }

        /// <summary>
        /// Gets an indicator that states whether or not the attempt on the Activity was successful.
        /// </summary>
        [JsonProperty("success", Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        public bool? Success { get; }

        /// <summary>
        /// Starts the fluent creation of a Result instance.
        /// </summary>
        /// <returns>The builder that allows to fluently construct a Result.</returns>
        public static IResultBuilder Create()
        {
            return new ResultBuilder();
        }
    }
}