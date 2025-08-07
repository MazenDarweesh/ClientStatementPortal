public class AccountTransactionDto
{
    public string Type { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public DateTime Date { get; set; }
    public string Notes { get; set; }
    public string Status { get; set; }
}