using Mos.xApi.Data.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mos.xApi.Data
{
    public static class JsonSerializerSettingsFactory
    {
        public static JsonSerializerSettings CreateSettings()
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
