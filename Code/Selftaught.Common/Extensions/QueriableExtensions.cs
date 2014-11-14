namespace Selftaught.Common.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    public static class QueriableExtensions
    {
        public static SortedSet<T> ToSortedSet<T>(this IQueryable<T> elems)
        {
            var set = new SortedSet<T>();
            foreach (var elem in elems) set.Add(elem);
            return set;
        }
    }
}
