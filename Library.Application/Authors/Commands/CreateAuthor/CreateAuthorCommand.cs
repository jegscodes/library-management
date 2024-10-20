using Library.Application.Contracts.Responses;

namespace Library.Application.Authors.Commands.CreateAuthor;

public record CreateAuthorCommand(string Name, string Email) : IRequest<Result<EntityCreatedResponse>>, IAuthorCommand;
