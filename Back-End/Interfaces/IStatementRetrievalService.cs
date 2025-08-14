using ClientStatementPortal.DTOs;
using System.Threading.Tasks;

public interface IStatementRetrievalService
{
    Task<ApiResponse<PersonalDetailsDto>> GetCustomerDetailsAsync(string companyKey, string hash);
    Task<ApiResponse<PersonalDetailsDto>> GetSupplierDetailsAsync(string companyKey, string hash);
    Task<ApiResponse<List<StatementEntryDto>>> GetCustomerTransactionsAsync(string companyKey, string hash);
    Task<ApiResponse<List<StatementEntryDto>>> GetSupplierTransactionsAsync(string companyKey, string hash);

}