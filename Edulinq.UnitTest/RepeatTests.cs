using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Edulinq.UnitTests
{
    using System.Linq;

    [TestFixture]
    public class RepeatTest
    {
        [Test]
        public void SimpleRepeat()
        {
            System.Linq.Enumerable.Repeat("foo", 3).AssertSequenceEqual("foo", "foo", "foo");
        }

        [Test]
        public void EmptyRepeat()
        {
            System.Linq.Enumerable.Repeat("foo", 0).AssertSequenceEqual();
        }

        [Test]
        public void NullElement()
        {
            System.Linq.Enumerable.Repeat<string>(null, 2).AssertSequenceEqual(null, null);
        }

        [Test]
        public void NegativeCount()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => System.Linq.Enumerable.Repeat("foo", -1));
        }
    }
}
