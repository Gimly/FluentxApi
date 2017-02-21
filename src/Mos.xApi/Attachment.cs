using System;

namespace Mos.xApi
{
    /// <summary>
    /// In some cases an Attachment is logically an important part of a Learning Record. It could be an essay, a video, etc.
    /// <para>Another example of such an Attachment is (the image of) a certificate that was granted as a result of an experience.</para>
    /// <para>It is useful to have a way to store these Attachments in and retrieve them from an LRS.</para>
    /// </summary>
    public class Attachment
    {
        /// <summary>
        /// Initializes a new instance of an Attachment.
        /// </summary>
        /// <param name="usageType">Identifies the usage of this Attachment. For example: one expected use case for Attachments is to include a "completion certificate".<para>An IRI corresponding to this usage MUST be coined, and used with completion certificate attachments.</para></param>
        /// <param name="display">Display name (title) of this Attachment.</param>
        /// <param name="contentType">The content type of the Attachment.</param>
        /// <param name="length">The length of the Attachment data in octets.</param>
        /// <param name="sha2">The SHA-2 hash of the Attachment data. This property is always required, even if fileURL is also specified.</param>
        /// <param name="description">A description of the Attachment</param>
        /// <param name="fileUrl">An IRL at which the Attachment data can be retrieved, or from which it used to be retrievable.</param>
        public Attachment(
            Uri usageType,
            ILanguageMap display,
            string contentType,
            int length,
            string sha2,
            ILanguageMap description = null,
            Uri fileUrl = null)
        {
            UsageType = usageType;
            Display = display;
            ContentType = contentType;
            Length = length;
            Sha2 = sha2;
            Description = description;
            FileUrl = fileUrl;
        }

        /// <summary>
        /// Gets the unique identifier that identifies the usage of this Attachment. For example: one expected use case for Attachments is to include a "completion certificate".<para>An IRI corresponding to this usage MUST be coined, and used with completion certificate attachments.</para>
        /// </summary>
        public Uri UsageType { get; }

        /// <summary>
        /// Gets the display name (title) of this Attachment.
        /// </summary>
        public ILanguageMap Display { get; }

        /// <summary>
        /// Gets the content type of the Attachment.
        /// </summary>
        public string ContentType { get; }

        /// <summary>
        /// Gets the length of the Attachment data in octets.
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Gets the SHA-2 hash of the Attachment data. This property is always required, even if fileURL is also specified.
        /// </summary>
        public string Sha2 { get; }

        /// <summary>
        /// Gets a description of the Attachment
        /// </summary>
        public ILanguageMap Description { get; }

        /// <summary>
        /// Gets an IRL at which the Attachment data can be retrieved, or from which it used to be retrievable.
        /// </summary>
        public Uri FileUrl { get; }
    }
}
