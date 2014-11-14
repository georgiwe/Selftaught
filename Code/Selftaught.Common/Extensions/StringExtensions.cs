namespace Selftaught.Common.Extensions
{
    using System;

    public static class StringExtensions
    {
        public static string ToTitleCase(this string str)
        {
            var lowercase = str.ToLower();
            var titleCase = char.ToUpper(lowercase[0]) + lowercase.Substring(1);

            return titleCase;
        }

        public static T ToEnum<T>(this string str)
        {
            return (T)Enum.Parse(typeof(T), str, true);
        }
    }
}
