
namespace Library.Application.Authors.Queries.GetAuthors;

public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, IPaginated<Author>>
{
    private readonly IAuthorRepository _authorRepository;
    public GetAuthorsQueryHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }
    public async Task<IPaginated<Author>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        var authors = await _authorRepository.GetPaginatedListAsync(request.PageNumber, request.PageSize);

        if(authors is null)
        {
            return new PaginatedList<Author>([], 0, request.PageNumber, request.PageSize);
        }

        return authors;
    }
}
