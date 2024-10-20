using Library.Application.Contracts;
using Library.Domain.Common;

namespace Library.Application.Books.Queries.GetBook;

public class GetBookQuery : IRequest<Result<Book>>
{
    public int Id { get; }
    public GetBookQuery(int id)
    {
        Id = id;    
    }
}

