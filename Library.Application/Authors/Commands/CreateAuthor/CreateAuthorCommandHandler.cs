using Library.Application.Contracts.Responses;
using Library.Domain.Entities.Authors;

namespace Library.Application.Authors.Commands.CreateAuthor;

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Result<EntityCreatedResponse>>
{
    private readonly IAuthorRepository _authorRepository;
    public CreateAuthorCommandHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
    }

    public async Task<Result<EntityCreatedResponse>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);
        var authorExist = await _authorRepository.CheckEmailIfExist(email);

        if(authorExist)
        {
            return Result.Fail<EntityCreatedResponse>($"Author with email, {request.Email} already exist.");
        }

        var author = new Author(request.Name, email);

        _authorRepository.Add(author);

        await _authorRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok<EntityCreatedResponse>(author.Id);
    }
}
