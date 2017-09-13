using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookMarkUpdate;
using CommonLib;

namespace BookMarkUpdateTest
{
    [TestClass]
    public class RegistryHelperTest
    {
        [TestMethod]
        public void GetInstallationPathTest()
        {
            var instalPath = RegistryHelper.GetInstallationPath("AcroRd32.exe");
            Assert.IsTrue(string.Equals(@"C:\Install\AdobeReader\Reader\", instalPath));

            //var chromePath = RegistryHelper.GetInstallationPath("excel.exe");
        }
    }
}
