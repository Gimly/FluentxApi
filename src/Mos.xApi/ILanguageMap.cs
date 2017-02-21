using System.Collections.Generic;

namespace Mos.xApi
{
    /// <summary>
    /// Implements a dictionary where the key is a RFC 5646 Language Tag, 
    /// and the value is a string in the language specified in the tag. 
    /// </summary>
    public interface ILanguageMap : IDictionary<string, string>
    {
        /// <summary>
        /// Returns, given the RFC 5646 language tag, the string in the language specified.
        /// </summary>
        /// <param name="languageCode">The RFC 5646 language tag</param>
        /// <returns>The text translated in the given language</returns>
        string GetTranslation(string languageCode);
    }
}
