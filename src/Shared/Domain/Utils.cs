using System;
using System.Globalization;
using System.Linq;

namespace CodelyTv.Shared.Domain
{
    public static class Utils
    {
        public static string DateToString(DateTime date)
        {
            return date.ToString("s", CultureInfo.CurrentCulture);
        }

        public static DateTime StringToDate(string date)
        {
            return DateTime.ParseExact(date, "s", CultureInfo.CurrentCulture);
        }

        public static string ToSnake(this string text)
        {
            return string.Concat(text.Select((x, i) =>
                    i > 0 && char.IsUpper(x) ? "_" + x : x.ToString(CultureInfo.InvariantCulture)))
                .ToLowerInvariant();
        }

        public static string ToCamelFirstUpper(this string text)
        {
            var textInfo = new CultureInfo(CultureInfo.CurrentCulture.ToString(), false).TextInfo;
            return textInfo.ToTitleCase(text).Replace("_", string.Empty);
        }
    }
}
