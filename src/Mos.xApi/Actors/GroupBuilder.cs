using Mos.xApi.InverseFunctionalIdentifiers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mos.xApi.Actors
{
    /// <summary>
    /// Builder class used to simplify the creation of a Group, in a fluent interface manner.
    /// </summary>
    internal class GroupBuilder : IGroupBuilder
    {
        /// <summary>
        /// The name of the group
        /// </summary>
        private readonly string _name;

        /// <summary>
        /// The members of the group.
        /// </summary>
        private readonly List<Agent> _agents;

        /// <summary>
        /// Initializes a new instance of a GroupBuilder class.
        /// </summary>
        /// <param name="name">The name of the group.</param>
        public GroupBuilder(string name)
        {
            _name = name;
            _agents = new List<Agent>();
        }

        /// <summary>
        /// Add an Agent to the group.
        /// </summary>
        /// <param name="agent">The agent to be added.</param>
        /// <returns>The Group builder, to continue the fluent configuration.</returns>
        public IGroupBuilder Add(Agent agent)
        {
            _agents.Add(agent);
            return this;
        }

        /// <summary>
        /// Adds a list of Agents to the group.
        /// </summary>
        /// <param name="agents">The list of agents to be added.</param>
        /// <returns>The Group builder, to continue the fluent configuration.</returns>
        public IGroupBuilder AddRange(IEnumerable<Agent> agents)
        {
            _agents.AddRange(agents);
            return this;
        }

        /// <summary>
        /// Creates a Group as an anonymous group, with no Inverse Functional Identifier.
        /// <para>An Anonymous Group is used describe a cluster of people where there is no ready identifier for this cluster, e.g. an ad hoc team.</para>
        /// </summary>
        /// <returns>The created group.</returns>
        public Group AsAnonymous()
        {
            if (!_agents.Any())
            {
                throw new InvalidOperationException("Cannot create an anonymous group with no agents.");
            }

            return new Group(_agents, _name);
        }

        /// <summary>
        /// Creates a Group as an identified group, using an Account as an Inverse Functional Identifier. 
        /// <para>A user account on an existing system, such as a private system (LMS or intranet) or a public system (social networking site).</para>
        /// </summary>
        /// <param name="name">The unique id or name used to log in to this account. This is based on FOAF's accountName.</param>
        /// <param name="homePage">The canonical home page for the system the account is on. This is based on FOAF's accountServiceHomePage.</param>
        /// <returns>The created Group.</returns>
        public Group WithAccount(string name, string homePage)
        {
            return new Group(new Account(name, new Uri(homePage)), _name, _agents.Any() ? _agents : null);
        }

        /// <summary>
        /// Creates a Group as an identified group, using an Account as an Inverse Functional Identifier. 
        /// <para>A user account on an existing system, such as a private system (LMS or intranet) or a public system (social networking site).</para>
        /// </summary>
        /// <param name="name">The unique id or name used to log in to this account. This is based on FOAF's accountName.</param>
        /// <param name="homePage">The canonical home page for the system the account is on. This is based on FOAF's accountServiceHomePage.</param>
        /// <returns>The created Group.</returns>
        public Group WithAccount(string name, Uri homePage)
        {
            return new Group(new Account(name, homePage), _name, _agents.Any() ? _agents : null);
        }

        /// <summary>
        /// Creates a Group as an identified group, using a hashed email address as an Inverse Functional Identifier.
        /// <para>The hex-encoded SHA1 hash of a mailto IRI (i.e. the value of an mbox property). An LRS MAY include Groups with a matching hash when a request is based on an mbox.</para>
        /// </summary>
        /// <param name="hashedEmailAddress">The sha1sum of a personal mailbox URI name</param>
        /// <returns>The created Group.</returns>
        public Group WithHashedMailBox(string hashedEmailAddress)
        {
            return new Group(new HashedMailBox(hashedEmailAddress), _name, _agents.Any() ? _agents : null);
        }

        /// <summary>
        /// Creates a Group as an identified group, using a hashed email address as an Inverse Functional Identifier.
        /// <para>The hex-encoded SHA1 hash of a mailto IRI (i.e. the value of an mbox property). An LRS MAY include Agents with a matching hash when a request is based on an mbox.</para>
        /// </summary>
        /// <param name="emailAddress">An email address from which the sha1sum will be calculated and stored (do not include mailto:)</param>
        /// <returns>The created Group.</returns>
        public Group WithHashedMailBoxFromEmail(string emailAddress)
        {
            return new Group(HashedMailBox.FromEmailAddress(emailAddress), _name, _agents.Any() ? _agents : null);
        }

        /// <summary>
        /// Creates a Group as an identified group, using an email address as an Inverse Functional Identifier.
        /// <para>Only email addresses that have only ever been and will ever be assigned to this Group, but no others, SHOULD be used for this property and mbox_sha1sum.</para>
        /// </summary>
        /// <param name="emailAddress">The email address that identifies the Group. Do not add "mailto:", it is added automatically.</param>
        /// <returns>The created Group.</returns>
        public Group WithMailBox(string emailAddress)
        {
            return new Group(new MailBox(emailAddress), _name, _agents.Any() ? _agents : null);
        }

        /// <summary>
        /// Creates a Group as an identified group, using an OpenId URI as an Inverse Functional Identifier.
        /// </summary>
        /// <param name="openIdUri">An openID that uniquely identifies the Group.</param>
        /// <returns>The created Group.</returns>
        public Group WithOpenId(string openIdUri)
        {
            return new Group(new OpenId(new Uri(openIdUri)), _name, _agents.Any() ? _agents : null);
        }

        /// <summary>
        /// Creates a Group as an identified group, using an OpenId URI as an Inverse Functional Identifier.
        /// </summary>
        /// <param name="openIdUri">An openID that uniquely identifies the Group.</param>
        /// <returns>The created Group.</returns>
        public Group WithOpenId(Uri openIdUri)
        {
            return new Group(new OpenId(openIdUri), _name, _agents.Any() ? _agents : null);
        }
    }
}
