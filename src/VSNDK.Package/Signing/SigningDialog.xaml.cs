﻿//* Copyright 2010-2011 Research In Motion Limited.
//*
//* Licensed under the Apache License, Version 2.0 (the "License");
//* you may not use this file except in compliance with the License.
//* You may obtain a copy of the License at
//*
//* http://www.apache.org/licenses/LICENSE-2.0
//*
//* Unless required by applicable law or agreed to in writing, software
//* distributed under the License is distributed on an "AS IS" BASIS,
//* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//* See the License for the specific language governing permissions and
//* limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RIM.VSNDK_Package.Signing.Models;
using System.IO;
using Microsoft.VisualStudio.PlatformUI;

namespace RIM.VSNDK_Package.Signing
{
    /// <summary>
    /// Interaction logic for SigningDialog.xaml
    /// </summary>
    public partial class SigningDialog : DialogWindow
    {
        private string certPath;

        public SigningDialog()
        {
            InitializeComponent();

            certPath = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +  @"\Research In Motion\author.p12";
            UpdateUI(File.Exists(certPath));

        }

        /// <summary>
        /// Private method to update the screen UI
        /// </summary>
        /// <param name="registered"></param>
        private void UpdateUI(bool registered)
        {
            RIMSiginingAuthorityData data = gbRIMSigningAuthority.DataContext as RIMSiginingAuthorityData;
            if (data != null)
            {
                data.Registered = registered;
                btnRegister.IsEnabled = !registered;
                btnUnregister.IsEnabled = registered;
            }
        }

        /// <summary>
        /// Show the Regisration Dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow win = new RegistrationWindow();
            bool? res = win.ShowDialog();
            UpdateUI(File.Exists(certPath));
        }

        /// <summary>
        /// Unregister your signing keys.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUnregister_Click(object sender, RoutedEventArgs e)
        {
            DeRegisterWindow win = new DeRegisterWindow();
            bool? res = win.ShowDialog();
            UpdateUI(File.Exists(certPath));    
        }

        /// <summary>
        /// Backup your signing keys.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackup_Click(object sender, RoutedEventArgs e)
        {
            BackupRestoreData brData = gbBackupRestore.DataContext as BackupRestoreData;
            string zipfile = string.Empty;
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "signingkey";
            dlg.DefaultExt = ".zip"; // Default file extension
            dlg.Filter = "zip files (.zip)|*.zip"; // Filter files by extension
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                zipfile = dlg.FileName;
                brData.Backup(System.IO.Path.GetDirectoryName(certPath), zipfile);
            }
        }

        /// <summary>
        /// Restore your signing keys.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            BackupRestoreData brData = gbBackupRestore.DataContext as BackupRestoreData;
            string zipfile = string.Empty;
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".zip"; // Default file extension
            dlg.Filter = "zip files (.zip)|*.zip"; // Filter files by extension
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                zipfile = dlg.FileName;
                brData.Restore(zipfile, System.IO.Path.GetDirectoryName(certPath));
                UpdateUI(File.Exists(certPath));
            }
        }

    }
}
