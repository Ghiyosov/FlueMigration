using System.Net;
using Dapper;
using Domein.DTOs;
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

    public async Task<Response<List<BooksWithAuthorsDto>>> BooksWithAuthors()
    {
        var sqlBooks = @"select * from Books";
        var sqlAuthors = @"select * from Authors";
        var res = await _context.Connection().QueryAsync<BooksWithAuthorsDto>(sqlBooks);
        var books = res.ToList();
        foreach (var x in books)
        {
            var resA = await _context.Connection().QueryAsync<Author>(sqlAuthors);
            x.Authors = resA.ToList();
        }
        return new Response<List<BooksWithAuthorsDto>>(books);
    }

    public async Task<Response<List<Book>>> GetBooksByAuthor(string author)
    {
        var sqlBooks = @"select * from Books 
         where AuthorId = select AuthorId from Authors
            where Name = @author";
        var res = await _context.Connection().QueryAsync<Book>(sqlBooks, new { author });
        return new Response<List<Book>>(res.ToList());
    }

    public async Task<Response<List<Book>>> GetBooksByAuthorId(int id)
    {
        var sql = @"select * from Books where AuthorId = @id";
        var res = await _context.Connection().QueryAsync<Book>(sql, new { id });
        return new Response<List<Book>>(res.ToList());
    }
}