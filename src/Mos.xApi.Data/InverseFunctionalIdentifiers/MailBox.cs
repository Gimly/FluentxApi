using Mos.xApi.Data.Utilities;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Mos.xApi.Data.InverseFunctionalIdentifiers
{
    public class MailBox : IInverseFunctionalIdentifier
    {
        public MailBox(string emailAddress)
        {
            var emailAddressValidator = new EmailAddressAttribute();
            if (!emailAddressValidator.IsValid(emailAddress))
            {
                throw new ArgumentException($"{emailAddress} is not a valid e-mail address.", nameof(emailAddress));
            }

            EmailAddress = emailAddress;
        }

        [JsonProperty("mbox")]
        [JsonConverter(typeof(EmailJsonConverter))]
        public string EmailAddress { get; }
    }
}