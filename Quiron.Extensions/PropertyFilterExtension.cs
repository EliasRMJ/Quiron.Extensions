using System.Dynamic;

namespace Quiron.Extensions
{
    public static class PropertyFilterExtension
    {
        public static IEnumerable<IDictionary<string, object>> ToFilterString<T>(this IEnumerable<T> list, string? fields = "")
        {
            if (list is null || !list.Any())
                return [];

            var fieldList = fields?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                           .Select(prop => prop.ToLower())
                           .ToHashSet();

            return list.Select(obj =>
            {
                var expando = new ExpandoObject() as IDictionary<string, object>;

                var props = typeof(T).GetProperties();

                foreach (var prop in props)
                {
                    if (fieldList is null || fieldList.Count == 0 || fieldList.Contains(prop.Name.ToLower()))
                    {
                        var value = prop.GetValue(obj);
                        if (value is not null)
                        {
                            var camelCaseName = char.ToLower(prop.Name[0]) + prop.Name[1..];
                            expando[camelCaseName] = value;
                        }
                    }
                }

                return expando;
            });
        }

        public static IEnumerable<IDictionary<string, object>> ToFilterString<T>(this List<T> list, string? fields = "")
        {
            return ToFilterString<T>(list.AsEnumerable(), fields);
        }
    }
}