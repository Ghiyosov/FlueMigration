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
}