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
using System.Windows.Forms;

namespace WinForm
{
    public partial class FMain : Form
    {
        public FMain()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Program.RefreshConfiguration();
            GetAppSettings();
        }

        private void GetAppSettings()
        {
            // Access AppSettings values from the static variable rather than ConfigurationManager.AppSettings
            txtVersion.Text = Program.AppSettings.Settings["AspNet:Settings:Version"].Value;
            txtBlobConnectionString.Text = Program.AppSettings.Settings["AspNet:Settings:BlobConnectionString"].Value;
            txtB2CTenant.Text = Program.AppSettings.Settings["AspNet:Settings:B2CTenant"].Value;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Program.RefreshConfiguration();
            GetAppSettings();
        }

    }
}
