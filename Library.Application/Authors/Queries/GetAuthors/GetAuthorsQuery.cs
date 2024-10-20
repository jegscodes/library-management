namespace Library.Application.Authors.Queries.GetAuthors;

public class GetAuthorsQuery : IRequest<IPaginated<Author>>
{
    public int PageSize { get; }
    public int PageNumber { get; }
    public GetAuthorsQuery(int pageNumber = 1, int pageSize = 50)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
    }
}
