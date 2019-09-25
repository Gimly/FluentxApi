using Mos.xApi.InverseFunctionalIdentifiers;
using System;

namespace Mos.xApi.Actors
{
    /// <summary>
    /// Builder class used to simplify the creation of an Agent, in a fluent interface manner.
    /// </summary>
    internal class AgentBuilder : IAgentBuilder
    {
        /// <summary>
        /// The full name of the Agent.
        /// </summary>
        private readonly string _name;

        /// <summary>
        /// Initializes a new instance of an Agent class.
        /// </summary>
        /// <param name="name">The full name of the Agent.</param>
        internal AgentBuilder(string name)
        {
            _name = name;
        }

        /// <summary>
        /// Creates the Agent with an Account as an Inverse Functional Identifier. 
        /// A user account on an existing system, such as a private system (LMS or intranet) or a public system (social networking site).
        /// </summary>
        /// <param name="name">The unique id or name used to log in to this account. This is based on FOAF's accountName.</param>
        /// <param name="homePage">The canonical home page for the system the account is on. This is based on FOAF's accountServiceHomePage.</param>
        /// <returns>The created Agent.</returns>
        public Agent WithAccount(string name, string homePage)
        {
            return new Agent(new Account(name, new Uri(homePage)), _name);
        }

        /// <summary>
        /// Creates the Agent with an Account as an Inverse Functional Identifier. 
        /// A user account on an existing system, such as a private system (LMS or intranet) or a public system (social networking site).
        /// </summary>
        /// <param name="name">The unique id or name used to log in to this account. This is based on FOAF's accountName.</param>
        /// <param name="homePage">The canonical home page for the system the account is on. This is based on FOAF's accountServiceHomePage.</param>
        /// <returns>The created Agent.</returns>
        public Agent WithAccount(string name, Uri homePage)
        {
            return new Agent(new Account(name, homePage), _name);
        }

        /// <summary>
        /// Creates the Agent with a hashed email address as an Inverse Functional Identifier.
        /// <para>The hex-encoded SHA1 hash of a mailto IRI (i.e. the value of an mbox property). An LRS MAY include Agents with a matching hash when a request is based on an mbox.</para>
        /// </summary>
        /// <param name="hashedEmailAddress">The sha1sum of a personal mailbox URI name</param>
        /// <returns>The created Agent.</returns>
        public Agent WithHashedMailBox(string hashedEmailAddress)
        {
            return new Agent(new HashedMailBox(hashedEmailAddress), _name);
        }

        /// <summary>
        /// Creates the Agent with a hashed email address as an Inverse Functional Identifier.
        /// <para>The hex-encoded SHA1 hash of a mailto IRI (i.e. the value of an mbox property). An LRS MAY include Agents with a matching hash when a request is based on an mbox.</para>
        /// </summary>
        /// <param name="emailAddress">An email address from which the sha1sum will be calculated and stored (do not include mailto:)</param>
        /// <returns>The created Agent.</returns>
        public Agent WithHashedMailBoxFromEmail(string emailAddress)
        {
            return new Agent(HashedMailBox.FromEmailAddress(emailAddress), _name);
        }

        /// <summary>
        /// Creates the Agent with an email address as an Inverse Functional Identifier.
        /// <para>Only email addresses that have only ever been and will ever be assigned to this Agent, but no others, SHOULD be used for this property and mbox_sha1sum.</para>
        /// </summary>
        /// <param name="emailAddress">The email address that identifies the Agent. Do not add "mailto:", it is added automatically.</param>
        /// <returns>The created Agent.</returns>
        public Agent WithMailBox(string emailAddress)
        {
            return new Agent(new MailBox(emailAddress), _name);
        }

        /// <summary>
        /// Creates the Agent with an OpenId URI as an Inverse Functional Identifier.
        /// </summary>
        /// <param name="openIdUri">An openID that uniquely identifies the Agent.</param>
        /// <returns>The created Agent.</returns>
        public Agent WithOpenId(string openIdUri)
        {
            return new Agent(new OpenId(new Uri(openIdUri)), _name);
        }

        /// <summary>
        /// Creates the Agent with an OpenId URI as an Inverse Functional Identifier.
        /// </summary>
        /// <param name="openIdUri">An openID that uniquely identifies the Agent.</param>
        /// <returns>The created Agent.</returns>
        public Agent WithOpenId(Uri openIdUri)
        {
            return new Agent(new OpenId(openIdUri), _name);
        }
    }
}
