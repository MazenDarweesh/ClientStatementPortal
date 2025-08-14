namespace ClientStatementPortal.DTOs
{
    /// <summary>
    /// Represents a single entry in a statement.
    /// </summary>
    public sealed class StatementEntryDto
    {
        /// <summary>
        /// The entry date.
        /// </summary>
        public string EDate { get; set; }

        /// <summary>
        /// A description for the entry.
        /// </summary>
        public string EDescription { get; set; }

        /// <summary>
        /// Debit amount.
        /// </summary>
        public decimal Debit { get; set; }

        /// <summary>
        /// Credit amount.
        /// </summary>
        public decimal Credit { get; set; }

        /// <summary>
        /// The resulting balance.
        /// </summary>
        public decimal Balance { get; set; }
    }
}