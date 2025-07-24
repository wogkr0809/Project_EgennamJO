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
    public partial class AIModuleProp : UserControl
    {
        SAIGEAI _saigeAI;
        string _modelPath = string.Empty;
        public AIModuleProp()
        {
            InitializeComponent();
        }
        private void btnSelAIModel_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "AI 모델 선택";
                openFileDialog.Filter = "AI Files|*.*";
                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _modelPath = openFileDialog.FileName;
                    textBox1.Text = _modelPath;
                }

            }
        }
    }
}
