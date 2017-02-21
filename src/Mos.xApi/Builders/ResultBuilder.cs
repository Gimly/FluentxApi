using System;

namespace Mos.xApi.Builders
{
    /// <summary>
    /// Builder class used to simplify the creation of a Result, in a fluent interface manner.
    /// </summary>
    internal class ResultBuilder : IResultBuilder
    {
        /// <summary>
        /// A flag indicating whether or not the Activity was completed.
        /// </summary>
        private bool? _completion;

        /// <summary>
        /// Period of time over which the Statement occurred.
        /// </summary>
        private TimeSpan? _duration;

        /// <summary>
        /// A map of other properties as needed.
        /// </summary>
        private Extension _extensions;

        /// <summary>
        /// A response appropriately formatted for the given Activity.
        /// </summary>
        private string _response;

        /// <summary>
        /// The score of the Agent in relation to the success or quality of the experience.
        /// </summary>
        private Score _score;

        /// <summary>
        /// Indicates whether or not the attempt on the Activity was successful.
        /// </summary>
        private bool? _success;

        /// <summary>
        /// Initializes a new instance of the ResultBuilder class.
        /// </summary>
        public ResultBuilder()
        {
            _extensions = new Extension();
        }

        /// <summary>
        /// Adds an extension represented by an Extension instance.
        /// </summary>
        /// <param name="extension">The extension representation.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        public IResultBuilder AddExtension(Extension extension)
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
        public IResultBuilder AddExtension(string extensionUri, string jsonContent) => 
            AddExtension(new Uri(extensionUri), jsonContent);

        /// <summary>
        /// Adds an extension represented by an IRI and json content.
        /// </summary>
        /// <param name="extension">The IRI of the extension</param>
        /// <param name="jsonContent">The json representation of the extension value</param>
        /// <returns>The builder class, for the fluent API.</returns>
        public IResultBuilder AddExtension(Uri extension, string jsonContent)
        {
            _extensions.Add(extension, jsonContent);
            return this;
        }

        /// <summary>
        /// Creates the Result object constructed from the
        /// fluent configuration.
        /// </summary>
        /// <returns>The Result object constructed.</returns>
        public Result Build()
        {
            return new Result(_score, _success, _completion, _response, _duration, _extensions);
        }

        /// <summary>
        /// Sets a flag indicating whether or not the Activity was completed.
        /// </summary>
        /// <param name="completion">True if the Activity was completed, otherwise false.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        public IResultBuilder WithCompletion(bool completion)
        {
            _completion = completion;
            return this;
        }

        /// <summary>
        /// Sets a period of time over which the Statement occurred..
        /// </summary>
        /// <param name="duration">A time duration.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        public IResultBuilder WithDuration(TimeSpan duration)
        {
            _duration = duration;
            return this;
        }

        /// <summary>
        /// Sets a response appropriately formatted for the given Activity.
        /// </summary>
        /// <param name="response">The response the user gave to the Activity</param>
        /// <returns>The builder class, for the fluent API.</returns>
        public IResultBuilder WithResponse(string response)
        {
            _response = response;
            return this;
        }

        /// <summary>
        /// Sets the score of the Agent in relation to the success or quality of the experience.
        /// </summary>
        /// <param name="score">The score the agent attained in the Activity.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        public IResultBuilder WithScore(Score score)
        {
            _score = score;
            return this;
        }

        /// <summary>
        /// Sets an indicator stating whether or not the attempt on the Activity was successful.
        /// </summary>
        /// <param name="result">True if the attempt was successful, otherwise false.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        public IResultBuilder WithSuccess(bool result)
        {
            _success = result;
            return this;
        }
    }
}