
using Library.Domain.Common;

namespace Library.Application.Books.Queries.GetBook;

public class GetBookQueryHandler : IRequestHandler<GetBookQuery, Result<Book>>
{
    private readonly IBookRepository _bookRepository;
    public GetBookQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    public async Task<Result<Book>> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.Id);

        if(book is null)
        {
            return Result.Fail<Book>($"Book with ID {request.Id} could not be found.");
        }

        return Result.Ok(book);
    }
}
