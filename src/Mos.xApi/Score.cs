using Newtonsoft.Json;

namespace Mos.xApi
{
    public class Score
    {
        public Score(double scaled, double? raw = null, double? min = null, double? max = null)
        {
            Scaled = scaled;
            Raw = raw;
            Min = min;
            Max = max;
        }

        [JsonProperty("max", Order = 3, NullValueHandling = NullValueHandling.Ignore)]
        public double? Max { get; }

        [JsonProperty("min", Order = 2, NullValueHandling = NullValueHandling.Ignore)]
        public double? Min { get; }

        [JsonProperty("raw", Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        public double? Raw { get; }

        [JsonProperty("scaled", Order = 0, Required = Required.Always)]
        public double Scaled { get; }
    }
}