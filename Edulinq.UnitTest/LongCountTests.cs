using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Edulinq.UnitTests
{
    using System.Linq;

    [TestFixture]
    public class LongCountTests
    {
        [Test]
        public void NonCollectionCount()
        {
            Assert.AreEqual(5, Enumerable.Range(2, 5).LongCount());
        }

        [Test]
        public void GenericOnlyCollectionCount()
        {
            Assert.AreEqual(5, new GenericOnlyCollection<int>(Enumerable.Range(2, 5)).LongCount());
        }

        [Test]
        public void SemiGenericCollectionCount()
        {
            Assert.AreEqual(5, new SemiGenericCollection(Enumerable.Range(2, 5)).LongCount());
        }

        [Test]
        public void RegularGenericCollectionCount()
        {
            Assert.AreEqual(5, new List<int>(Enumerable.Range(2, 5)).LongCount());
        }

        [Test]
        public void NullSourceThrowsArgumentNullException()
        {
            IEnumerable<int> source = null;
            Assert.Throws<ArgumentNullException>(() => source.LongCount());
        }

        [Test]
        public void PredicatedNullSourceThrowsArgumentNullException()
        {
            IEnumerable<int> source = null;
            Assert.Throws<ArgumentNullException>(() => source.LongCount(x => x == 1));
        }

        [Test]
        public void PredicatedNullPredicateThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new int[0].LongCount(null));
        }

        [Test]
        public void PredicatedCount()
        {
            // Counts even numbers within 2, 3, 4, 5, 6
            Assert.AreEqual(3, Enumerable.Range(2, 5).LongCount(x => x % 2 == 0));
        }

        [Test]
        [Ignore("Takes an enormous amount of time!")]
        public void CollectionBiggerThanMaxInt32CanBeCountedWithLongCount()
        {
            var hugeCollection = Enumerable.Range(0, int.MaxValue).Concat(Enumerable.Range(0, 1));
            Assert.AreEqual(int.MaxValue + 1L, hugeCollection.LongCount());
        }

        [Test]
        [Ignore("Takes an enormous amount of time!")]
        public void CollectionBiggerThanMaxInt32CanBeCountedWithLongCountWithPredicate()
        {
            var hugeCollection = Enumerable.Range(0, int.MaxValue).Concat(Enumerable.Range(0, 1));
            Assert.AreEqual(int.MaxValue + 1L, hugeCollection.LongCount(x => x >= 0));
        }
    }
}
