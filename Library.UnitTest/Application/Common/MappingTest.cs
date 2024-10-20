namespace Library.UnitTest.Application.Common;

public class MappingTest
{
    public MappingTest() { }

    [Fact]
    public void MapBook_ShouldBe_TypeOfBookResponse()
    {
        var book = TestData.CreateDefaultBook();

        var result = book.ToBookResponse();

        result.Should().NotBeNull();
        result.Should().BeOfType<GetBookResponse>();    
    }

    [Fact]
    public void MapBook_Should_ReturnCorrectValue()
    {
        var book = TestData.CreateDefaultBook();

        var result = book.ToBookResponse();

        result.Title.Should().Be(book.Title);
        result.PublishedDate.Should().Be(book.PublishedDate.ToString());
        result.ISBN.Should().Be(result.ISBN);
        result.Author.Should().Be(result.Author);
    }


    [Fact]
    public void MapAuthor_ShouldBe_TypeOfAuthorResponse()
    {
        var author = TestData.CreateDefaultAuthor();

        var result = author.ToAuthorResponse();

        result.Should().NotBeNull();
        result.Should().BeOfType<GetAuthorResponse>();
    }

    [Fact]
    public void MapAuthor_Should_ReturnCorrectValue()
    {
        var author = TestData.CreateDefaultAuthor();

        var result = author.ToAuthorResponse();

        result.Email.Should().Be(author.Email.Value);
        result.Name.Should().Be(author.Name);
        result.Email.Should().Contain("@");
    }

    [Fact]
    public void MapPaginatedListT_Should_ReturnTypeOfPaginatedListT()
    {
        var authors = new List<Author>()
        {
                TestData.CreateAuthor(1, "Jane Smith", "jsmith@test.com"),
                TestData.CreateAuthor(2, "John Doe", "jdoe@test.com"),
                TestData.CreateAuthor(3, "Alice Johnson", "ajohnson@test.com"),
                TestData.CreateAuthor(4, "Bob Brown", "bbrown@test.com")
        };

        var paginatedList = new PaginatedList<Author>(authors, 4, 1, 2);

        var result = paginatedList.ToPaginatedAuthorReponse();

        result.Should().NotBeNull();
        result.Should().BeOfType<PaginatedList<GetAuthorResponse>>();
    }

    [Fact]
    public void MapPaginatedListT_Should_ReturnCorrectValues()
    {
        var authors = new List<Author>()
        {
                TestData.CreateAuthor(1, "Jane Smith", "jsmith@test.com"),
                TestData.CreateAuthor(2, "John Doe", "jdoe@test.com"),
                TestData.CreateAuthor(3, "Alice Johnson", "ajohnson@test.com"),
                TestData.CreateAuthor(4, "Bob Brown", "bbrown@test.com")
        };

        var paginatedList = new PaginatedList<Author>(authors, 4, 1, 2);

        var result = paginatedList.ToPaginatedAuthorReponse();

        result.PageSize.Should().Be(2);
        result.PageNumber.Should().Be(1);
        result.HasNextPage.Should().BeTrue();
        result.HasPreviousPage.Should().BeFalse();
        result.Items.Count.Should().Be(4);
    }
}
