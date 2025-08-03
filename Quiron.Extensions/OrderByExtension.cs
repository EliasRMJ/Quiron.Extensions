using System.Reflection;

namespace Quiron.Extensions
{
    public static class OrderByExtension
    {
        public static IOrderedEnumerable<IDictionary<string, object>> OrderByMultipleKeys(this IEnumerable<IDictionary<string, object>> source
            , string? orderBy)
        {
            IOrderedEnumerable<IDictionary<string, object>>? result = null;
            var keys = ParseOrderBy(orderBy);

            foreach (var (key, ascending) in keys)
            {
                var camelCaseName = char.ToLower(key[0]) + key[1..];
                if (result is null)
                {
                    result = ascending
                        ? source.OrderBy(dict => GetComparable(dict, camelCaseName))
                        : source.OrderByDescending(dict => GetComparable(dict, camelCaseName));
                }
                else
                {
                    result = ascending
                        ? result.ThenBy(dict => GetComparable(dict, camelCaseName))
                        : result.ThenByDescending(dict => GetComparable(dict, camelCaseName));
                }
            }

            return result ?? source.OrderBy(dict => 0);
        }

        public static IOrderedEnumerable<IDictionary<string, object>> OrderByMultipleKeys(this List<IDictionary<string, object>> source
            , string? orderBy)
        {
            return OrderByMultipleKeys(source.AsEnumerable(), orderBy);
        }

        public static IOrderedEnumerable<T> OrderByMultiple<T>(this IEnumerable<T> source, string? orderBy)
        {
            IOrderedEnumerable<T>? result = null;
            var keys = ParseOrderBy(orderBy);

            foreach (var (key, ascending) in keys)
            {
                var originalProperty = MatchPascalCaseProperty<T>(key) ??
                    throw new ArgumentException($"Property '{key}' not found!");

                var propInfo = typeof(T).GetProperty(originalProperty, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance) ??
                    throw new ArgumentException($"Property '{originalProperty}' not found!");

                if (result is null)
                {
                    result = ascending
                        ? source.OrderBy(obj => propInfo.GetValue(obj, null))
                        : source.OrderByDescending(obj => propInfo.GetValue(obj, null));
                }
                else
                {
                    result = ascending
                        ? result.ThenBy(obj => propInfo.GetValue(obj, null))
                        : result.ThenByDescending(obj => propInfo.GetValue(obj, null));
                }
            }

            return result ?? source.OrderBy(obj => 0);
        }

        public static IOrderedEnumerable<T> OrderByMultiple<T>(this List<T> source, string? orderBy)
        {
            return OrderByMultiple<T>(source.AsEnumerable(), orderBy);
        }

        private static (string Key, bool Ascending)[] ParseOrderBy(string? orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return [];

            return [.. orderBy
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(part =>
                {
                    var tokens = part.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    var ascending = tokens.Length == 1 || tokens[1].Equals("asc", StringComparison.OrdinalIgnoreCase);
                    return (tokens[0], ascending);
                })];
        }

        private static IComparable? GetComparable(IDictionary<string, object> dict, string key)
        {
            return dict.TryGetValue(key, out var value) && value is IComparable cmp ? cmp : null;
        }

        private static string? MatchPascalCaseProperty<T>(string input)
        {
            return typeof(T)
                .GetProperties()
                .FirstOrDefault(property => property.Name.ToLower() == input.ToLower())?.Name;
        }
    }
}