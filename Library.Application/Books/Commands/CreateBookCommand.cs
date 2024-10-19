using Library.Application.Contracts;
using Library.Domain.Common;

namespace Library.Application.Books.Commands;

public record CreateBookCommand(int AuthorId, string Title, DateTime publishedDate, string Isbn) : IRequest<Result<BookCreatedResponse>>;
