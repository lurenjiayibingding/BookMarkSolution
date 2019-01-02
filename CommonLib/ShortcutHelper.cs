using IWshRuntimeLibrary;
using System;
using IO = System.IO;

namespace CommonLib
{
    public class ShortcutHelper
    {
        /// <summary>
        /// 创建URL快捷方式
        /// </summary>
        /// <param name="filePath">快捷方式保存地址</param>
        /// <param name="targetPath">快捷方式指向的地址</param>
        public static void GreateURLShortcutPath(string filePath, string targetPath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ShortcutException("快捷方式的保存路径为空");
            }

            if (string.IsNullOrWhiteSpace(targetPath))
            {
                throw new ShortcutException("快捷方式的指向路径为空");
            }

            if (IO.File.Exists(filePath))
            {
                throw new ShortcutException("已经存在同名的快捷方式");
            }

            try
            {
                //呵呵呵
                WshShell shell = new WshShell();
                IWshURLShortcut urlShort = shell.CreateShortcut(filePath) as IWshURLShortcut;
                urlShort.TargetPath = targetPath;
                urlShort.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 得到URL快捷方式指向的地址
        /// </summary>
        /// <param name="filePath">快捷方式文件的路径</param>
        /// <returns></returns>
        public static string GetURLShortcutPath(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return string.Empty;
            }
            if (!IO.File.Exists(filePath))
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

    /// <summary>
    /// 快捷方式相关的异常
    /// </summary>
    public class ShortcutException : Exception
    {
        /// <summary>
        /// 异常信息
        /// </summary>
        private string errorMessage;

        public string ErrorMessage { get => errorMessage; set => errorMessage = value; }

        public ShortcutException(string message) : base(message)
        {
            errorMessage = message;
        }
    }
}
