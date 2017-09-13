using Microsoft.Win32;
using System;

namespace CommonLib
{
    public class RegistryHelper
    {
        /// <summary>
        /// 根据注册列表得到安装路径
        /// </summary>
        /// <param name="appName">注册列表中的名称</param>
        /// <returns></returns>
        public static string GetInstallationPath(string appName)
        {
            string installPath = string.Empty;
            try
            {
                string strKeyName = "path";
                string softPath = @"Software\Microsoft\Windows\CurrentVersion\App Paths\";

                RegistryKey regSubKey = GetRegistryKey(softPath + appName);
                if (regSubKey == null)
                    regSubKey = GetCurrentUserRegistryKey(softPath + appName);


                if (regSubKey != null)
                {
                    object objResult = regSubKey.GetValue(strKeyName);
                    RegistryValueKind regValueKind = regSubKey.GetValueKind(strKeyName);
                    if (regValueKind == RegistryValueKind.String)
                    {
                        installPath = objResult.ToString();
                    }
                    return installPath;
                }

                else
                {
                    //记录日志，未找到软件
                    //抛出异常
                    throw new RegistryException("未找到安装的程序");
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 根据当前的操作系统和路径实例化HKEY_LOCAL_MACHINE相关的RegistryKey
        /// </summary>
        /// <param name="keyPath"></param>
        /// <returns></returns>
        private static RegistryKey GetRegistryKey(string keyPath)
        {
            RegistryView viewer = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;
            RegistryKey localMachineRegistry = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, viewer);
            return string.IsNullOrEmpty(keyPath) ? localMachineRegistry : localMachineRegistry.OpenSubKey(keyPath);
        }

        /// <summary>
        /// 根据应用程序的路径实例化HKEY_CURRENT_USER相关的RegistryKey
        /// </summary>
        /// <param name="keyPath"></param>
        /// <returns></returns>
        private static RegistryKey GetCurrentUserRegistryKey(string keyPath)
        {
            RegistryKey current = Registry.CurrentUser;
            return current.OpenSubKey(keyPath);
        }

    }

    /// <summary>
    /// 注册表相关的异常
    /// </summary>
    public class RegistryException : Exception
    {
        public RegistryException(string message) : base(message)
        {
        }
    }
}
