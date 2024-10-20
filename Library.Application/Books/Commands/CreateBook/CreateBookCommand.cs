using Library.Application.Contracts.Responses;

namespace Library.Application.Books.Commands;

public record CreateBookCommand(int AuthorId, string Title, DateTime PublishedDate, string ISBN) : IRequest<Result<EntityCreatedResponse>>, IBookCommand;
