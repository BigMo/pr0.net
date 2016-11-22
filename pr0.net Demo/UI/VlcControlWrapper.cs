using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Vlc.DotNet.Forms;

namespace pr0.net_Demo.UI
{
    public static class VlcControlWrapper
    {
        #region VARIABLES
        private const string REGISTRY_PATH_DEFAULT = @"SOFTWARE\VideoLAN\VLC";
        private const string REGISTRY_PATH_X86 = @"SOFTWARE\Wow6432Node\VideoLAN\VLC";
        private const string REGISTRY_VALUE_NAME = "InstallDir";
        private static string installDir;
        #endregion

        #region PROPERTIES
        public static string InstallDir
        {
            get
            {
                if (installDir == null)
                    installDir = GetVLCPath();

                return installDir;
            }
        }
        #endregion

        #region METHODS
        private static string GetVLCPath()
        {
            string registryPath = "";
            if (Environment.Is64BitOperatingSystem)
                if (Environment.Is64BitProcess)
                    registryPath = REGISTRY_PATH_DEFAULT;
                else
                    registryPath = REGISTRY_PATH_X86;
            else
                registryPath = REGISTRY_PATH_DEFAULT;

            RegistryKey key = Registry.LocalMachine.OpenSubKey(registryPath);
            if (key == null)
                return null;

            return (string)key.GetValue(REGISTRY_VALUE_NAME, null);
        }
        #endregion


        public static VlcControl CraftClvControl()
        {
            try
            {
                VlcControl vlc = new VlcControl();
                vlc.VlcLibDirectory = new DirectoryInfo(InstallDir);
                vlc.EndInit();
                return vlc;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
