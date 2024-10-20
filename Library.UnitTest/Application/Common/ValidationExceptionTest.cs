using FluentValidation.Results;
using Library.Application.Common.Exceptions;

namespace Library.UnitTest.Application.Common;

public class ValidationExceptionTest
{
    public ValidationExceptionTest()
    {
        
    }

    [Fact]
    public void ValidationException_Constructor_ShouldContainEmptyFailures()
    {
        var failures = new ValidationException().Failures;

        failures.Keys.Should().BeEmpty();
        failures.Values.Should().BeEmpty();
        failures.Count.Should().Be(0);
    }

    [Fact]
    public void ValidationException_SingleFailur_ShouldContainOneFailure()
    {
        var failures = new List<ValidationFailure>
        {
            new ValidationFailure("Email", "Email cannot be null"),
        };

        var result = new ValidationException(failures).Failures;

        result.Keys.Should().Contain("Email");
        result["Email"].Should().Contain("Email cannot be null");
        result.Count.Should().Be(1);
    }

    [Fact]
    public void ValidationException_SingleFailur_ShouldContainMultipleFailure()
    {
        var failures = new List<ValidationFailure>
        {
            new ValidationFailure("Email", "Invalid email format"),
            new ValidationFailure("Email", "Email cannot be null"),
            new ValidationFailure("ISBN", "Invalid ISBN format"),
            new ValidationFailure("PublishedDate", "Published date shouldn't exceed today's date")
        };

        var result = new ValidationException(failures).Failures;

        result.Keys.Should().BeEquivalentTo(["Email", "ISBN", "PublishedDate"]);

        result["Email"].Should().BeEquivalentTo(["Invalid email format", "Email cannot be null"]);

        result.Count.Should().Be(3);
    }
}
