//===============================================================================
// Microsoft FastTrack for Azure
// Azure App Configuration Samples
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
using System;
using System.Configuration;
using System.Windows.Forms;

namespace WinForm
{
    static class Program
    {
        internal static DateTime LastCacheRefresh;
        internal static int CacheRefreshMinutes = 1;
        internal static AppSettingsSection AppSettings;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Load the AppSettings into a static variable rather than using ConfigurationManager.AppSettings
            AppSettings = ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetEntryAssembly().Location).AppSettings;
            LastCacheRefresh = DateTime.Now;
            Application.Run(new FMain());
        }

        internal static void RefreshConfiguration()
        {
            if (DateTime.Now.Subtract(Program.LastCacheRefresh).TotalMinutes > Program.CacheRefreshMinutes)
            {
                // Force a reload of the AppSettings
                Program.AppSettings = ConfigurationManager.OpenExeConfiguration(System.Reflection.Assembly.GetEntryAssembly().Location).AppSettings;
            }
        }
    }
}
