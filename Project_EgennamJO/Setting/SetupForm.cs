﻿using Project_EgennamJO.Core;
using Project_EgennamJO.Grab;
using Project_EgennamJO.Setting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_EgennamJO
{
    public partial class SetupForm : Form
    {
        public enum SettingType
        {
            SettingPath = 0,
            SettingCamera
        }
        public SetupForm()
        {
            InitializeComponent();

            InitTabControl();
        }
        private void InitTabControl()
        {
            CameraSetting cameraSetting = new CameraSetting();
            AddTabControl(cameraSetting, "Camera");

            PathSetting pathSetting = new PathSetting();
            AddTabControl(pathSetting, "Path");

            tabSetting.SelectTab(0);
        }
        private void AddTabControl(UserControl control, string tabName)
        {
            TabPage newTab = new TabPage(tabName);
            {
                Dock = DockStyle.Fill;
            }
            ;
            control.Dock = DockStyle.Fill;
            newTab.Controls.Add(control);
            tabSetting.TabPages.Add(newTab);
        }
    }
}
