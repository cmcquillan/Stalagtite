using NUnit.Framework;
using Stalagtite.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StalagtiteTests
{
    [TestFixture]
    public class ParserTests
    {
        [Test]
        public void ParserSeparatesVerb()
        {
            var parser = new TextCommandParser();
            var cmd = parser.GetCommand("say Hello");
            Assert.AreEqual("say", cmd.Verb);
        }

        [Test]
        public void ParserLowercasesVerb()
        {
            var parser = new TextCommandParser();
            var cmd = parser.GetCommand("SAY Hello");
            Assert.AreEqual("say", cmd.Verb);
        }

        [Test]
        public void ParserCreatesEmptyCommandOnEmptyText()
        {
            var parser = new TextCommandParser();
            var cmd = parser.GetCommand("");
            Assert.AreEqual(Command.Empty, cmd);
        }

        [Test]
        public void EqualityEvaluatesTrueForEmptyCommand()
        {
            var parser = new TextCommandParser();
            var cmd = parser.GetCommand("");
            Assert.IsTrue(cmd == Command.Empty);
        }

        [Test]
        public void NonEqualityEvaluatesTrueForDifferentCommands()
        {
            var parser = new TextCommandParser();
            var cmd = parser.GetCommand("say Hello");
            Assert.IsTrue(cmd != Command.Empty);
        }

        [Test]
        public void CommandHashCodeMatchesThatOfVerb()
        {
            var parser = new TextCommandParser();
            var cmd = parser.GetCommand("say Hello");
            Assert.AreEqual("say".GetHashCode(), cmd.GetHashCode());
        }

        [Test]
        public void CommandDoesNotEqualNonCommandObject()
        {
            var parser = new TextCommandParser();
            var cmd = parser.GetCommand("say Hello");
            Assert.AreNotEqual("say", cmd);
        }

        [Test]
        public void CommandEqualsEquivalentCommandWithDifferentArgs()
        {
            var parser = new TextCommandParser();
            var cmd1 = parser.GetCommand("say Hello");
            var cmd2 = parser.GetCommand("say Goodbye");
            Assert.AreEqual(cmd1, cmd2);
        }
    }
}
