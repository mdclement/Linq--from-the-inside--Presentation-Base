using System;
using NUnit.Framework;

namespace Edulinq.UnitTests
{
    using System.Linq;

    [TestFixture]
    public class ZipTest
    {
        [Test]
        public void NullFirst()
        {
            string[] first = null;
            int[] second = { };
            Func<string, int, string> resultSelector = (x, y) => x + ":" + y;
            Assert.Throws<ArgumentNullException>(() => first.Zip(second, resultSelector));
        }

        [Test]
        public void NullSecond()
        {
            string[] first = { };
            int[] second = null;
            Func<string, int, string> resultSelector = (x, y) => x + ":" + y;
            Assert.Throws<ArgumentNullException>(() => first.Zip(second, resultSelector));
        }

        [Test]
        public void NullResultSelector()
        {
            string[] first = { };
            int[] second = { };
            Func<string, int, string> resultSelector = null;
            Assert.Throws<ArgumentNullException>(() => first.Zip(second, resultSelector));
        }

        [Test]
        public void ExecutionIsDeferred()
        {
            var first = new ThrowingEnumerable();
            var second = new ThrowingEnumerable();
            first.Zip(second, (x, y) => x + y);
        }

        [Test]
        public void ShortFirst()
        {
            string[] first = { "a", "b", "c" };
            var second = Enumerable.Range(5, 10);
            Func<string, int, string> resultSelector = (x, y) => x + ":" + y;
            var query = first.Zip(second, resultSelector);
            query.AssertSequenceEqual("a:5", "b:6", "c:7");
        }

        [Test]
        public void ShortSecond()
        {
            string[] first = { "a", "b", "c", "d", "e" };
            var second = Enumerable.Range(5, 3);
            Func<string, int, string> resultSelector = (x, y) => x + ":" + y;
            var query = first.Zip(second, resultSelector);
            query.AssertSequenceEqual("a:5", "b:6", "c:7");
        }

        [Test]
        public void EqualLengthSequences()
        {
            string[] first = { "a", "b", "c" };
            var second = Enumerable.Range(5, 3);
            Func<string, int, string> resultSelector = (x, y) => x + ":" + y;
            var query = first.Zip(second, resultSelector);
            query.AssertSequenceEqual("a:5", "b:6", "c:7");
        }

        [Test]
        public void AdjacentElements()
        {
            string[] elements = { "a", "b", "c", "d", "e" };
            var query = elements.Zip(elements.Skip(1), (x, y) => x + y);
            query.AssertSequenceEqual("ab", "bc", "cd", "de");
        }
    }
}
