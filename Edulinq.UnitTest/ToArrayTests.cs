using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Edulinq.UnitTests
{
    using System.Linq;

    [TestFixture]
    public class ToArrayTests
    {
        [Test]
        public void ResultIsIndependentOfSource()
        {
            List<string> source = new List<string> { "xyz", "abc" };
            // Make it obvious we're not calling List<T>.ToArray
            string[] result = Enumerable.ToArray(source);
            result.AssertSequenceEqual("xyz", "abc");

            // Change the source: result won't have changed
            source[0] = "xxx";
            Assert.AreEqual("xyz", result[0]);

            // And the reverse
            result[1] = "yyy";
            Assert.AreEqual("abc", source[1]);
        }

        [Test]
        public void SequenceIsEvaluatedEagerly()
        {
            int[] numbers = { 5, 3, 0 };
            var query = numbers.Select(x => 10 / x);
            Assert.Throws<DivideByZeroException>(() => query.ToArray());
        }

        [Test]
        public void NullSource()
        {
            IEnumerable<string> source = null;
            Assert.Throws<ArgumentNullException>(() => source.ToArray());
        }

        [Test]
        public void ConversionOfLazilyEvaluatedSequence()
        {
            var range = Enumerable.Range(3, 3);
            var query = range.Select(x => x * 2);
            var list = query.ToArray();
            list.AssertSequenceEqual(6, 8, 10);
        }

        [Test]
        public void ICollectionOptimization()
        {
            //var source = new NonEnumerableCollection<string> { "hello", "there" };
            //// If ToList just iterated over the list, this would throw
            //var list = source.ToArray();
            //list.AssertSequenceEqual("hello", "there");
        }
    }
}
