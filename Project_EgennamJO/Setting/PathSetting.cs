﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_EgennamJO.Setting
{
    public partial class PathSetting : UserControl
    {
        public PathSetting()
        {
            InitializeComponent();

            LoadSetting();
        }
        private void LoadSetting()
        {
            txtModelDir.Text = SettingXml.Inst.ModelDir;
            txtImageDir.Text = SettingXml.Inst.ImageDir;
        }
        private void SaveSetting()
        {
            SettingXml.Inst.ModelDir = txtImageDir.Text;
            SettingXml.Inst.ImageDir = txtImageDir.Text;

            SettingXml.Save();
        }

        private void btnSelModeDir_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "폴더를 선택하세요.";
                folderDialog.ShowNewFolderButton = true;

                if(folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtModelDir.Text = folderDialog.SelectedPath;
                }
            }
        }
        private void btnSelImageDir_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "폴더를 선택하세요.";
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtModelDir.Text = folderDialog.SelectedPath;
                }
            }
        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            SaveSetting();
        }
    }
}
