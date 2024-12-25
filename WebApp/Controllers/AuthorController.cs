using Domein.Models;
using Infrastructure.Interface;
using Infrastructure.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController(IAuthor _author) : ControllerBase
{
    [HttpGet("GetAuthors")]
    public async Task<Response<List<Author>>> GetAuthors() => await _author.GetAuthors();

    [HttpGet("GetAuthor/{id}")]
    public async Task<Response<Author>> GetAuthor(int id) => await _author.GetAuthorByID(id);
    
    [HttpPost("AddAuthor")]
    public async Task<Response<bool>> AddAuthor(Author author) => await _author.AddAuthor(author);
    
    [HttpPut("UpdateAuthor")]
    public async Task<Response<bool>> UpdateAuthor(Author author) => await _author.UpdateAuthor(author);
    
    [HttpDelete("DeleteAuthor")]
    public async Task<Response<bool>> DeleteAuthor(int id) => await _author.DeleteAuthor(id);
}