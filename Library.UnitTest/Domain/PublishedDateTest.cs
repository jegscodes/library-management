namespace Library.UnitTest.Domain;

/// <summary>
/// Unit tests for the <see cref="PublishedDate"/> class, following the Arrange-Act-Assert (AAA) pattern.
/// Each test is structured to clearly separate the setup of test data (Arrange), 
/// the execution of the code being tested (Act), and the validation of the results (Assert).
/// </summary>
public class PublishedDateTest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PublishedDateTest"/> class.
    /// </summary>
    public PublishedDateTest() { }

    /// <summary>
    /// Tests that creating a <see cref="PublishedDate"/> with a valid date does not throw an exception.
    /// </summary>
    [Fact]
    public void PublishDate_ValidDate_ShouldNotThrow()
    {
        // Arrange
        var date = new DateTime(2024, 10, 18);

        // Act
        var publishedDate = PublishedDate.Create(date);

        // Assert
        publishedDate.Should().NotBeNull();
    }

    /// <summary>
    /// Tests that creating a <see cref="PublishedDate"/> with the current date does not throw an exception.
    /// </summary>
    [Fact]
    public void PublishDate_Now_ShouldNotThrow()
    {
        // Arrange
        var date = DateTime.Now;

        // Act
        var publishedDate = PublishedDate.Create(date);

        // Assert
        publishedDate.Should().NotBeNull();
    }

    /// <summary>
    /// Tests that creating a <see cref="PublishedDate"/> with various valid dates sets the value correctly.
    /// </summary>
    /// <param name="previousDate">A valid date string to be parsed into a DateTime.</param>
    [Theory]
    [InlineData("1986-12-20")]
    [InlineData("2000-01-01")]
    [InlineData("1895-02-03")]
    public void PublishDate_ValidDate_ShouldSetValueCorrectly(string previousDate)
    {
        // Arrange
        var date = DateTime.Parse(previousDate);

        // Act
        var publishedDate = PublishedDate.Create(date);

        // Assert
        publishedDate.Should().NotBeNull();
        publishedDate.Value.Should().Be(date);
    }

    /// <summary>
    /// Tests that creating a <see cref="PublishedDate"/> with a future date throws a <see cref="PublishedDateException"/>.
    /// </summary>
    [Fact]
    public void PublishDate_FutureDate_ShouldThrowPublishedDateException()
    {
        // Arrange
        var futureDate = new DateTime(2025, 10, 18);

        // Act & Assert
        Assert.Throws<PublishedDateException>(() => PublishedDate.Create(futureDate));
    }

    /// <summary>
    /// Tests that the exception message thrown for a future date is correct.
    /// </summary>
    [Fact]
    public void PublishDate_FutureDate_ShouldThrowMessagePublishDate()
    {
        // Arrange
        var futureDate = new DateTime(2025, 10, 18);

        // Act & Assert
        var exception = Assert.Throws<PublishedDateException>(() => PublishedDate.Create(futureDate));
        exception.Message.Should().Be("Published date shouldn't exceed today's date.");
    }

    /// <summary>
    /// Tests that creating a <see cref="PublishedDate"/> with a valid date maintains the same month.
    /// </summary>
    [Fact]
    public void PublishDate_ValidDate_ShouldHaveSimilarMonth()
    {
        // Arrange
        var date = new DateTime(1991, 08, 03);

        // Act
        var publishedDate = PublishedDate.Create(date);

        // Assert
        publishedDate.Value.Month.Should().Be(date.Month);
    }

    /// <summary>
    /// Tests that creating a <see cref="PublishedDate"/> with a valid date maintains the same day.
    /// </summary>
    [Fact]
    public void PublishDate_ValidDate_ShouldHaveSimilarDay()
    {
        // Arrange
        var date = new DateTime(1991, 04, 02);

        // Act
        var publishedDate = PublishedDate.Create(date);

        // Assert
        publishedDate.Value.Day.Should().Be(date.Day);
    }

    /// <summary>
    /// Tests that creating a <see cref="PublishedDate"/> with a valid date maintains the same year.
    /// </summary>
    [Fact]
    public void PublishDate_ValidDate_ShouldHaveSimilarYear()
    {
        // Arrange
        var date = new DateTime(1890, 12, 31);

        // Act
        var publishedDate = PublishedDate.Create(date);

        // Assert
        publishedDate.Value.Year.Should().Be(date.Year);
    }
    
}
