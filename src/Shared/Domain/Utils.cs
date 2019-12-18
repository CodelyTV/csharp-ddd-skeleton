namespace CodelyTv.Shared.Domain
{
    using System;
    using System.Globalization;

    public sealed class Utils
    {
        public static string DateToString(DateTime date)
        {
            return date.ToString("s", CultureInfo.CurrentCulture);
        }
    }
}