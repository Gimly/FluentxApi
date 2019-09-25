using System.Collections.Generic;

namespace Mos.xApi
{
    /// <summary>
    /// Implements a dictionary where the key is a RFC 5646 Language Tag, 
    /// and the value is a string in the language specified in the tag. 
    /// </summary>
    public class LanguageMap : Dictionary<string, string>, ILanguageMap
    {
        /// <summary>
        /// Initializes a new instance of the LanguageMap dictionary which is empty.
        /// </summary>
        public LanguageMap() { }

        /// <summary>
        /// Initializes a new instance of the LanguageMap dictionary that contains 
        /// elements that are copied from the passed dictionary.
        /// </summary>
        /// <param name="dictionary">The dictionary who's values are copied to the LanguageMap</param>
        public LanguageMap(IDictionary<string, string> dictionary) : base(dictionary) { }

        /// <summary>
        /// Returns, given the RFC 5646 language tag, the string in the language specified.
        /// </summary>
        /// <param name="languageCode">The RFC 5646 language tag</param>
        /// <returns>The text translated in the given language</returns>
        public string GetTranslation(string languageCode) => this[languageCode];
    }
}
