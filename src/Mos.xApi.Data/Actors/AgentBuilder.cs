using Mos.xApi.Data.InverseFunctionalIdentifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mos.xApi.Data.Actors
{
    internal class AgentBuilder : IAgentBuilder
    {
        private string _name;

        internal AgentBuilder(string name)
        {
            _name = name;
        }

        public Agent WithAccount(string name, string homePage)
        {
            return new Agent(new Account(name, new Uri(homePage)), _name);
        }

        public Agent WithAccount(string name, Uri homePage)
        {
            return new Agent(new Account(name, homePage), _name);
        }

        public Agent WithHashedMailBox(string hashedEmailAddress)
        {
            return new Agent(new HashedMailBox(hashedEmailAddress), _name);
        }

        public Agent WithHashedMailBoxFromEmail(string emailAddress)
        {
            return new Agent(HashedMailBox.FromEmailAddress(emailAddress), _name);
        }

        public Agent WithMailBox(string emailAddress)
        {
            return new Agent(new MailBox(emailAddress), _name);
        }

        public Agent WithOpenId(string openIdUri)
        {
            return new Agent(new OpenId(new Uri(openIdUri)), _name);
        }

        public Agent WithOpenId(Uri openIdUri)
        {
            return new Agent(new OpenId(openIdUri), _name);
        }
    }
}
