using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(2512202401)]
public class CreateAuthorsTable : Migration
{
    public override void Up()
    {
        Create.Table("authors")
            .WithColumn("authorid").AsInt32().PrimaryKey().Identity()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("country").AsString().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("authors");
    }
}