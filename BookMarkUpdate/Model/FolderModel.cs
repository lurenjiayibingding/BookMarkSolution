using System.Collections.Generic;

namespace BookMarkUpdate
{
    /// <summary>
    /// 浏览器收藏夹内的文件夹对象
    /// </summary>
    public class FolderModel
    {
        public FolderModel()
        {
            ChildrenFolders = new List<FolderModel>();
            ChildrenBookMark = new List<BookMarkModel>();
        }

        /// <summary>
        /// 收藏夹的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 收藏夹下包含的收藏夹
        /// </summary>
        public List<FolderModel> ChildrenFolders { get; set; }

        /// <summary>
        /// 收藏夹下包含的书签
        /// </summary>
        public List<BookMarkModel> ChildrenBookMark { get; set; }
    }
}
