using Domein.Models;

namespace Domein.DTOs;

public class BooksWithAuthorsDto
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public int AuthorId { get; set; }
    public int PublishedYear { get; set; }
    public string Genre { get; set; }
    public bool IsAvailable { get; set; }
    public List<Author> Authors { get; set; } = [];
}