namespace Mos.xApi.Client
{
    /// <summary>
    /// Enum values that defines in which format the information in the result
    /// of the query should be returned.
    /// </summary>
    public enum StatementQueryFormat
    {
        /// <summary>
        /// Returns Agent, Activity, Verb and Group Objects populated exactly as they were
        /// when the Statement was received.
        /// </summary>
        Exact,

        /// <summary>
        /// Returns only the minimum information necessary in Agent, Verb and Group Objects
        /// to identify them.
        /// <para>For Anonymous Groups, this means including the minimum information
        /// needed to identify each member.</para>
        /// </summary>
        Ids,

        /// <summary>
        /// Returns Activity Objects and Verbs populated with the canonical definition of the
        /// Activity Objects and Display of the Verbs as determined by the LRS, after applying
        /// the language filtering process, and return the original Agent and Group Objects as
        /// in "exact" mode.
        /// </summary>
        Canonical
    }
}