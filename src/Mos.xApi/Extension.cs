using System;
using System.Collections.Generic;

namespace Mos.xApi
{
    /// <summary>
    /// Represents a list of extension represented as a dictionary whose key is the IRI of the extension
    /// and the value is the json representation of the extension's value.
    /// <para>
    ///     Extensions are available as part of Activity Definitions, as part of a Statement's "context" property, 
    ///     or as part of a Statement's "result" property.
    /// </para>
    /// <para>
    ///     In each case, extensions are intended to provide a 
    ///     natural way to extend those properties for some specialized use.
    /// </para>
    /// <para>
    ///     The contents of these extensions might be something valuable to just one application, or it might be 
    ///     a convention used by an entire Community of Practice.
    /// </para>
    /// </summary>
    public class Extension : Dictionary<Uri, string>
    {
        /// <summary>
        /// Initializes a new instance of the Extension dictionary, that contains 
        /// elements that are copied from the passed dictionary.
        /// </summary>
        /// <param name="extensions">The dictionary whose values are copied to the Extension</param>
        public Extension(IDictionary<Uri, string> extensions) : base(extensions)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Extension disctionary which is empty.
        /// </summary>
        public Extension() { }
    }
}