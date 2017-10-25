using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IWshRuntimeLibrary;

namespace CommonLib
{
    public class ShortcutHelper
    {
        /// <summary>
        /// 得到URL快捷方式指向的地址
        /// </summary>
        /// <param name="filePath">快捷方式指向的路径</param>
        /// <returns></returns>
        public static string GetURLShortcutPath(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return string.Empty;
            }
            if (!System.IO.File.Exists(filePath))
            {
                return string.Empty;
            }

            WshShell shell = new WshShell();
            IWshURLShortcut shortcut = shell.CreateShortcut(filePath) as IWshURLShortcut;
            if (shortcut == null)
                return string.Empty;
            else
                return shortcut.TargetPath;
        }
    }
}
