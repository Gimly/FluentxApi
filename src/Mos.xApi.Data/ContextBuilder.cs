using Mos.xApi.Data.Actors;
using Mos.xApi.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mos.xApi.Data
{
    internal class ContextBuilder : IContextBuilder
    {
        private List<Activity> _categories;
        private Extension _extensions;
        private List<Activity> _groupings;
        private Actor _instructor;
        private string _language;
        private List<Activity> _others;
        private List<Activity> _parents;
        private string _platform;
        private Guid? _registration;
        private string _revision;
        private Guid? _statementReference;
        private Group _team;

        public ContextBuilder()
        {
            _categories = new List<Activity>();
            _parents = new List<Activity>();
            _others = new List<Activity>();
            _groupings = new List<Activity>();
            _extensions = new Extension();
        }

        public bool HasContextActivities => _categories.Any() || _parents.Any() || _others.Any() || _groupings.Any();

        public IContextBuilder AddCategories(IEnumerable<Activity> activities)
        {
            _categories.AddRange(activities);
            return this;
        }

        public IContextBuilder AddCategory(IActivityBuilder activityBuilder) => AddCategory(activityBuilder.Build());

        public IContextBuilder AddCategory(Activity activity)
        {
            _categories.Add(activity);
            return this;
        }

        public IContextBuilder AddExtension(Extension extension)
        {
            foreach (var item in extension)
            {
                _extensions.Add(item.Key, item.Value);
            }
            return this;
        }

        public IContextBuilder AddExtension(Uri extension, string jsonContent)
        {
            _extensions.Add(extension, jsonContent);
            return this;
        }

        public IContextBuilder AddExtension(string extensionUri, string jsonContent)
        {
            _extensions.Add(new Uri(extensionUri), jsonContent);
            return this;
        }

        public IContextBuilder AddGrouping(Activity grouping)
        {
            _groupings.Add(grouping);
            return this;
        }

        public IContextBuilder AddGroupings(IEnumerable<Activity> groupings)
        {
            _groupings.AddRange(groupings);
            return this;
        }

        public IContextBuilder AddOther(IActivityBuilder activityBuilder) => AddOther(activityBuilder.Build());

        public IContextBuilder AddOther(Activity activity)
        {
            _others.Add(activity);
            return this;
        }

        public IContextBuilder AddOthers(IEnumerable<Activity> activities)
        {
            _others.AddRange(activities);
            return this;
        }

        public IContextBuilder AddParent(Activity activity)
        {
            _parents.Add(activity);
            return this;
        }

        public IContextBuilder AddParents(IEnumerable<Activity> activities)
        {
            _parents.AddRange(_parents);
            return this;
        }

        public Context Build()
        {
            ContextActivities contextActivities = null;
            if (HasContextActivities)
            {
                contextActivities = new ContextActivities(_parents, _groupings, _categories, _others);
            }

            return new Context(_registration,
                               _instructor,
                               _team,
                               contextActivities,
                               _revision,
                               _platform,
                               _language,
                               _statementReference.HasValue ? new StatementReference(_statementReference.Value) : null,
                               _extensions);
        }

        public IContextBuilder WithInstructor(Actor actor)
        {
            _instructor = actor;
            return this;
        }

        public IContextBuilder WithLanguage(string language)
        {
            _language = language;
            return this;
        }

        public IContextBuilder WithPlatform(string platform)
        {
            _platform = platform;
            return this;
        }

        public IContextBuilder WithRegistration(Guid registrationId)
        {
            _registration = registrationId;
            return this;
        }

        public IContextBuilder WithRevision(string revision)
        {
            _revision = revision;
            return this;
        }

        public IContextBuilder WithStatementReference(Guid statementReference)
        {
            _statementReference = statementReference;
            return this;
        }

        public IContextBuilder WithTeam(Group team)
        {
            _team = team;
            return this;
        }
    }
}