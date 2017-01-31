using Mos.xApi.Data.Actors;
using System;

namespace Mos.xApi.Data.Objects
{
    public abstract class StatementObject
    {
        public static IActivityBuilder CreateActivity(string idUri) => new ActivityBuilder(idUri);

        public static IActivityBuilder CreateActivity(Uri id) => new ActivityBuilder(id);

        public static StatementReference CreateStatementReference(Guid referenceId) => new StatementReference(referenceId);

        public static ISubStatementBuilder CreateSubStatement(Actor actor, Verb verb, StatementObject statementObject)
        {
            if (statementObject is SubStatement)
            {
                throw new ArgumentException("A substatement cannot have a substatement as object");
            }
            return new SubStatementBuilder(actor, verb, statementObject);
        }

        public static ISubStatementBuilder CreateSubStatement(Agent agent, IVerbBuilder verbBuilder, IActivityBuilder activityBuilder)
        {
            return CreateSubStatement(agent, verbBuilder.Build(), activityBuilder.Build());
        }
    }
}