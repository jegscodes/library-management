using Library.Application.Contracts.Responses;
using Library.Domain.Common;
using Library.Domain.Entities.Authors;

namespace Library.Application.Books.Commands;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Result<EntityCreatedResponse>>
{
    private readonly IAuthorRepository _authorRepository;
    public CreateBookCommandHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));  
    }
    public async Task<Result<EntityCreatedResponse>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetByIdAsync(request.AuthorId);

        if(author is null)
        {
            return Result.Fail<EntityCreatedResponse>($"Author with ID {request.AuthorId} could not be found.");
        }

        var isbn = BookIdentifier.Create(request.ISBN);
        var publishedDate = PublishedDate.Create(request.PublishedDate);
        var book = new Book(request.AuthorId, request.Title, publishedDate, isbn);

        author.AddBook(book);

        await _authorRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok<EntityCreatedResponse>(book.Id);
    }
}
