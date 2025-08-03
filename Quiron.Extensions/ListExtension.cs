namespace Quiron.Extensions
{
    public static class ListExtension
    {
        public static ICollection<T>? ToCollection<T>(this IEnumerable<T>? collections)
        {
            ICollection<T> returnList = [];
            if (collections is not null && collections.Any())
                collections.ToList().ForEach(orig => returnList.Add(orig));

            return returnList;
        }

        public static bool IsNotNull<T>(this ICollection<T> collections)
        {
            return (collections is not null && collections.Count > 0);
        }

        public static bool IsNotNullIn<T>(this ICollection<T>? collections)
        {
            return (collections is not null && collections.Count > 0);
        }

        public static bool IsNotNull<T>(this List<T> collections)
        {
            return (collections is not null && collections.Count > 0);
        }

        public static bool IsNotNullIn<T>(this List<T>? collections)
        {
            return (collections is not null && collections.Count > 0);
        }

        public static bool IsNotNull<T>(this IEnumerable<T> collections)
        {
            return (collections is not null && collections.Any());
        }

        public static bool IsNotNullIn<T>(this IEnumerable<T>? collections)
        {
            return (collections is not null && collections.Any());
        }

        public static bool IsNullOrZero<T>(this ICollection<T> collections)
        {
            return (collections is null || collections.Count == 0);
        }

        public static bool IsNullOrZeroIn<T>(this ICollection<T>? collections)
        {
            return (collections is null || collections.Count == 0);
        }

        public static bool IsNullOrZero<T>(this List<T> collections)
        {
            return (collections is null || collections.Count == 0);
        }

        public static bool IsNullOrZeroIn<T>(this List<T>? collections)
        {
            return (collections is null || collections.Count == 0);
        }

        public static bool IsNullOrZero<T>(this IEnumerable<T> collections)
        {
            return (collections is null || !collections.Any());
        }

        public static bool IsNullOrZeroIn<T>(this IEnumerable<T>? collections)
        {
            return (collections is null || !collections.Any());
        }

        public static void Clean<T>(this ICollection<T> collections)
        {
            if (collections.IsNotNull())
                collections.Clear();
        }

        public static void Clean<T>(this List<T> collections)
        {
            if (collections.IsNotNull())
                collections!.Clear();
        }

        public static void Clean<T>(this IEnumerable<T> collections)
        {
            collections.ToList()
                .Clear();
        }
    }
}