using NUnit.Framework;
using Stalagtite.IO;
using Stalagtite.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StalagtiteTests
{
    [TestFixture]
    public class ClientTests
    {
        [Test]
        public void TextReaderClientRecognizesCommands()
        {
            using (var ms = new MemoryStream())
            {
                var writer = new StreamWriter(ms);
                
                var reader = new StreamReader(ms);
                writer.WriteLine("say \"Hello\"");
                writer.Flush();
                ms.Seek(0, SeekOrigin.Begin);

                using (var client = new TextReaderClient(reader, writer, new TextCommandParser()))
                {
                    var cmd = client.NextCommand();
                    Assert.IsFalse(String.IsNullOrEmpty(cmd.Verb));
                }
            }
        }

        [Test]
        public void TextReaderClientReadsNextCommand()
        {
            using(var ms = new MemoryStream())
            {
                var writer = new StreamWriter(ms);
                var reader = new StreamReader(ms);
                writer.WriteLine("say \"Hello\"");
                writer.Flush();
                ms.Seek(0, SeekOrigin.Begin);

                using(var client = new TextReaderClient(reader, writer, new TextCommandParser()))
                {
                    Assert.AreEqual("say \"Hello\"", client.NextCommand().CmdText);
                }
            }
        }

        [Test]
        public void TextReaderClientRecognizesMultipleCommands()
        {
            using(var ms = new MemoryStream())
            {
                var writer = new StreamWriter(ms);
                var reader = new StreamReader(ms);
                writer.WriteLine("{0}{1}{2}", "say \"Hello\"", Environment.NewLine, "say \"Goodbye\"");
                writer.Flush();
                ms.Seek(0, SeekOrigin.Begin);

                using(var client = new TextReaderClient(reader, writer, new TextCommandParser()))
                {
                    Assert.AreEqual("say \"Hello\"", client.NextCommand().CmdText);
                    Assert.AreEqual("say \"Goodbye\"", client.NextCommand().CmdText);
                }
            }
        }

        [Test]
        public void TextReaderClientCollectsReceiverOutput()
        {
            using (var ms = new MemoryStream())
            {
                var writer = new StreamWriter(ms);
                var reader = new StreamReader(ms);

                using (var client = new TextReaderClient(reader, writer, new TextCommandParser()))
                {
                    client.Write("Hello, World!");
                    Task.Delay(100).Wait();

                    ms.Seek(0, SeekOrigin.Begin);
                    var b = new byte[13];
                    ms.Read(b, 0, b.Length);
                    Assert.AreEqual("Hello, World!", Encoding.Default.GetString(b));
                }
            }
        }

        [Test]
        public void TextReaderClientThrowsWhenDisposed()
        {
            using(var ms = new MemoryStream())
            {
                var writer = new StreamWriter(ms);
                var reader = new StreamReader(ms);

                var client = new TextReaderClient(reader, writer, new TextCommandParser());
                
                using(client)
                {
                    client.Write("Hello, World!");
                    Thread.Sleep(50);
                }

                Assert.Throws(
                    typeof(ObjectDisposedException), 
                    new TestDelegate(() => client.NextCommand()));
                
            }
        }
    }
}