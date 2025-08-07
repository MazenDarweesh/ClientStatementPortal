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

    private async Task<string> GetConnectionStringAsync(string companyKey)
    {
        var connection = await _context.CompanyConnections.AsNoTracking()
            .FirstOrDefaultAsync(x => x.CompanyKey == companyKey);

        if (connection == null)
            throw new ArgumentException($"Company with key '{companyKey}' not found.");

        return connection.ConnectionString;
    }

    public async Task<IEnumerable<T>> ExecuteAsync<T>(string companyKey, string spName, object parameters)
    {
        var connectionString = await GetConnectionStringAsync(companyKey);
        using var connection = new SqlConnection(connectionString);
        return await connection.QueryAsync<T>(spName, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task<T> ExecuteFirstOrDefaultAsync<T>(string companyKey, string spName, object parameters)
    {
        var connectionString = await GetConnectionStringAsync(companyKey);
        using var connection = new SqlConnection(connectionString);
        return await connection.QueryFirstOrDefaultAsync<T>(spName, parameters, commandType: CommandType.StoredProcedure);
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
