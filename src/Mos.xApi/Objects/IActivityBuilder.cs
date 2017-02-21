using System;

namespace Mos.xApi.Objects
{
    /// <summary>
    /// Interface that defines the methods used to fluently build an Activity
    /// </summary>
    public interface IActivityBuilder : IContainsExtension<IActivityBuilder>
    {
        /// <summary>
        /// Adds the description of the activity in the language specified by the language code.
        /// </summary>
        /// <param name="languageCode">A RFC 5646 Language Tag that defines the language of the description.</param>
        /// <param name="content">The description, in the language specified by the languageCode.</param>
        /// <returns>The activity builder, to continue the fluent configuration.</returns>
        IActivityBuilder AddDescription(string languageCode, string content);

        /// <summary>
        /// Adds a list of translated descriptions stored in a ILanguageMap.
        /// </summary>
        /// <param name="languageMap">The language map containing descriptions in multiple languages.</param>
        /// <returns>The activity builder, to continue the fluent configuration.</returns>
        IActivityBuilder AddDescription(ILanguageMap languageMap);

        /// <summary>
        /// Adds a name to the activity, which is the human readable/visual name of the Activity.
        /// </summary>
        /// <param name="languageCode">A RFC 5646 Language Tag that defines the language of the description.</param>
        /// <param name="content">The name, in the language specified by the languageCode.</param>
        /// <returns>The activity builder, to continue the fluent configuration.</returns>
        IActivityBuilder AddName(string languageCode, string content);

        /// <summary>
        /// Adds a list of translated names stored in a ILanguageMap.
        /// </summary>
        /// <param name="languageMap">The language map containing descriptions in multiple languages.</param>
        /// <returns>The activity builder, to continue the fluent configuration.</returns>
        IActivityBuilder AddName(ILanguageMap languageMap);

        /// <summary>
        /// Creates the Activity object constructed from the
        /// fluent configuration.
        /// </summary>
        /// <returns>The Activity object constructed.</returns>
        Activity Build();

        /// <summary>
        /// Sets the type of activity.
        /// </summary>
        /// <param name="typeUri">The IRI that defines the type of the Activity.</param>
        /// <returns>The activity builder, to continue the fluent configuration.</returns>
        IActivityBuilder WithActivityType(string typeUri);

        /// <summary>
        /// Sets the type of activity.
        /// </summary>
        /// <param name="type">The IRI that defines the type of the Activity.</param>
        /// <returns>The activity builder, to continue the fluent configuration.</returns>
        IActivityBuilder WithActivityType(Uri type);

        /// <summary>
        /// Sets more info IRI. The IRI should resolves to a document with human-readable 
        /// information about the Activity, which could include a way to launch the activity.
        /// </summary>
        /// <param name="moreInfo">The IRI to the more info document.</param>
        /// <returns>The activity builder, to continue the fluent configuration.</returns>
        IActivityBuilder WithMoreInfo(Uri moreInfo);

        /// <summary>
        /// Sets more info IRI. The IRI should resolves to a document with human-readable 
        /// information about the Activity, which could include a way to launch the activity.
        /// </summary>
        /// <param name="moreInfoUri">The IRI to the more info document.</param>
        /// <returns>The activity builder, to continue the fluent configuration.</returns>
        IActivityBuilder WithMoreInfo(string moreInfoUri);
    }
}