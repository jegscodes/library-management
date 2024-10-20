namespace Library.Application.Books.Queries.GetBooks;

public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IPaginated<Book>>
{
    private readonly IBookRepository _bookRepository;

    public GetBooksQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IPaginated<Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetPaginatedListAsync(request.PageNumber, request.PageSize);

        if(books is null)
        {
            return new PaginatedList<Book>([], 0, request.PageNumber, request.PageSize);
        }

        return books;
    }
}
