using System;
using NUnit.Framework;

namespace Edulinq.UnitTests
{
    using System.Linq;

    [TestFixture]
    public class SkipTests
    {
        [Test]
        public void ExecutionIsDeferred()
        {
            new ThrowingEnumerable().Skip(10);
        }

        [Test]
        public void NullSource()
        {
            string[] source = null;
            Assert.Throws<ArgumentNullException>(() => source.Skip(10));
        }

        [Test]
        public void NegativeCount()
        {
            Enumerable.Range(0, 5).Skip(-5).AssertSequenceEqual(0, 1, 2, 3, 4);
        }

        [Test]
        public void ZeroCount()
        {
            Enumerable.Range(0, 5).Skip(0).AssertSequenceEqual(0, 1, 2, 3, 4);
        }

        [Test]
        public void NegativeCountWithArray()
        {
            new int[] { 0, 1, 2, 3, 4 }.Skip(-5).AssertSequenceEqual(0, 1, 2, 3, 4);
        }

        [Test]
        public void ZeroCountWithArray()
        {
            new int[] { 0, 1, 2, 3, 4 }.Skip(0).AssertSequenceEqual(0, 1, 2, 3, 4);
        }

        [Test]
        public void CountShorterThanSource()
        {
            Enumerable.Range(0, 5).Skip(3).AssertSequenceEqual(3, 4);
        }

        [Test]
        public void CountEqualToSourceLength()
        {
            Enumerable.Range(0, 5).Skip(5).AssertSequenceEqual();
        }

        [Test]
        public void CountGreaterThanSourceLength()
        {
            Enumerable.Range(0, 5).Skip(100).AssertSequenceEqual();
        }
    }
}
