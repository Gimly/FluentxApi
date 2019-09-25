using Mos.xApi.Builders;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Mos.xApi
{
    /// <summary>
    /// A <see cref="Verb"/> defines the action between an <see cref="Actors.Actor"/> and an <see cref="Objects.Activity"/>.
    /// </summary>
    public class Verb
    {
        /// <summary>
        /// Private constructor is used Json.Net when deserializing.
        /// </summary>
        private Verb()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Verb"/> class with an IRI as an identifier and display information.
        /// </summary>
        /// <param name="id">IRI that corresponds to a Verb definition.</param>
        /// <param name="display">The human readable representation of the Verb in one or more languages. Key is the language code.</param>
        public Verb(Uri id, ILanguageMap display)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            Id = id;

            if (display != null && display.Any())
            {
                Display = display;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Verb"/> class with an IRI as an identifier and display information.
        /// </summary>
        /// <remarks>While not mandatory, it should be preferred to pass the display.</remarks>
        /// <param name="id">IRI that corresponds to a Verb definition.</param>
        public Verb(Uri id) : this(id, null) { }

        /// <summary>
        /// Gets the IRI that corresponds to a Verb definition.
        /// </summary>
        [JsonProperty("id", Order = 0)]
        public Uri Id { get; private set; }

        /// <summary>
        /// The human readable representation of the Verb in one or more languages. Key is the language code.
        /// Returns null if no display is defined.
        /// </summary>
        [JsonProperty("display", Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        public ILanguageMap Display { get; private set; }

        /// <summary>
        /// Starts creating a new Verb with an IRI as an identifier.
        /// </summary>
        /// <param name="idUri">Corresponds to a Verb definition. Each Verb definition corresponds to the meaning of a Verb, not the word.</param>
        /// <returns>A builder that helps creating the rest of the Verb's properties.</returns>
        public static IVerbBuilder Create(string idUri)
        {
            return new VerbBuilder(idUri);
        }
        /// <summary>
        /// Starts creating a new Verb with an IRI as an identifier.
        /// </summary>
        /// <param name="id">Corresponds to a Verb definition. Each Verb definition corresponds to the meaning of a Verb, not the word.</param>
        /// <returns>A builder that helps creating the rest of the Verb's properties.</returns>
        public static IVerbBuilder Create(Uri id)
        {
            return new VerbBuilder(id);
        }
    }
}