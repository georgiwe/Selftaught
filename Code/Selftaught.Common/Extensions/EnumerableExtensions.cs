namespace Selftaught.Common.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumerableExtensions
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> elems, Action<T> action)
        {
            foreach (var elem in elems) action(elem);
            return elems;
        }
    }
}
