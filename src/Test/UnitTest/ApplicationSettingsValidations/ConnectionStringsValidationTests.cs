using Domain.Setting;

namespace UnitTest.ApplicationSettingsValidations;

public class ConnectionStringsValidationTests
{
    [Theory]
    [InlineData(null, null)]
    [InlineData("", "")]
    public void Validate_SqlServerPostgresIsNullAndEmpty_FailedShouldBeTrue(string postgresCs, string sqlServerCs)
    {
        // Arrange
        var connectionStrings = new ConnectionStrings
        {
            Postgres = postgresCs,
            SqlServer = sqlServerCs,
        };
        var connectionStringsValidation = new ConnectionStringsValidation();

        // Act
        var validateOptionsResult = connectionStringsValidation.Validate(string.Empty, connectionStrings);

        // Assert
        validateOptionsResult.Failed.Should().BeTrue();
    }

    [Theory]
    [InlineData("Host=localhost;Database=DotNetCore-VueJs;Username=postgres;Password=Password123", null)]
    [InlineData(null, "Server=.;Database=DotNetCore-VueJs;User Id=YourUsername;Password=YourPassword")]
    public void Validate_OneOfDatabasesHasValue_SucceededShouldBeTrue(string postgresCs, string sqlServerCs)
    {
        // Arrange
        var connectionStrings = new ConnectionStrings
        {
            Postgres = postgresCs,
            SqlServer = sqlServerCs
        };
        var connectionStringsValidation = new ConnectionStringsValidation();

        // Act
        var validateOptionsResult = connectionStringsValidation.Validate(string.Empty, connectionStrings);

        // Assert
        validateOptionsResult.Succeeded.Should().BeTrue();
    }
}