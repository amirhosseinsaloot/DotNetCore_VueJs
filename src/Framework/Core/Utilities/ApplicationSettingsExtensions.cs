using Core.Setting;

namespace Core.Utilities;

public static class ApplicationSettingsHelper
{
    public static bool ValidateApplicationSettings(this ApplicationSettings applicationSettings)
    {
        if (string.IsNullOrEmpty(applicationSettings.DatabaseSetting.ConnectionStrings.SqlServer)
         && string.IsNullOrEmpty(applicationSettings.DatabaseSetting.ConnectionStrings.Postgres))
        {
            throw new Exception("ConnectionString can not be empty (Choose between Postgres and SqlServer)");
        }

        if (applicationSettings.MailSetting.Port == default)
        {
            throw new Exception("Port in mail setting not configured");
        }

        return true;
    }
}