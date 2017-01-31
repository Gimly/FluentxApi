using Mos.xApi.Data.InverseFunctionalIdentifiers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mos.xApi.Data.Actors
{
    public class Group : Actor
    {
        /// <summary>
        /// Initializes a new intance of <see cref="Group"/>. This constructor call creates an anonymous group.
        /// </summary>
        /// <param name="members"></param>
        /// <param name="name"></param>
        public Group(IEnumerable<Agent> members, string name = null) : base(null, name)
        {
            if (members == null || !members.Any())
                throw new ArgumentNullException(nameof(members));

            Members = members;
        }

        public Group(IInverseFunctionalIdentifier identifier, string name = null, IEnumerable<Agent> members = null)
            : base(identifier, name)
        {
            if (identifier == null)
                throw new ArgumentNullException(nameof(identifier));

            if (members != null && !members.Any())
                throw new ArgumentException("If members is passed, it has to have elements. Pass null if meant to be empty.", nameof(members));

            Members = members;
        }

        public override string ObjectType => nameof(Group);

        [JsonProperty("member", Order = 3)]
        public IEnumerable<Agent> Members { get; }
    }
}