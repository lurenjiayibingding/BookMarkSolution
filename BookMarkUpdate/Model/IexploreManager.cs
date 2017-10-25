using BookMarkUpdate.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using IWshRuntimeLibrary;
using CommonLib;

namespace BookMarkUpdate
{
    public class IExploreManager : BrowserManager
    {
        /// <summary>
        /// 导出浏览器的书签保存到文件中
        /// </summary>
        public override void DeriveBookmark()
        {
            //找到IE浏览器收藏夹的路径
            //读取路径下的文件夹或者快捷方式添加到XM文件中
            //保存XML文件

            string userName = Environment.UserName;
            string path = @"C:\Users\" + userName + @"\Favorites";

            var favorites = new FolderModel("收藏夹");
            GetAllFolderModel(favorites, path);

            if (favorites != null)
            {

            }

        }

        /// <summary>
        /// 从文件中导入书签栏内容
        /// </summary>
        /// <param name="import">书签的导入方式</param>
        public override void ImportBookmark(string bakPath, ImportEnum import)
        {

        }

        public void GetAllFolderModel(FolderModel folder, string rootPath)
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
                        GetAllFolderModel(currentFolder, item.FullName);
                    }
                }

                var bookMarkList = directoryInfo.GetFiles();
                if (bookMarkList.Length > 0)
                {
                    foreach (var item in bookMarkList)
                    {
                        if (!string.Equals(item.Extension, ".url", StringComparison.CurrentCultureIgnoreCase))
                        {
                            continue;
                        }

                        folder.ChildrenBookMark.Add(new BookMarkModel
                        {
                            Name = item.Name,
                            Url = ShortcutHelper.GetURLShortcutPath(item.FullName)
                        });
                    }
                }
            }
        }

    }
}
