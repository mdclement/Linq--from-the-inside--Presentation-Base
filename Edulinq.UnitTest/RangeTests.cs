using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Edulinq.UnitTests
{
    using System.Linq;

    [TestFixture]
    public class RangeTests
    {
        [Test]
        public void NegativeCount()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => System.Linq.Enumerable.Range(10, -1));
        }

        [Test]
        public void CountTooLarge()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => System.Linq.Enumerable.Range(int.MaxValue, 2));
            Assert.Throws<ArgumentOutOfRangeException>(() => System.Linq.Enumerable.Range(2, int.MaxValue));
            // int.MaxValue is odd, hence the +3 instead of +2
            Assert.Throws<ArgumentOutOfRangeException>(() => System.Linq.Enumerable.Range(int.MaxValue / 2, (int.MaxValue / 2) + 3));
        }

        [Test]
        public void LargeButValidCount()
        {
            // Essentially the edge conditions for CountTooLarge, but just below the boundary
            System.Linq.Enumerable.Range(int.MaxValue, 1);
            System.Linq.Enumerable.Range(1, int.MaxValue);
            System.Linq.Enumerable.Range(int.MaxValue / 2, (int.MaxValue / 2) + 2);
        }

        [Test]
        public void ValidRange()
        {
            System.Linq.Enumerable.Range(5, 3).AssertSequenceEqual(5, 6, 7);
        }

        [Test]
        public void NegativeStart()
        {
            System.Linq.Enumerable.Range(-2, 5).AssertSequenceEqual(-2, -1, 0, 1, 2);
        }

        [Test]
        public void EmptyRange()
        {
            System.Linq.Enumerable.Range(100, 0).AssertSequenceEqual();
        }

        [Test]
        public void SingleValueOfMaxInt32()
        {
            System.Linq.Enumerable.Range(int.MaxValue, 1).AssertSequenceEqual(int.MaxValue);
        }

        [Test]
        public void EmptyRangeStartingAtMinInt32()
        {
            System.Linq.Enumerable.Range(int.MinValue, 0).AssertSequenceEqual();
        }
    }
}
