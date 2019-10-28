namespace CodelyTv.Mooc.Shared.Infrastructure.Persistence.EntityFramework.Extension
{
    using System.Globalization;

    public static class ConfigurationExtension
    {
        public static string FormatDatabaseName(this string value)
        {
            return value.ToLower(CultureInfo.InvariantCulture);
        }
    }
}