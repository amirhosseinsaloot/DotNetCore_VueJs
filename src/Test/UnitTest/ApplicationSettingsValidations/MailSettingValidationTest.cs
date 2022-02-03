using Core.Setting;

namespace UnitTest.ApplicationSettingsValidations;
public class MailSettingValidationTest
{
    [Theory]

    // Invalid EmailAddress
    [InlineData(null, "Joe", "123456", "Host@Host.com", 587), InlineData("", "Joe", "123456", "Host@Host.com", 587)]

    // Invalid DisplayName
    [InlineData("Test", null, "123456", "Host@Host.com", 587), InlineData("Test", "", "123456", "Host@Host.com", 587)]

    // Invalid Password
    [InlineData("Test", "Joe", null, "Host@Host.com", 587), InlineData("Test", "Joe", "", "Host@Host.com", 587)]

    // Invalid SmtpServer
    [InlineData("Test", "Joe", "123456", null, 587), InlineData("Test", "Joe", "123456", "", 587)]

    // Invalid Port
    [InlineData("Test", "Joe", "123456", "Host@Host.com", 0), InlineData("Test", "Joe", "123456", "Host@Host.com", -1)]
    public void Validate_InvalidProperties_FailedShouldBeTrue(string emailAddress, string DisplayName, string Password, string smtpServer, int port)
    {
        // Arrange
        var mailSetting = new MailSetting
        {
            EmailAddress = emailAddress,
            DisplayName = DisplayName,
            Password = Password,
            SmtpServer = smtpServer,
            Port = port
        };

        var mailSettingValidation = new MailSettingValidation();

        // Act
        var validateOptionsResult = mailSettingValidation.Validate(string.Empty, mailSetting);

        // Assert
        validateOptionsResult.Failed.Should().BeTrue();
    }

    [Theory]
    [InlineData("Test", "Joe", "123456", "Host@Host.com", 587)]
    public void Validate_ValidProperties_SucceededShouldBeTrue(string emailAddress, string DisplayName, string Password, string smtpServer, int port)
    {
        // Arrange
        var mailSetting = new MailSetting
        {
            EmailAddress = emailAddress,
            DisplayName = DisplayName,
            Password = Password,
            SmtpServer = smtpServer,
            Port = port
        };

        var mailSettingValidation = new MailSettingValidation();

        // Act
        var validateOptionsResult = mailSettingValidation.Validate(string.Empty, mailSetting);

        // Assert
        validateOptionsResult.Succeeded.Should().BeTrue();
    }
}