
public class ClientAccountStatementDto
{
    public string ClientName { get; set; }
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public List<AccountTransactionDto> Transactions { get; set; }
}