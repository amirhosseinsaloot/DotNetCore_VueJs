using System.ComponentModel.DataAnnotations;

namespace Core.Utilities;

public static class EnumExtensions
{
    public static string? GetDisplayName(this Enum enumValue)
    {
        if (enumValue is null)
        {
            throw new ArgumentNullException($"{nameof(enumValue)} : {typeof(Enum)}");
        }

        var displayName = enumValue.ToString();
        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

        if (fieldInfo is not null)
        {
            var attrs = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), true);
            if (attrs is not null && attrs.Length > 0)
            {
                displayName = ((DisplayAttribute)attrs[0]).Name;
            }
        }

        return displayName;
    }
}
