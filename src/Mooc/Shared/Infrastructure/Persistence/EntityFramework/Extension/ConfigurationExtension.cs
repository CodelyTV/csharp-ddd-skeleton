namespace CodelyTv.Mooc.Shared.Infrastructure.Persistence.EntityFramework.Extension
{
    using System;
    using System.Linq;

    public static class ConfigurationExtension
    {
        static Func<char, string> AddUnderscoreBeforeCapitalLetter = x => Char.IsUpper(x) ? "_" + x : x.ToString();

        public static string ToDatabaseFormat(this string value)
        {
            return string.Concat(value.Select(AddUnderscoreBeforeCapitalLetter)).Substring(1).ToLower();
        }
    }
}