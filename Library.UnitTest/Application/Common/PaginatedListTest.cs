using Microsoft.EntityFrameworkCore;
using Moq;

namespace Library.UnitTest.Application.Common;

public class PaginatedListTest
{
    public PaginatedListTest()
    {
        
    }

    [Fact]
    public void PaginatedList_Constructor_ShouldInitializePropertiesCorrectly()
    {
        var items = new List<string> { "Sample 1", "Sample 2" };
        int count = 5;
        int pageNumber = 1;
        int pageSize = 2;

        var paginatedList = new PaginatedList<string>(items, count, pageNumber, pageSize);

        paginatedList.Items.Count().Should().Be(2);
        paginatedList.PageNumber.Should().Be(1);
        paginatedList.PageSize.Should().Be(2);
        paginatedList.HasNextPage.Should().BeTrue();
        paginatedList.HasPreviousPage.Should().BeFalse();
    }
}
