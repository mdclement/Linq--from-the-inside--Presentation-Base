using System;
using System.Collections.Generic;
using System.Text;

namespace Edulinq.UnitTests
{
    using System.Linq;

    /// <summary>
    /// Testing against LinqBridge, we can't use StringEx.Join(string, IEnumerable[T]) because it's
    /// in .NET 4. This is as simple an equivalent as we can easily achieve.
    /// </summary>
    public static class StringEx
    {
        public static string Join<T>(string delimiter, IEnumerable<T> source)
        {
            return string.Join(delimiter, source.Select(x => x.ToString()).ToArray());
        }
    }
}
