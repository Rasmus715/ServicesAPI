using System.Data;
using Dapper;
using ServicesAPI.Data;
using ServicesAPI.Models;
using ServicesAPI.ViewModels;
using ServicesAPI.ViewModels.ServiceCategories;

namespace ServicesAPI.Services;

public interface IServiceCategoryService
{
    Task<ServiceCategory?> Create(ServiceCategoryViewModel vm);
    Task Delete(IdViewModel vm);
    Task<ServiceCategory> Get(Guid id);
    Task<IEnumerable<ServiceCategory>> Get();
    Task<ServiceCategory> Update(ServiceCategory serviceCategory);
}

public class ServiceCategoryService : IServiceCategoryService
{
    private readonly IDbConnection _dbConnection;

    public ServiceCategoryService(DapperContext dapperContext)
    {
        _dbConnection = dapperContext.CreateConnection();
    }

    public async Task<ServiceCategory?> Create(ServiceCategoryViewModel vm)
    {
        const string insertSqlQuery = "INSERT INTO \"ServiceCategories\" (\"Id\",\"CategoryName\", \"TimeSlotSize\") VALUES(@Id, @CategoryName, @TimeSlotSize) RETURNING \"Id\"";
        var parameters = new DynamicParameters();
        parameters.Add("Id",Guid.NewGuid());
        parameters.Add("CategoryName",vm.CategoryName);
        parameters.Add("TimeSlotSize",vm.TimeSlotSize);
        var id = await _dbConnection.ExecuteScalarAsync(insertSqlQuery, parameters);
        return await _dbConnection.QueryFirstAsync<ServiceCategory>("SELECT * FROM \"ServiceCategories\" WHERE \"Id\" = @id", new {id});
    }

    public async Task Delete(IdViewModel vm)
    {
        var sqlQuery = "DELETE FROM \"ServiceCategories\" WHERE \"Id\" = @id";
        await _dbConnection.ExecuteAsync(sqlQuery, new { vm.Id });
    }

    public async Task<ServiceCategory> Get(Guid id)
    {
        return await _dbConnection.QueryFirstOrDefaultAsync<ServiceCategory>(
            "SELECT * FROM  \"ServiceCategories\"  WHERE \"Id\" = @id", new {id});
    }

    public async Task<IEnumerable<ServiceCategory>> Get()
    {
        return await _dbConnection.QueryAsync<ServiceCategory>("SELECT * FROM \"ServiceCategories\"");
    }

    public async Task<ServiceCategory> Update(ServiceCategory serviceCategory)
    {
        await _dbConnection.ExecuteAsync(
            "UPDATE \"ServiceCategories\" SET \"CategoryName\" = @CategoryName, \"TimeSlotSize\" = @TimeSlotSize WHERE \"Id\" = @Id",serviceCategory);
        return await Get(serviceCategory.Id);
    }
}