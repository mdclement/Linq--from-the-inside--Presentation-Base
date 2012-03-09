using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Edulinq.UnitTests
{
    using System.Linq;

    [TestFixture]
    public class ThenByDescendingTests
    {
        [Test]
        public void ExecutionIsDeferred()
        {
            new ThrowingEnumerable().OrderBy(x => x).ThenByDescending(x => x);
        }

        [Test]
        public void NullSourceNoComparer()
        {
            IOrderedEnumerable<int> source = null;
            Func<int, int> keySelector = x => x;
            Assert.Throws<ArgumentNullException>(() => source.ThenByDescending(keySelector));
        }

        [Test]
        public void NullKeySelectorNoComparer()
        {
            int[] source = new int[0];
            Func<int, int> keySelector = null;
            Assert.Throws<ArgumentNullException>(() => source.OrderBy(x => x).ThenByDescending(keySelector));
        }

        [Test]
        public void NullSourceWithComparer()
        {
            IOrderedEnumerable<int> source = null;
            Func<int, int> keySelector = x => x;
            Assert.Throws<ArgumentNullException>(() => source.ThenByDescending(keySelector, Comparer<int>.Default));
        }

        [Test]
        public void NullKeySelectorWithComparer()
        {
            int[] source = new int[0];
            Func<int, int> keySelector = null;
            Assert.Throws<ArgumentNullException>(() => source.OrderBy(x => x).ThenByDescending(keySelector, Comparer<int>.Default));
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
                              .ThenByDescending(x => x.SecondaryKey)
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
                              .ThenByDescending(x => x.SecondaryKey)
                              .Select(x => x.Value);
            query.AssertSequenceEqual(1, 3, 2);
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
                              .ThenByDescending(x => x.SecondaryKey)
                              .ThenByDescending(x => x.TertiaryKey)
                              .Select(x => x.Value);
            query.AssertSequenceEqual(1, 3, 4, 2);
        }

        [Test]
        public void TertiaryKeysWithMixedOrdering()
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
                              .ThenByDescending(x => x.TertiaryKey)
                              .Select(x => x.Value);
            query.AssertSequenceEqual(3, 4, 1, 2);
        }

        [Test]
        public void ThenByDescendingAfterOrderByDescending()
        {
            var source = new[]
            {
                new { Value = 1, PrimaryKey = 10, SecondaryKey = 22 },
                new { Value = 2, PrimaryKey = 12, SecondaryKey = 21 },
                new { Value = 3, PrimaryKey = 10, SecondaryKey = 20 }
            };
            var query = source.OrderByDescending(x => x.PrimaryKey)
                              .ThenByDescending(x => x.SecondaryKey)
                              .Select(x => x.Value);
            query.AssertSequenceEqual(2, 1, 3);
        }

        [Test]
        public void NullsAreLast()
        {
            var source = new[]
            {
                new { Value = 1, PrimaryKey = 1, SecondaryKey = "abc" },
                new { Value = 2, PrimaryKey = 1, SecondaryKey = (string) null },
                new { Value = 3, PrimaryKey = 1, SecondaryKey = "def" }
            };
            var query = source.OrderBy(x => x.PrimaryKey)
                              .ThenByDescending(x => x.SecondaryKey, StringComparer.Ordinal)
                              .Select(x => x.Value);
            query.AssertSequenceEqual(3, 1, 2);
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
                              .ThenByDescending(x => x.SecondaryKey)
                              .Select(x => x.Value);
            query.AssertSequenceEqual(2, 3, 1, 4);
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
                              .ThenByDescending(x => x.SecondaryKey, null)
                              .Select(x => x.Value);
            query.AssertSequenceEqual(1, 3, 2);
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
                              .ThenByDescending(x => x.SecondaryKey, new AbsoluteValueComparer())
                              .Select(x => x.Value);
            query.AssertSequenceEqual(1, 2, 3);
        }
    }
}
