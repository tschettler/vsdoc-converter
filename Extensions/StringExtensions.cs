namespace VSDocConverter.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;

    public static class StringExtensions
    {
        public static object Deserialize(this string value, Type type)
        {
            var serializer = new XmlSerializer(type);
            using (var sr = new StringReader(value))
            {
                return serializer.Deserialize(sr);
            }
        }

        public static T Deserialize<T>(this string value)
        {
            return (T)Deserialize(value, typeof(T));
        }

        public static int GetIndentLevel(this string value)
        {
            var tabLength = 4;
            var tabSpaces = new string(' ', tabLength);

            value = value.Replace("\t", tabSpaces);

            var indentLevel = value.Length - value.TrimStart().Length;

            return indentLevel;
        }

        public static IEnumerable<string> SplitByLine(this string value)
        {
            return value.Split(new[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string AddLeadAsterisk(this string value)
        {
            var parts = value.SplitByLine();
            var result = string.Join(Environment.NewLine + " * ", parts);
            return result;
        }

        public static string AddLeadAsterisk(this IEnumerable<string> value)
        {
            var result = " * " + string.Join(Environment.NewLine + " * ", value);
            return result;
        }
    }
}
