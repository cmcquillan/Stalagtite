using NUnit.Framework;
using Stalagtite.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StalagtiteTests
{
    [TestFixture]
    public class UtilityTests
    {
        [Test]
        public void CharacterSearchFindsNewLine()
        {
            char[] testString = String.Format("{0}{1}{2}", "Hello", Environment.NewLine, "World!").ToCharArray();

            int position = CharacterSearch.FindPosition(testString, Environment.NewLine.ToCharArray());

            Assert.AreEqual(5, position);
        }

        [Test]
        public void CharacterSearchReturnsZeroIfNotFound()
        {
            char[] testString = "Hello, World!".ToCharArray();

            int position = CharacterSearch.FindPosition(testString, Environment.NewLine.ToCharArray());

            Assert.AreEqual(-1, position);
        }
    }
}
