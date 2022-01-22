using Core.Utilities;

namespace UnitTest.Utilities;

public class StringExtensionsTest
{
    private class ActualExpected
    {
        public string Actual { get; set; } = default!;

        public string Expected { get; set; } = default!;
    }

    [Fact]
    public void ToSnakeCase()
    {
        // Arrange
        var actualExpecteds = new List<ActualExpected>
            {
                new ActualExpected { Actual = "", Expected = "" },
                new ActualExpected { Actual = "0", Expected = "0" },
                new ActualExpected { Actual = "10", Expected = "10" },
                new ActualExpected { Actual = "A", Expected = "a" },
                new ActualExpected { Actual = "AB", Expected = "ab" },
                new ActualExpected { Actual = "ABa", Expected = "aba" },
                new ActualExpected { Actual = "AbA", Expected = "ab_a" },
                new ActualExpected { Actual = "camelCase", Expected = "camel_case" },
                new ActualExpected { Actual = "PascalCase", Expected = "pascal_case" }
            };

        foreach (var item in actualExpecteds)
        {
            // Act
            item.Actual = item.Actual.ToSnakeCase();

            // Assert
            item.Actual.Should().Be(item.Expected);
        }
    }
}
