using BookMarkUpdate.Enum;
using CommonLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace BookMarkUpdate
{
    public class ChromeManager : BrowserManager
    {
        /// <summary>
        /// 导出浏览器的书签保存到文件中
        /// </summary>
        public override void DeriveBookmark()
        {
            string bookmarksPath = GetBookmarksPath();
            using (FileStream fs = new FileStream(bookmarksPath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    var json = sr.ReadToEnd();
                    if (!string.IsNullOrEmpty(json))
                    {
                        ChromeFavorites model = JsonConvert.DeserializeObject<ChromeFavorites>(json);

                        if (model != null)
                        {
                            List<FolderModel> browerFavorites = new List<FolderModel>();
                            //处理书签栏文件夹
                            var bookmark_bar = GetChromeFolder(model.roots.bookmark_bar, "bookmark_bar");
                            if (bookmark_bar != null)
                            {
                                browerFavorites.Add(bookmark_bar);
                            }

                            //处理其他文件夹
                            var synced = GetChromeFolder(model.roots.synced, "synced");
                            if (synced != null)
                            {
                                browerFavorites.Add(synced);
                            }

                            //处理同步文件夹
                            var other = GetChromeFolder(model.roots.other, "other");
                            if (other != null)
                            {
                                browerFavorites.Add(other);
                            }

                            BuilderXml(browerFavorites, "Chrome");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 从文件中导入书签栏内容
        /// </summary>
        /// <param name="import">书签的导入方式</param>
        public override void ImportBookmark(string bakPath, ImportEnum import)
        {

            var bookmark_barFolder = new ChromeFolder
            {
                date_added = DateTime.Now.Ticks.ToString(),
                name = "书签栏",
                type = "folder",
            };

            XmlDocument document = new XmlDocument();
            document.Load(bakPath);

            if (document != null)
            {
                var favoritesColumnNode = document.SelectSingleNode("/FavoritesColumn");
                if (favoritesColumnNode != null)
                {
                    //将XML文件中的收藏夹栏转变成ChromeFolder对象
                    var bookmark_barNode = favoritesColumnNode.SelectSingleNode("Folder[@Name='bookmark_bar']");
                    if (bookmark_barNode != null)
                    {
                        ConvertXmlNodeToFolderModel(bookmark_barNode, bookmark_barFolder);
                    }

                    //读取现有的浏览器收藏夹文件
                    //并且将XML文件中的内容写到该文件中
                    //重新保存该文件
                    string bookmarksPath = GetBookmarksPath();
                    ChromeFavorites favoritesModel = null;

                    using (FileStream fs = new FileStream(bookmarksPath, FileMode.Open, FileAccess.Read))
                    {
                        using (StreamReader sr = new StreamReader(fs))
                        {
                            var json = sr.ReadToEnd();
                            if (!string.IsNullOrEmpty(json))
                            {
                                favoritesModel = JsonConvert.DeserializeObject<ChromeFavorites>(json);

                                if (favoritesModel != null)
                                {
                                    favoritesModel.roots.bookmark_bar = bookmark_barFolder;
                                }
                            }
                        }
                    }

                    var jsonFavorites = JsonConvert.SerializeObject(favoritesModel);
                    using (FileStream fs = new FileStream(bookmarksPath, FileMode.Create, FileAccess.Write))
                    {
                        var arrayFavorites = System.Text.Encoding.UTF8.GetBytes(jsonFavorites);
                        fs.Write(arrayFavorites, 0, arrayFavorites.Length);
                    }
                }
            }
        }

        /// <summary>
        /// 反序列化字符串得到浏览器书签栏收藏夹
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <param name="browerFavorites"></param>
        private FolderModel GetChromeFolder(ChromeFolder obj, string name)
        {
            var children = obj.children;
            if (children != null && children.Count > 0)
            {
                FolderModel folder = new FolderModel();
                folder.Name = name;
                foreach (var bookmarkFavorites in children)
                {
                    //书签栏下可能是链接也可能是文件夹
                    ProcessingChromeBase(bookmarkFavorites, folder);
                }
                return folder;
            }
            return null;
        }

        /// <summary>
        /// 处理浏览器收藏夹下的所有对象
        /// </summary>
        /// <param name="chromeBase"></param>
        /// <param name="browserFavorites"></param>
        private void ProcessingChromeBase(Object chromeBase, FolderModel browserFavorites)
        {
            var baseModel = JsonConvert.DeserializeObject<ChromeBasse>(chromeBase.ToString());
            if (string.Equals(baseModel.type, "folder", StringComparison.CurrentCultureIgnoreCase))
            {
                var currentFavorites = JsonConvert.DeserializeObject<ChromeFolder>(chromeBase.ToString());
                ProcessingFavorites(currentFavorites, browserFavorites);
            }
            if (string.Equals(baseModel.type, "url", StringComparison.CurrentCultureIgnoreCase))
            {
                var currentBookMark = JsonConvert.DeserializeObject<ChromeBookMark>(chromeBase.ToString());
                var browserBookMark = new BookMarkModel
                {
                    Name = currentBookMark.name,
                    Url = currentBookMark.url,
                };
                browserFavorites.ChildrenBookMark.Add(browserBookMark);
            }
        }

        private void ProcessingFavorites(ChromeFolder chrome, FolderModel browser)
        {
            if (chrome == null)
                return;

            if (chrome.children == null)
                return;

            var browserFavorites = new FolderModel { Name = chrome.name };
            browser.ChildrenFolders.Add(browserFavorites);

            foreach (var item in chrome.children)
            {
                ProcessingChromeBase(item, browserFavorites);
            }
        }

        /// <summary>
        /// 将XML节点转为浏览器收藏夹对象
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <param name="folderModel"></param>
        private void ConvertXmlNodeToFolderModel(XmlNode xmlNode, ChromeFolder folderModel)
        {
            folderModel.children = new List<object>();

            foreach (XmlNode item in xmlNode.ChildNodes)
            {
                if (string.Equals(item.Name, "Folder"))
                {
                    ChromeFolder newfolderModel = new ChromeFolder
                    {
                        date_added = DateTime.Now.Ticks.ToString(),
                        name = item.Attributes["Name"].Value,
                        type = "folder",
                    };
                    folderModel.children.Add(newfolderModel);
                    ConvertXmlNodeToFolderModel(item, newfolderModel);
                }

                if (string.Equals(item.Name, "BookMark"))
                {
                    folderModel.children.Add(new ChromeBookMark
                    {
                        date_added = DateTime.Now.Ticks.ToString(),
                        name = item.Attributes["Name"].Value,
                        type = "url",
                        url = item.Attributes["Url"].Value
                    });
                }
            }
        }

        /// <summary>
        /// 得到收藏夹文件的路径
        /// </summary>
        /// <returns></returns>
        private string GetBookmarksPath()
        {
            try
            {
                var instalPath = RegistryHelper.GetInstallationPath("chrome.exe");
                DirectoryInfo info = new DirectoryInfo(instalPath);
                return info.Parent.FullName + @"\User Data\Default\Bookmarks";
            }
            catch (RegistryException ex)
            {
                throw ex;
            }
        }
    }
}
