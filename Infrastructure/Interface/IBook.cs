using Domein.DTOs;
using Domein.Models;
using Infrastructure.Responses;

namespace Infrastructure.Interface;

public interface IBook
{
    public Task<Response<List<Book>>> GetBooks();
    public Task<Response<Book>> GetBookByID(int id);
    public Task<Response<bool>> AddBook(Book book);
    public Task<Response<bool>> UpdateBook(Book book);
    public Task<Response<bool>> DeleteBook(int id);
    public Task<Response<List<BooksWithAuthorsDto>>> BooksWithAuthors();
    public Task<Response<List<Book>>> GetBooksByAuthor(string author);
    public Task<Response<List<Book>>> GetBooksByAuthorId(int id);
}