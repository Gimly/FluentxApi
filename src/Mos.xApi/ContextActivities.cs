using Mos.xApi.Objects;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Mos.xApi
{
    /// <summary>
    /// A class allowing to add a map of the types of learning activity context that the Statement is related to. 
    /// <para>Valid context types are: parent, "grouping", "category" and "other".</para>
    /// </summary>
    public class ContextActivities
    {
        /// <summary>
        /// Initializes a new instance of a ContextActivities containing the passed activities as context.
        /// </summary>
        /// <param name="parents">An Activity with a direct relation to the Activity which is the Object of the Statement. <para>In almost all cases there is only one sensible parent or none, not multiple. For example: a Statement about a quiz question would have the quiz as its parent Activity.</para></param>
        /// <param name="groupings">An Activity with an indirect relation to the Activity which is the Object of the Statement. <para>For example: a course that is part of a qualification. The course has several classes. The course relates to a class as the parent, the qualification relates to the class as the grouping.</para></param>
        /// <param name="categories">An Activity used to categorize the Statement. "Tags" would be a synonym. <para>Category SHOULD be used to indicate a profile of xAPI behaviors, as well as other categorizations.</para><para>For example: Anna attempts a biology exam, and the Statement is tracked using the cmi5 profile. The Statement's Activity refers to the exam, and the category is the cmi5 profile.</para></param>
        /// <param name="others">A context Activity that doesn't fit one of the other properties. <para>For example: Anna studies a textbook for a biology exam. The Statement's Activity refers to the textbook, and the exam is a contextActivity of type other.</para></param>
        public ContextActivities(
            IEnumerable<Activity> parents = null,
            IEnumerable<Activity> groupings = null,
            IEnumerable<Activity> categories = null,
            IEnumerable<Activity> others = null)
        {
            if (parents != null && parents.Any())
            {
                Parents = parents;
            }

            if (groupings != null && groupings.Any())
            {
                Groupings = groupings;
            }

            if (categories != null && categories.Any())
            {
                Categories = categories;
            }

            if (others != null && others.Any())
            {
                Others = others;
            }
        }

        /// <summary>
        /// Gets Activities with a direct relation to the Activity which is the Object of the Statement. <para>In almost all cases there is only one sensible parent or none, not multiple. For example: a Statement about a quiz question would have the quiz as its parent Activity.</para>
        /// </summary>
        [JsonProperty("category", Order = 2, NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Activity> Categories { get; }

        /// <summary>
        /// Gets Activities with an indirect relation to the Activity which is the Object of the Statement. <para>For example: a course that is part of a qualification. The course has several classes. The course relates to a class as the parent, the qualification relates to the class as the grouping.</para>
        /// </summary>
        [JsonProperty("grouping", Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Activity> Groupings { get; }

        /// <summary>
        /// Gets Activities used to categorize the Statement. "Tags" would be a synonym. <para>Category SHOULD be used to indicate a profile of xAPI behaviors, as well as other categorizations.</para><para>For example: Anna attempts a biology exam, and the Statement is tracked using the cmi5 profile. The Statement's Activity refers to the exam, and the category is the cmi5 profile.</para>
        /// </summary>
        [JsonProperty("other", Order = 3, NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Activity> Others { get; }

        /// <summary>
        /// Gets context Activities that doesn't fit one of the other properties. <para>For example: Anna studies a textbook for a biology exam. The Statement's Activity refers to the textbook, and the exam is a contextActivity of type other.</para>
        /// </summary>
        [JsonProperty("parent", Order = 0, NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Activity> Parents { get; }
    }
}