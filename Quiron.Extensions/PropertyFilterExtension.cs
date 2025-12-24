using System.Dynamic;

namespace Quiron.Extensions
{
    public static class PropertyFilterExtension
    {
        public static IEnumerable<IDictionary<string, object>> ToFilterString<T>(this IEnumerable<T> list
            , string? fields = "")
        {
            if (list is null || !list.Any())
                return [];

            var fieldTree = BuildFieldTree(fields);

            return list.Select(obj =>
            {
                var expando = new ExpandoObject() as IDictionary<string, object>;
                FillObject(expando!, obj!, fieldTree);

                return expando!;
            });
        }

        public static IEnumerable<IDictionary<string, object>> ToFilterString<T>(this List<T> list
            , string? fields = "")
        {
            return ToFilterString<T>(list.AsEnumerable(), fields);
        }

        private static Dictionary<string, object?> BuildFieldTree(string? fields)
        {
            var tree = new Dictionary<string, object?>();

            if (string.IsNullOrWhiteSpace(fields))
                return tree;

            foreach (var field in fields.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            {
                var parts = field.Split('.');
                var current = tree;

                foreach (var part in parts)
                {
                    var key = part.ToLower();

                    if (!current.ContainsKey(key))
                        current[key] = new Dictionary<string, object?>();

                    current = (Dictionary<string, object?>)current[key]!;
                }
            }

            return tree;
        }

        private static void FillObject(IDictionary<string, object> target,object source
            , Dictionary<string, object?> fieldTree)
        {
            var props = source.GetType().GetProperties();

            foreach (var prop in props)
            {
                var propKey = prop.Name.ToLower();

                if (fieldTree.Count > 0 && !fieldTree.ContainsKey(propKey))
                    continue;

                var value = prop.GetValue(source);
                if (value is null)
                    continue;

                var camelCaseName = char.ToLower(prop.Name[0]) + prop.Name[1..];
                var subTree = fieldTree[propKey] as Dictionary<string, object?>;

                if (IsSimpleType(prop.PropertyType))
                {
                    target[camelCaseName] = value;
                    continue;
                }

                if (IsEnumerable(prop.PropertyType))
                {
                    var list = new List<object>();

                    foreach (var item in (System.Collections.IEnumerable)value)
                    {
                        var nested = new ExpandoObject() as IDictionary<string, object>;
                        FillObject(nested!, item!, subTree ?? new());
                        list.Add(nested!);
                    }

                    target[camelCaseName] = list;
                    continue;
                }

                var nestedObject = new ExpandoObject() as IDictionary<string, object>;
            
                FillObject(nestedObject!, value, subTree ?? []);
                target[camelCaseName] = nestedObject!;
            }
        }

        private static bool IsEnumerable(Type type)
        {
            if (type == typeof(string))
                return false;

            return typeof(System.Collections.IEnumerable).IsAssignableFrom(type);
        }

        private static bool IsSimpleType(Type type)
        {
            type = Nullable.GetUnderlyingType(type) ?? type;

            return type.IsPrimitive
                || type.IsEnum
                || type == typeof(int)
                || type == typeof(long)
                || type == typeof(string)
                || type == typeof(DateTime)
                || type == typeof(DateOnly)
                || type == typeof(TimeOnly)
                || type == typeof(decimal)
                || type == typeof(double)
                || type == typeof(Guid);
        }
    }
}