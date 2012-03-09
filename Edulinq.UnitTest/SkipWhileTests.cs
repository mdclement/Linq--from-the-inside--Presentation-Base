using System;
using System.Linq;
using NUnit.Framework;

namespace Edulinq.UnitTests
{
    [TestFixture]
    public class SkipWhileTests
    {
        [Test]
        public void ExecutionIsDeferred()
        {
            new ThrowingEnumerable().SkipWhile(x => x > 10);
        }

        [Test]
        public void NullSourceNoIndex()
        {
            int[] source = null;
            Assert.Throws<ArgumentNullException>(() => source.SkipWhile(x => x > 10));
        }

        [Test]
        public void NullSourceUsingIndex()
        {
            int[] source = null;
            Assert.Throws<ArgumentNullException>(() => source.SkipWhile((x, index) => x > 10));
        }

        [Test]
        public void NullPredicateNoIndex()
        {
            int[] source = { 1, 2 };
            Func<int, bool> predicate = null;
            Assert.Throws<ArgumentNullException>(() => source.SkipWhile(predicate));
        }

        [Test]
        public void NullPredicateUsingIndex()
        {
            int[] source = { 1, 2 };
            Func<int, int, bool> predicate = null;
            Assert.Throws<ArgumentNullException>(() => source.SkipWhile(predicate));
        }

        [Test]
        public void PredicateFailingFirstElement()
        {
            string[] source = { "zero", "one", "two", "three", "four", "five" };
            source.SkipWhile(x => x.Length > 4).AssertSequenceEqual("zero", "one", "two", "three", "four", "five");
        }

        [Test]
        public void PredicateWithIndexFailingFirstElement()
        {
            string[] source = { "zero", "one", "two", "three", "four", "five" };
            source.SkipWhile((x, index) => index + x.Length > 4).AssertSequenceEqual("zero", "one", "two", "three", "four", "five");
        }

        [Test]
        public void PredicateMatchingSomeElements()
        {
            string[] source = { "zero", "one", "two", "three", "four", "five" };
            source.SkipWhile(x => x.Length < 5).AssertSequenceEqual("three", "four", "five");
        }

        [Test]
        public void PredicateWithIndexMatchingSomeElements()
        {
            string[] source = { "zero", "one", "two", "three", "four", "five" };
            source.SkipWhile((x, index) => x.Length > index).AssertSequenceEqual("four", "five");
        }

        [Test]
        public void PredicateMatchingAllElements()
        {
            string[] source = { "zero", "one", "two", "three", "four", "five" };
            source.SkipWhile(x => x.Length < 100).AssertSequenceEqual();
        }

        [Test]
        public void PredicateWithIndexMatchingAllElements()
        {
            string[] source = { "zero", "one", "two", "three", "four", "five" };
            source.SkipWhile((x, index) => x.Length < 100).AssertSequenceEqual();
        }
    }
}
