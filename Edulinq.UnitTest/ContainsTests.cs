using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Edulinq.UnitTests
{
    using System.Linq;

    [TestFixture]
    public class ContainsTests
    {
        [Test]
        public void NullSourceNoComparer()
        {
            string[] source = null;
            Assert.Throws<ArgumentNullException>(() => source.Contains("x"));
        }

        [Test]
        public void NullSourceWithComparer()
        {
            string[] source = null;
            Assert.Throws<ArgumentNullException>(() => source.Contains("x", StringComparer.Ordinal));
        }

        [Test]
        public void NoMatchNoComparer()
        {
            // Default equality comparer is ordinal
            string[] source = { "foo", "bar", "baz" };
            Assert.IsFalse(source.Contains("BAR"));
        }

        [Test]
        public void MatchNoComparer()
        {
            // Default equality comparer is ordinal
            string[] source = { "foo", "bar", "baz" };
            // Clone the string to verify it's not just using reference identity
            string barClone = new String("bar".ToCharArray());
            Assert.IsTrue(source.Contains(barClone));
        }

        [Test]
        public void NoMatchNullComparer()
        {
            // Default equality comparer is ordinal
            string[] source = { "foo", "bar", "baz" };
            Assert.IsFalse(source.Contains("BAR", null));
        }

        [Test]
        public void MatchNullComparer()
        {
            // Default equality comparer is ordinal
            string[] source = { "foo", "bar", "baz" };
            // Clone the string to verify it's not just using reference identity
            string barClone = new String("bar".ToCharArray());
            Assert.IsTrue(source.Contains(barClone, null));
        }

        [Test]
        public void NoMatchWithCustomComparer()
        {
            // Default equality comparer is ordinal
            string[] source = { "foo", "bar", "baz" };
            Assert.IsFalse(source.Contains("gronk", StringComparer.OrdinalIgnoreCase));
        }

        [Test]
        public void MatchWithCustomComparer()
        {
            // Default equality comparer is ordinal
            string[] source = { "foo", "bar", "baz" };
            Assert.IsTrue(source.Contains("BAR", StringComparer.OrdinalIgnoreCase));
        }

        [Test]
        public void ImmediateReturnWhenMatchIsFound()
        {
            int[] source = { 10, 1, 5, 0 };
            var query = source.Select(x => 10 / x);
            // If we continued past 2, we'd see a division by zero exception
            Assert.IsTrue(query.Contains(2));
        }
    }
}
