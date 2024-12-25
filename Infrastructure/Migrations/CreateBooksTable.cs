using FluentMigrator;

namespace Infrastructure.Migrations;

[Migration(2512202402)]
public class CreateBooksTable : Migration
{
    public override void Up()
    {
        Create.Table("books")
            .WithColumn("bookid").AsInt32().PrimaryKey().Identity()
            .WithColumn("title").AsString().NotNullable()
            .WithColumn("authorid").AsInt32().NotNullable()
            .WithColumn("publishedyear").AsInt32().NotNullable()
            .WithColumn("genre").AsString().NotNullable()
            .WithColumn("isavailable").AsBoolean().NotNullable();
        
        Create.ForeignKey("FK_Books_Authors")
            .FromTable("books").ForeignColumn("authorid")
            .ToTable("authors").PrimaryColumn("authorid");

    }

    public override void Down()
    {
        Delete.Table("books");
    }
}