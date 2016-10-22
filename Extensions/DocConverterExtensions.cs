namespace VSDocConverter.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using VSDocConverter.Elements;

    public static class DocConverterExtensions
    {
        public static string ConvertElement(this IDocConverter value, DocElement element)
        {
            if (element == null)
            {
                return null;
            }

            var result = (string)value.GetType()
                .InvokeMember(
                    "Convert",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod,
                    null,
                    value,
                    new object[] { element });

            return result;
        }

        public static IEnumerable<string> ConvertToLines(this IDocConverter value, DocElement element)
        {
            var converted = ConvertElement(value, element);

            var result = (converted ?? string.Empty).Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            return result;
        }
    }
}
