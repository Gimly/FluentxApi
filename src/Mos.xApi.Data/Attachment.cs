using System;

namespace Mos.xApi.Data
{
    public class Attachment
    {
        public Attachment(Uri usageType)
        {
            UsageType = usageType;
        }

        public Uri UsageType { get; set; }
    }
}
