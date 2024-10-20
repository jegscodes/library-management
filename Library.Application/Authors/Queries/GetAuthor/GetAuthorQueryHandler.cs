using Library.Domain.Entities.Authors;

namespace Library.Application.Authors.Queries.GetAuthor;

public class GetAuthorQueryHandler : IRequestHandler<GetAuthorQuery, Result<Author>>
{
    private readonly IAuthorRepository _authorRepository;
    public GetAuthorQueryHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }
    public async Task<Result<Author>> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetByIdAsync(request.Id);

        if(author == null)
        {
            return Result.Fail<Author>($"Author with ID {request.Id} couldn't be found");
        }

        return Result.Ok(author);
    }
}
