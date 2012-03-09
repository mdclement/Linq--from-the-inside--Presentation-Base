using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Edulinq.UnitTests
{
    using System.Linq;

    [TestFixture]
    public class CountTests
    {
        [Test]
        public void NonCollectionCount()
        {
            Assert.AreEqual(5, Enumerable.Range(2, 5).Count());
        }

        [Test]
        public void GenericOnlyCollectionCount()
        {
            Assert.AreEqual(5, new GenericOnlyCollection<int>(Enumerable.Range(2, 5)).Count());
        }

        [Test]
        public void SemiGenericCollectionCount()
        {
            Assert.AreEqual(5, new SemiGenericCollection(Enumerable.Range(2, 5)).Count());
        }

        [Test]
        public void RegularGenericCollectionCount()
        {
            Assert.AreEqual(5, new List<int>(Enumerable.Range(2, 5)).Count());
        }

        [Test]
        public void NullSourceThrowsArgumentNullException()
        {
            IEnumerable<int> source = null;
            Assert.Throws<ArgumentNullException>(() => source.Count());
        }

        [Test]
        public void PredicatedNullSourceThrowsArgumentNullException()
        {
            IEnumerable<int> source = null;
            Assert.Throws<ArgumentNullException>(() => source.Count(x => x == 1));
        }

        [Test]
        public void PredicatedNullPredicateThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new int[0].Count(null));
        }

        [Test]
        public void PredicatedCount()
        {
            // Counts even numbers within 2, 3, 4, 5, 6
            Assert.AreEqual(3, Enumerable.Range(2, 5).Count(x => x % 2 == 0));
        }

        [Test]
        [Ignore("Takes an enormous amount of time!")]
        public void Overflow()
        {
            var largeSequence = Enumerable.Range(0, int.MaxValue)
                                          .Concat(Enumerable.Range(0, 1));
            Assert.Throws<OverflowException>(() => largeSequence.Count());
        }

        [Test]
        [Ignore("Takes an enormous amount of time!")]
        public void OverflowWithPredicate()
        {
            var largeSequence = Enumerable.Range(0, int.MaxValue)
                                          .Concat(Enumerable.Range(0, 1));
            Assert.Throws<OverflowException>(() => largeSequence.Count(x => x >= 0));
        }
    }
}
