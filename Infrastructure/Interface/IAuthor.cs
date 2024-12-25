using Domein.Models;
using Infrastructure.Responses;

namespace Infrastructure.Interface;

public interface IAuthor
{
    public Task<Response<List<Author>>> GetAuthors();
    public Task<Response<Author>> GetAuthorByID(int id);
    public Task<Response<bool>> AddAuthor(Author author);
    public Task<Response<bool>> UpdateAuthor(Author author);
    public Task<Response<bool>> DeleteAuthor(int id);
    
}