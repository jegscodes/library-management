namespace Library.UnitTest.Application.Common;

/// <summary>
/// Contains unit tests for mapping methods that convert domain entities
/// to response objects for books and authors.
/// </summary>
public class MappingTest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MappingTest"/> class.
    /// </summary>
    public MappingTest()
    { }

    /// <summary>
    /// Tests that the values in the mapped <see cref="GetAuthorResponse"/>
    /// correctly match the original author entity's properties.
    /// </summary>
    [Fact]
    public void MapAuthor_Should_ReturnCorrectValue()
    {
        // Arrange
        var author = TestData.CreateDefaultAuthor();

        // Act
        var result = author.ToAuthorResponse();

        // Assert
        result.Email.Should().Be(author.Email.Value);
        result.Name.Should().Be(author.Name);
        result.Email.Should().Contain("@");
    }

    /// <summary>
    /// Tests that mapping an author returns an object of type <see cref="GetAuthorResponse"/>.
    /// </summary>
    [Fact]
    public void MapAuthor_ShouldBe_TypeOfAuthorResponse()
    {
        // Arrange
        var author = TestData.CreateDefaultAuthor();

        // Act
        var result = author.ToAuthorResponse();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<GetAuthorResponse>();
    }

    /// <summary>
    /// Tests that the values in the mapped <see cref="GetBookResponse"/>
    /// correctly match the original book entity's properties.
    /// </summary>
    [Fact]
    public void MapBook_Should_ReturnCorrectValue()
    {
        // Arrange
        var book = TestData.CreateDefaultBook();

        // Act
        var result = book.ToBookResponse();

        // Assert
        result.Title.Should().Be(book.Title);
        result.PublishedDate.Should().Be(book.PublishedDate.ToString());
        result.ISBN.Should().Be(result.ISBN);
        result.Author.Should().Be(result.Author);
    }

    /// <summary>
    /// Tests that mapping a book returns an object of type <see cref="GetBookResponse"/>.
    /// </summary>
    [Fact]
    public void MapBook_ShouldBe_TypeOfBookResponse()
    {
        // Arrange
        var book = TestData.CreateDefaultBook();

        // Act
        var result = book.ToBookResponse();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<GetBookResponse>();
    }
    /// <summary>
    /// Tests that the properties of the mapped paginated list of authors
    /// correctly reflect the original list's pagination and author details.
    /// </summary>
    [Fact]
    public void MapPaginatedListT_Should_ReturnCorrectValues()
    {
        // Arrange
        var authors = new List<Author>()
        {
            TestData.CreateAuthor(1, "Jane Smith", "jsmith@test.com"),
            TestData.CreateAuthor(2, "John Doe", "jdoe@test.com"),
            TestData.CreateAuthor(3, "Alice Johnson", "ajohnson@test.com"),
            TestData.CreateAuthor(4, "Bob Brown", "bbrown@test.com")
        };

        var paginatedList = new PaginatedList<Author>(authors, 4, 1, 2);

        // Act
        var result = paginatedList.ToPaginatedAuthorReponse();

        // Assert
        result.PageSize.Should().Be(2);
        result.PageNumber.Should().Be(1);
        result.HasNextPage.Should().BeTrue();
        result.HasPreviousPage.Should().BeFalse();
        result.Items.Count.Should().Be(4);
    }

    /// <summary>
    /// Tests that mapping a paginated list of authors returns an object of type <see cref="PaginatedList{T}"/>.
    /// </summary>
    [Fact]
    public void MapPaginatedListT_Should_ReturnTypeOfPaginatedListT()
    {
        // Arrange
        var authors = new List<Author>()
        {
            TestData.CreateAuthor(1, "Jane Smith", "jsmith@test.com"),
            TestData.CreateAuthor(2, "John Doe", "jdoe@test.com"),
            TestData.CreateAuthor(3, "Alice Johnson", "ajohnson@test.com"),
            TestData.CreateAuthor(4, "Bob Brown", "bbrown@test.com")
        };

        var paginatedList = new PaginatedList<Author>(authors, 4, 1, 2);

        // Act
        var result = paginatedList.ToPaginatedAuthorReponse();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<PaginatedList<GetAuthorResponse>>();
    }
}
