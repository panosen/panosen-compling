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

            var ch1 = reader.Read();
            Assert.AreEqual('z', ch1);
            Assert.AreEqual(1, reader.Row);
            Assert.AreEqual(2, reader.Col);

            for (int i = 0; i < 3; i++)
            {
                var chtmp = reader.ViewOne();
                Assert.AreEqual('h', chtmp);
                Assert.AreEqual(1, reader.Row);
                Assert.AreEqual(2, reader.Col);
            }

            var ch2 = reader.Read();
            Assert.AreEqual('h', ch2);

            var ch3 = reader.Read();
            Assert.AreEqual('\r', ch3);

            var ch4 = reader.Read();
            Assert.AreEqual('\n', ch4);

            var ch5 = reader.Read();
            Assert.AreEqual('a', ch5);
        }
    }
}
