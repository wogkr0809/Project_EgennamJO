using Project_EgennamJO.Core;
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

        private void btnLoadModel_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(_modelPath ))
            {
                MessageBox.Show("모델 파일을 선택해주세요.", "오류", MessageBoxButtons.OK , MessageBoxIcon.Error);
                return;
            }
            if(_saigeAI == null)
            {
                _saigeAI = Global.inst.inspStage.AIModule;
            }

            _saigeAI.LoadEngine(_modelPath);
            MessageBox.Show("모델이 성공적으로 로드되었습니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnInspAI_Click(object sender, EventArgs e)
        {
            if (_saigeAI == null)
            {
                MessageBox.Show("AI 모듈이 초기화되지 않았습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }
            Bitmap bitmap = Global.inst.inspStage.GetCurrentImage();
            _saigeAI.InspIAD(bitmap);

            Bitmap resultImage = _saigeAI.GetResultImage(); 

            Global.inst.inspStage.UpdateDisplay(resultImage);
        }
    }
}
