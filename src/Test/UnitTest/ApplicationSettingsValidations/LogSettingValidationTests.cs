using Domain.Setting;

namespace UnitTest.ApplicationSettingsValidations;
public class LogSettingValidationTests
{
    [Theory]
    [InlineData(""), InlineData(null)]
    public void Validate_TableNameIsNullAndEmpty_FailedShouldBeTrue(string tableName)
    {
        // Arrange
        var logSetting = new LogSetting
        {
            TableName = tableName
        };

        var logSettingValidation = new LogSettingValidation();

        // Act
        var validateOptionsResult = logSettingValidation.Validate(string.Empty, logSetting);

        // Assert
        validateOptionsResult.Failed.Should().BeTrue();
    }

    [Theory]
    [InlineData("SysError"), InlineData("Log")]
    public void Validate_TableNameHasValue_SucceededShouldBeTrue(string tableName)
    {
        // Arrange
        var logSetting = new LogSetting
        {
            TableName = tableName
        };

        var logSettingValidation = new LogSettingValidation();

        // Act
        var validateOptionsResult = logSettingValidation.Validate(string.Empty, logSetting);

        // Assert
        validateOptionsResult.Succeeded.Should().BeTrue();
    }
}