namespace Mos.xApi.Builders
{
    /// <summary>
    /// Interface that defines a builder used to simplify the creation of a Verb, 
    /// in a fluent interface manner.
    /// </summary>
    public interface IVerbBuilder
    {
        /// <summary>
        /// Adds a human readable representation of the Verb in the language specified by the code.
        /// <para>This does not have any impact on the meaning of the Statement, but serves to give a human-readable display of the meaning already determined by the chosen Verb.</para>
        /// </summary>
        /// <param name="languageCode">RFC5646 language code that represents the language the display is written in.</param>
        /// <param name="content">Human readable representation of the verb in the language.</param>
        /// <returns>The statement builder, to continue the fluent configuration.</returns>
        IVerbBuilder AddDisplay(string languageCode, string content);

        /// <summary>
        /// The human readable representation of the Verb in one or more languages. 
        /// <para>This does not have any impact on the meaning of the Statement, but serves to give a human-readable display of the meaning already determined by the chosen Verb.</para>
        /// </summary>
        /// <param name="languageMap">A language map defining the verb in multiple languages</param>
        /// <returns>The statement builder, to continue the fluent configuration.</returns>
        IVerbBuilder AddDisplay(ILanguageMap languageMap);

        /// <summary>
        /// Creates the Verb object constructed from the
        /// fluent configuration.
        /// </summary>
        /// <returns>The Verb object constructed.</returns>
        Verb Build();
    }
}