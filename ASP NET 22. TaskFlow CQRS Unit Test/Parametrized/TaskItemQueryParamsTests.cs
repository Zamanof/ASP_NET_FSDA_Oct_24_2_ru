using ASP_NET_22._TaskFlow_CQRS.Application.DTOs;
using FluentAssertions;

namespace ASP_NET_22._TaskFlow_CQRS_Unit_Test.Parametrized;

public class TaskItemQueryParamsTests
{
    [Theory]
    [InlineData(0, 12, 1, 12)]
    [InlineData(45, -5, 45, 10)]
    [InlineData(45, 150, 45, 100)]
    [InlineData(5, 13, 5, 13)]
    public void Validate_NormalizesPageAndSize(
        int page,
        int size,
        int expectedPage,
        int expectedSize
        )
    {
        // Arrange
        var param = new TaskItemQueryParams
        {
            Page = page,
            Size = size
        };

        // Act
        param.Validate();

        // Assert
        //Assert.Equal(param.Page, expectedPage);
        //Assert.Equal(param.Size, expectedSize);

        param.Page.Should().Be(expectedPage);
        param.Size.Should().Be(expectedSize);
    }
}
