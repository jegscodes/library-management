using Library.Domain.Entities.Authors;

namespace Library.Application.Authors.Queries.GetAuthor;

public record GetAuthorQuery(int Id) : IRequest<Result<Author>>;
