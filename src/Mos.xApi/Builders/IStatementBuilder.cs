using Mos.xApi.Actors;
using System;
using System.Collections.Generic;

namespace Mos.xApi.Builders
{
    /// <summary>
    /// Interface that defines a builder used to simplify the creation of a Statement, 
    /// in a fluent interface manner.
    /// </summary>
    public interface IStatementBuilder
    {
        /// <summary>
        /// Adds an Attachment to the Statement
        /// </summary>
        /// <param name="attachment">The attachment to add</param>
        /// <returns>The statement builder, to continue the fluent configuration.</returns>
        IStatementBuilder AddAttachment(Attachment attachment);

        /// <summary>
        /// Adds a list of Attachment to the Statement
        /// </summary>
        /// <param name="attachments">The list of attachments to add</param>
        /// <returns>The statement builder, to continue the fluent configuration.</returns>
        IStatementBuilder AddAttachments(IEnumerable<Attachment> attachments);

        /// <summary>
        /// Creates the Statement object constructed from the
        /// fluent configuration.
        /// </summary>
        /// <returns>The Statement object constructed.</returns>
        Statement Build();

        /// <summary>
        /// Sets an Agent or Group who is asserting this Statement is true. Verified by the LRS based on authentication. 
        /// <para>Set by LRS if not provided or if a strong trust relationship between the Learning Record Provider and LRS has not been established.</para>
        /// </summary>
        /// <param name="authority">The Agent or Group who is asserting the Statement is true.</param>
        /// <returns>The statement builder, to continue the fluent configuration.</returns>
        IStatementBuilder WithAuthority(Actor authority);

        /// <summary>
        /// Sets the Context that gives the Statement more meaning. 
        /// <para>Examples: a team the Actor is working with, altitude at which a scenario was attempted in a flight simulator.</para>
        /// </summary>
        /// <param name="context">The context of the Statement.</param>
        /// <returns>The statement builder, to continue the fluent configuration.</returns>
        IStatementBuilder WithContext(Context context);

        /// <summary>
        /// Sets the Context that gives the Statement more meaning. 
        /// <para>Examples: a team the Actor is working with, altitude at which a scenario was attempted in a flight simulator.</para>
        /// </summary>
        /// <param name="contextBuilder">A context builder.</param>
        /// <returns>The statement builder, to continue the fluent configuration.</returns>
        IStatementBuilder WithContext(IContextBuilder contextBuilder);

        /// <summary>
        /// Sets the Result Object, further details representing a measured outcome.
        /// </summary>
        /// <param name="result">The result object</param>
        /// <returns>The statement builder, to continue the fluent configuration.</returns>
        IStatementBuilder WithResult(Result result);

        /// <summary>
        /// Sets the Result Object, further details representing a measured outcome.
        /// </summary>
        /// <param name="resultBuilder">A result builder.</param>
        /// <returns>The statement builder, to continue the fluent configuration.</returns>
        IStatementBuilder WithResult(IResultBuilder resultBuilder);

        /// <summary>
        /// Sets the Timestamp of when the events described within this Statement occurred. Set by the LRS if not provided.
        /// </summary>
        /// <param name="timeStamp">The timestamp of when the events occured.</param>
        /// <returns>The statement builder, to continue the fluent configuration.</returns>
        IStatementBuilder WithTimeStamp(DateTime timeStamp);
    }
}