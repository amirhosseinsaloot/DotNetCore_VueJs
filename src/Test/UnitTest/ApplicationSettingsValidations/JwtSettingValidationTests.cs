using Domain.Setting;

namespace UnitTest.ApplicationSettingsValidations;

public class JwtSettingValidationTests
{
    [Theory]
    [InlineData(-1)]
    public void Validate_NotBeforeMinutesIsLessThan0_FailedShouldBeTrue(int number)
    {
        // Arrange
        var jwtSetting = new JwtSetting
        {
            NotBeforeMinutes = number
        };

        var jwtSettingValidation = new JwtSettingValidation();

        // Act
        var validateOptionsResult = jwtSettingValidation.Validate(string.Empty, jwtSetting);

        // Assert
        validateOptionsResult.Failed.Should().BeTrue();
    }

    [Theory]
    [InlineData(0), InlineData(1)]
    public void Validate_NotBeforeMinutesIsGreaterThanEqual0_SucceededShouldBeTrue(int number)
    {
        // Arrange
        var jwtSetting = new JwtSetting
        {
            NotBeforeMinutes = number
        };

        var jwtSettingValidation = new JwtSettingValidation();

        // Act
        var validateOptionsResult = jwtSettingValidation.Validate(string.Empty, jwtSetting);

        // Assert
        validateOptionsResult.Succeeded.Should().BeTrue();
    }

    [Theory]
    [InlineData(0)]
    public void Validate_AccessTokenExpirationDaysIsLessThan1_FailedShouldBeTrue(int number)
    {
        // Arrange
        var jwtSetting = new JwtSetting
        {
            AccessTokenExpirationDays = number
        };

        var jwtSettingValidation = new JwtSettingValidation();

        // Act
        var validateOptionsResult = jwtSettingValidation.Validate(string.Empty, jwtSetting);

        // Assert
        validateOptionsResult.Failed.Should().BeTrue();
    }

    [Theory]
    [InlineData(1), InlineData(2)]
    public void Validate_AccessTokenExpirationDaysIsGreaterThanEqual1_SucceededShouldBeTrue(int number)
    {
        // Arrange
        var jwtSetting = new JwtSetting
        {
            AccessTokenExpirationDays = number
        };

        var jwtSettingValidation = new JwtSettingValidation();

        // Act
        var validateOptionsResult = jwtSettingValidation.Validate(string.Empty, jwtSetting);

        // Assert
        validateOptionsResult.Succeeded.Should().BeTrue();
    }

    [Theory]
    [InlineData(0)]
    public void Validate_RefreshTokenExpirationDaysIsLessThan1_FailedShouldBeTrue(int number)
    {
        // Arrange
        var jwtSetting = new JwtSetting
        {
            RefreshTokenExpirationDays = number
        };

        var jwtSettingValidation = new JwtSettingValidation();

        // Act
        var validateOptionsResult = jwtSettingValidation.Validate(string.Empty, jwtSetting);

        // Assert
        validateOptionsResult.Failed.Should().BeTrue();
    }

    [Theory]
    [InlineData(1), InlineData(2)]
    public void Validate_RefreshTokenExpirationDaysIsGreaterThanEqual1_SucceededShouldBeTrue(int number)
    {
        // Arrange
        var jwtSetting = new JwtSetting
        {
            RefreshTokenExpirationDays = number
        };

        var jwtSettingValidation = new JwtSettingValidation();

        // Act
        var validateOptionsResult = jwtSettingValidation.Validate(string.Empty, jwtSetting);

        // Assert
        validateOptionsResult.Succeeded.Should().BeTrue();
    }
}