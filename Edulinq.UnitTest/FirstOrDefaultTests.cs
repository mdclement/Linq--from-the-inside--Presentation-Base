using System;
using NUnit.Framework;

namespace Edulinq.UnitTests
{
    using System.Linq;

    [TestFixture]
    public class FirstOrDefaultTests
    {
        [Test]
        public void NullSourceWithoutPredicate()
        {
            int[] source = null;
            Assert.Throws<ArgumentNullException>(() => source.FirstOrDefault());
        }

        [Test]
        public void NullSourceWithPredicate()
        {
            int[] source = null;
            Assert.Throws<ArgumentNullException>(() => source.FirstOrDefault(x => x > 3));
        }

        [Test]
        public void NullPredicate()
        {
            int[] source = { 1, 3, 5 };
            Assert.Throws<ArgumentNullException>(() => source.FirstOrDefault(null));
        }

        [Test]
        public void EmptySequenceWithoutPredicate()
        {
            int[] source = { };
            Assert.AreEqual(0, source.FirstOrDefault());
        }

        [Test]
        public void SingleElementSequenceWithoutPredicate()
        {
            int[] source = { 5 };
            Assert.AreEqual(5, source.FirstOrDefault());
        }

        [Test]
        public void MultipleElementSequenceWithoutPredicate()
        {
            int[] source = { 5, 10 };
            Assert.AreEqual(5, source.FirstOrDefault());
        }

        [Test]
        public void EmptySequenceWithPredicate()
        {
            int[] source = { };
            Assert.AreEqual(0, source.FirstOrDefault(x => x > 3));
        }

        [Test]
        public void SingleElementSequenceWithMatchingPredicate()
        {
            int[] source = { 5 };
            Assert.AreEqual(5, source.FirstOrDefault(x => x > 3));
        }

        [Test]
        public void SingleElementSequenceWithNonMatchingPredicate()
        {
            int[] source = { 2 };
            Assert.AreEqual(0, source.FirstOrDefault(x => x > 3));
        }

        [Test]
        public void MultipleElementSequenceWithNoPredicateMatches()
        {
            int[] source = { 1, 2, 2, 1 };
            Assert.AreEqual(0, source.FirstOrDefault(x => x > 3));
        }

        [Test]
        public void MultipleElementSequenceWithSinglePredicateMatch()
        {
            int[] source = { 1, 2, 5, 2, 1 };
            Assert.AreEqual(5, source.FirstOrDefault(x => x > 3));
        }

        [Test]
        public void MultipleElementSequenceWithMultiplePredicateMatches()
        {
            int[] source = { 1, 2, 5, 10, 2, 1 };
            Assert.AreEqual(5, source.FirstOrDefault(x => x > 3));
        }

        [Test]
        public void EarlyOutAfterFirstElementWithoutPredicate()
        {
            int[] source = { 15, 1, 0, 3 };
            var query = source.Select(x => 10 / x);
            // We finish before getting as far as dividing by 0
            Assert.AreEqual(0, query.FirstOrDefault());
        }

        [Test]
        public void EarlyOutAfterFirstElementWithPredicate()
        {
            int[] source = { 15, 1, 0, 3 };
            var query = source.Select(x => 10 / x);
            // We finish before getting as far as dividing by 0
            Assert.AreEqual(10, query.FirstOrDefault(y => y > 5));
        }
    }
}
