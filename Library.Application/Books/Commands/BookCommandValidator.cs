namespace Library.Application.Books.Commands;

public class BookCommandValidator<T> : AbstractValidator<T> where T : IBookCommand
{
    public BookCommandValidator()
    {
        RuleFor(b => b.Title)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(b => b.AuthorId)
            .NotEmpty()
            .NotEqual(0);

        RuleFor(b => b.ISBN)
            .NotEmpty()
            .Length(13, 17);

        RuleFor(b => b.PublishedDate)
            .NotEmpty()
            .Must(NotBeFutureDate)
            .WithMessage("Published date must not be in the future.");
    }

    private bool NotBeFutureDate(DateTime publishedDate)
    {
        return publishedDate.Date <= DateTime.Now.Date;
    }
}
