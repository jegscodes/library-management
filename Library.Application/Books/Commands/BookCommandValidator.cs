namespace Library.Application.Books.Commands;

/// <summary>
/// Validates commands related to book operations.
/// </summary>
/// <typeparam name="T">The type of command that implements <see cref="IBookCommand"/>.</typeparam>
public class BookCommandValidator<T> : AbstractValidator<T> where T : IBookCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BookCommandValidator{T}"/> class.
    /// </summary>
    public BookCommandValidator()
    {
        // Rule for validating the Title
        RuleFor(b => b.Title)
            .NotEmpty()
            .MaximumLength(255);

        // Rule for validating the AuthorId
        RuleFor(b => b.AuthorId)
            .NotEmpty()
            .NotEqual(0);

        // Rule for validating the ISBN
        RuleFor(b => b.ISBN)
            .NotEmpty()
            .Length(13, 17);

        // Rule for validating the PublishedDate
        RuleFor(b => b.PublishedDate)
            .NotEmpty()
            .Must(NotBeFutureDate)
            .WithMessage("Published date must not be in the future.");
    }

    /// <summary>
    /// Determines whether the specified published date is not in the future.
    /// </summary>
    /// <param name="publishedDate">The date to validate.</param>
    /// <returns>True if the published date is today or in the past; otherwise, false.</returns>
    private bool NotBeFutureDate(DateTime publishedDate)
    {
        return publishedDate.Date <= DateTime.Now.Date;
    }
}
