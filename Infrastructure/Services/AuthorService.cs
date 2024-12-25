using System.Net;
using Dapper;
using Domein.Models;
using Infrastructure.DataContext;
using Infrastructure.Interface;
using Infrastructure.Responses;

namespace Infrastructure.Services;

public class AuthorService(IContext _context): IAuthor
{
    public async Task<Response<List<Author>>> GetAuthors()
    {
        var sql = @"select * from Authors";
        var res = await _context.Connection().QueryAsync<Author>(sql);
        return new Response<List<Author>>(res.ToList());
    }

    public async Task<Response<Author>> GetAuthorByID(int id)
    {
        var sql = @"select * from Authors where AuthorId = @id";
        var res = await _context.Connection().QueryFirstOrDefaultAsync<Author>(sql, new { id });
        return new Response<Author>(res);
    }

    public async Task<Response<bool>> AddAuthor(Author author)
    {
        var sql = @"insert into Authors(Name, Country) values (@Name, @Country)";
        var res = await _context.Connection().ExecuteAsync(sql, author);
        return res == 0
                ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
                : new Response<bool>(HttpStatusCode.Created, "Created");
    }

    public async Task<Response<bool>> UpdateAuthor(Author author)
    {
        var sql = @"update Authors set Name = @Name, Country = @Country where AuthorId = @AuthorId";
        var res = await _context.Connection().ExecuteAsync(sql, author);
        return res == 0
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Response<bool>(HttpStatusCode.OK, "Updated");
    }

    public async Task<Response<bool>> DeleteAuthor(int id)
    {
        var sql = @"delete from Authors where AuthorId = @id";
        var res = await _context.Connection().ExecuteAsync(sql, new { id });
        return res == 0
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Response<bool>(HttpStatusCode.OK, "Deleted");
    }
}