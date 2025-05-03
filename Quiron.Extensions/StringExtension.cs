using System.Text;

namespace Quiron.Extensions
{
    public static class StringExtension
    {
        public static string ToStr(this string value, int maxLen)
        {
            return value.Length <= maxLen ? value : $"{value[..maxLen]}...";
        }

        public static string JoinIn(this IEnumerable<string>? list, char delimitor)
        {
            if (list is null)
                return string.Empty;

            var stringBuilder = new StringBuilder();
            for (var i = 0; i < list.Count(); i++)
                stringBuilder.AppendFormat("{0} {1} ", list.ElementAt(i), delimitor);

            return !string.IsNullOrEmpty(stringBuilder.ToString()) ? stringBuilder.ToString()[..^2] : string.Empty;
        }

        public static string Join<T>(this string joinWith, IEnumerable<T> list)
        {
            if (list is null)
                return string.Empty;

            var stringBuilder = new StringBuilder();
            var enumerator = list.GetEnumerator();

            if (!enumerator.MoveNext())
                return string.Empty;

            while (true)
            {
                stringBuilder.Append(enumerator.Current);
                if (!enumerator.MoveNext())
                    break;

                stringBuilder.Append(joinWith);
            }

            return stringBuilder.ToString();
        }

        public static byte[]? FromBase64String(this string base64)
        {
            if (string.IsNullOrEmpty(base64))
                return null;

            return Convert.FromBase64String(base64);
        }

        public static string ToBase64String(this byte[] arrByte)
        {
            if (arrByte is null)
                return string.Empty;

            return Convert.ToBase64String(arrByte);
        }

        public static string Initcap(this string text)
        {
            var newWord = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(text))
            {
                newWord.Append(text[..1].ToUpper());
                for (var i = 1; i < text.Length; i++)
                {
                    if ((text.Substring(i, 1) == " ") || 
                        (text.Substring(i, 1) == "-") && (i != text.Length - 1))
                    {
                        newWord.AppendFormat(" {0}", text.Substring(i + 1, 1).ToUpper());
                        i++;
                    }
                    else
                    {
                        newWord.Append(text.Substring(i, 1).ToLower());
                    }
                }
            }

            return newWord.ToString();
        }
    }
}