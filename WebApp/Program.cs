using System.Data;
using FluentMigrator.Runner;
using Infrastructure.DataContext;
using Infrastructure.Interface;
using Infrastructure.Migrations;
using Infrastructure.Services;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddScoped<IDbConnection>( db => 
    new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")) );
builder.Services.AddScoped<IContext, Context>();
builder.Services.AddScoped<IAuthor, AuthorService>();
builder.Services.AddScoped<IBook, BookService>();

builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddPostgres()
        .WithGlobalConnectionString(builder.Configuration.GetConnectionString("DefaultConnection"))
        .ScanIn(typeof(CreateBooksTable).Assembly).For.Migrations());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "WebApp v1"));
}

using var scope = app.Services.CreateScope();
var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
try
{
    runner.MigrateUp();
}
catch (Exception ex)
{
    Console.WriteLine($"Error applying migrations: {ex.Message}");
    
}

app.UseHttpsRedirection();



app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();


