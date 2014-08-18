﻿using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace Screenshot
{
    public class Installer
    {
        public const string Path = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

        public static bool Installed
        {
            get
            {
                using (var key = Registry.CurrentUser.OpenSubKey(Path))
                {
                    return key.GetValue(AppDomain.CurrentDomain.FriendlyName) != null;
                }
            }
        }

        public static void Install()
        {
            if (Installed) return;

            using (var key = Registry.CurrentUser.OpenSubKey(Path, true))
            {
                key.SetValue(AppDomain.CurrentDomain.FriendlyName, Application.ExecutablePath);
            }
        }

        public static void Uninstall()
        {
            if (!Installed) return;
                
            using (var key = Registry.CurrentUser.OpenSubKey(Path, true))
            {
                key.DeleteValue(AppDomain.CurrentDomain.FriendlyName);
            }
        }
    }
}