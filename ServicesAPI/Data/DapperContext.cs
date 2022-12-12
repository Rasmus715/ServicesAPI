using System.Data;
using Npgsql;

namespace ServicesAPI.Data;

public class DapperContext
{
    private readonly IConfiguration _configuration;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreateConnection() =>
         new NpgsqlConnection(_configuration.GetConnectionString("ServicesDB"));

    public IDbConnection CreateMasterConnection() => 
        new NpgsqlConnection(_configuration.GetConnectionString("Master"));
}