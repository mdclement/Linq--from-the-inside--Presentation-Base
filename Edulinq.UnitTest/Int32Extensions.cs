using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Edulinq.UnitTest
{
    /// <summary>
    /// Simple class to let us convert an Int32 to a string using the invariant culture,
    /// without explicitly having to use CultureInfo.InvariantCulture everywhere.
    /// </summary>
    public static class Int32Extensions
    {
        public static string ToInvariantString(this int value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
    }

}
