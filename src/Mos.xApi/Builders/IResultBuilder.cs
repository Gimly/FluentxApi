using System;

namespace Mos.xApi.Builders
{
    /// <summary>
    /// Interface that defines a builder used to simplify the creation of a Result, 
    /// in a fluent interface manner.
    /// </summary>
    public interface IResultBuilder : IContainsExtension<IResultBuilder>
    {
        /// <summary>
        /// Creates the Result object constructed from the
        /// fluent configuration.
        /// </summary>
        /// <returns>The Result object constructed.</returns>
        Result Build();

        /// <summary>
        /// Sets a flag indicating whether or not the Activity was completed.
        /// </summary>
        /// <param name="completion">True if the Activity was completed, otherwise false.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        IResultBuilder WithCompletion(bool completion);

        /// <summary>
        /// Sets a period of time over which the Statement occurred..
        /// </summary>
        /// <param name="duration">A time duration.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        IResultBuilder WithDuration(TimeSpan duration);

        /// <summary>
        /// Sets a response appropriately formatted for the given Activity.
        /// </summary>
        /// <param name="response">The response the user gave to the Activity</param>
        /// <returns>The builder class, for the fluent API.</returns>
        IResultBuilder WithResponse(string response);

        /// <summary>
        /// Sets the score of the Agent in relation to the success or quality of the experience.
        /// </summary>
        /// <param name="score">The score the agent attained in the Activity.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        IResultBuilder WithScore(Score score);

        /// <summary>
        /// Sets an indicator stating whether or not the attempt on the Activity was successful.
        /// </summary>
        /// <param name="result">True if the attempt was successful, otherwise false.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        IResultBuilder WithSuccess(bool result);
    }
}