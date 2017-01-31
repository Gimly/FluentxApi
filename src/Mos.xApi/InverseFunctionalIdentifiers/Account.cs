using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mos.xApi.InverseFunctionalIdentifiers
{
    public class Account : IInverseFunctionalIdentifier
    {
        /// <summary>
        /// <para>
        ///     Returns a new instance of <see cref="Account"/> class. This class represents an inverse functional identifier
        ///     of type account.
        /// </para>
        /// <para>
        ///     It's used to identify a user on an existing system such as a private system (LMS or intranet) or
        ///     a public system (social networking site).
        /// </para>
        /// </summary>
        /// <remarks>
        /// <para>If the system that provides the account object uses OpenID, it should use the OpenID class instead.</para>
        /// <para>
        ///     The <see cref="Uri"/> class is used here because it is the same as an IRI in the .Net world. Difference between URIs and IRIs
        ///     is that IRIs accept international characters. Since .Net 4.5 this is the default behavior for the <see cref="Uri"/>
        ///     class, and therefore it can parse IRI without issues.
        ///     See https://msdn.microsoft.com/en-us/library/system.uri(v=vs.110).aspx#Remarks for more information.
        /// </para>
        /// </remarks>
        /// <param name="name">The unique id or name used to log in to this account. This is based on FOAF's accountName.</param>
        /// <param name="homePage">The canonical home page for the system this account is on.  This is based on FOAF's accountServiceHomePage.</param>
        public Account(string name, Uri homePage)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!homePage.IsWellFormedOriginalString())
            {
                throw new ArgumentException($"Homepage uri is malformed: {homePage.ToString()}", nameof(homePage));
            }

            Name = name;
            HomePage = homePage;
        }

        /// <summary>
        /// Gets the unique id or name used to log in to this account.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; }

        /// <summary>
        /// Gets the canonical home page for the system this account is on.
        /// </summary>
        [JsonProperty("homePage")]
        public Uri HomePage { get; }
    }
}
