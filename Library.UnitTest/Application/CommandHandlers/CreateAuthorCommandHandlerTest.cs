namespace Library.UnitTest.Application.CommandHandlers;

/// <summary>
/// Contains unit tests for the <see cref="CreateAuthorCommandHandler"/> class,
/// which handles the creation of author commands.
/// </summary>
public class CreateAuthorCommandHandlerTest : BaseCommandHandlerTest
{
    private readonly CreateAuthorCommandHandler _createAuthorCommandHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateAuthorCommandHandlerTest"/> class.
    /// </summary>
    public CreateAuthorCommandHandlerTest()
    {
        _createAuthorCommandHandler = new CreateAuthorCommandHandler(_mockAuthorRepository.Object);
    }

    /// <summary>
    /// Tests that the handler successfully creates an author when the email does not already exist.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Fact]
    public async Task CreateCommand_EmailDoesntExist_ShouldReturnSuccess()
    {
        // Arrange
        var author = TestData.CreateDefaultAuthor();
        var command = new CreateAuthorCommand(author.Name, author.Email.Value);

        SetUpAuthorMocks(1, author, 1);
        _mockAuthorRepository.Setup(em => em.CheckEmailIfExist(author.Email)).ReturnsAsync(false);

        // Act
        var result = await _createAuthorCommandHandler.Handle(command, default);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }

    /// <summary>
    /// Tests that the handler returns a failure when attempting to create an author
    /// with an email that already exists.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Fact]
    public async Task CreateCommand_EmailExist_ShouldReturnFailure()
    {
        // Arrange
        var author = TestData.CreateDefaultAuthor();
        var command = new CreateAuthorCommand(author.Name, author.Email.Value);

        _mockAuthorRepository.Setup(em => em.CheckEmailIfExist(author.Email)).ReturnsAsync(true);

        // Act
        var result = await _createAuthorCommandHandler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
    }
}
