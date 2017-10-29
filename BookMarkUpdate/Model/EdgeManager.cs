using CommonLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMarkUpdate.Model
{
    public class EdgeManager
    {
        /// <summary>
        /// 得到IE浏览器收藏夹下的所有收藏夹和收藏的地址
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="rootPath"></param>
        public void GetAllBookMark(FolderModel folder, string rootPath)
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
