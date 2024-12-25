using Domein.DTOs;
using Domein.Models;
using Infrastructure.Interface;
using Infrastructure.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("[controller]")]
public class BookController(IBook _book) : ControllerBase
{
    [HttpGet("GetBooks")]
    public async Task<Response<List<Book>>> GetBooks() => await _book.GetBooks();

    [HttpGet("GetBook/{id}")]
    public async Task<Response<Book>> GetBookById(int id) => await _book.GetBookByID(id);
    
    [HttpPost("AddBook")]
    public async Task<Response<bool>> AddBook(Book book) => await _book.AddBook(book);
    
    [HttpPut("UpdateBook")]
    public async Task<Response<bool>> UpdateBook(Book book) => await _book.UpdateBook(book);
    
    [HttpDelete("DeleteBook/{id}")]
    public async Task<Response<bool>> DeleteBook(int id) => await _book.DeleteBook(id);

    [HttpGet("GetBooksWhithAuthors")]
    public async Task<Response<List<BooksWithAuthorsDto>>> GetBooksWhithAuthors() => await _book.BooksWithAuthors();

    [HttpGet("GetBooksByAuthor/{author}")]
    public async Task<Response<List<Book>>> GetBooksByAuthor(string author) => await _book.GetBooksByAuthor(author);
    
    [HttpGet("GetBooksByAuthorId/{id}")]
    public async Task<Response<List<Book>>> GetBooksByAuthorId(int id)=> await _book.GetBooksByAuthorId(id);
}