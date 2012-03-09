using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Edulinq.UnitTests
{
    [TestFixture]
    public class DeferredExectution
    {
        [Test]
        public void ShowDeferredExecution()
        {
            var quotes = GenerateStrings().Where(x => x.Contains("you"));
            foreach (var quote in quotes)
            {
                Console.WriteLine(quote);
            }
        }

        private IEnumerable<string> GenerateStrings()
        {
            yield return "May the Force be with you.";
            yield return "I have a bad feeling about this.";
            yield return "These are not the droids you're looking for.";
        }
    }
}
