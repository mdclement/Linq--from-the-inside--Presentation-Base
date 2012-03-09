using System;
using NUnit.Framework;

namespace Edulinq.UnitTests
{
    using System.Linq;

    [TestFixture]
    public class TakeTests
    {
        [Test]
        public void ExecutionIsDeferred()
        {
            new ThrowingEnumerable().Take(10);
        }

        [Test]
        public void NullSource()
        {
            string[] source = null;
            Assert.Throws<ArgumentNullException>(() => source.Take(10));
        }

        [Test]
        public void NegativeCount()
        {
            Enumerable.Range(0, 5).Take(-5).AssertSequenceEqual();
        }

        [Test]
        public void ZeroCount()
        {
            Enumerable.Range(0, 5).Take(-5).AssertSequenceEqual();
        }

        [Test]
        public void CountShorterThanSource()
        {
            Enumerable.Range(0, 5).Take(3).AssertSequenceEqual(0, 1, 2);
        }

        [Test]
        public void CountEqualToSourceLength()
        {
            Enumerable.Range(0, 5).Take(5).AssertSequenceEqual(0, 1, 2, 3, 4);
        }

        [Test]
        public void CountGreaterThanSourceLength()
        {
            Enumerable.Range(0, 5).Take(100).AssertSequenceEqual(0, 1, 2, 3, 4);
        }

        [Test]
        public void OnlyEnumerateTheGivenNumberOfElements()
        {
            int[] source = { 1, 2, 0 };
            // If we try to move onto the third element, we'll die.
            var query = source.Select(x => 10 / x);
            query.Take(2).AssertSequenceEqual(10, 5);
        }
    }
}
