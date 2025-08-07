public class SupplierAccountStatementDto 
{
    public string SupplierName { get; set; }
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public List<AccountTransactionDto> Transactions { get; set; }

}
