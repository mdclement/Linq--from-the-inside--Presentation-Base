using System;
using NUnit.Framework;

namespace Edulinq.UnitTests
{
    using System.Linq;

    [TestFixture]
    public class AnyTests
    {
        [Test]
        public void NullSourceWithoutPredicate()
        {
            int[] src = null;
            Assert.Throws<ArgumentNullException>(() => src.Any());
        }

        [Test]
        public void NullSourceWithPredicate()
        {
            int[] src = null;
            Assert.Throws<ArgumentNullException>(() => src.Any(x => x > 10));
        }

        [Test]
        public void NullPredicate()
        {
            int[] src = { 1, 3, 5 };
            Assert.Throws<ArgumentNullException>(() => src.Any(null));
        }

        [Test]
        public void EmptySequenceWithoutPredicate()
        {
            Assert.IsFalse(new int[0].Any());
        }

        [Test]
        public void EmptySequenceWithPredicate()
        {
            Assert.IsFalse(new int[0].Any(x => x > 10));
        }

        [Test]
        public void NonEmptySequenceWithoutPredicate()
        {
            Assert.IsTrue(new int[1].Any());
        }

        [Test]
        public void NonEmptySequenceWithPredicateMatchingElement()
        {
            int[] src = { 1, 5, 20, 30 };
            Assert.IsTrue(src.Any(x => x > 10));
        }

        [Test]
        public void NonEmptySequenceWithPredicateNotMatchingElement()
        {
            int[] src = { 1, 5, 8, 9 };
            Assert.IsFalse(src.Any(x => x > 10));
        }

        [Test]
        public void SequenceIsNotEvaluatedAfterFirstMatch()
        {
            int[] src = { 10, 2, 0, 3 };
            var query = src.Select(x => 10 / x);
            // This will finish at the second element (x = 2, so 10/x = 5)
            // It won't evaluate 10/0, which would throw an exception
            Assert.IsTrue(query.Any(y => y > 2));
        }
    }
}
