using System.Net;
using Dapper;
using Domein.Models;
using Infrastructure.DataContext;
using Infrastructure.Interface;
using Infrastructure.Responses;

namespace Infrastructure.Services;

public class BookService(IContext _context) : IBook
{
    public async Task<Response<List<Book>>> GetBooks()
    {
        var sql = @"select * from Books";
        var res = await _context.Connection().QueryAsync<Book>(sql);
        return new Response<List<Book>>(res.ToList());
    }

    public async Task<Response<Book>> GetBookByID(int id)
    {
        var sql = @"select * from Books where BookId = @id";
        var res = await _context.Connection().QueryFirstOrDefaultAsync<Book>(sql, new { id });
        return new Response<Book>(res);
    }

    public async Task<Response<bool>> AddBook(Book author)
    {
        var sql = @"insert into Books(Title, AuthorId, PublishedYear, Genre, IsAvailable) values (@Title, @AuthorId, @PublishedYear, @Genre, @IsAvailable)";
        var res = await _context.Connection().ExecuteAsync(sql, author);
        return res == 0
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Response<bool>(HttpStatusCode.Created, "Created");
    }

    public async Task<Response<bool>> UpdateBook(Book book)
    {
        var sql = @"inser into Books(BookId, Title, AuthorId, PublishedYear, Genre, IsAvailable) values (@BookId, @Title, @AuthorId, @PublishedYear, @Genre, @IsAvailable)";
        var res = await _context.Connection().ExecuteAsync(sql, book);
        return res == 0
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Response<bool>(HttpStatusCode.Created, "Updated");
    }

    public async Task<Response<bool>> DeleteBook(int id)
    {
        var sql = @"delete from Books where BookId = @id";
        var res = await _context.Connection().ExecuteAsync(sql, new { id });
        return res == 0
            ? new Response<bool>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Response<bool>(HttpStatusCode.Created, "Deleted");
    }
}