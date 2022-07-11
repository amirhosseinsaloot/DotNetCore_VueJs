using Domain.Setting;

namespace UnitTest.ApplicationSettingsValidations;

public class IdentitySettingValidationTests
{
    [Theory]
    [InlineData(5), InlineData(-1)]
    public void Validate_PasswordRequiredLengthIsLessThan6_FailedShouldBeTrue(int number)
    {
        // Arrange
        var identitySetting = new IdentitySetting
        {
            PasswordRequiredLength = number
        };

        var identitySettingValidation = new IdentitySettingValidation();

        // Act
        var validateOptionsResult = identitySettingValidation.Validate(string.Empty, identitySetting);

        // Assert
        validateOptionsResult.Failed.Should().BeTrue();
    }

    [Theory]
    [InlineData(6), InlineData(10)]
    public void Validate_NotBeforeMinutesIsGreaterThanEqual6_SucceededShouldBeTrue(int number)
    {
        // Arrange
        var identitySetting = new IdentitySetting
        {
            PasswordRequiredLength = number
        };

        var identitySettingValidation = new IdentitySettingValidation();

        // Act
        var validateOptionsResult = identitySettingValidation.Validate(string.Empty, identitySetting);

        // Assert
        validateOptionsResult.Succeeded.Should().BeTrue();
    }
}
