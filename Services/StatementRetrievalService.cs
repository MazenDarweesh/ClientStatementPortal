using ClientStatementPortal.Models;

public class StatementRetrievalService : IStatementRetrievalService
{

    private readonly StoredProcedureRunner _spRunner;

    public StatementRetrievalService(DbMapperContext context, StoredProcedureRunner spRunner)
    {
        _spRunner = spRunner;
    }

    public async Task<ApiResponse<ClientAccountStatementDto>> GetCustomerStatementAsync(string companyKey, string hash)
    {
        var parameters = new Dapper.DynamicParameters();
        parameters.Add("@hash", hash);

        var result = await _spRunner.ExecuteFirstOrDefaultAsync<ClientAccountStatementDto>(
            companyKey,
            "sp_GetCustomerStatement",
            parameters);

        return result != null
            ? ApiResponse<ClientAccountStatementDto>.Ok(result)
            : ApiResponse<ClientAccountStatementDto>.Fail("Statement not found", "NotFound");
    }
    public async Task<ApiResponse<List<AccountTransactionDto>>> GetCustomerTransactionsAsync(string companyKey, string hash)
    {
        var parameters = new Dapper.DynamicParameters();
        parameters.Add("@hash", hash);

        var result = await _spRunner.ExecuteAsync<AccountTransactionDto>(
            companyKey,
            "sp_GetCustomerTransactions",
            parameters);

        var list = result.ToList();

        return list.Any()
            ? ApiResponse<List<AccountTransactionDto>>.Ok(list)
            : ApiResponse<List<AccountTransactionDto>>.Fail("No transactions found", "NotFound");
    }

    public async Task<ApiResponse<SupplierAccountStatementDto>> GetSupplierStatementAsync(string companyKey, string hash)
    {
        var parameters = new Dapper.DynamicParameters();
        parameters.Add("@hash", hash);

        var result = await _spRunner.ExecuteFirstOrDefaultAsync<SupplierAccountStatementDto>(
            companyKey,
            "sp_GetSupplierStatement", 
            parameters);

        return result != null
            ? ApiResponse<SupplierAccountStatementDto>.Ok(result)
            : ApiResponse<SupplierAccountStatementDto>.Fail("Supplier statement not found", "NotFound");
    }

    public async Task<ApiResponse<List<AccountTransactionDto>>> GetSupplierTransactionsAsync(string companyKey, string hash)
    {
        var parameters = new Dapper.DynamicParameters();
        parameters.Add("@hash", hash);

        var result = await _spRunner.ExecuteAsync<AccountTransactionDto>(
            companyKey,
            "sp_GetSupplierTransactions", // your actual stored procedure name
            parameters);

        return result != null && result.Any()
            ? ApiResponse<List<AccountTransactionDto>>.Ok(result.ToList())
            : ApiResponse<List<AccountTransactionDto>>.Fail("No supplier transactions found", "NotFound");
    }
}
