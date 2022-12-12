using FluentMigrator.Runner;
using ServicesAPI.Migrations;

namespace ServicesAPI.Extensions;

public static class MigrationManager
{
    public static async Task MigrateDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var databaseService = scope.ServiceProvider.GetRequiredService<Database>();
        var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        
        await databaseService.CreateDatabase("ServicesDb");
        migrationService.ListMigrations();
        migrationService.MigrateUp();
        
    }
}