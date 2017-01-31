using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Mos.xApi
{
    public class LanguageMap : Dictionary<string, string>, ILanguageMap
    {
        public LanguageMap() { }

        public LanguageMap(IDictionary<string, string> dictionary) : base(dictionary)
        {
        }

        public string GetTranslation(string languageCode) => this[languageCode];
    }
}
