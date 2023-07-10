using Mos.xApi.Actors;
using Mos.xApi.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mos.xApi.Builders
{
    /// <summary>
    /// Builder class used to simplify the creation of a Context, in a fluent interface manner.
    /// </summary>
    internal class ContextBuilder : IContextBuilder
    {
        /// <summary>
        /// List of activities that are used to categorize a Statement
        /// </summary>
        private readonly List<Activity> _categories;

        /// <summary>
        /// The list of extensions available to the Context
        /// </summary>
        private readonly Extension _extensions;

        /// <summary>
        /// A list of activities with an indirect relation to the Activity which is the Object of the Statement.
        /// </summary>
        private readonly List<Activity> _groupings;

        /// <summary>
        /// Instructor that the Statement relates to, if not included as the Actor of the Statement.
        /// </summary>
        private Actor _instructor;

        /// <summary>
        /// RFC5646 Code representing the language in which the experience being recorded in this Statement (mainly) occurred in, if applicable and known.
        /// </summary>
        private string _language;

        /// <summary>
        /// A list of context Activities that doesn't fit one of the other properties
        /// </summary>
        private readonly List<Activity> _others;

        /// <summary>
        /// A list of activities with a direct relation to the Activity which is the Object of the Statement.
        /// </summary>
        private readonly List<Activity> _parents;

        /// <summary>
        /// Platform used in the experience of this learning activity.
        /// </summary>
        private string _platform;

        /// <summary>
        /// The registration that the Statement is associated with.
        /// </summary>
        private Guid? _registration;

        /// <summary>
        /// Revision of the learning activity associated with this Statement.
        /// </summary>
        private string _revision;

        /// <summary>
        /// Another Statement to be considered as context for this Statement.
        /// </summary>
        private Guid? _statementReference;

        /// <summary>
        /// Team that this Statement relates to, if not included as the Actor of the Statement.
        /// </summary>
        private Group _team;

        /// <summary>
        /// Initializes a new instance of the ContextBuilder class.
        /// </summary>
        public ContextBuilder()
        {
            _categories = new List<Activity>();
            _parents = new List<Activity>();
            _others = new List<Activity>();
            _groupings = new List<Activity>();
            _extensions = new Extension();
        }

        /// <summary>
        /// Gets whether or not the Context has context activities. (he's considered to have one if he has
        /// categories, parents, groupings or other Activities).
        /// </summary>
        public bool HasContextActivities => 
            _categories.Any() || _parents.Any() || _others.Any() || _groupings.Any();

        /// <summary>
        /// Adds a list of activities that are used to categorize the Statement. "Tags" would be a synonym.
        /// <para>Category SHOULD be used to indicate a profile of xAPI behaviors, as well as other categorizations.</para>
        /// <para>For example: Anna attempts a biology exam, and the Statement is tracked using the cmi5 profile. The Statement's Activity refers to the exam, and the category is the cmi5 profile.</para>
        /// </summary>
        /// <param name="activities">The list of activities that represent a category of the Statement</param>
        /// <returns>The context builder, to continue the fluent configuration.</returns>
        public IContextBuilder AddCategories(IEnumerable<Activity> activities)
        {
            _categories.AddRange(activities);
            return this;
        }

        /// <summary>
        /// Adds an activity that is used to categorize the Statement. "Tags" would be a synonym.
        /// <para>Category SHOULD be used to indicate a profile of xAPI behaviors, as well as other categorizations.</para>
        /// <para>For example: Anna attempts a biology exam, and the Statement is tracked using the cmi5 profile. The Statement's Activity refers to the exam, and the category is the cmi5 profile.</para>
        /// </summary>
        /// <param name="activityBuilder">An Activity builder that is used to build a concrete Activity</param>
        /// <returns>The context builder, to continue the fluent configuration.</returns>
        public IContextBuilder AddCategory(IActivityBuilder activityBuilder) => 
            AddCategory(activityBuilder.Build());

        /// <summary>
        /// Adds an activity that is used to categorize the Statement. "Tags" would be a synonym.
        /// <para>Category SHOULD be used to indicate a profile of xAPI behaviors, as well as other categorizations.</para>
        /// <para>For example: Anna attempts a biology exam, and the Statement is tracked using the cmi5 profile. The Statement's Activity refers to the exam, and the category is the cmi5 profile.</para>
        /// </summary>
        /// <param name="activity">The Activity that represents a category of the Statement.</param>
        /// <returns>The context builder, to continue the fluent configuration.</returns>
        public IContextBuilder AddCategory(Activity activity)
        {
            _categories.Add(activity);
            return this;
        }

        /// <summary>
        /// Adds an extension represented by an Extension instance.
        /// </summary>
        /// <param name="extension">The extension representation.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        public IContextBuilder AddExtension(Extension extension)
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
        /// <param name="extension">The IRI of the extension</param>
        /// <param name="jsonContent">The json representation of the extension value</param>
        /// <returns>The builder class, for the fluent API.</returns>
        public IContextBuilder AddExtension(Uri extension, string jsonContent)
        {
            _extensions.Add(extension, jsonContent);
            return this;
        }

        /// <summary>
        /// Adds an extension represented by an IRI and json content.
        /// </summary>
        /// <param name="extensionUri">The IRI of the extension</param>
        /// <param name="jsonContent">The json representation of the extension value</param>
        /// <returns>The builder class, for the fluent API.</returns>
        public IContextBuilder AddExtension(string extensionUri, string jsonContent)
        {
            _extensions.Add(new Uri(extensionUri), jsonContent);
            return this;
        }

        /// <summary>
        /// Adds an Activity with an indirect relation to the Activity which is the Object of the Statement. 
        /// <para>For example: a course that is part of a qualification. The course has several classes. The course relates to a class as the parent, the qualification relates to the class as the grouping.</para>
        /// </summary>
        /// <param name="grouping">The activity that has an indirect relation to the Activity</param>
        /// <returns>The builder class, for the fluent API.</returns>
        public IContextBuilder AddGrouping(Activity grouping)
        {
            _groupings.Add(grouping);
            return this;
        }

        /// <summary>
        /// Adds a list of Activities with an indirect relation to the Activity which is the Object of the Statement. 
        /// <para>For example: a course that is part of a qualification. The course has several classes. The course relates to a class as the parent, the qualification relates to the class as the grouping.</para>
        /// </summary>
        /// <param name="groupings">The list of activities that have an indirect relation to the Activity</param>
        /// <returns>The builder class, for the fluent API.</returns>
        public IContextBuilder AddGroupings(IEnumerable<Activity> groupings)
        {
            _groupings.AddRange(groupings);
            return this;
        }

        /// <summary>
        /// Adds an Activity with an indirect relation to the Activity which is the Object of the Statement. 
        /// <para>For example: a course that is part of a qualification. The course has several classes. The course relates to a class as the parent, the qualification relates to the class as the grouping.</para>
        /// </summary>
        /// <param name="activityBuilder">An Activity builder that is used to build a concrete Activity</param>
        /// <returns>The builder class, for the fluent API.</returns>
        public IContextBuilder AddGroupings(IActivityBuilder activityBuilder) => 
            AddGrouping(activityBuilder.Build());

        /// <summary>
        /// Adds an Activity that that doesn't fit one of the other properties. 
        /// <para>For example: Anna studies a textbook for a biology exam. The Statement's Activity refers to the textbook, and the exam is a contextActivity of type other.</para>
        /// </summary>
        /// <param name="activityBuilder">An Activity builder that is used to build a concrete Activity</param>
        /// <returns>The builder class, for the fluent API.</returns>
        public IContextBuilder AddOther(IActivityBuilder activityBuilder) => 
            AddOther(activityBuilder.Build());

        /// <summary>
        /// Adds an Activity that that doesn't fit one of the other properties. 
        /// <para>For example: Anna studies a textbook for a biology exam. The Statement's Activity refers to the textbook, and the exam is a contextActivity of type other.</para>
        /// </summary>
        /// <param name="activity">The Activity to add as other.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        public IContextBuilder AddOther(Activity activity)
        {
            _others.Add(activity);
            return this;
        }

        /// <summary>
        /// Adds a list of Activities that that doesn't fit one of the other properties. 
        /// <para>For example: Anna studies a textbook for a biology exam. The Statement's Activity refers to the textbook, and the exam is a contextActivity of type other.</para>
        /// </summary>
        /// <param name="activities">The Activities to add as other.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        public IContextBuilder AddOthers(IEnumerable<Activity> activities)
        {
            _others.AddRange(activities);
            return this;
        }

        /// <summary>
        /// Adds an Activity with a direct relation to the Activity which is the Object of the Statement.
        /// In almost all cases there is only one sensible parent or none, not multiple.
        /// <para>For example: a Statement about a quiz question would have the quiz as its parent Activity.</para>
        /// </summary>
        /// <param name="activity">The Activity as a parent of the Statement.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        public IContextBuilder AddParent(Activity activity)
        {
            _parents.Add(activity);
            return this;
        }

        /// <summary>
        /// Adds an Activity with a direct relation to the Activity which is the Object of the Statement.
        /// In almost all cases there is only one sensible parent or none, not multiple.
        /// <para>For example: a Statement about a quiz question would have the quiz as its parent Activity.</para>
        /// </summary>
        /// <param name="activityBuilder">An Activity builder that is used to build a concrete Activity.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        public IContextBuilder AddParent(IActivityBuilder activityBuilder) => 
            AddParent(activityBuilder.Build());

        /// <summary>
        /// Adds a list of Activities with a direct relation to the Activity which is the Object of the Statement.
        /// In almost all cases there is only one sensible parent or none, not multiple.
        /// <para>For example: a Statement about a quiz question would have the quiz as its parent Activity.</para>
        /// </summary>
        /// <param name="activities">A list of Activities builder that is used to build a concrete Activity.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        public IContextBuilder AddParents(IEnumerable<Activity> activities)
        {
            _parents.AddRange(activities);
            return this;
        }

        /// <summary>
        /// Creates the Context object constructed from the
        /// fluent configuration.
        /// </summary>
        /// <returns>The Context object constructed.</returns>
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

        /// <summary>
        /// Sets the instructor that the Statement relates to, if not included as the Actor of the Statement.
        /// </summary>
        /// <param name="actor">The instructor relating to the Statement.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        public IContextBuilder WithInstructor(Actor actor)
        {
            _instructor = actor;
            return this;
        }

        /// <summary>
        /// Sets the RFC5646 code representing the language in which the experience being recorded in this Statement 
        /// (mainly) occurred in, if applicable and known.
        /// </summary>
        /// <param name="language">The RFC5646 language code.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        public IContextBuilder WithLanguage(string language)
        {
            _language = language;
            return this;
        }

        /// <summary>
        /// Sets the platform used in the experience of this learning activity.
        /// </summary>
        /// <param name="platform">The platform used in the experience of this learning activity.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        public IContextBuilder WithPlatform(string platform)
        {
            _platform = platform;
            return this;
        }

        /// <summary>
        /// Sets the registration that the Statement is associated with.
        /// </summary>
        /// <param name="registrationId">The identifier of the registration.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        public IContextBuilder WithRegistration(Guid registrationId)
        {
            _registration = registrationId;
            return this;
        }

        /// <summary>
        /// Sets the revision of the learning activity associated with this Statement. Format is free.
        /// </summary>
        /// <param name="revision">The revision of the learning activity.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        public IContextBuilder WithRevision(string revision)
        {
            _revision = revision;
            return this;
        }

        /// <summary>
        /// Sets another Statement to be considered as context for this Statement.
        /// </summary>
        /// <param name="statementReference">The identifier of the statement reference.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        public IContextBuilder WithStatementReference(Guid statementReference)
        {
            _statementReference = statementReference;
            return this;
        }

        /// <summary>
        /// Sets the team that this Statement relates to, if not included as the Actor of the Statement.
        /// </summary>
        /// <param name="team">The team that this Statement relates to.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        public IContextBuilder WithTeam(Group team)
        {
            _team = team;
            return this;
        }
    }
}