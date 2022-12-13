using System.Reflection;
using FluentMigrator.Runner;
using ServicesAPI.Data;
using ServicesAPI.Extensions;
using ServicesAPI.Migrations;
using ServicesAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<DapperContext>();
builder.Services.AddScoped<Database>();
builder.Services.AddScoped<IServiceCategoryService,ServiceCategoryService>();
builder.Services.AddScoped<IServiceService, ServiceService>();

builder.Services.AddLogging(c => c.AddFluentMigratorConsole())
    .AddFluentMigratorCore()
    .ConfigureRunner(c => c.AddPostgres()
        .WithGlobalConnectionString(builder.Configuration.GetConnectionString("ServicesDb"))
        .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations());

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Applying migrations
await app.MigrateDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();