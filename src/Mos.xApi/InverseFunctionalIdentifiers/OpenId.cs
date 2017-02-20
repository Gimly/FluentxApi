using Newtonsoft.Json;
using System;

namespace Mos.xApi.InverseFunctionalIdentifiers
{
    /// <summary>
    /// Defines the identifier for an Actor that uses
    /// an OpenId as unique identification.
    /// </summary>
    public class OpenId : IInverseFunctionalIdentifier
    {
        /// <summary>
        /// Initializes a new instance of an OpenId class
        /// </summary>
        /// <param name="openIdUri">The unique OpenId URI identifying the Actor.</param>
        public OpenId(Uri openIdUri)
        {
            OpenIdUri = openIdUri;
        }

        /// <summary>
        /// Gets the unique OpenId URI identifying the Actor.
        /// </summary>
        [JsonProperty("openid")]
        public Uri OpenIdUri { get; }
    }
}
