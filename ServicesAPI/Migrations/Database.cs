using Dapper;
using ServicesAPI.Data;

namespace ServicesAPI.Migrations;

public class Database
{
    private readonly DapperContext _context;
    
    public Database(DapperContext context)
    {
        _context = context;
    }
    
    public async Task CreateDatabase(string dbName)
    {
        var query = "SELECT * FROM pg_database WHERE datname = @datname";
        var parameters = new DynamicParameters();
        parameters.Add("datname", dbName);
        using var connection = _context.CreateMasterConnection();
        var records = await connection.QueryAsync(query, parameters);
        if (!records.Any())
            await connection.ExecuteAsync("CREATE DATABASE @datname", parameters);
    }
}