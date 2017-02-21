using System;
using System.Collections.Generic;

namespace Mos.xApi.Objects
{
    /// <summary>
    /// Interface that defines the methods used to fluently build a SubStatement.
    /// </summary>
    public interface ISubStatementBuilder
    {
        /// <summary>
        /// Adds an attachment to the SubStatement
        /// </summary>
        /// <param name="attachment">The attachment to be added</param>
        /// <returns>The substatement builder, to continue the fluent configuration.</returns>
        ISubStatementBuilder AddAttachment(Attachment attachment);

        /// <summary>
        /// Adds a list of attachments to the SubStatement
        /// </summary>
        /// <param name="attachments">The list of attachments to be added</param>
        /// <returns>The substatement builder, to continue the fluent configuration.</returns>
        ISubStatementBuilder AddAttachments(IEnumerable<Attachment> attachments);
        
        /// <summary>
        /// Creates the SubStatement object constructed from the
        /// fluent configuration.
        /// </summary>
        /// <returns>The SubStatement object constructed.</returns>
        SubStatement Build();

        /// <summary>
        /// Specifies a context to the SubStatement.
        /// </summary>
        /// <param name="context">Context that gives the Statement more meaning. Examples: a team the Actor is working with, altitude at which a scenario was attempted in a flight simulator.</param>
        /// <returns>The substatement builder, to continue the fluent configuration.</returns>
        ISubStatementBuilder WithContext(Context context);

        /// <summary>
        /// Specifies a result to the SubStatement.
        /// </summary>
        /// <param name="result">Result Object, further details representing a measured outcome.</param>
        /// <returns>The substatement builder, to continue the fluent configuration.</returns>
        ISubStatementBuilder WithResult(Result result);

        /// <summary>
        /// Adds a time stamp to the SubStatement.
        /// </summary>
        /// <param name="timeStamp">Timestamp of when the events described within this Statement occurred. Set by the LRS if not provided.</param>
        /// <returns>The substatement builder, to continue the fluent configuration.</returns>
        ISubStatementBuilder WithTimeStamp(DateTime timeStamp);
    }
}