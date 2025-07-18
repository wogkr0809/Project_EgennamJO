using System;
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
    public partial class CameraForm : DockContent
    {
        public CameraForm()
        {
            InitializeComponent();
        }
        public void LoadImage(string filPath)
        {
            if (File.Exists(filPath) == false)
                return;
            picMainview.Image = Image.FromFile(filPath);

        }
        private void picMainview_Click(object sender, EventArgs e)
        {

        }


    }
}
