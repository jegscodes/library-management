using FluentValidation.Results;
using Library.Application.Common.Exceptions;

namespace Library.UnitTest.Application.Common;

/// <summary>
/// Contains unit tests for the <see cref="ValidationException"/> class,
/// validating its behavior and the handling of validation failures.
/// </summary>
public class ValidationExceptionTest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationExceptionTest"/> class.
    /// </summary>
    public ValidationExceptionTest()
    { }

    /// <summary>
    /// Tests that the constructor of <see cref="ValidationException"/> initializes
    /// the Failures property as empty.
    /// </summary>
    [Fact]
    public void ValidationException_Constructor_ShouldContainEmptyFailures()
    {
        // Act
        var failures = new ValidationException().Failures;

        // Assert
        failures.Keys.Should().BeEmpty();
        failures.Values.Should().BeEmpty();
        failures.Count.Should().Be(0);
    }

    /// <summary>
    /// Tests that creating a <see cref="ValidationException"/> with multiple failures
    /// correctly populates the Failures property with the expected keys and values.
    /// </summary>
    [Fact]
    public void ValidationException_MultipleFailures_ShouldContainAllFailures()
    {
        // Arrange
        var failures = new List<ValidationFailure>
        {
            new ValidationFailure("Email", "Invalid email format"),
            new ValidationFailure("Email", "Email cannot be null"),
            new ValidationFailure("ISBN", "Invalid ISBN format"),
            new ValidationFailure("PublishedDate", "Published date shouldn't exceed today's date")
        };

        // Act
        var result = new ValidationException(failures).Failures;

        // Assert
        result.Keys.Should().BeEquivalentTo(new[] { "Email", "ISBN", "PublishedDate" });
        result["Email"].Should().BeEquivalentTo(new[] { "Invalid email format", "Email cannot be null" });
        result.Count.Should().Be(3);
    }

    /// <summary>
    /// Tests that creating a <see cref="ValidationException"/> with a single failure
    /// correctly populates the Failures property with one entry.
    /// </summary>
    [Fact]
    public void ValidationException_SingleFailure_ShouldContainOneFailure()
    {
        // Arrange
        var failures = new List<ValidationFailure>
        {
            new ValidationFailure("Email", "Email cannot be null"),
        };

        // Act
        var result = new ValidationException(failures).Failures;

        // Assert
        result.Keys.Should().Contain("Email");
        result["Email"].Should().Contain("Email cannot be null");
        result.Count.Should().Be(1);
    }
}
