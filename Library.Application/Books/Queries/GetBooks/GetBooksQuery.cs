using Library.Domain.Common;

namespace Library.Application.Books.Queries.GetBooks;

public class GetBooksQuery : IRequest<IPaginated<Book>>
{
    public int PageSize { get; }
    public int PageNumber { get; }

    public GetBooksQuery(int pageNumber = 1, int pageSize = 50)
    {
        PageSize = pageSize;
        PageNumber  = pageNumber;
    }
}
