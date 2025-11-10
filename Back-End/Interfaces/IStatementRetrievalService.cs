using ClientStatementPortal.DTOs;

public interface IStatementRetrievalService
{
    Task<ApiResponse<PersonalDetailsDto>> GetCustomerDetailsAsync(string companyKey, string hash);
    Task<ApiResponse<PersonalDetailsDto>> GetSupplierDetailsAsync(string companyKey, string hash);
    Task<ApiResponse<PersonalDetailsDto>> GetCustomerSupplierDetailsAsync(string companyKey, string hash);


    Task<ApiResponse<List<StatementEntryDto>>> GetCustomerTransactionsAsync(string companyKey, string hash);
    Task<ApiResponse<List<StatementEntryDto>>> GetSupplierTransactionsAsync(string companyKey, string hash);
    Task<ApiResponse<List<StatementEntryDto>>> GetCustomerSupplierTransactionsAsync(string companyKey, string hash);


    Task<ApiResponse<StatmentTotalsDto>> GetCustomerStatementTotalsAsync(string companyKey, string hash);
    Task<ApiResponse<StatmentTotalsDto>> GetSupplierStatementTotalsAsync(string companyKey, string hash);
    Task<ApiResponse<StatmentTotalsDto>> GetCustomerSupplierStatementTotalsAsync(string companyKey, string hash);

}