using System.Data;
using Dapper;
using ServicesAPI.Data;
using ServicesAPI.Models;
using ServicesAPI.ViewModels;
using ServicesAPI.ViewModels.Service;

namespace ServicesAPI.Services;

public interface IServiceService
{
    Task<Service?> Create(CreateServiceViewModel vm);
    Task Delete(IdViewModel vm);
    Task<Service> Get(Guid id);
    Task<IEnumerable<Service>> Get();
    Task<Service> Update(Service service);
}

public class ServiceService : IServiceService
{
    private readonly IDbConnection _dbConnection;
    public ServiceService(DapperContext dapperContext)
    {
        _dbConnection = dapperContext.CreateConnection();
    }
    
    public async Task<Service?> Create(CreateServiceViewModel vm)
    {
        const string insertSqlQuery = "INSERT INTO \"Services\" (\"Id\", \"CategoryId\", \"ServiceName\", \"Price\", \"IsActive\") VALUES (@Id, @CategoryId, @ServiceName, @Price, true) RETURNING \"Id\"";
        var service = new Service
        {
            Id = Guid.NewGuid(),
            CategoryId = vm.CategoryId,
            ServiceName = vm.ServiceName,
            Price = vm.Price,
            IsActive = true
        };
        
        var parameters = new DynamicParameters();
        var id = await _dbConnection.ExecuteScalarAsync(insertSqlQuery, service);
        
        parameters.Add("Id",id);
        return await _dbConnection.QueryFirstOrDefaultAsync<Service>(
            "SELECT * FROM \"Services\" WHERE \"Id\" = @id", parameters);
    }

    public async Task Delete(IdViewModel vm)
    {
        var sqlQuery = "UPDATE \"Services\" SET \"IsActive\" = false WHERE \"Id\" = @id";
        await _dbConnection.ExecuteScalarAsync(sqlQuery, new { vm.Id });
    }

    public async Task<Service> Get(Guid id)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<Service>(
            "SELECT * FROM \"Services\" WHERE \"Id\" = @id", id);
    }

    public async Task<IEnumerable<Service>> Get()
    {
        return await _dbConnection.QueryAsync<Service>("SELECT * FROM \"Services\" WHERE \"IsActive\" = true");
    }

    public async Task<Service> Update(Service service)
    {
        await _dbConnection.ExecuteAsync(
            "UPDATE \"Services\" SET \"CategoryId\" = @CategoryId, \"Price\" = @TimeSlotSize WHERE \"Id\" = @Id", service);
        return await Get(service.Id);
    }
}