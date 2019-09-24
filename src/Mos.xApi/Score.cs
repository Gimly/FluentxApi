using Newtonsoft.Json;
using System;

namespace Mos.xApi
{
    /// <summary>
    /// This class defines a score attached to a statement. A score is an optional property 
    /// that represents the outcome of a graded Activity achieved by an Agent.
    /// </summary>
    public class Score
    {
        /// <summary>
        /// Initializes a new instance of a score to be attached to a Statement.
        /// </summary>
        /// <param name="scaled">
        ///     The score related to the experience as modified by scaling and/or normalization.
        ///     <para>Must be between -1 and 1, inclusive.</para></param>
        /// <param name="raw">
        ///     The score achieved by the Actor in the experience described by the Statement. 
        ///     This is not modified by any scaling or normalization.
        ///     <para>Must be between min and max (if present, otherwise unrestricted), inclusive</para>
        /// </param>
        /// <param name="min">
        ///     The lowest possible score for the experience described by the Statement.
        ///     <para>Must be less than max (if present)</para>
        /// </param>
        /// <param name="max">
        ///     The highest possible score for the experience described by the Statement.
        ///     <para>Must be greater than min (if present)</para>
        /// </param>
        public Score(double scaled, double? raw = null, double? min = null, double? max = null)
        {
            if (scaled < -1 || scaled > 1)
            {
                throw new ArgumentException("The value for scaled must be between -1 and 1.", nameof(scaled));
            }

            if (min.HasValue)
            {
                if (max.HasValue && max.Value < min.Value)
                {
                    throw new ArgumentException("The value for max must be higher than the value for min.", nameof(max));
                }
            }

            if (raw.HasValue)
            {
                if (min.HasValue && raw.Value < min.Value)
                {
                    throw new ArgumentException("The value for raw must be greater than or equal to the value of min if min is specified.", nameof(raw));
                }

                if (max.HasValue && raw.Value > max.Value)
                {
                    throw new ArgumentException("The value for raw must be smaller than or equal to the value of max if max is specified.", nameof(raw));
                }
            }

            Scaled = scaled;
            Raw = raw;
            Min = min;
            Max = max;
        }

        /// <summary>
        /// Gets the highest possible score for the experience described by the Statement.
        /// </summary>
        [JsonProperty("max", Order = 3, NullValueHandling = NullValueHandling.Ignore)]
        public double? Max { get; }

        /// <summary>
        /// Gets the lowest possible score for the experience described by the Statement.
        /// </summary>
        [JsonProperty("min", Order = 2, NullValueHandling = NullValueHandling.Ignore)]
        public double? Min { get; }

        /// <summary>
        /// Gets the score achieved by the Actor in the experience described by the Statement. 
        /// This is not modified by any scaling or normalization.
        /// </summary>
        [JsonProperty("raw", Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        public double? Raw { get; }

        /// <summary>
        /// Gets the score related to the experience as modified by scaling and/or normalization.
        /// </summary>
        [JsonProperty("scaled", Order = 0, Required = Required.Always)]
        public double Scaled { get; }
    }
}