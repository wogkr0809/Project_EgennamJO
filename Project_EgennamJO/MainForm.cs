﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking; 

namespace Project_EgennamJO
{
    public partial class MainForm : Form
    {
        private static DockPanel _dockPanel;
        public MainForm()
        {
            InitializeComponent();
  
            _dockPanel = new DockPanel
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(_dockPanel);
            
            _dockPanel.Theme = new VS2015BlueTheme();

            LoadDockingWindows();
        }
        private void LoadDockingWindows()
        {
            _dockPanel.AllowEndUserDocking = false;

            var cameraWindow = new CameraForm();
            cameraWindow.Show(_dockPanel, DockState.Document);
           
            var propWindow = new PropertiesForm();
            propWindow.Show(_dockPanel, DockState.DockRight);

        }
        public static T GetDockForm<T>() where T : DockContent
        {
            var findForm = _dockPanel.Contents.OfType<T>().FirstOrDefault();
            return findForm;
        }

        private void imageOpenToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CameraForm cameraForm = GetDockForm<CameraForm>();
            if (cameraForm == null)
                return;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "이미지 파일 선택";
                openFileDialog.Filter = "Image Files|* .bmp;* .jpg;* .jpeg;* .png;* .gif";
                openFileDialog.Multiselect = false; 
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    cameraForm.LoadImage(filePath);
                }
            }
        }
    }
}
