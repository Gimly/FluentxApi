using System;
using System.Text;

namespace Mos.xApi
{
    public abstract class Document
    {
        protected Document(string id, DateTime updated, byte[] content)
        {
            Id = id;
            Updated = updated;
            Content = content;
        }

        public string Id { get; }

        DateTime Updated { get; }

        byte[] Content { get; }

        string StringContent
        {
            get
            {
                return Encoding.ASCII.GetString(Content);
            }
        }
    }
}
