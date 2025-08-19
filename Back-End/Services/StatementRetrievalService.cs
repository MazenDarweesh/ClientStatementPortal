using ClientStatementPortal.DTOs;
using ClientStatementPortal.Models;
using System.Collections.Generic;
public class StatementRetrievalService : IStatementRetrievalService
{
    private readonly StoredProcedureRunner _spRunner;
    private readonly DbMapperContext _dbMapperContext;

    public StatementRetrievalService(DbMapperContext context, StoredProcedureRunner spRunner)
    {
        _dbMapperContext = context;
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

        if(result != null)
        {
            // Query the DynamicProURL value from the DB mapper
            result.DynamicProUrl = _dbMapperContext.CompanyConnections.FirstOrDefault(cc => cc.CompanyKey == companyKey)?.DynamicProUrl;
            return ApiResponse<PersonalDetailsDto>.Ok(result);
        }
        else
            return ApiResponse<PersonalDetailsDto>.Fail("Can't Access");
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

        if (list.Any())
            return ApiResponse<List<StatementEntryDto>>.Ok(list);
        else
            return ApiResponse<List<StatementEntryDto>>.Fail("Can't Access");
    }

    public async Task<ApiResponse<PersonalDetailsDto>> GetSupplierDetailsAsync(string companyKey, string hash)
    {
        var parameters = new Dapper.DynamicParameters();
        parameters.Add("@URLHash", hash);

        var result = await _spRunner.ExecuteFirstOrDefaultAsync<PersonalDetailsDto>(
            companyKey,
            "SP_GetSupplierDetailsByURLHash",
            parameters);

        if (result != null)
        {
            result.DynamicProUrl = _dbMapperContext.CompanyConnections.FirstOrDefault(cc => cc.CompanyKey == companyKey)?.DynamicProUrl;
            return ApiResponse<PersonalDetailsDto>.Ok(result);
        }

        // Return success even if no supplier details found.
        return ApiResponse<PersonalDetailsDto>.Ok(null, "Supplier details not found");
    }

    public async Task<ApiResponse<List<StatementEntryDto>>> GetSupplierTransactionsAsync(string companyKey, string hash)
    {
        var parameters = new Dapper.DynamicParameters();
        parameters.Add("@URLHash", hash);

        var result = await _spRunner.ExecuteAsync<StatementEntryDto>(
            companyKey,
            "SP_GetSupplierStatmentByURLHash",
            parameters);

        var list = result.ToList();

        // Return success with empty list if no supplier transactions found.
        return ApiResponse<List<StatementEntryDto>>.Ok(list, list.Any() ? null : "No supplier transactions found");
    }
}
