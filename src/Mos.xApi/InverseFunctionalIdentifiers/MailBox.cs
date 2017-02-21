using Mos.xApi.Utilities;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Mos.xApi.InverseFunctionalIdentifiers
{
    /// <summary>
    /// Defines the identifier for an Actor that uses
    /// a MailBox (an email address) as unique identification.
    /// </summary>
    public class MailBox : IInverseFunctionalIdentifier
    {
        /// <summary>
        /// Initializes a new instance of the MailBox class.
        /// </summary>
        /// <param name="emailAddress">The email address identifying the Actor.</param>
        public MailBox(string emailAddress)
        {
            var emailAddressValidator = new EmailAddressAttribute();
            if (!emailAddressValidator.IsValid(emailAddress))
            {
                throw new ArgumentException($"{emailAddress} is not a valid e-mail address.", nameof(emailAddress));
            }

            EmailAddress = emailAddress;
        }

        /// <summary>
        /// Gets the email address identifying the Actor
        /// </summary>
        [JsonProperty("mbox")]
        [JsonConverter(typeof(EmailJsonConverter))]
        public string EmailAddress { get; }
    }
}