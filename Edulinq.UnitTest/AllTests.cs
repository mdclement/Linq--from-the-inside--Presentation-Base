using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Edulinq.UnitTests
{
    using System.Linq;

    [TestFixture]
    public class AllTest
    {
        [Test]
        public void NullSource()
        {
            int[] src = null;
            Assert.Throws<ArgumentNullException>(() => src.All(x => x > 10));
        }

        [Test]
        public void NullPredicate()
        {
            int[] src = { 1, 3, 5 };
            Assert.Throws<ArgumentNullException>(() => src.All(null));
        }

        [Test]
        public void EmptySequenceReturnsTrue()
        {
            Assert.IsTrue(new int[0].All(x => x > 0));
        }

        [Test]
        public void PredicateMatchingNoElements()
        {
            int[] src = { 1, 5, 20, 30 };
            Assert.IsFalse(src.All(x => x < 0));
        }

        [Test]
        public void PredicateMatchingSomeElements()
        {
            int[] src = { 1, 5, 8, 9 };
            Assert.IsFalse(src.All(x => x > 3));
        }

        [Test]
        public void PredicateMatchingAllElements()
        {
            int[] src = { 1, 5, 8, 9 };
            Assert.IsTrue(src.All(x => x > 0));
        }

        [Test]
        public void SequenceIsNotEvaluatedAfterFirstNonMatch()
        {
            int[] src = { 2, 10, 0, 3 };
            var query = src.Select(x => 10 / x);
            // This will finish at the second element (x = 10, so 10/x = 1)
            // It won't evaluate 10/0, which would throw an exception
            Assert.IsFalse(query.All(y => y > 2));
        }
    }
}
