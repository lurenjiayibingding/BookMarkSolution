using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookMarkUpdate;

namespace BookMarkUpdateTest
{
    /// <summary>
    /// IExploreManagerTest 的摘要说明
    /// </summary>
    [TestClass]
    public class IExploreManagerTest
    {
        IExploreManager ie;

        public IExploreManagerTest()
        {
            ie = new IExploreManager();
        }

        [TestMethod]
        public void DeriveBookmarkTest()
        {
            ie.DeriveBookmark();
        }

        [TestMethod]
        public void ImportBookmarkTest()
        {
            ie.ImportBookmark(@"D:\MyProject\BookMarkSolution\BookMarkUpdateTest\bin\Debug\Chrome.xml", BookMarkUpdate.Enum.ImportEnum.Override);
        }
    }
}
