namespace Library.UnitTest.Application.Common;

/// <summary>
/// Contains unit tests for the <see cref="PaginatedList{T}"/> class,
/// validating the correct initialization and properties of a paginated list.
/// </summary>
public class PaginatedListTest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PaginatedListTest"/> class.
    /// </summary>
    public PaginatedListTest()
    { }

    /// <summary>
    /// Tests that the constructor of <see cref="PaginatedList{T}"/> initializes
    /// the properties correctly based on the provided parameters.
    /// </summary>
    [Fact]
    public void PaginatedList_Constructor_ShouldInitializePropertiesCorrectly()
    {
        // Arrange
        var items = new List<string> { "Sample 1", "Sample 2" };
        int count = 5;
        int pageNumber = 1;
        int pageSize = 2;

        // Act
        var paginatedList = new PaginatedList<string>(items, count, pageNumber, pageSize);

        // Assert
        paginatedList.Items.Count().Should().Be(2);
        paginatedList.PageNumber.Should().Be(1);
        paginatedList.PageSize.Should().Be(2);
        paginatedList.HasNextPage.Should().BeTrue();
        paginatedList.HasPreviousPage.Should().BeFalse();
    }
}
