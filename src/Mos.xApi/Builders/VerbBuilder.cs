using System;

namespace Mos.xApi.Builders
{
    internal class VerbBuilder : IVerbBuilder
    {
        private readonly Uri _id;
        private readonly LanguageMap _languageMap;

        public VerbBuilder(string idUri) : this(new Uri(idUri)) { }

        public VerbBuilder(Uri id)
        {
            _id = id;
            _languageMap = new LanguageMap();
        }

        public IVerbBuilder AddDisplay(ILanguageMap languageMap)
        {
            foreach (var item in languageMap)
            {
                _languageMap.Add(item.Key, item.Value);
            }
            return this;
        }

        public IVerbBuilder AddDisplay(string languageCode, string content)
        {
            _languageMap.Add(languageCode, content);
            return this;
        }

        public Verb Build()
        {
            return new Verb(_id, _languageMap);
        }
    }
}