using System.Reflection.Metadata;

namespace ASP_NET_22._TaskFlow_CQRS_Unit_Test.Basic;

public class CalculatorTests
{
    // Fact
    // Theory

    // AAA (Arrange + Act + Assert)

    [Fact]
    public void Add_IntValuePlusIntValue_ReturnIntValue()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Add(2, 3);

        // Assert
        Assert.Equal(5, result);
    }


    public static IEnumerable<object[]> AddData()
    {
        yield return new object[] { 1, 5, 6 };
        yield return new object[] { -1, -5, -6 };
        yield return new object[] { 0, 0, 0 };
        yield return new object[] { 13, 57, 70 };
    }


    [Theory]
    //[InlineData(1, 5, 6)]
    //[InlineData(-1, -5, -6)]
    //[InlineData(0, 0, 0)]
    //[InlineData(13, 57, 70)]
    [MemberData(nameof(AddData))]
    public void Add_ReturnsExpectedResult(int left, int right, int expectedResult)
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Add(left, right);

        // Assert
        Assert.Equal(expectedResult, result);
    }



}
