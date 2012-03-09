using System;
using System.Collections.Generic;
using System.Text;

namespace Edulinq
{
    public static partial class Enumerable
    {
        public static bool Any<TSource>(
            this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            throw new NotImplementedException();
        }

        public static bool Any<TSource>(
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

            throw new NotImplementedException();
        }
    }
}
