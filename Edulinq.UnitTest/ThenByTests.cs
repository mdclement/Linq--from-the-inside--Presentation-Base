using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Edulinq.UnitTests
{
    using System.Linq;

    [TestFixture]
    public class ThenByTests
    {
        [Test]
        public void ExecutionIsDeferred()
        {
            new ThrowingEnumerable().OrderBy(x => x).ThenBy(x => x);
        }

        [Test]
        public void NullSourceNoComparer()
        {
            IOrderedEnumerable<int> source = null;
            Func<int, int> keySelector = x => x;
            Assert.Throws<ArgumentNullException>(() => source.ThenBy(keySelector));
        }

        [Test]
        public void NullKeySelectorNoComparer()
        {
            int[] source = new int[0];
            Func<int, int> keySelector = null;
            Assert.Throws<ArgumentNullException>(() => source.OrderBy(x => x).ThenBy(keySelector));
        }

        [Test]
        public void NullSourceWithComparer()
        {
            IOrderedEnumerable<int> source = null;
            Func<int, int> keySelector = x => x;
            Assert.Throws<ArgumentNullException>(() => source.ThenBy(keySelector, Comparer<int>.Default));
        }

        [Test]
        public void NullKeySelectorWithComparer()
        {
            int[] source = new int[0];
            Func<int, int> keySelector = null;
            Assert.Throws<ArgumentNullException>(() => source.OrderBy(x => x).ThenBy(keySelector, Comparer<int>.Default));
        }

        [Test]
        public void PrimaryOrderingTakesPrecedence()
        {
            var source = new[]
            {
                new { Value = 1, PrimaryKey = 10, SecondaryKey = 20 },
                new { Value = 2, PrimaryKey = 12, SecondaryKey = 21 },
                new { Value = 3, PrimaryKey = 11, SecondaryKey = 22 }
            };
            var query = source.OrderBy(x => x.PrimaryKey)
                              .ThenBy(x => x.SecondaryKey)
                              .Select(x => x.Value);
            query.AssertSequenceEqual(1, 3, 2);
        }

        [Test]
        public void SecondOrderingIsUsedWhenPrimariesAreEqual()
        {
            var source = new[]
            {
                new { Value = 1, PrimaryKey = 10, SecondaryKey = 22 },
                new { Value = 2, PrimaryKey = 12, SecondaryKey = 21 },
                new { Value = 3, PrimaryKey = 10, SecondaryKey = 20 }
            };
            var query = source.OrderBy(x => x.PrimaryKey)
                              .ThenBy(x => x.SecondaryKey)
                              .Select(x => x.Value);
            query.AssertSequenceEqual(3, 1, 2);
        }

        [Test]
        public void TertiaryKeys()
        {
            var source = new[]
            {
                new { Value = 1, PrimaryKey = 10, SecondaryKey = 22, TertiaryKey = 30 },
                new { Value = 2, PrimaryKey = 12, SecondaryKey = 21, TertiaryKey = 31, },
                new { Value = 3, PrimaryKey = 10, SecondaryKey = 20, TertiaryKey = 33 },
                new { Value = 4, PrimaryKey = 10, SecondaryKey = 20, TertiaryKey = 32 }
            };
            var query = source.OrderBy(x => x.PrimaryKey)
                              .ThenBy(x => x.SecondaryKey)
                              .ThenBy(x => x.TertiaryKey)
                              .Select(x => x.Value);
            query.AssertSequenceEqual(4, 3, 1, 2);
        }

        [Test]
        public void ThenByAfterOrderByDescending()
        {
            var source = new[]
            {
                new { Value = 1, PrimaryKey = 10, SecondaryKey = 22 },
                new { Value = 2, PrimaryKey = 12, SecondaryKey = 21 },
                new { Value = 3, PrimaryKey = 10, SecondaryKey = 20 }
            };
            var query = source.OrderByDescending(x => x.PrimaryKey)
                              .ThenBy(x => x.SecondaryKey)
                              .Select(x => x.Value);
            query.AssertSequenceEqual(2, 3, 1);
        }

        [Test]
        public void NullsAreFirst()
        {
            var source = new[]
            {
                new { Value = 1, PrimaryKey = 1, SecondaryKey = "abc" },
                new { Value = 2, PrimaryKey = 1, SecondaryKey = (string) null },
                new { Value = 3, PrimaryKey = 1, SecondaryKey = "def" }
            };
            var query = source.OrderBy(x => x.PrimaryKey)
                              .ThenBy(x => x.SecondaryKey, StringComparer.Ordinal)
                              .Select(x => x.Value);
            query.AssertSequenceEqual(2, 1, 3);
        }

        [Test]
        public void OrderingIsStable()
        {
            var source = new[]
            {
                new { Value = 1, PrimaryKey = 1, SecondaryKey = 10 },
                new { Value = 2, PrimaryKey = 1, SecondaryKey = 11 },
                new { Value = 3, PrimaryKey = 1, SecondaryKey = 11 },
                new { Value = 4, PrimaryKey = 1, SecondaryKey = 10 },
            };
            var query = source.OrderBy(x => x.PrimaryKey)
                              .ThenBy(x => x.SecondaryKey)
                              .Select(x => x.Value);
            query.AssertSequenceEqual(1, 4, 2, 3);
        }

        [Test]
        public void NullComparerIsDefault()
        {
            var source = new[]
            {
                new { Value = 1, PrimaryKey = 1, SecondaryKey = 15 },
                new { Value = 2, PrimaryKey = 1, SecondaryKey = -13 },
                new { Value = 3, PrimaryKey = 1, SecondaryKey = 11 }
            };
            var query = source.OrderBy(x => x.PrimaryKey)
                              .ThenBy(x => x.SecondaryKey, null)
                              .Select(x => x.Value);
            query.AssertSequenceEqual(2, 3, 1);
        }

        [Test]
        public void CustomComparer()
        {
            var source = new[]
            {
                new { Value = 1, PrimaryKey = 1, SecondaryKey = 15 },
                new { Value = 2, PrimaryKey = 1, SecondaryKey = -13 },
                new { Value = 3, PrimaryKey = 1, SecondaryKey = 11 }
            };
            var query = source.OrderBy(x => x.PrimaryKey)
                              .ThenBy(x => x.SecondaryKey, new AbsoluteValueComparer())
                              .Select(x => x.Value);
            query.AssertSequenceEqual(3, 2, 1);
        }
    }
}
