using Mos.xApi.Actors;
using Mos.xApi.Builders;
using System;

namespace Mos.xApi.Objects
{
    /// <summary>
    /// Base class for all the Statement object types. A statement object is an Activity, Agent, or another 
    /// Statement that is the Object of the Statement.
    /// </summary>
    public abstract class StatementObject
    {
        /// <summary>
        /// Starts the creation of an Activity object.
        /// </summary>
        /// <param name="idUri">Unique identifier that defines the activity</param>
        /// <returns>A builder class that allows to fluently configure an Activity.</returns>
        public static IActivityBuilder CreateActivity(string idUri) => new ActivityBuilder(idUri);

        /// <summary>
        /// Starts the creation of an Activity object.
        /// </summary>
        /// <param name="id">Unique identifier that defines the activity</param>
        /// <returns>A builder class that allows to fluently configure an Activity.</returns>
        public static IActivityBuilder CreateActivity(Uri id) => new ActivityBuilder(id);

        /// <summary>
        /// Creates a StatementReference object.
        /// </summary>
        /// <param name="referenceId">The identifier of the referenced statement.</param>
        /// <returns></returns>
        public static StatementReference CreateStatementReference(Guid referenceId) => new StatementReference(referenceId);

        /// <summary>
        /// Starts the creation of a SubStatement object.
        /// </summary>
        /// <param name="actor">Whom the SubStatement is about, as an Agent or Group Object.</param>
        /// <param name="verb">Action taken by the Actor.</param>
        /// <param name="statementObject">Activity or Agent that is the Object of the SubStatement.</param>
        /// <returns>A builder class that allows to fluently configure a SubStatement.</returns>
        public static ISubStatementBuilder CreateSubStatement(Actor actor, Verb verb, StatementObject statementObject)
        {
            if (statementObject is SubStatement)
            {
                throw new ArgumentException("A substatement cannot have a substatement as object");
            }
            return new SubStatementBuilder(actor, verb, statementObject);
        }

        /// <summary>
        /// Starts the creation of a SubStatement object.
        /// </summary>
        /// <param name="actor">Whom the SubStatement is about, as an Agent or Group Object.</param>
        /// <param name="verbBuilder">A builder class that creates a Verb.</param>
        /// <param name="activityBuilder">A builder class that creates an Activity.</param>
        /// <returns>A builder class that allows to fluently configure a SubStatement.</returns>
        public static ISubStatementBuilder CreateSubStatement(Actor actor, IVerbBuilder verbBuilder, IActivityBuilder activityBuilder)
        {
            return CreateSubStatement(actor, verbBuilder.Build(), activityBuilder.Build());
        }
    }
}