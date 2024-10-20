namespace Library.Application.Authors.Commands;

public interface IAuthorCommand
{
    string Name { get; }

    string Email { get; }
}
