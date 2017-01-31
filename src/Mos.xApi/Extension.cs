using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Mos.xApi
{
    public class Extension : Dictionary<Uri, string>
    {
        public Extension(IDictionary<Uri, string> extensions) : base(extensions)
        {
        }

        public Extension() { }
    }
}