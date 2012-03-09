using NUnit.Framework;
using System.Collections;

namespace Edulinq.UnitTests
{
    using System.Linq;

    /// <summary>
    /// Tests for query expressions which use multiple operators
    /// (and thus don't belong in a single operator's test case).
    /// </summary>
    [TestFixture]
    public class QueryExpressionTests
    {
        [Test]
        public void WhereAndSelect()
        {
            int[] source = { 1, 3, 4, 2, 8, 1 };
            var result = from x in source
                         where x < 4
                         select x * 2;
            result.AssertSequenceEqual(2, 6, 4, 2);
        }

        [Test]
        public void Join()
        {
            int[] outer = { 5, 3, 7 };
            string[] inner = { "bee", "giraffe", "tiger", "badger", "ox", "cat", "dog" };

            var query = from x in outer
                        join y in inner on x equals y.Length
                        select x + ":" + y;
            query.AssertSequenceEqual("5:tiger", "3:bee", "3:cat", "3:dog", "7:giraffe");
        }

        [Test]
        public void GroupJoin()
        {
            int[] outer = { 5, 3, 7 };
            string[] inner = { "bee", "giraffe", "tiger", "badger", "ox", "cat", "dog" };

            var query = from x in outer
                        join y in inner on x equals y.Length into matches
                        select x + ":" + StringEx.Join(";", matches);
            query.AssertSequenceEqual("5:tiger", "3:bee;cat;dog", "7:giraffe");
        }

        [Test]
        public void GroupJoinWithDefaultIfEmpty()
        {
            int[] outer = { 5, 3, 4, 7 };
            string[] inner = { "bee", "giraffe", "tiger", "badger", "ox", "cat", "dog" };

            var query = from x in outer
                        join y in inner on x equals y.Length into matches
                        select x + ":" + StringEx.Join(";", matches.DefaultIfEmpty("null"));
            query.AssertSequenceEqual("5:tiger", "3:bee;cat;dog", "4:null", "7:giraffe");
        }

        [Test]
        public void GroupJoinWithDefaultIfEmptyAndSelectMany()
        {
            int[] outer = { 5, 3, 4, 7 };
            string[] inner = { "bee", "giraffe", "tiger", "badger", "ox", "cat", "dog" };

            var query = from x in outer
                        join y in inner on x equals y.Length into matches
                        from z in matches.DefaultIfEmpty("null")
                        select x + ":" + z;
            query.AssertSequenceEqual("5:tiger", "3:bee", "3:cat", "3:dog", "4:null", "7:giraffe");
        }

        // Equivalent to GroupByTest.GroupByWithElementProjection
        [Test]
        public void GroupBy()
        {
            string[] source = { "abc", "hello", "def", "there", "four" };
            var groups = from x in source
                         group x[0] by x.Length;

            var list = groups.ToList();
            Assert.AreEqual(3, list.Count);

            list[0].AssertSequenceEqual('a', 'd');
            Assert.AreEqual(3, list[0].Key);

            list[1].AssertSequenceEqual('h', 't');
            Assert.AreEqual(5, list[1].Key);

            list[2].AssertSequenceEqual('f');
            Assert.AreEqual(4, list[2].Key);
        }

        [Test]
        public void CastWithFrom()
        {
            IEnumerable strings = new[] { "first", "second", "third" };
            var query = from string x in strings
                        select x;
            query.AssertSequenceEqual("first", "second", "third");
        }

        [Test]
        public void CastWithJoin()
        {
            var ints = Enumerable.Range(0, 10);
            IEnumerable strings = new[] { "first", "second", "third" };
            var query = from x in ints
                        join string y in strings on x equals y.Length
                        select x + ":" + y;
            query.AssertSequenceEqual("5:first", "5:third", "6:second");
        }

    }
}
