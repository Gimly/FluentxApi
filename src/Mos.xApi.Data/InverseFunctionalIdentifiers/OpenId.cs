using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mos.xApi.Data.InverseFunctionalIdentifiers
{
    public class OpenId : IInverseFunctionalIdentifier
    {
        public OpenId(Uri openIdUri)
        {
            OpenIdUri = openIdUri;
        }

        [JsonProperty("openid")]
        public Uri OpenIdUri { get; }
    }
}
