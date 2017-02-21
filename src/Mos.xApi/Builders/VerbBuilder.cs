using System;

namespace Mos.xApi.Builders
{
    /// <summary>
    /// Class that defines a builder used to simplify the creation of a Statement, 
    /// in a fluent interface manner.
    /// </summary>
    internal class VerbBuilder : IVerbBuilder
    {
        /// <summary>
        /// The IRI uniquely identifying the Verb
        /// </summary>
        private readonly Uri _id;

        /// <summary>
        /// The human readable representation of the Verb in one or more languages.
        /// </summary>
        private readonly LanguageMap _languageMap;

        /// <summary>
        /// Initializes a new instance of the VerbBuilder class.
        /// </summary>
        /// <param name="idUri">The IRI uniquely identifying the Verb</param>
        public VerbBuilder(string idUri) : this(new Uri(idUri)) { }

        /// <summary>
        /// Initializes a new instance of the VerbBuilder class.
        /// </summary>
        /// <param name="id">The IRI uniquely identifying the Verb</param>
        public VerbBuilder(Uri id)
        {
            _id = id;
            _languageMap = new LanguageMap();
        }

        /// <summary>
        /// The human readable representation of the Verb in one or more languages. 
        /// <para>This does not have any impact on the meaning of the Statement, but serves to give a human-readable display of the meaning already determined by the chosen Verb.</para>
        /// </summary>
        /// <param name="languageMap">A language map defining the verb in multiple languages</param>
        /// <returns>The statement builder, to continue the fluent configuration.</returns>
        public IVerbBuilder AddDisplay(ILanguageMap languageMap)
        {
            foreach (var item in languageMap)
            {
                _languageMap.Add(item.Key, item.Value);
            }
            return this;
        }

        /// <summary>
        /// Adds a human readable representation of the Verb in the language specified by the code.
        /// <para>This does not have any impact on the meaning of the Statement, but serves to give a human-readable display of the meaning already determined by the chosen Verb.</para>
        /// </summary>
        /// <param name="languageCode">RFC5646 language code that represents the language the display is written in.</param>
        /// <param name="content">Human readable representation of the verb in the language.</param>
        /// <returns>The statement builder, to continue the fluent configuration.</returns>
        public IVerbBuilder AddDisplay(string languageCode, string content)
        {
            _languageMap.Add(languageCode, content);
            return this;
        }

        /// <summary>
        /// Creates the Verb object constructed from the
        /// fluent configuration.
        /// </summary>
        /// <returns>The Verb object constructed.</returns>
        public Verb Build()
        {
            return new Verb(_id, _languageMap);
        }
    }
}