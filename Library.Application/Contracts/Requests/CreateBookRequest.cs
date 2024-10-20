using Library.Application.Books.Commands;

namespace Library.Application.Contracts.Requests;

public class CreateBookRequest : IBookCommand
{
    public int AuthorId { get; set; }
    public string ISBN { get; set; } = string.Empty;
    public DateTime PublishedDate { get; set; }
    public string Title { get; set; } = string.Empty;
}
