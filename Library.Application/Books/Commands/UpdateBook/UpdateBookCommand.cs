namespace Library.Application.Books.Commands.UpdateBook;

public record UpdateBookCommand(int Id, int AuthorId, string Title, string ISBN, DateTime PublishedDate) : IRequest<Result>, IBookCommand;
