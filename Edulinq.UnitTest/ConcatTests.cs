using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Edulinq.UnitTests
{
    using System.Linq;

    [TestFixture]
    public class ConcatTests
    {
        [Test]
        public void SimpleConcatenation()
        {
            IEnumerable<string> first = new string[] { "a", "b" };
            IEnumerable<string> second = new string[] { "c", "d" };
            first.Concat(second).AssertSequenceEqual("a", "b", "c", "d");
        }

        [Test]
        public void NullFirstThrowsNullArgumentException()
        {
            IEnumerable<string> first = null;
            IEnumerable<string> second = new string[] { "hello" };
            Assert.Throws<ArgumentNullException>(() => first.Concat(second));
        }

        [Test]
        public void NullSecondThrowsNullArgumentException()
        {
            IEnumerable<string> first = new string[] { "hello" };
            IEnumerable<string> second = null;
            Assert.Throws<ArgumentNullException>(() => first.Concat(second));
        }

        [Test]
        public void FirstSequenceIsntAccessedBeforeFirstUse()
        {
            IEnumerable<int> first = new ThrowingEnumerable();
            IEnumerable<int> second = new int[] { 5 };
            // No exception yet...
            var query = first.Concat(second);
            // Still no exception...
            using (var iterator = query.GetEnumerator())
            {
                // Now it will go bang
                Assert.Throws<InvalidOperationException>(() => iterator.MoveNext());
            }
        }

        [Test]
        public void SecondSequenceIsntAccessedBeforeFirstUse()
        {
            IEnumerable<int> first = new int[] { 5 };
            IEnumerable<int> second = new ThrowingEnumerable();
            // No exception yet...
            var query = first.Concat(second);
            // Still no exception...
            using (var iterator = query.GetEnumerator())
            {
                // First element is fine...
                Assert.IsTrue(iterator.MoveNext());
                Assert.AreEqual(5, iterator.Current);
                // Now it will go bang, as we move into the second sequence
                Assert.Throws<InvalidOperationException>(() => iterator.MoveNext());
            }
        }
    }
}
