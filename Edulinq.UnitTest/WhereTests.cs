using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Edulinq.UnitTests
{
    using System.Linq;

    [TestFixture]
    public class WhereTests
    {
        [Test]
        public void NullSourceThrowsNullArgumentException()
        {
        }

        [Test]
        public void NullPredicateThrowsNullArgumentException()
        {
        }

        [Test]
        public void SimpleFiltering()
        {
        }

        [Test]
        public void SimpleFilteringWithQueryExpression()
        {
        }

        [Test]
        public void EmptySource()
        {
        }

        [Test]
        public void ExecutionIsDeferred()
        {
        }

        [Test]
        public void WithIndexNullSourceThrowsNullArgumentException()
        {
            IEnumerable<int> source = null;
            Assert.Throws<ArgumentNullException>(() => source.Where((x, index) => x > 5));
        }

        [Test]
        public void WithIndexNullPredicateThrowsNullArgumentException()
        {
            int[] source = { 1, 3, 7, 9, 10 };
            Func<int, int, bool> predicate = null;
            Assert.Throws<ArgumentNullException>(() => source.Where(predicate));
        }

        [Test]
        public void WithIndexSimpleFiltering()
        {
            int[] source = { 1, 3, 4, 2, 8, 1 };
            var result = source.Where((x, index) => x < index);
            result.AssertSequenceEqual(2, 1);
        }

        [Test]
        public void WithIndexEmptySource()
        {
            int[] source = new int[0];
            var result = source.Where((x, index) => x < 4);
            result.AssertSequenceEqual();
        }

        [Test]
        public void WithIndexExecutionIsDeferred()
        {
            ThrowingEnumerable.AssertDeferred(src => src.Where((x, index) => x > 0));
        }
    }
}
