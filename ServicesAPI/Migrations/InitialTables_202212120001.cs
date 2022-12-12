using FluentMigrator;

namespace ServicesAPI.Migrations;

[Migration(202212120001)]
public class InitialTables_202212120001 : Migration
{
    public override void Up()
    {
        Create.Table("ServiceCategories")
            .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
            .WithColumn("CategoryName").AsString(255).NotNullable()
            .WithColumn("TimeSlotSize").AsInt32().NotNullable();

        Create.Table("Services")
            .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
            .WithColumn("CategoryId").AsGuid().ForeignKey("ServiceCategories","Id")
            .WithColumn("ServiceName").AsString(255).NotNullable()
            .WithColumn("Price").AsDecimal()
            .WithColumn("IsActive").AsBoolean();
    }

    public override void Down()
    {
        Delete.Table("Services");
        Delete.Table("ServiceCategories");
    }
}