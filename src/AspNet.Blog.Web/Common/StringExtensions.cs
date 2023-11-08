using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;

namespace AspNet.Blog.Web.Common;

public static class StringExtensions
{
    public static String ToSlug(this string text, string separator = "-")
    {
        text = text ?? String.Empty;
        separator = separator ?? string.Empty;
        String value = text.Normalize(NormalizationForm.FormD).Trim();
        StringBuilder builder = new StringBuilder();

        foreach (char c in text.ToCharArray())
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            {
                builder.Append(c);
            }
        }

        value = builder.ToString();
        byte[] bytes = Encoding.GetEncoding("Cyrillic").GetBytes(text);
        value = Regex.Replace(Regex.Replace(Encoding.ASCII.GetString(bytes), @"\s{2,}|[^\w]", " ", RegexOptions.ECMAScript).Trim(), @"\s+", separator);

        return value.ToLowerInvariant();
    }
}
