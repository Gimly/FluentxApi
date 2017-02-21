using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mos.xApi.Objects
{
    /// <summary>
    /// Builder class used to simplify the creation of
    /// an Activity.
    /// </summary>
    internal class ActivityBuilder : IActivityBuilder
    {
        /// <summary>
        /// The identifier of the Activity.
        /// </summary>
        private readonly Uri _id;

        /// <summary>
        /// The type of Activity.
        /// </summary>
        private Uri _activityType;

        /// <summary>
        /// A description of the Activity
        /// </summary>
        private LanguageMap _descriptionLanguageMap;

        /// <summary>
        /// A map of other properties as needed
        /// </summary>
        private Extension _extensions;

        /// <summary>
        /// Resolves to a document with human-readable information about the Activity, which could include a way to launch the activity.
        /// </summary>
        private Uri _moreInfo;

        /// <summary>
        /// The human readable/visual name of the Activity
        /// </summary>
        private LanguageMap _nameLanguageMap;

        /// <summary>
        /// Initializes a new instance of the ActivityBuilder class.
        /// </summary>
        /// <param name="id">The unique identifier of the Activity.</param>
        public ActivityBuilder(Uri id)
        {
            _id = id;
            _descriptionLanguageMap = new LanguageMap();
            _nameLanguageMap = new LanguageMap();
            _extensions = new Extension();
        }

        /// <summary>
        /// Initializes a new instance of the ActivityBuilder class.
        /// </summary>
        /// <param name="id">The unique identifier of the Activity (must be an IRI).</param>
        public ActivityBuilder(string id) : this(new Uri(id)) { }

        /// <summary>
        /// Adds a list of translated descriptions stored in a ILanguageMap.
        /// </summary>
        /// <param name="languageMap">The language map containing descriptions in multiple languages.</param>
        /// <returns>The activity builder, to continue the fluent configuration.</returns>
        public IActivityBuilder AddDescription(ILanguageMap languageMap)
        {
            foreach (var item in languageMap)
            {
                _descriptionLanguageMap.Add(item.Key, item.Value);
            }
            return this;
        }

        /// <summary>
        /// Adds the description of the activity in the language specified by the language code.
        /// </summary>
        /// <param name="languageCode">A RFC 5646 Language Tag that defines the language of the description.</param>
        /// <param name="content">The description, in the language specified by the languageCode.</param>
        /// <returns>The activity builder, to continue the fluent configuration.</returns>
        public IActivityBuilder AddDescription(string languageCode, string content)
        {
            _descriptionLanguageMap.Add(languageCode, content);
            return this;
        }

        /// <summary>
        /// Adds an extension represented by an IRI and json content.
        /// </summary>
        /// <param name="extension">The IRI of the extension</param>
        /// <param name="jsonContent">The json representation of the extension value</param>
        /// <returns>The builder class, for the fluent API.</returns>
        public IActivityBuilder AddExtension(Uri extension, string jsonContent)
        {
            _extensions.Add(extension, jsonContent);
            return this;
        }

        /// <summary>
        /// Adds an extension represented by an Extension instance.
        /// </summary>
        public IActivityBuilder AddExtension(Extension extension)
        {
            foreach (var item in extension)
            {
                _extensions.Add(item.Key, item.Value);
            }
            return this;
        }

        /// <summary>
        /// Adds an extension represented by an IRI and json content.
        /// </summary>
        /// <param name="extensionUri">The IRI of the extension</param>
        /// <param name="jsonContent">The json representation of the extension value</param>
        /// <returns>The builder class, for the fluent API.</returns>
        public IActivityBuilder AddExtension(string extensionUri, string jsonContent) => AddExtension(new Uri(extensionUri), jsonContent);

        /// <summary>
        /// Adds a list of translated names stored in a ILanguageMap.
        /// </summary>
        /// <param name="languageMap">The language map containing descriptions in multiple languages.</param>
        /// <returns>The activity builder, to continue the fluent configuration.</returns>
        public IActivityBuilder AddName(ILanguageMap languageMap)
        {
            foreach (var item in languageMap)
            {
                _nameLanguageMap.Add(item.Key, item.Value);
            }
            return this;
        }

        /// <summary>
        /// Adds a name to the activity, which is the human readable/visual name of the Activity.
        /// </summary>
        /// <param name="languageCode">A RFC 5646 Language Tag that defines the language of the description.</param>
        /// <param name="content">The name, in the language specified by the languageCode.</param>
        /// <returns>The activity builder, to continue the fluent configuration.</returns>
        public IActivityBuilder AddName(string languageCode, string content)
        {
            _nameLanguageMap.Add(languageCode, content);
            return this;
        }

        /// <summary>
        /// Creates the Activity object constructed from the
        /// fluent configuration.
        /// </summary>
        /// <returns>The Activity object constructed.</returns>
        public Activity Build()
        {
            ActivityDefinition definition = null;
            if (HasActivityDefinition())
            {
                definition = new ActivityDefinition(_nameLanguageMap, _activityType, _descriptionLanguageMap, _moreInfo, _extensions);
            }

            return new Activity(_id, definition);
        }

        /// <summary>
        /// Sets the type of activity.
        /// </summary>
        /// <param name="typeUri">The IRI that defines the type of the Activity.</param>
        /// <returns>The activity builder, to continue the fluent configuration.</returns>
        public IActivityBuilder WithActivityType(string typeUri) => WithActivityType(new Uri(typeUri));

        /// <summary>
        /// Sets the type of activity.
        /// </summary>
        /// <param name="type">The IRI that defines the type of the Activity.</param>
        /// <returns>The activity builder, to continue the fluent configuration.</returns>
        public IActivityBuilder WithActivityType(Uri type)
        {
            _activityType = type;
            return this;
        }

        /// <summary>
        /// Sets more info IRI. The IRI should resolves to a document with human-readable 
        /// information about the Activity, which could include a way to launch the activity.
        /// </summary>
        /// <param name="moreInfo">The IRI to the more info document.</param>
        /// <returns>The activity builder, to continue the fluent configuration.</returns>
        public IActivityBuilder WithMoreInfo(Uri moreInfo)
        {
            _moreInfo = moreInfo;
            return this;
        }

        /// <summary>
        /// Sets more info IRI. The IRI should resolves to a document with human-readable 
        /// information about the Activity, which could include a way to launch the activity.
        /// </summary>
        /// <param name="moreInfoUri">The IRI to the more info document.</param>
        /// <returns>The activity builder, to continue the fluent configuration.</returns>
        public IActivityBuilder WithMoreInfo(string moreInfoUri) => WithMoreInfo(new Uri(moreInfoUri));

        /// <summary>
        /// Checks whether the activity should contain an activity definition.
        /// </summary>
        /// <returns>True if the activity contains an activity definition, otherwise false.</returns>
        private bool HasActivityDefinition()
        {
            return _activityType != null || _descriptionLanguageMap.Any() || _nameLanguageMap.Any() || _extensions.Any();
        }
    }
}
