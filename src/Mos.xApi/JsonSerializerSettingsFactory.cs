using Mos.xApi.Utilities;
using Newtonsoft.Json;

namespace Mos.xApi
{
    /// <summary>
    /// Helper factory class that creates the JsonSerializerSettings
    /// configured to serialize and deserialize the library's objects
    /// from/to Experience API JSON strings.
    /// </summary>
    internal static class JsonSerializerSettingsFactory
    {
        /// <summary>
        /// Returns the JsonSerializerSettings configured to parse and serialize
        /// Experience API json strings.
        /// </summary>
        /// <returns>A correctly configured instance of JsonSerializerSettings.</returns>
        internal static JsonSerializerSettings CreateSettings()
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new ActorJsonConverter());
            settings.Converters.Add(new TimeSpanJsonConverter());
            settings.Converters.Add(new ExtensionJsonConverter());
            settings.Converters.Add(new LanguageMapConverter());
            settings.Converters.Add(new StatementObjectConverter());

            return settings;
        }
    }
}
