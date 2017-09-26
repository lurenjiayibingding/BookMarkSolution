using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMarkUpdate
{
    /// <summary>
    /// 收藏夹
    /// </summary>
    class ChromeFavorites
    {
        public string checksum { get; set; }

        public ChromeFoldersCollection roots { get; set; }

        public int version { get; set; }
    }

    /// <summary>
    /// 收藏夹文件夹集合
    /// </summary>
    class ChromeFoldersCollection
    {
        public ChromeFolder bookmark_bar { get; set; }

        public ChromeFolder other { get; set; }

        public ChromeFolder synced { get; set; }
    }

    /// <summary>
    /// 收藏夹基类
    /// </summary>
    class ChromeBasse
    {
        public string type { get; set; }
    }

    /// <summary>
    /// 收藏夹内的文件夹
    /// </summary>
    class ChromeFolder
    {
        public List<object> children { get; set; }

        public string date_added { get; set; }

        public string date_modified { get; set; }

        public string id { get; set; }

        public string name { get; set; }

        public string type { get; set; }
    }

    /// <summary>
    /// 收藏夹内的链接
    /// </summary>
    class ChromeBookMark
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string date_added { get; set; }
        public string url { get; set; }
    }
}
