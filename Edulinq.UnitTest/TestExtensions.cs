using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Edulinq.UnitTest
{
    public static class TestExtensions
    {
        /// <summary>
        /// Make testing even easier - a params array makes for readable tests :)
        /// The sequence is evaluated exactly once.
        /// </summary>
        public static void AssertSequenceEqual<T>(this IEnumerable<T> actual, params T[] expected)
        {
            // Working with a copy means we can look over it more than once.
            // We're safe to do that with the array anyway.
            List<T> copy = new List<T>(actual);
            Assert.AreEqual(expected.Length, copy.Count, "Expected counts to be equal");

            for (int i = 0; i < copy.Count; i++)
            {
                if (!EqualityComparer<T>.Default.Equals(expected[i], copy[i]))
                {
                    Assert.Fail("Expected sequences differ at index " + i + ": expected " + expected[i]
                        + "; was " + copy[i]);
                }
            }
        }
    }
}
