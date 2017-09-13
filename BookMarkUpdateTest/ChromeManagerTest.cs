using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookMarkUpdate;
using BookMarkUpdate.Enum;

namespace BookMarkUpdateTest
{
    [TestClass]
    public class ChromeManagerTest
    {
        [TestMethod]
        public void DeriveBookmarkTest()
        {
            ChromeManager manager = new ChromeManager();
            manager.DeriveBookmark();
            Assert.IsNull(null);
        }

        [TestMethod]
        public void ImportBookmarkTest()
        {
            ChromeManager manager = new ChromeManager();
            manager.ImportBookmark(@"D:\Project\BookMarkSolution\BookMarkUpdateTest\bin\Debug\Chrome.xml", ImportEnum.Override);
            Assert.IsNull(null);
        }
    }
}
