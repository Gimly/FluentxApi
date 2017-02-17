using Newtonsoft.Json;
using System;

namespace Mos.xApi.Objects
{
    /// <summary>
    /// This class defines a statement object that is called Statement Reference which a pointer to another pre-existing Statement.
    /// </summary>
    public class StatementReference : StatementObject
    {
        /// <summary>
        /// Initializes a new instance of a StatementReference class.
        /// </summary>
        /// <param name="id">The unique identifier of the referenced statement.</param>
        public StatementReference(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// The type of the object, used by the JSON serializer.
        /// </summary>
        [JsonProperty("objectType")]
        public const string ObjectType = "StatementRef";

        /// <summary>
        /// Gets the unique identifier of the referenced statement.
        /// </summary>
        [JsonProperty("id")]
        public Guid Id { get; }
    }
}
