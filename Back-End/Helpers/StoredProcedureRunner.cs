using ClientStatementPortal.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

public class StoredProcedureRunner
{
    private readonly DbMapperContext _context;

    public StoredProcedureRunner(DbMapperContext context)
    {
        _context = context;
    }

    private async Task<ApiResponse<string>> GetConnectionStringAsync(string companyKey)
    {
        var connection = await _context.CompanyConnections.AsNoTracking()
            .FirstOrDefaultAsync(x => x.CompanyKey == companyKey);

        if (connection == null)
            return ApiResponse<string>.Fail($"Company with key '{companyKey}' not found.","NotFound");

        return ApiResponse<string>.Ok(connection.ConnectionString);
    }

    public async Task<ApiResponse<IEnumerable<T>>> ExecuteAsync<T>(string companyKey, string spName, object parameters)
    {
        var connectionString = await GetConnectionStringAsync(companyKey);
        if (!connectionString.Success)
            return ApiResponse<IEnumerable<T>>.Fail(connectionString.Message, connectionString.ErrorType);
        
        using var connection = new SqlConnection(connectionString.Data);
        var result = await connection.QueryAsync<T>(spName, parameters, commandType: CommandType.StoredProcedure);

        return ApiResponse<IEnumerable<T>>.Ok(result);
    }

    public async Task<ApiResponse<T?>> ExecuteFirstOrDefaultAsync<T>(string companyKey, string spName, object parameters)
    {
        var connectionString = await GetConnectionStringAsync(companyKey);
        if (!connectionString.Success)
            return ApiResponse<T?>.Fail(connectionString.Message, connectionString.ErrorType);

        using var connection = new SqlConnection(connectionString.Data);
        var result = await connection.QueryFirstOrDefaultAsync<T>(spName, parameters, commandType: CommandType.StoredProcedure);
        return ApiResponse<T?>.Ok(result);
    }
}
//Why use AsNoTracking()?
//Because:

//You don’t need to update or track changes to the CompanyConnections entity.

//You're just reading the connection info to build a connection string and use it to query another database via Dapper.

//Tracking this object would:

//Consume more memory.

//Be slower (EF sets up change tracking infrastructure).

//Be useless in your case because you’re not calling SaveChanges() or modifying the entity.

//So:
//✅ Use AsNoTracking() when you're doing read-only queries.
