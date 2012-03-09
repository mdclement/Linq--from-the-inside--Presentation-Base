using System;
using System.Collections.Generic;
using System.Text;

namespace Edulinq
{
    public static partial class Enumerable
    {
        public static TSource FirstOrDefault<TSource>(
            this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            using (IEnumerator<TSource> iterator = source.GetEnumerator())
            {
                return iterator.MoveNext() ? iterator.Current : default(TSource);
            }
        }

        public static TSource FirstOrDefault<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    return item;
                }
            }
            return default(TSource);
        }

    }
}
