using Mos.xApi.InverseFunctionalIdentifiers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mos.xApi.Actors
{
    /// <summary>
    /// A Group represents a collection of Agents and can be used in most of the same situations an Agent can be used. There are two types of Groups: Anonymous Groups and Identified Groups.
    /// </summary>
    public class Group : Actor
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Group"/>. This constructor call creates an anonymous group.
        /// <para>An Anonymous Group is used describe a cluster of people where there is no ready identifier for this cluster, e.g. an ad hoc team.</para>
        /// </summary>
        /// <param name="members">The members of this Group. This is an unordered list.</param>
        /// <param name="name">Name of the Group.</param>
        public Group(IEnumerable<Agent> members, string name = null) : base(null, name)
        {
            if (members == null || !members.Any())
                throw new ArgumentNullException(nameof(members));

            Members = members;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Group"/>. This constructor call creates an identified group.
        /// <para>An Identified Group is used to uniquely identify a cluster of Agents.</para>
        /// </summary>
        /// <param name="identifier">An Inverse Functional Identifier unique to the Group.</param>
        /// <param name="members">The members of this Group. This is an unordered list.</param>
        /// <param name="name">Name of the Group.</param>
        public Group(IInverseFunctionalIdentifier identifier, string name = null, IEnumerable<Agent> members = null)
            : base(identifier, name)
        {
            if (identifier == null)
                throw new ArgumentNullException(nameof(identifier));

            if (members != null && !members.Any())
                throw new ArgumentException("If members is passed, it has to have elements. Pass null if meant to be empty.", nameof(members));

            Members = members;
        }

        /// <summary>
        /// The type of object, in this case "Group".
        /// </summary>
        public override string ObjectType => nameof(Group);

        /// <summary>
        /// Gets the members of this Group. This is an unordered list.
        /// </summary>
        [JsonProperty("member", Order = 3)]
        public IEnumerable<Agent> Members { get; }
    }
}