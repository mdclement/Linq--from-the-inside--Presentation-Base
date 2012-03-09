using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Edulinq.UnitTests
{
    [TestFixture]
    public class OfTypeTests
    {
        [Test]
        public void NullSource()
        {
            IEnumerable source = null;
            Assert.Throws<ArgumentNullException>(() => source.OfType<string>());
        }

        [Test]
        public void ExecutionIsDeferred()
        {
            IEnumerable source = new ThrowingEnumerable();
            // No exception
            source.OfType<string>();
        }

        [Test]
        public void OriginalSourceNotReturnedForReferenceTypes()
        {
            IEnumerable strings = new List<string>();
            Assert.AreNotSame(strings, strings.OfType<string>());
        }

        [Test]
        public void OriginalSourceNotReturnedForNullableValueTypes()
        {
            IEnumerable nullableInts = new List<int?>();
            Assert.AreNotSame(nullableInts, nullableInts.OfType<int?>());
        }

        [Test]
        [Ignore("Fails in LINQ to Objects - see blog for design discussion")]
        public void OriginalSourceReturnedForSequenceOfCorrectNonNullableValueType()
        {
            IEnumerable ints = new List<int>();
            Assert.AreSame(ints, ints.OfType<int>());
        }

        [Test]
        public void SequenceWithAllValidValues()
        {
            IEnumerable strings = new object[] { "first", "second", "third" };
            strings.OfType<string>().AssertSequenceEqual("first", "second", "third");
        }

        [Test]
        public void NullsAreExcluded()
        {
            IEnumerable strings = new object[] { "first", null, "third" };
            strings.OfType<string>().AssertSequenceEqual("first", "third");
        }

        [Test]
        public void UnboxToInt32()
        {
            IEnumerable ints = new object[] { 10, 30, 50 };
            ints.OfType<int>().AssertSequenceEqual(10, 30, 50);
        }

        [Test]
        public void UnboxToNullableInt32WithNulls()
        {
            IEnumerable ints = new object[] { 10, null, 30, null, 50 };
            ints.OfType<int?>().AssertSequenceEqual(10, 30, 50);
        }

        [Test]
        public void WrongElementTypesAreIgnored()
        {
            IEnumerable objects = new object[] { "first", new object(), "third" };
            objects.OfType<string>().AssertSequenceEqual("first", "third");
            using (IEnumerator<string> iterator = objects.Cast<string>().GetEnumerator())
            {
                Assert.IsTrue(iterator.MoveNext());
                Assert.AreEqual("first", iterator.Current);
                Assert.Throws<InvalidCastException>(() => iterator.MoveNext());
            }
        }

        [Test]
        public void UnboxingWithWrongElementTypes()
        {
            IEnumerable objects = new object[] { 100L, 100, 300L };
            objects.OfType<long>().AssertSequenceEqual(100L, 300L);
        }
    }
}
