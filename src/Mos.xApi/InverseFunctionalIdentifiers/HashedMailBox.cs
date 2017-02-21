using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace Mos.xApi.InverseFunctionalIdentifiers
{
    /// <summary>
    /// Defines the identifier for an Actor that uses
    /// a HashedMailBox (a SHA1 hashed email address) as unique identification.
    /// </summary>
    public class HashedMailBox : IInverseFunctionalIdentifier
    {
        /// <summary>
        /// Initializes a new instance of the HashedMailBox class.
        /// </summary>
        /// <param name="hashedEmailAddress">The SHA-1 hashed representation of an email address uniquely identifying the Actor.</param>
        public HashedMailBox(string hashedEmailAddress)
        {
            HashedEmailAddress = hashedEmailAddress;
        }

        /// <summary>
        /// Gets the SHA-1 hashed representation of an email address uniquely identifying the Actor.
        /// </summary>
        [JsonProperty("mbox_sha1sum")]
        public string HashedEmailAddress { get; }

        /// <summary>
        /// Creates a HashedMailBox instance from a non-hashed email address. The emailAddress should not contain a mailto: (it is added automatically)
        /// </summary>
        /// <param name="emailAddress">The email address to create the HashedMailBox from.</param>
        /// <returns>The HashedMailBox instance that contains the passed email address.</returns>
        public static HashedMailBox FromEmailAddress(string emailAddress)
        {
            var emailAddressValidator = new EmailAddressAttribute();
            if (!emailAddressValidator.IsValid(emailAddress))
            {
                throw new ArgumentException($"{emailAddress} is not a valid e-mail address.", nameof(emailAddress));
            }

            var mailtoEmail = $"mailto:{emailAddress}";

            var sha1 = SHA1.Create();

            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(mailtoEmail));

            return new HashedMailBox(Encoding.UTF8.GetString(hash));
        }
    }
}
