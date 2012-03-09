using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Edulinq.UnitTests
{
    /// <summary>
    /// Implementation of IComparer[int] which simply compares absolute values.
    /// </summary>
    public sealed class AbsoluteValueComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return Math.Abs(x).CompareTo(Math.Abs(y));
        }
    }
}
