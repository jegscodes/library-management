using Library.Application.Authors.Commands.CreateAuthor;

namespace Library.UnitTest.Application.CommandHandlers;

public class CreateAuthorCommandHandlerTest : BaseCommandHandlerTest
{
    private CreateAuthorCommandHandler _createAuthorCommandHandler;
    public CreateAuthorCommandHandlerTest()
    {
        _createAuthorCommandHandler = new CreateAuthorCommandHandler(_mockAuthorRepository.Object);
    }

    [Fact]
    public async Task CreateCommand_EmailDoesntExist_ShouldReturnSucces()
    {
        var author = TestData.CreateDefaultAuthor();

        var command = new CreateAuthorCommand(author.Name, author.Email.Value);

        SetUpAuthorMocks(1, author, 1);

        _mockAuthorRepository.Setup(em => em.CheckEmailIfExist(author!.Email)).ReturnsAsync(false);

        var result = await _createAuthorCommandHandler.Handle(command, default);

        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }

    [Fact]
    public async Task CreateCommand_EmailExist_ShouldReturnFailure()
    {
        var author = TestData.CreateDefaultAuthor();

        var command = new CreateAuthorCommand(author.Name, author.Email.Value);

        _mockAuthorRepository.Setup(em => em.CheckEmailIfExist(author!.Email)).ReturnsAsync(true);

        var result = await _createAuthorCommandHandler.Handle(command, default);

        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
    }
}
