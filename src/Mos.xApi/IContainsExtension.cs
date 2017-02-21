using System;

namespace Mos.xApi
{
    /// <summary>
    /// Interface that implements behavior for all the
    /// builder classes that contains the possibility in Experience API
    /// to add extensions.
    /// </summary>
    /// <typeparam name="T">The type of the builder class.</typeparam>
    public interface IContainsExtension<T>
    {
        /// <summary>
        /// Adds an extension represented by an IRI and json content.
        /// </summary>
        /// <param name="extensionUri">The IRI of the extension</param>
        /// <param name="jsonContent">The json representation of the extension value</param>
        /// <returns>The builder class, for the fluent API.</returns>
        T AddExtension(string extensionUri, string jsonContent);

        /// <summary>
        /// Adds an extension represented by an IRI and json content.
        /// </summary>
        /// <param name="extension">The IRI of the extension</param>
        /// <param name="jsonContent">The json representation of the extension value</param>
        /// <returns>The builder class, for the fluent API.</returns>
        T AddExtension(Uri extension, string jsonContent);

        /// <summary>
        /// Adds an extension represented by an Extension instance.
        /// </summary>
        /// <param name="extension">The extension representation.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        T AddExtension(Extension extension);
    }
}