using Mos.xApi.Data.Actors;
using Mos.xApi.Data.Objects;
using System;
using System.Collections.Generic;

namespace Mos.xApi.Data
{
    public interface IContextBuilder : IContainsExtension<IContextBuilder>
    {
        IContextBuilder AddCategories(IEnumerable<Activity> activities);

        IContextBuilder AddCategory(Activity activity);

        IContextBuilder AddCategory(IActivityBuilder activityBuilder);

        IContextBuilder AddGrouping(Activity grouping);

        IContextBuilder AddGroupings(IEnumerable<Activity> groupings);

        IContextBuilder AddOther(Activity activity);

        IContextBuilder AddOther(IActivityBuilder activityBuilder);

        IContextBuilder AddOthers(IEnumerable<Activity> activities);

        IContextBuilder AddParent(Activity activity);

        IContextBuilder AddParents(IEnumerable<Activity> activities);

        Context Build();

        IContextBuilder WithInstructor(Actor actor);

        IContextBuilder WithLanguage(string language);

        IContextBuilder WithPlatform(string platform);

        IContextBuilder WithRegistration(Guid registrationId);

        IContextBuilder WithRevision(string revision);

        IContextBuilder WithStatementReference(Guid statementReference);

        IContextBuilder WithTeam(Group team);
    }
}