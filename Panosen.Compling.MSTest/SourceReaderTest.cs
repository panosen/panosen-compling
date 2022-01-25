using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Panosen.Compling.MSTest
{
    [TestClass]
    public class SourceReaderTest
    {
        [TestMethod]
        public void Test()
        {
            var reader = new SourceReader("zh\r\nangsan");
            Assert.AreEqual(1, reader.Row);
            Assert.AreEqual(1, reader.Col);

            {
                Assert.AreEqual(1, reader.Row);
                Assert.AreEqual(1, reader.Col);
                Assert.AreEqual('z', reader.Read());
            }

            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(1, reader.Row);
                Assert.AreEqual(2, reader.Col);
                Assert.AreEqual('h', reader.ViewOne());
            }

            {
                Assert.AreEqual(1, reader.Row);
                Assert.AreEqual(2, reader.Col);
                Assert.AreEqual('h', reader.Read());
            }

            {
                Assert.AreEqual(1, reader.Row);
                Assert.AreEqual(3, reader.Col);
                Assert.AreEqual('\r', reader.Read());
            }

            {
                Assert.AreEqual(1, reader.Row);
                Assert.AreEqual(1, reader.Col);
                Assert.AreEqual('\n', reader.Read());
            }

            {
                Assert.AreEqual(2, reader.Row);
                Assert.AreEqual(1, reader.Col);
                Assert.AreEqual('a', reader.Read());
            }

            {
                Assert.AreEqual(2, reader.Row);
                Assert.AreEqual(2, reader.Col);
                Assert.AreEqual('n', reader.Read());
            }

            {
                Assert.AreEqual(2, reader.Row);
                Assert.AreEqual(3, reader.Col);
                Assert.AreEqual('g', reader.Read());
            }
        }
    }
}
