using ClientStatementPortal.DTOs;
using ClientStatementPortal.Models;

public class StatementRetrievalService : IStatementRetrievalService         
{
    private readonly StoredProcedureRunner _spRunner;

    public StatementRetrievalService(DbMapperContext context, StoredProcedureRunner spRunner)
    {
        _spRunner = spRunner;
    }

    public async Task<ApiResponse<PersonalDetailsDto>> GetCustomerDetailsAsync(string companyKey, string hash)
    {
        var parameters = new Dapper.DynamicParameters();
        parameters.Add("@URLHash", hash);

        var result = await _spRunner.ExecuteFirstOrDefaultAsync<PersonalDetailsDto>(
            companyKey,
            "SP_GetCustomerDetailsByURLHash",
            parameters);

        return result != null
            ? ApiResponse<PersonalDetailsDto>.Ok(result)
            : ApiResponse<PersonalDetailsDto>.Fail("Customer details not found", "NotFound");
    }

    public async Task<ApiResponse<List<StatementEntryDto>>> GetCustomerTransactionsAsync(string companyKey, string hash)
    {
        var parameters = new Dapper.DynamicParameters();
        parameters.Add("@URLHash", hash);

        var result = await _spRunner.ExecuteAsync<StatementEntryDto>(
            companyKey,
            "SP_GetCustomerStatmentByURLHash",
            parameters);

        var list = result.ToList();

        return list.Any()
            ? ApiResponse<List<StatementEntryDto>>.Ok(list)
            : ApiResponse<List<StatementEntryDto>>.Fail("No customer transactions found", "NotFound");
    }

    public async Task<ApiResponse<PersonalDetailsDto>> GetSupplierDetailsAsync(string companyKey, string hash)
    {
        var parameters = new Dapper.DynamicParameters();
        parameters.Add("@URLHash", hash);

        var result = await _spRunner.ExecuteFirstOrDefaultAsync<PersonalDetailsDto>(
            companyKey,
            "SP_GetSupplierDetailsByURLHash",
            parameters);

        return result != null
            ? ApiResponse<PersonalDetailsDto>.Ok(result)
            : ApiResponse<PersonalDetailsDto>.Fail("Supplier details not found", "NotFound");
    }

    public async Task<ApiResponse<List<StatementEntryDto>>> GetSupplierTransactionsAsync(string companyKey, string hash)
    {
        var parameters = new Dapper.DynamicParameters();
        parameters.Add("@URLHash", hash);

        var result = await _spRunner.ExecuteAsync<StatementEntryDto>(
            companyKey,
            "SP_GetSupplierStatmentByURLHash",
            parameters);

        return result != null && result.Any()
            ? ApiResponse<List<StatementEntryDto>>.Ok(result.ToList())
            : ApiResponse<List<StatementEntryDto>>.Fail("No supplier transactions found", "NotFound");
    }
}
