using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookMarkUpdate.Model;
using BookMarkUpdate;

namespace BookMarkUpdateTest
{
    /// <summary>
    /// EdgeManagerTest 的摘要说明
    /// </summary>
    [TestClass]
    public class EdgeManagerTest
    {
        private EdgeManager edge;
        public EdgeManagerTest()
        {
            edge = new EdgeManager();
        }

        [TestMethod]
        public void TestMethod1()
        {
            FolderModel model = new FolderModel();
            edge.GetAllBookMark(model, @"C:\Users\ljg\AppData\Local\Packages\Microsoft.MicrosoftEdge_8wekyb3d8bbwe\AC\MicrosoftEdge\User\Default\Favorites");
        }
    }
}
