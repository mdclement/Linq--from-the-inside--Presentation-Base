using System;
using System.Linq;
using NUnit.Framework;

namespace Edulinq.UnitTests
{
    [TestFixture]
    public class TakeWhileTests
    {
        [Test]
        public void ExecutionIsDeferred()
        {
            new ThrowingEnumerable().TakeWhile(x => x > 10);
        }

        [Test]
        public void NullSourceNoIndex()
        {
            int[] source = null;
            Assert.Throws<ArgumentNullException>(() => source.TakeWhile(x => x > 10));
        }

        [Test]
        public void NullSourceUsingIndex()
        {
            int[] source = null;
            Assert.Throws<ArgumentNullException>(() => source.TakeWhile((x, index) => x > 10));
        }

        [Test]
        public void NullPredicateNoIndex()
        {
            int[] source = { 1, 2 };
            Func<int, bool> predicate = null;
            Assert.Throws<ArgumentNullException>(() => source.TakeWhile(predicate));
        }

        [Test]
        public void NullPredicateUsingIndex()
        {
            int[] source = { 1, 2 };
            Func<int, int, bool> predicate = null;
            Assert.Throws<ArgumentNullException>(() => source.TakeWhile(predicate));
        }

        [Test]
        public void PredicateFailingFirstElement()
        {
            string[] source = { "zero", "one", "two", "three", "four", "five", "six" };
            source.TakeWhile(x => x.Length > 4).AssertSequenceEqual();
        }

        [Test]
        public void PredicateWithIndexFailingFirstElement()
        {
            string[] source = { "zero", "one", "two", "three", "four", "five" };
            source.TakeWhile((x, index) => index + x.Length > 4).AssertSequenceEqual();
        }

        [Test]
        public void PredicateMatchingSomeElements()
        {
            string[] source = { "zero", "one", "two", "three", "four", "five" };
            source.TakeWhile(x => x.Length < 5).AssertSequenceEqual("zero", "one", "two");
        }

        [Test]
        public void PredicateWithIndexMatchingSomeElements()
        {
            string[] source = { "zero", "one", "two", "three", "four", "five" };
            source.TakeWhile((x, index) => x.Length > index).AssertSequenceEqual("zero", "one", "two", "three");
        }

        [Test]
        public void PredicateMatchingAllElements()
        {
            string[] source = { "zero", "one", "two", "three", "four", "five" };
            source.TakeWhile(x => x.Length < 100).AssertSequenceEqual("zero", "one", "two", "three", "four", "five");
        }

        [Test]
        public void PredicateWithIndexMatchingAllElements()
        {
            string[] source = { "zero", "one", "two", "three", "four", "five" };
            source.TakeWhile((x, index) => x.Length < 100).AssertSequenceEqual("zero", "one", "two", "three", "four", "five");
        }
    }
}
