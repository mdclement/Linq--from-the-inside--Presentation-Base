using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Edulinq.UnitTests
{
    using System.Linq;

    [TestFixture]
    public class EmptyTest
    {
        [Test]
        public void EmptyContainsNoElements()
        {
            using (var empty = System.Linq.Enumerable.Empty<int>().GetEnumerator())
            {
                Assert.IsFalse(empty.MoveNext());
            }
        }

        [Test]
        public void EmptyIsASingletonPerElementType()
        {
            Assert.AreSame(System.Linq.Enumerable.Empty<int>(), System.Linq.Enumerable.Empty<int>());
            Assert.AreSame(System.Linq.Enumerable.Empty<long>(), System.Linq.Enumerable.Empty<long>());
            Assert.AreSame(System.Linq.Enumerable.Empty<string>(), System.Linq.Enumerable.Empty<string>());
            Assert.AreSame(System.Linq.Enumerable.Empty<object>(), System.Linq.Enumerable.Empty<object>());

            Assert.AreNotSame(System.Linq.Enumerable.Empty<long>(), System.Linq.Enumerable.Empty<int>());
            Assert.AreNotSame(System.Linq.Enumerable.Empty<string>(), System.Linq.Enumerable.Empty<object>());
        }
    }
}
