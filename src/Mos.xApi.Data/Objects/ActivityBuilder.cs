using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mos.xApi.Data.Objects
{
    internal class ActivityBuilder : IActivityBuilder
    {
        private readonly Uri _id;
        private Uri _activityType;
        private LanguageMap _descriptionLanguageMap;
        private Extension _extensions;
        private Uri _moreInfo;
        private LanguageMap _nameLanguageMap;
        public ActivityBuilder(Uri id)
        {
            _id = id;
            _descriptionLanguageMap = new LanguageMap();
            _nameLanguageMap = new LanguageMap();
            _extensions = new Extension();
        }

        public ActivityBuilder(string id) : this(new Uri(id)) { }

        public IActivityBuilder AddDescription(ILanguageMap languageMap)
        {
            foreach (var item in languageMap)
            {
                _descriptionLanguageMap.Add(item.Key, item.Value);
            }
            return this;
        }

        public IActivityBuilder AddDescription(string languageCode, string content)
        {
            _descriptionLanguageMap.Add(languageCode, content);
            return this;
        }

        public IActivityBuilder AddExtension(Uri extension, string jsonContent)
        {
            _extensions.Add(extension, jsonContent);
            return this;
        }

        public IActivityBuilder AddExtension(Extension extension)
        {
            foreach (var item in extension)
            {
                _extensions.Add(item.Key, item.Value);
            }
            return this;
        }

        public IActivityBuilder AddExtension(string extensionUri, string jsonContent) => AddExtension(new Uri(extensionUri), jsonContent);

        public IActivityBuilder AddName(ILanguageMap languageMap)
        {
            foreach (var item in languageMap)
            {
                _nameLanguageMap.Add(item.Key, item.Value);
            }
            return this;
        }

        public IActivityBuilder AddName(string languageCode, string content)
        {
            _nameLanguageMap.Add(languageCode, content);
            return this;
        }

        public Activity Build()
        {
            ActivityDefinition definition = null;
            if (HasActivityDefinition())
            {
                definition = new ActivityDefinition(_nameLanguageMap, _activityType, _descriptionLanguageMap, _moreInfo, _extensions);
            }

            return new Activity(_id, definition);
        }

        public IActivityBuilder WithActivityType(string typeUri) => WithActivityType(new Uri(typeUri));

        public IActivityBuilder WithActivityType(Uri type)
        {
            _activityType = type;
            return this;
        }

        public IActivityBuilder WithMoreInfo(Uri moreInfo)
        {
            _moreInfo = moreInfo;
            return this;
        }

        public IActivityBuilder WithMoreInfo(string moreInfoUri) => WithMoreInfo(new Uri(moreInfoUri));

        private bool HasActivityDefinition()
        {
            return _activityType != null || _descriptionLanguageMap.Any() || _nameLanguageMap.Any() || _extensions.Any();
        }
    }
}
