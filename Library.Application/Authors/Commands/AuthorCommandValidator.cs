namespace Library.Application.Authors.Commands;

/// <summary>
/// Validates commands related to author operations.
/// </summary>
/// <typeparam name="T">The type of command that implements the <see cref="IAuthorCommand"/> interface.</typeparam>
public class AuthorCommandValidator<T> : AbstractValidator<T> where T : IAuthorCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorCommandValidator{T}"/> class.
    /// </summary>
    public AuthorCommandValidator()
    {
        RuleFor(a => a.Name)
            .NotEmpty()
            .MaximumLength(255)
            .WithMessage("Name shouldn't be empty and must not exceed 255 characters.");

        RuleFor(a => a.Email)
            .NotEmpty()
            .MaximumLength(255)
            .WithMessage("Email shouldn't be empty and must not exceed 255 characters.");
    }
}
