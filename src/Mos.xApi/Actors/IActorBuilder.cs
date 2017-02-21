using System;

namespace Mos.xApi.Actors
{
    /// <summary>
    /// Base interface for the Actor builders interfaces.
    /// </summary>
    /// <typeparam name="T">The type of Actor being built, either Agent or Group</typeparam>
    public interface IActorBuilder<T> where T : Actor
    {
        /// <summary>
        /// Creates the Actor with an email address as an Inverse Functional Identifier.
        /// <para>Only email addresses that have only ever been and will ever be assigned to this Actor, but no others, SHOULD be used for this property and mbox_sha1sum.</para>
        /// </summary>
        /// <param name="emailAddress">The email address that identifies the Actor. Do not add "mailto:", it is added automatically.</param>
        /// <returns>The created Actor.</returns>
        T WithMailBox(string emailAddress);

        /// <summary>
        /// Creates the Actor with a hashed email address as an Inverse Functional Identifier.
        /// <para>The hex-encoded SHA1 hash of a mailto IRI (i.e. the value of an mbox property). An LRS MAY include Actors with a matching hash when a request is based on an mbox.</para>
        /// </summary>
        /// <param name="hashedEmailAddress">The sha1sum of a personal mailbox URI name</param>
        /// <returns>The created Actor.</returns>
        T WithHashedMailBox(string hashedEmailAddress);

        /// <summary>
        /// Creates the Actor with a hashed email address as an Inverse Functional Identifier.
        /// <para>The hex-encoded SHA1 hash of a mailto IRI (i.e. the value of an mbox property). An LRS MAY include Actors with a matching hash when a request is based on an mbox.</para>
        /// </summary>
        /// <param name="emailAddress">An email address from which the sha1sum will be calculated and stored (do not include mailto:)</param>
        /// <returns>The created Actor.</returns>
        T WithHashedMailBoxFromEmail(string emailAddress);

        /// <summary>
        /// Creates the Actor with an OpenId URI as an Inverse Functional Identifier.
        /// </summary>
        /// <param name="openIdUri">An openID that uniquely identifies the Actor.</param>
        /// <returns>The created Actor.</returns>
        T WithOpenId(Uri openIdUri);

        /// <summary>
        /// Creates the Actor with an OpenId URI as an Inverse Functional Identifier.
        /// </summary>
        /// <param name="openIdUri">An openID that uniquely identifies the Actor.</param>
        /// <returns>The created Actor.</returns>
        T WithOpenId(string openIdUri);

        /// <summary>
        /// Creates the Actor with an Account as an Inverse Functional Identifier. 
        /// A user account on an existing system, such as a private system (LMS or intranet) or a public system (social networking site).
        /// </summary>
        /// <param name="name">The unique id or name used to log in to this account. This is based on FOAF's accountName.</param>
        /// <param name="homePage">The canonical home page for the system the account is on. This is based on FOAF's accountServiceHomePage.</param>
        /// <returns>The created Actor.</returns>
        T WithAccount(string name, Uri homePage);

        /// <summary>
        /// Creates the Actor with an Account as an Inverse Functional Identifier. 
        /// A user account on an existing system, such as a private system (LMS or intranet) or a public system (social networking site).
        /// </summary>
        /// <param name="name">The unique id or name used to log in to this account. This is based on FOAF's accountName.</param>
        /// <param name="homePage">The canonical home page for the system the account is on. This is based on FOAF's accountServiceHomePage.</param>
        /// <returns>The created Actor.</returns>
        T WithAccount(string name, string homePage);
    }
}
