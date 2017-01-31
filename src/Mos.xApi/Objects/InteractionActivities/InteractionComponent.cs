using System;

namespace Mos.xApi.Objects.InteractionActivities
{
    public class InteractionComponent
    {
        public InteractionComponent(string id, ILanguageMap description)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            Id = id;
            Description = description;
        }

        public InteractionComponent(string id) : this(id, null)
        {
        }

        public ILanguageMap Description { get; }
        public string Id { get; }
    }
}