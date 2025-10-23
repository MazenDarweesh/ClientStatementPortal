namespace ClientStatementPortal.DTOs
{
    public class StatmentTotalsDto
    {
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
