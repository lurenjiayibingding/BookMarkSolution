using BookMarkUpdate.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        /// <summary>
        /// 从文件中导入书签栏内容
        /// </summary>
        /// <param name="import">书签的导入方式</param>
        public override void ImportBookmark(string bakPath, ImportEnum import)
        {

        }
    }
}
