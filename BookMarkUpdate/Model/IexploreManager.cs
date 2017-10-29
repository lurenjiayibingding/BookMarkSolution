using BookMarkUpdate.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using IWshRuntimeLibrary;
using CommonLib;
using System.Xml;

namespace BookMarkUpdate
{
    public class IExploreManager : BrowserManager
    {
        /// <summary>
        /// 导出浏览器的书签保存到文件中
        /// </summary>
        public override void DeriveBookmark()
        {
            string userName = Environment.UserName;
            string path = @"C:\Users\" + userName + @"\Favorites";

            var favoritesModel = new FolderModel("bookmark_bar");
            GetAllBookMark(favoritesModel, path);

            if (favoritesModel != null)
            {
                List<FolderModel> list = new List<FolderModel>
                {
                    favoritesModel
                };
                BuilderXml(list, "iExplore");
            }

        }

        /// <summary>
        /// 从文件中导入书签栏内容
        /// </summary>
        /// <param name="import">书签的导入方式</param>
        public override void ImportBookmark(string bakPath, ImportEnum import)
        {
            XmlDocument document = new XmlDocument();
            document.Load(bakPath);

            XmlNode xmlNode = document.SelectSingleNode("FavoritesColumn/Folder");
            if (xmlNode != null)
            {
                string path = @"c:\users\" + Environment.UserName + @"\Favorites";
                DirectoryInfo info = new DirectoryInfo(path);
                CreateAllBookMark(xmlNode, info);
            }
        }

        /// <summary>
        /// 得到IE浏览器收藏夹下的所有收藏夹和收藏的地址
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="rootPath"></param>
        private void GetAllBookMark(FolderModel folder, string rootPath)
        {
            if (folder == null)
            {
                return;
            }

            if (Directory.Exists(rootPath))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(rootPath);
                var folderModelList = directoryInfo.GetDirectories();
                if (folderModelList.Length > 0)
                {
                    foreach (var item in folderModelList)
                    {
                        var currentFolder = new FolderModel
                        {
                            Name = item.Name
                        };
                        folder.ChildrenFolders.Add(currentFolder);
                        GetAllBookMark(currentFolder, item.FullName);
                    }
                }

                var bookMarkList = directoryInfo.GetFiles();
                if (bookMarkList.Length > 0)
                {
                    foreach (FileInfo item in bookMarkList)
                    {
                        if (!string.Equals(item.Extension, ".url", StringComparison.CurrentCultureIgnoreCase))
                        {
                            continue;
                        }

                        folder.ChildrenBookMark.Add(new BookMarkModel
                        {
                            Name = item.Name.Replace(item.Extension, ""),
                            Url = ShortcutHelper.GetURLShortcutPath(item.FullName)
                        });
                    }
                }
            }
        }

        /// <summary>
        /// 创建收藏夹
        /// </summary>
        /// <param name="ieFavorites"></param>
        /// <param name="currentDirectory"></param>
        private void CreateAllBookMark(XmlNode ieFavorites, DirectoryInfo currentDirectory)
        {
            var currentNodes = ieFavorites.ChildNodes;
            foreach (XmlNode item in currentNodes)
            {
                if (string.Equals(item.Name, "Folder", StringComparison.CurrentCultureIgnoreCase))
                {
                    DirectoryInfo currentDirect = new DirectoryInfo(currentDirectory.FullName + @"\" + item.Attributes["Name"].Value);
                    currentDirect.Create();

                    CreateAllBookMark(item, currentDirect);
                }
                if (string.Equals(item.Name, "BookMark", StringComparison.CurrentCultureIgnoreCase))
                {
                    var filePath = currentDirectory.FullName + @"\" + item.Attributes["Name"].Value + ".url";
                    var targetPath = item.Attributes["Url"].Value;

                    ShortcutHelper.GreateURLShortcutPath(filePath, targetPath);
                }
            }
        }

    }
}
