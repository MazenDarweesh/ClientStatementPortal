using ClientStatementPortal.DTOs;
using ClientStatementPortal.Models;
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

        if (!result.Success)
            return ApiResponse<PersonalDetailsDto>.Fail(result.Message, result.ErrorType);
        if(result.Data == null)
            return ApiResponse<PersonalDetailsDto>.Fail("Can't Access", "NotFound");

        // Query the DynamicProURL value from the DB mapper
        result.Data.DynamicProUrl = _dbMapperContext.CompanyConnections.FirstOrDefault(cc => cc.CompanyKey == companyKey)?.DynamicProUrl;
        return ApiResponse<PersonalDetailsDto>.Ok(result.Data);
    }
    public async Task<ApiResponse<PersonalDetailsDto>> GetSupplierDetailsAsync(string companyKey, string hash)
    {
        var parameters = new Dapper.DynamicParameters();
        parameters.Add("@URLHash", hash);

        var result = await _spRunner.ExecuteFirstOrDefaultAsync<PersonalDetailsDto>(
            companyKey,
            "SP_GetSupplierDetailsByURLHash",
            parameters);

        if (!result.Success)
            return ApiResponse<PersonalDetailsDto>.Fail(result.Message, result.ErrorType);
        if (result.Data == null)
            return ApiResponse<PersonalDetailsDto>.Fail("Can't Access", "NotFound");

        // Query the DynamicProURL value from the DB mapper
        result.Data.DynamicProUrl = _dbMapperContext.CompanyConnections.FirstOrDefault(cc => cc.CompanyKey == companyKey)?.DynamicProUrl;
        return ApiResponse<PersonalDetailsDto>.Ok(result.Data);
    }
    public async Task<ApiResponse<PersonalDetailsDto>> GetCustomerSupplierDetailsAsync(string companyKey, string hash)
    {
        var parameters = new Dapper.DynamicParameters();
        parameters.Add("@URLHash", hash);
        var result = await _spRunner.ExecuteFirstOrDefaultAsync<PersonalDetailsDto>(
            companyKey,
            "SP_GetCustomerSupplierDetailsByURLHash",
            parameters);
        if (!result.Success)
            return ApiResponse<PersonalDetailsDto>.Fail(result.Message, result.ErrorType);
        if (result.Data == null)
            return ApiResponse<PersonalDetailsDto>.Fail("Can't Access", "NotFound");
        // Query the DynamicProURL value from the DB mapper
        result.Data.DynamicProUrl = _dbMapperContext.CompanyConnections.FirstOrDefault(cc => cc.CompanyKey == companyKey)?.DynamicProUrl;
        return ApiResponse<PersonalDetailsDto>.Ok(result.Data);
    }

    public async Task<ApiResponse<List<StatementEntryDto>>> GetCustomerTransactionsAsync(string companyKey, string hash)
    {
        var parameters = new Dapper.DynamicParameters();
        parameters.Add("@URLHash", hash);

        var result = await _spRunner.ExecuteAsync<StatementEntryDto>(
            companyKey,
            "SP_GetCustomerStatmentByURLHash",
            parameters);

        if (!result.Success) 
            return ApiResponse<List<StatementEntryDto>>.Fail(result.Message, result.ErrorType);

        if (result.Data == null)
            return ApiResponse<List<StatementEntryDto>>.Fail("No Customer transactions found", "NoData");

        var list = result.Data.ToList();

        if (list.Count == 0)
            return ApiResponse<List<StatementEntryDto>>.Fail("No Customer transactions found", "Empty");

        return ApiResponse<List<StatementEntryDto>>.Ok(list);
    }
    public async Task<ApiResponse<List<StatementEntryDto>>> GetSupplierTransactionsAsync(string companyKey, string hash)
    {
        var parameters = new Dapper.DynamicParameters();
        parameters.Add("@URLHash", hash);

        var result = await _spRunner.ExecuteAsync<StatementEntryDto>(
            companyKey,
            "SP_GetSupplierStatmentByURLHash",
            parameters);
        
        if (!result.Success)
            return ApiResponse<List<StatementEntryDto>>.Fail(result.Message, result.ErrorType);

        if (result.Data == null)
            return ApiResponse<List<StatementEntryDto>>.Fail("No supplier transactions found", "NoData");

        var list = result.Data.ToList();

        if (list.Count == 0)
            return ApiResponse<List<StatementEntryDto>>.Fail("No supplier transactions found", "Empty");

        return ApiResponse<List<StatementEntryDto>>.Ok(list);
    }
    public async Task<ApiResponse<List<StatementEntryDto>>> GetCustomerSupplierTransactionsAsync(string companyKey, string hash)
        {
        var parameters = new Dapper.DynamicParameters();
        parameters.Add("@URLHash", hash);
        var result = await _spRunner.ExecuteAsync<StatementEntryDto>(
            companyKey,
            "SP_GetCustomerSupplierStatmentByURLHash",
            parameters);
        if (!result.Success)
            return ApiResponse<List<StatementEntryDto>>.Fail(result.Message, result.ErrorType);
        if (result.Data == null)
            return ApiResponse<List<StatementEntryDto>>.Fail("No transactions found", "NoData");
        var list = result.Data.ToList();
        if (list.Count == 0)
            return ApiResponse<List<StatementEntryDto>>.Fail("No transactions found", "Empty");
        return ApiResponse<List<StatementEntryDto>>.Ok(list);
    }

    public async Task<ApiResponse<StatmentTotalsDto>> GetCustomerStatementTotalsAsync(string companyKey, string hash)
    {
        var parameters = new Dapper.DynamicParameters();
        parameters.Add("@URLHash", hash);
        var result = await _spRunner.ExecuteFirstOrDefaultAsync<StatmentTotalsDto>(
            companyKey,
            "SP_GetCustomerStatmentTotalsByURLHash",
            parameters);
        if (!result.Success)
            return ApiResponse<StatmentTotalsDto>.Fail(result.Message, result.ErrorType);
        if (result.Data == null)
            return ApiResponse<StatmentTotalsDto>.Fail("Can't Access", "NotFound");
        return ApiResponse<StatmentTotalsDto>.Ok(result.Data);
    }
    public async Task<ApiResponse<StatmentTotalsDto>> GetSupplierStatementTotalsAsync(string companyKey, string hash)
    {
        var parameters = new Dapper.DynamicParameters();
        parameters.Add("@URLHash", hash);
        var result = await _spRunner.ExecuteFirstOrDefaultAsync<StatmentTotalsDto>(
            companyKey,
            "SP_GetSupplierStatmentTotalsByURLHash",
            parameters);
        if (!result.Success)
            return ApiResponse<StatmentTotalsDto>.Fail(result.Message, result.ErrorType);
        if (result.Data == null)
            return ApiResponse<StatmentTotalsDto>.Fail("Can't Access", "NotFound");
        return ApiResponse<StatmentTotalsDto>.Ok(result.Data);
    }
    public async Task<ApiResponse<StatmentTotalsDto>> GetCustomerSupplierStatementTotalsAsync(string companyKey, string hash)
        {
        var parameters = new Dapper.DynamicParameters();
        parameters.Add("@URLHash", hash);
        var result = await _spRunner.ExecuteFirstOrDefaultAsync<StatmentTotalsDto>(
            companyKey,
            "SP_GetCustomerSupplierStatmentTotalsByURLHash",
            parameters);
        if (!result.Success)
            return ApiResponse<StatmentTotalsDto>.Fail(result.Message, result.ErrorType);
        if (result.Data == null)
            return ApiResponse<StatmentTotalsDto>.Fail("Can't Access", "NotFound");
        return ApiResponse<StatmentTotalsDto>.Ok(result.Data);
    }

}
