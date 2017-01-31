using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Mos.xApi.InverseFunctionalIdentifiers
{
    public class HashedMailBox : IInverseFunctionalIdentifier
    {
        public HashedMailBox(string hashedEmailAddress)
        {
            HashedEmailAddress = hashedEmailAddress;
        }

        [JsonProperty("mbox_sha1sum")]
        public string HashedEmailAddress { get; }

        public static HashedMailBox FromEmailAddress(string emailAddress)
        {
            //TODO Add checks that the email address is valid

            var sha1 = SHA1.Create();

            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(emailAddress));

            return new HashedMailBox(Encoding.UTF8.GetString(hash));
        }
    }
}
