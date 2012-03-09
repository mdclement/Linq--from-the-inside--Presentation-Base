using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Edulinq.UnitTests
{
    using System.Linq;

    [TestFixture]
    public class OrderByTests
    {
        [Test]
        public void ExecutionIsDeferred()
        {
            new ThrowingEnumerable().OrderBy(x => x);
        }

        [Test]
        public void NullSourceNoComparer()
        {
            int[] source = null;
            Func<int, int> keySelector = x => x;
            Assert.Throws<ArgumentNullException>(() => source.OrderBy(keySelector));
        }

        [Test]
        public void NullKeySelectorNoComparer()
        {
            int[] source = new int[0];
            Func<int, int> keySelector = null;
            Assert.Throws<ArgumentNullException>(() => source.OrderBy(keySelector));
        }

        [Test]
        public void NullSourceWithComparer()
        {
            int[] source = null;
            Func<int, int> keySelector = x => x;
            Assert.Throws<ArgumentNullException>(() => source.OrderBy(keySelector, Comparer<int>.Default));
        }

        [Test]
        public void NullKeySelectorWithComparer()
        {
            int[] source = new int[0];
            Func<int, int> keySelector = null;
            Assert.Throws<ArgumentNullException>(() => source.OrderBy(keySelector, Comparer<int>.Default));
        }

        [Test]
        public void SimpleUniqueKeys()
        {
            var source = new[]
            {
                new { Value = 1, Key = 10 },
                new { Value = 2, Key = 12 },
                new { Value = 3, Key = 11 }
            };
            var query = source.OrderBy(x => x.Key)
                              .Select(x => x.Value);
            query.AssertSequenceEqual(1, 3, 2);
        }

        [Test]
        public void NullsAreFirst()
        {
            var source = new[]
            {
                new { Value = 1, Key = "abc" },
                new { Value = 2, Key = (string) null },
                new { Value = 3, Key = "def" }
            };
            var query = source.OrderBy(x => x.Key, StringComparer.Ordinal)
                              .Select(x => x.Value);
            query.AssertSequenceEqual(2, 1, 3);
        }

        [Test]
        public void OrderingIsStable()
        {
            var source = new[]
            {
                new { Value = 1, Key = 10 },
                new { Value = 2, Key = 11 },
                new { Value = 3, Key = 11 },
                new { Value = 4, Key = 10 },
            };
            var query = source.OrderBy(x => x.Key)
                              .Select(x => x.Value);
            query.AssertSequenceEqual(1, 4, 2, 3);
        }

        [Test]
        public void NullComparerIsDefault()
        {
            var source = new[]
            {
                new { Value = 1, Key = 15 },
                new { Value = 2, Key = -13 },
                new { Value = 3, Key = 11 }
            };
            var query = source.OrderBy(x => x.Key, null)
                              .Select(x => x.Value);
            query.AssertSequenceEqual(2, 3, 1);
        }

        [Test]
        public void CustomComparer()
        {
            var source = new[]
            {
                new { Value = 1, Key = 15 },
                new { Value = 2, Key = -13 },
                new { Value = 3, Key = 11 }
            };
            var query = source.OrderBy(x => x.Key, new AbsoluteValueComparer())
                              .Select(x => x.Value);
            query.AssertSequenceEqual(3, 2, 1);
        }

        [Test]
        public void KeySelectorIsCalledExactlyOncePerElement()
        {
            int[] values = { 1, 5, 4, 2, 3, 7, 6, 8, 9 };
            int count = 0;
            var query = values.OrderBy(x => { count++; return x; });
            query.AssertSequenceEqual(1, 2, 3, 4, 5, 6, 7, 8, 9);
            Assert.AreEqual(9, count);
        }
    }
}
