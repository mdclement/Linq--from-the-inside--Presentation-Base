using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Edulinq.UnitTest
{
    using System.Linq;

    [TestFixture]
    public class EmptyTest
    {
        [Test]
        public void EmptyContainsNoElements()
        {
            using (var empty = Enumerable.Empty<int>().GetEnumerator())
            {
                Assert.IsFalse(empty.MoveNext());
            }
        }

        [Test]
        public void EmptyIsASingletonPerElementType()
        {
            Assert.AreSame(Enumerable.Empty<int>(), Enumerable.Empty<int>());
            Assert.AreSame(Enumerable.Empty<long>(), Enumerable.Empty<long>());
            Assert.AreSame(Enumerable.Empty<string>(), Enumerable.Empty<string>());
            Assert.AreSame(Enumerable.Empty<object>(), Enumerable.Empty<object>());

            Assert.AreNotSame(Enumerable.Empty<long>(), Enumerable.Empty<int>());
            Assert.AreNotSame(Enumerable.Empty<string>(), Enumerable.Empty<object>());
        }
    }
}
