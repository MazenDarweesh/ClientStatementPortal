namespace ClientStatementPortal.DTOs
{
    /// <summary>
    /// Contains personal details for a client or supplier.
    /// </summary>
    public sealed class PersonalDetailsDto
    {
        /// <summary>
        /// The person’s name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The person’s phone number.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// The person’s address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The company name.
        /// </summary>
        public string Company { get; set; }

        public string  DynamicProUrl { get; set; }
    }
}