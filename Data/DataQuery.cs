using System;
using System.Collections.Generic;
using System.Linq;

namespace BananaMacro.Data
{
    public static class DataQuery
    {
        public static IEnumerable<T> Where<T>(IEnumerable<T> source, Func<T, bool> predicate)
            => source.Where(predicate);

        public static IEnumerable<TResult> Select<T, TResult>(IEnumerable<T> source, Func<T, TResult> selector)
            => source.Select(selector);

        public static T? FirstOrDefault<T>(IEnumerable<T> source, Func<T, bool> predicate)
            => source.FirstOrDefault(predicate);
    }
}