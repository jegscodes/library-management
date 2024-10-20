
using Library.Domain.Entities.Authors;

namespace Library.Application.Books.Commands.UpdateBook;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Result>
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    public UpdateBookCommandHandler(IBookRepository bookRepository, IAuthorRepository authorRepository)
    {
        _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
    }

    public async Task<Result> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.Id);

        if (book is null)
        {
            return Result.Fail($"Book with ID {request.Id} could not be found.");
        }

        if(book.AuthorChanged(request.AuthorId))
        {
            var author = await _authorRepository.GetByIdAsync(request.AuthorId);
            if (author is null)
            {
                return Result.Fail($"Author with ID {request.Id} could not be found.");
            }

            book.UpdateAuthor(author.Id);
        }

        book.Update(request.Title, BookIdentifier.Create(request.ISBN), PublishedDate.Create(request.PublishedDate));

        _bookRepository.Update(book);

        await _bookRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
