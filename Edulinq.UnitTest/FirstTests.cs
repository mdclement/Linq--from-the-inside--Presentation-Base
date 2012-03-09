using System;
using NUnit.Framework;

namespace Edulinq.UnitTests
{
    using System.Linq;

    [TestFixture]
    public class FirstTests
    {
        [Test]
        public void NullSourceWithoutPredicate()
        {
            int[] source = null;
            Assert.Throws<ArgumentNullException>(() => source.First());
        }

        [Test]
        public void NullSourceWithPredicate()
        {
            int[] source = null;
            Assert.Throws<ArgumentNullException>(() => source.First(x => x > 3));
        }

        [Test]
        public void NullPredicate()
        {
            int[] source = { 1, 3, 5 };
            Assert.Throws<ArgumentNullException>(() => source.First(null));
        }

        [Test]
        public void EmptySequenceWithoutPredicate()
        {
            int[] source = { };
            Assert.Throws<InvalidOperationException>(() => source.First());
        }

        [Test]
        public void SingleElementSequenceWithoutPredicate()
        {
            int[] source = { 5 };
            Assert.AreEqual(5, source.First());
        }

        [Test]
        public void MultipleElementSequenceWithoutPredicate()
        {
            int[] source = { 5, 10 };
            Assert.AreEqual(5, source.First());
        }

        [Test]
        public void EmptySequenceWithPredicate()
        {
            int[] source = { };
            Assert.Throws<InvalidOperationException>(() => source.First(x => x > 3));
        }

        [Test]
        public void SingleElementSequenceWithMatchingPredicate()
        {
            int[] source = { 5 };
            Assert.AreEqual(5, source.First(x => x > 3));
        }

        [Test]
        public void SingleElementSequenceWithNonMatchingPredicate()
        {
            int[] source = { 2 };
            Assert.Throws<InvalidOperationException>(() => source.First(x => x > 3));
        }

        [Test]
        public void MultipleElementSequenceWithNoPredicateMatches()
        {
            int[] source = { 1, 2, 2, 1 };
            Assert.Throws<InvalidOperationException>(() => source.First(x => x > 3));
        }

        [Test]
        public void MultipleElementSequenceWithSinglePredicateMatch()
        {
            int[] source = { 1, 2, 5, 2, 1 };
            Assert.AreEqual(5, source.First(x => x > 3));
        }

        [Test]
        public void MultipleElementSequenceWithMultiplePredicateMatches()
        {
            int[] source = { 1, 2, 5, 10, 2, 1 };
            Assert.AreEqual(5, source.First(x => x > 3));
        }

        [Test]
        public void EarlyOutAfterFirstElementWithoutPredicate()
        {
            int[] source = { 15, 1, 0, 3 };
            var query = source.Select(x => 10 / x);
            // We finish before getting as far as dividing by 0
            Assert.AreEqual(0, query.First());
        }

        [Test]
        public void EarlyOutAfterFirstElementWithPredicate()
        {
            int[] source = { 15, 1, 0, 3 };
            var query = source.Select(x => 10 / x);
            // We finish before getting as far as dividing by 0
            Assert.AreEqual(10, query.First(y => y > 5));
        }
    }
}
