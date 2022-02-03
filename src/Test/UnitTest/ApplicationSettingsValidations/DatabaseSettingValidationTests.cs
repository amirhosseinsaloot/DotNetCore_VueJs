using Core.Setting;

namespace UnitTest.ApplicationSettingsValidations;

public class DatabaseSettingValidationTests
{
    [Theory]
    [InlineData(null), InlineData("")]
    public void Validate_ProviderSetPostgresItsConnectionStringNullAndEmpty_FailedShouldBeTrue(string connectionString)
    {
        // Arrange
        var databaseSetting = new DatabaseSetting
        {
            DatabaseProvider = DatabaseProvider.Postgres,
            ConnectionStrings = new ConnectionStrings
            {
                Postgres = connectionString
            }
        };
        var databaseSettingValidation = new DatabaseSettingValidation();

        // Act
        var validateOptionsResult = databaseSettingValidation.Validate(string.Empty, databaseSetting);

        // Assert
        validateOptionsResult.Failed.Should().BeTrue();
    }

    [Fact]
    public void Validate_ProviderSetPostgresItsConnectionStringHasValue_SucceededShouldBeTrue()
    {
        // Arrange
        var databaseSetting = new DatabaseSetting
        {
            DatabaseProvider = DatabaseProvider.Postgres,
            ConnectionStrings = new ConnectionStrings { Postgres = "Host=localhost;Database=DotNetCore-VueJs;Username=postgres;Password=Password123" }
        };
        var databaseSettingValidation = new DatabaseSettingValidation();

        // Act
        var validateOptionsResult = databaseSettingValidation.Validate(string.Empty, databaseSetting);

        // Assert
        validateOptionsResult.Succeeded.Should().BeTrue();
    }

    [Theory]
    [InlineData(null), InlineData("")]
    public void Validate_ProviderSetSqlServerItsConnectionStringNullAndEmpty_FailedShouldBeTrue(string connectionString)
    {
        // Arrange
        var databaseSetting = new DatabaseSetting
        {
            DatabaseProvider = DatabaseProvider.SqlServer,
            ConnectionStrings = new ConnectionStrings
            {
                SqlServer = connectionString
            }
        };
        var databaseSettingValidation = new DatabaseSettingValidation();

        // Act
        var validateOptionsResult = databaseSettingValidation.Validate(string.Empty, databaseSetting);

        // Assert
        validateOptionsResult.Failed.Should().BeTrue();
    }

    [Fact]
    public void Validate_ProviderSetSqlServerItsConnectionStringHasValue_SucceededShouldBeTrue()
    {
        // Arrange
        var databaseSetting = new DatabaseSetting
        {
            DatabaseProvider = DatabaseProvider.SqlServer,
            ConnectionStrings = new ConnectionStrings { SqlServer = "Server=.;Database=DotNetCore-VueJs;User Id=YourUsername;Password=YourPassword" }
        };
        var databaseSettingValidation = new DatabaseSettingValidation();

        // Act
        var validateOptionsResult = databaseSettingValidation.Validate(string.Empty, databaseSetting);

        // Assert
        validateOptionsResult.Succeeded.Should().BeTrue();
    }
}