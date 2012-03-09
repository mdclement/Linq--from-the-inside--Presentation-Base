using System;
using NUnit.Framework;

namespace Edulinq.UnitTests
{
    using System.Linq;

    [TestFixture]
    public class DefaultIfEmptyTests
    {
        [Test]
        public void NullSourceNoDefaultValue()
        {
            int[] source = null;
            Assert.Throws<ArgumentNullException>(() => source.DefaultIfEmpty());
        }

        [Test]
        public void NullSourceWithDefaultValue()
        {
            int[] source = null;
            Assert.Throws<ArgumentNullException>(() => source.DefaultIfEmpty(5));
        }

        [Test]
        public void EmptySequenceNoDefaultValue()
        {
            Enumerable.Empty<int>().DefaultIfEmpty().AssertSequenceEqual(0);
        }

        [Test]
        public void EmptySequenceWithDefaultValue()
        {
            Enumerable.Empty<int>().DefaultIfEmpty(5).AssertSequenceEqual(5);
        }

        [Test]
        public void NonEmptySequenceNoDefaultValue()
        {
            int[] source = { 3, 1, 4 };
            source.DefaultIfEmpty().AssertSequenceEqual(source);
        }

        [Test]
        public void NonEmptySequenceWithDefaultValue()
        {
            int[] source = { 3, 1, 4 };
            source.DefaultIfEmpty(5).AssertSequenceEqual(source);
        }
    }
}
