using System;
using NUnit.Framework;

namespace Edulinq.UnitTests
{
    using System.Linq;

    [TestFixture]
    public class SingleTests
    {
        [Test]
        public void NullSourceWithoutPredicate()
        {
            int[] source = null;
            Assert.Throws<ArgumentNullException>(() => source.Single());
        }

        [Test]
        public void NullSourceWithPredicate()
        {
            int[] source = null;
            Assert.Throws<ArgumentNullException>(() => source.Single(x => x > 3));
        }

        [Test]
        public void NullPredicate()
        {
            int[] source = { 1, 3, 5 };
            Assert.Throws<ArgumentNullException>(() => source.Single(null));
        }

        [Test]
        public void EmptySequenceWithoutPredicate()
        {
            int[] source = { };
            Assert.Throws<InvalidOperationException>(() => source.Single());
        }

        [Test]
        public void SingleElementSequenceWithoutPredicate()
        {
            int[] source = { 5 };
            Assert.AreEqual(5, source.Single());
        }

        [Test]
        public void MultipleElementSequenceWithoutPredicate()
        {
            int[] source = { 5, 10 };
            Assert.Throws<InvalidOperationException>(() => source.Single());
        }

        [Test]
        public void EmptySequenceWithPredicate()
        {
            int[] source = { };
            Assert.Throws<InvalidOperationException>(() => source.Single(x => x > 3));
        }

        [Test]
        public void SingleElementSequenceWithMatchingPredicate()
        {
            int[] source = { 5 };
            Assert.AreEqual(5, source.Single(x => x > 3));
        }

        [Test]
        public void SingleElementSequenceWithNonMatchingPredicate()
        {
            int[] source = { 2 };
            Assert.Throws<InvalidOperationException>(() => source.Single(x => x > 3));
        }

        [Test]
        public void MultipleElementSequenceWithNoPredicateMatches()
        {
            int[] source = { 1, 2, 2, 1 };
            Assert.Throws<InvalidOperationException>(() => source.Single(x => x > 3));
        }

        [Test]
        public void MultipleElementSequenceWithSinglePredicateMatch()
        {
            int[] source = { 1, 2, 5, 2, 1 };
            Assert.AreEqual(5, source.Single(x => x > 3));
        }

        [Test]
        public void MultipleElementSequenceWithMultiplePredicateMatches()
        {
            int[] source = { 1, 2, 5, 10, 2, 1 };
            Assert.Throws<InvalidOperationException>(() => source.Single(x => x > 3));
        }

        [Test]
        public void EarlyOutWithoutPredicate()
        {
            int[] source = { 1, 2, 0 };
            var query = source.Select(x => 10 / x);
            // We don't get as far as the third element - we die when we see the second
            Assert.Throws<InvalidOperationException>(() => query.Single());
        }

        [Test]
        [Ignore("doesn't seem to work with Linq To Objects implementation")]
        public void EarlyOutWithPredicate()
        {
            int[] source = { 1, 2, 0 };
            var query = source.Select(x => 10 / x);
            // We don't get as far as the third element - we die when we see the second
            Assert.Throws<InvalidOperationException>(() => query.Single(x => true));
        }
    }
}
