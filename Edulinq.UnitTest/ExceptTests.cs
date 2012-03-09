using System;
using NUnit.Framework;

namespace Edulinq.UnitTests
{
    using System.Linq;

    [TestFixture]
    public class ExceptTests
    {
        [Test]
        public void NullFirstWithoutComparer()
        {
            string[] first = null;
            string[] second = { };
            Assert.Throws<ArgumentNullException>(() => first.Except(second));
        }

        [Test]
        public void NullSecondWithoutComparer()
        {
            string[] first = { };
            string[] second = null;
            Assert.Throws<ArgumentNullException>(() => first.Except(second));
        }

        [Test]
        public void NullFirstWithComparer()
        {
            string[] first = null;
            string[] second = { };
            Assert.Throws<ArgumentNullException>(() => first.Except(second, StringComparer.Ordinal));
        }

        [Test]
        public void NullSecondWithComparer()
        {
            string[] first = { };
            string[] second = null;
            Assert.Throws<ArgumentNullException>(() => first.Except(second, StringComparer.Ordinal));
        }

        [Test]
        public void NoComparerSpecified()
        {
            string[] first = { "A", "a", "b", "c", "b", "c" };
            string[] second = { "b", "a", "d", "a" };
            first.Except(second).AssertSequenceEqual("A", "c");
        }

        [Test]
        public void NullComparerSpecified()
        {
            string[] first = { "A", "a", "b", "c", "b", "c" };
            string[] second = { "b", "a", "d", "a" };
            first.Except(second, null).AssertSequenceEqual("A", "c");
        }

        [Test]
        public void CaseInsensitiveComparerSpecified()
        {
            string[] first = { "A", "a", "b", "c", "b" };
            string[] second = { "b", "a", "d", "a" };
            first.Except(second, StringComparer.OrdinalIgnoreCase).AssertSequenceEqual("c");
        }

        [Test]
        public void NoSequencesUsedBeforeIteration()
        {
            var first = new ThrowingEnumerable();
            var second = new ThrowingEnumerable();
            // No exceptions!
            var query = first.Union(second);
            // Still no exceptions... we're not calling MoveNext.
            using (var iterator = query.GetEnumerator())
            {
            }
        }

        [Test]
        public void SecondSequenceReadFullyOnFirstResultIteration()
        {
            int[] first = { 1 };
            var secondQuery = new[] { 10, 2, 0 }.Select(x => 10 / x);

            var query = first.Except(secondQuery);
            using (var iterator = query.GetEnumerator())
            {
                Assert.Throws<DivideByZeroException>(() => iterator.MoveNext());
            }
        }

        [Test]
        public void FirstSequenceOnlyReadAsResultsAreRead()
        {
            var firstQuery = new[] { 10, 2, 0, 2 }.Select(x => 10 / x);
            int[] second = { 1 };

            var query = firstQuery.Except(second);
            using (var iterator = query.GetEnumerator())
            {
                // We can get the first value with no problems
                Assert.IsTrue(iterator.MoveNext());
                Assert.AreEqual(5, iterator.Current);

                // Getting at the *second* value of the result sequence requires
                // reading from the first input sequence until the "bad" division
                Assert.Throws<DivideByZeroException>(() => iterator.MoveNext());
            }
        }
    }
}
