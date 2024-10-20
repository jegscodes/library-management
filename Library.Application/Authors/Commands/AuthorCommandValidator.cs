namespace Library.Application.Authors.Commands
{
    public class AuthorCommandValidator<T> : AbstractValidator<T> where T : IAuthorCommand
    {
        public AuthorCommandValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .MaximumLength(255);

            RuleFor(a => a.Email)
                .NotEmpty()
                .MaximumLength(255);
        }
    }
}
