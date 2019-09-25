using Mos.xApi.Actors;
using Mos.xApi.Objects;
using System;
using System.Collections.Generic;

namespace Mos.xApi.Builders
{
    /// <summary>
    /// Interface that defines a builder used to simplify the creation of a Context, 
    /// in a fluent interface manner.
    /// </summary>
    public interface IContextBuilder : IContainsExtension<IContextBuilder>
    {
        /// <summary>
        /// Adds a list of activities that are used to categorize the Statement. "Tags" would be a synonym.
        /// <para>Category SHOULD be used to indicate a profile of xAPI behaviors, as well as other categorizations.</para>
        /// <para>For example: Anna attempts a biology exam, and the Statement is tracked using the cmi5 profile. The Statement's Activity refers to the exam, and the category is the cmi5 profile.</para>
        /// </summary>
        /// <param name="activities">The list of activities that represent a category of the Statement</param>
        /// <returns>The context builder, to continue the fluent configuration.</returns>
        IContextBuilder AddCategories(IEnumerable<Activity> activities);
        
        /// <summary>
        /// Adds an activity that is used to categorize the Statement. "Tags" would be a synonym.
        /// <para>Category SHOULD be used to indicate a profile of xAPI behaviors, as well as other categorizations.</para>
        /// <para>For example: Anna attempts a biology exam, and the Statement is tracked using the cmi5 profile. The Statement's Activity refers to the exam, and the category is the cmi5 profile.</para>
        /// </summary>
        /// <param name="activity">The Activity that represents a category of the Statement.</param>
        /// <returns>The context builder, to continue the fluent configuration.</returns>
        IContextBuilder AddCategory(Activity activity);

        /// <summary>
        /// Adds an activity that is used to categorize the Statement. "Tags" would be a synonym.
        /// <para>Category SHOULD be used to indicate a profile of xAPI behaviors, as well as other categorizations.</para>
        /// <para>For example: Anna attempts a biology exam, and the Statement is tracked using the cmi5 profile. The Statement's Activity refers to the exam, and the category is the cmi5 profile.</para>
        /// </summary>
        /// <param name="activityBuilder">An Activity builder that is used to build a concrete Activity</param>
        /// <returns>The context builder, to continue the fluent configuration.</returns>
        IContextBuilder AddCategory(IActivityBuilder activityBuilder);

        /// <summary>
        /// Adds an Activity with an indirect relation to the Activity which is the Object of the Statement. 
        /// <para>For example: a course that is part of a qualification. The course has several classes. The course relates to a class as the parent, the qualification relates to the class as the grouping.</para>
        /// </summary>
        /// <param name="grouping">The activity that has an indirect relation to the Activity</param>
        /// <returns>The builder class, for the fluent API.</returns>
        IContextBuilder AddGrouping(Activity grouping);

        /// <summary>
        /// Adds a list of Activities with an indirect relation to the Activity which is the Object of the Statement. 
        /// <para>For example: a course that is part of a qualification. The course has several classes. The course relates to a class as the parent, the qualification relates to the class as the grouping.</para>
        /// </summary>
        /// <param name="groupings">The list of activities that have an indirect relation to the Activity</param>
        /// <returns>The builder class, for the fluent API.</returns>
        IContextBuilder AddGroupings(IEnumerable<Activity> groupings);

        /// <summary>
        /// Adds an Activity with an indirect relation to the Activity which is the Object of the Statement. 
        /// <para>For example: a course that is part of a qualification. The course has several classes. The course relates to a class as the parent, the qualification relates to the class as the grouping.</para>
        /// </summary>
        /// <param name="grouping">An Activity builder that is used to build a concrete Activity</param>
        /// <returns>The builder class, for the fluent API.</returns>
        IContextBuilder AddGroupings(IActivityBuilder grouping);

        /// <summary>
        /// Adds an Activity that that doesn't fit one of the other properties. 
        /// <para>For example: Anna studies a textbook for a biology exam. The Statement's Activity refers to the textbook, and the exam is a contextActivity of type other.</para>
        /// </summary>
        /// <param name="activity">The Activity to add as other.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        IContextBuilder AddOther(Activity activity);

        /// <summary>
        /// Adds an Activity that that doesn't fit one of the other properties. 
        /// <para>For example: Anna studies a textbook for a biology exam. The Statement's Activity refers to the textbook, and the exam is a contextActivity of type other.</para>
        /// </summary>
        /// <param name="activityBuilder">An Activity builder that is used to build a concrete Activity</param>
        /// <returns>The builder class, for the fluent API.</returns>
        IContextBuilder AddOther(IActivityBuilder activityBuilder);

        /// <summary>
        /// Adds a list of Activities that that doesn't fit one of the other properties. 
        /// <para>For example: Anna studies a textbook for a biology exam. The Statement's Activity refers to the textbook, and the exam is a contextActivity of type other.</para>
        /// </summary>
        /// <param name="activities">The Activities to add as other.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        IContextBuilder AddOthers(IEnumerable<Activity> activities);

        /// <summary>
        /// Adds an Activity with a direct relation to the Activity which is the Object of the Statement.
        /// In almost all cases there is only one sensible parent or none, not multiple.
        /// <para>For example: a Statement about a quiz question would have the quiz as its parent Activity.</para>
        /// </summary>
        /// <param name="activity">The Activity as a parent of the Statement.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        IContextBuilder AddParent(Activity activity);

        /// <summary>
        /// Adds an Activity with a direct relation to the Activity which is the Object of the Statement.
        /// In almost all cases there is only one sensible parent or none, not multiple.
        /// <para>For example: a Statement about a quiz question would have the quiz as its parent Activity.</para>
        /// </summary>
        /// <param name="activityBuilder">An Activity builder that is used to build a concrete Activity.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        IContextBuilder AddParent(IActivityBuilder activityBuilder);

        /// <summary>
        /// Adds a list of Activities with a direct relation to the Activity which is the Object of the Statement.
        /// In almost all cases there is only one sensible parent or none, not multiple.
        /// <para>For example: a Statement about a quiz question would have the quiz as its parent Activity.</para>
        /// </summary>
        /// <param name="activities">A list of Activities builder that is used to build a concrete Activity.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        IContextBuilder AddParents(IEnumerable<Activity> activities);

        /// <summary>
        /// Creates the Context object constructed from the
        /// fluent configuration.
        /// </summary>
        /// <returns>The Context object constructed.</returns>
        Context Build();

        /// <summary>
        /// Sets the instructor that the Statement relates to, if not included as the Actor of the Statement.
        /// </summary>
        /// <param name="actor">The instructor relating to the Statement.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        IContextBuilder WithInstructor(Actor actor);

        /// <summary>
        /// Sets the RFC5646 code representing the language in which the experience being recorded in this Statement 
        /// (mainly) occurred in, if applicable and known.
        /// </summary>
        /// <param name="language">The RFC5646 language code.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        IContextBuilder WithLanguage(string language);

        /// <summary>
        /// Sets the platform used in the experience of this learning activity.
        /// </summary>
        /// <param name="platform">The platform used in the experience of this learning activity.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        IContextBuilder WithPlatform(string platform);

        /// <summary>
        /// Sets the registration that the Statement is associated with.
        /// </summary>
        /// <param name="registrationId">The identifier of the registration.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        IContextBuilder WithRegistration(Guid registrationId);

        /// <summary>
        /// Sets the revision of the learning activity associated with this Statement. Format is free.
        /// </summary>
        /// <param name="revision">The revision of the learning activity.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        IContextBuilder WithRevision(string revision);

        /// <summary>
        /// Sets another Statement to be considered as context for this Statement.
        /// </summary>
        /// <param name="statementReference">The identifier of the statement reference.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        IContextBuilder WithStatementReference(Guid statementReference);

        /// <summary>
        /// Sets the team that this Statement relates to, if not included as the Actor of the Statement.
        /// </summary>
        /// <param name="team">The team that this Statement relates to.</param>
        /// <returns>The builder class, for the fluent API.</returns>
        IContextBuilder WithTeam(Group team);
    }
}