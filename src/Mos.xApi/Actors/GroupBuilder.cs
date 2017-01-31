using Mos.xApi.InverseFunctionalIdentifiers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mos.xApi.Actors
{
    internal class GroupBuilder : IGroupBuilder
    {
        private readonly string _name;
        private readonly List<Agent> _agents;

        public GroupBuilder(string name)
        {
            _name = name;
            _agents = new List<Agent>();
        }

        public IGroupBuilder Add(Agent agent)
        {
            _agents.Add(agent);
            return this;
        }

        public IGroupBuilder AddRange(IEnumerable<Agent> agents)
        {
            _agents.AddRange(agents);
            return this;
        }

        public Group AsAnonymous()
        {
            if (!_agents.Any())
            {
                throw new InvalidOperationException("Cannot create an anonymous group with no agents.");
            }

            return new Group(_agents, _name);
        }

        public Group WithAccount(string name, string homePage)
        {
            return new Group(new Account(name, new Uri(homePage)), _name, _agents.Any() ? _agents : null);
        }

        public Group WithAccount(string name, Uri homePage)
        {
            return new Group(new Account(name, homePage), _name, _agents.Any() ? _agents : null);
        }

        public Group WithHashedMailBox(string hashedEmailAddress)
        {
            return new Group(new HashedMailBox(hashedEmailAddress), _name, _agents.Any() ? _agents : null);
        }

        public Group WithHashedMailBoxFromEmail(string emailAddress)
        {
            return new Group(HashedMailBox.FromEmailAddress(emailAddress), _name, _agents.Any() ? _agents : null);
        }

        public Group WithMailBox(string emailAddress)
        {
            return new Group(new MailBox(emailAddress), _name, _agents.Any() ? _agents : null);
        }

        public Group WithOpenId(string openIdUri)
        {
            return new Group(new OpenId(new Uri(openIdUri)), _name, _agents.Any() ? _agents : null);
        }

        public Group WithOpenId(Uri openIdUri)
        {
            return new Group(new OpenId(openIdUri), _name, _agents.Any() ? _agents : null);
        }
    }
}
