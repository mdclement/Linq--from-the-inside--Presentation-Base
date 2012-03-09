using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Edulinq.UnitTests
{
    using System.Linq;

    [TestFixture]
    public class ElementAtOrDefaultTests
    {
        [Test]
        public void NullSource()
        {
            int[] source = null;
            Assert.Throws<ArgumentNullException>(() => source.ElementAtOrDefault(0));
        }

        [Test]
        public void NegativeIndex()
        {
            int[] source = { 90, 91, 92 };
            Assert.AreEqual(0, source.ElementAtOrDefault(-1));
        }

        [Test]
        [Ignore("LINQ to Objects doesn't test for collection separately")]
        public void OvershootIndexOnCollection()
        {
            //IEnumerable<int> source = new NonEnumerableCollection<int> { 90, 91, 92 };
            //Assert.AreEqual(0, source.ElementAtOrDefault(3));
        }

        [Test]
        public void OvershootIndexOnList()
        {
            IEnumerable<int> source = new NonEnumerableList<int> { 90, 91, 92 };
            Assert.AreEqual(0, source.ElementAtOrDefault(3));
        }

        [Test]
        public void OvershootIndexOnLazySequence()
        {
            IEnumerable<int> source = Enumerable.Range(0, 3);
            Assert.AreEqual(0, source.ElementAtOrDefault(3));
        }

        [Test]
        public void ValidIndexOnList()
        {
            IEnumerable<int> source = new NonEnumerableList<int>(100, 56, 93, 22);
            Assert.AreEqual(93, source.ElementAtOrDefault(2));
        }

        [Test]
        public void ValidIndexOnLazySequence()
        {
            IEnumerable<int> source = Enumerable.Range(10, 5);
            Assert.AreEqual(12, source.ElementAtOrDefault(2));
        }
    }
}
