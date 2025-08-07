using System.Threading.Tasks;

public interface IStatementRetrievalService
{
    Task<ApiResponse<ClientAccountStatementDto>> GetCustomerStatementAsync(string companyKey, string hash);
    Task<ApiResponse<List<AccountTransactionDto>>> GetCustomerTransactionsAsync(string companyKey, string hash);
    Task<ApiResponse<SupplierAccountStatementDto>> GetSupplierStatementAsync(string companyKey, string hash);
    Task<ApiResponse<List<AccountTransactionDto>>> GetSupplierTransactionsAsync(string companyKey, string hash);

}