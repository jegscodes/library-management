
using Library.Application.Contracts;
using Library.Domain.Common;
using Library.Domain.Entities.Authors;

namespace Library.Application.Books.Commands;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Result<BookCreatedResponse>>
{
    private readonly IAuthorRepository _authorRepository;
    public CreateBookCommandHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));  
    }
    public async Task<Result<BookCreatedResponse>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetByIdAsync(request.AuthorId);

        if(author is null)
        {
            return Result.Fail<BookCreatedResponse>($"Author not found with id {request.AuthorId}.");
        }

        var isbn = BookIdentifier.Create(request.Isbn);
        var publishedDate = PublishedDate.Create(request.publishedDate);
        var book = new Book(request.AuthorId, request.Title, publishedDate, isbn);

        author.AddBook(book);

        await _authorRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok<BookCreatedResponse>(book.Id);
    }
}
