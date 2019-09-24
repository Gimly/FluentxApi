using System;
using System.Text;

namespace Mos.xApi
{
    /// <summary>
    /// This class describes the base properties and functions for all the different
    /// Documents an LRS can store with the Document APIs.
    /// <para>
    ///     The Experience API provides a facility for Learning Record Providers to save arbitrary data 
    ///     in the form of documents, perhaps related to an Activity, Agent, or combination of both.</para>
    /// </summary>
    public abstract class Document
    {
        /// <summary>
        /// Initializes a new instance of the Document class.
        /// </summary>
        /// <param name="id">Identifier set by Learning Record Provider, unique within the scope of the Agent or Activity.</param>
        /// <param name="updated">When the document was most recently modified.</param>
        /// <param name="content">The contents of the document</param>
        protected Document(string id, DateTime updated, byte[] content)
        {
            Id = id;
            Updated = updated;
            Content = content;
        }

        /// <summary>
        /// Gets the identifier set by Learning Record Provider, unique within the scope of the Agent or Activity.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets the date and time when the document was most recently modified.
        /// </summary>
        public DateTime Updated { get; }

        /// <summary>
        /// Gets the contents of the document.
        /// </summary>
        public byte[] Content { get; }

        /// <summary>
        /// Gets the content of the document decoded from the byte array through ASCII encoding.
        /// </summary>
        public string StringContent
        {
            get
            {
                return Encoding.ASCII.GetString(Content);
            }
        }
    }
}
