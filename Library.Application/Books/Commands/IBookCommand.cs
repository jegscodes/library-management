namespace Library.Application.Books.Commands;

public interface IBookCommand
{
    public int AuthorId { get; }
    public string ISBN { get; }
    public DateTime PublishedDate { get; }
    public string Title { get; }
}
