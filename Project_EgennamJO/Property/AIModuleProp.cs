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
        private SAIGEAI.EngineType _engineType = SAIGEAI.EngineType.SEG;
        SAIGEAI _saigeAI;
        string _modelPath = string.Empty;

        public AIModuleProp()
        {
            InitializeComponent();
        }

        private void btnSelAIModel_Click(object sender, EventArgs e)
        {
            int selType = cbEngineList.SelectedIndex;
            string abc = "AI Files|*.*";
            switch (selType)
            {
                case 0:
                    abc = "AI Files|*.saigeiad;";
                    break;
                case 1:
                    abc = "AI Files|*.saigedet;";
                    break;
                case 2:
                    abc = "AI Files|*.saigeseg;";
                    break;
            }

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "AI 모델 선택";
                openFileDialog.Multiselect = false;
                openFileDialog.Filter = abc;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _modelPath = openFileDialog.FileName;
                    textBox1.Text = _modelPath;
                }
            }
        }
        private void btnLoadModel_Click(object sender, EventArgs e)
        {
             if (string.IsNullOrEmpty(_modelPath))
            {
                MessageBox.Show("모델 파일을 선택해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_saigeAI == null)
            {
                _saigeAI = Global.Inst.InspStage.AIModule;
            }

            _saigeAI.LoadEngine(_modelPath);
            MessageBox.Show("모델이 성공적으로 로드되었습니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnInspAI_Click(object sender, EventArgs e)
        {
            if(_saigeAI == null)
            {
                MessageBox.Show("AI 모듈이 초기화되지 않았습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Bitmap bitmap = Global.Inst.InspStage.GetCurrentImage();
            _saigeAI.Inspect(bitmap);
            Bitmap resultImage = _saigeAI.GetResultImage();
            Global.Inst.InspStage.UpdateDisplay(resultImage);
        }

        private void cbEngineList_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cbEngineList.SelectedIndex)
            {
                case 0:
                    _engineType = SAIGEAI.EngineType.IAD;
                    break;
                case 1:
                    _engineType = SAIGEAI.EngineType.DET;
                    break;
                case 2:
                    _engineType = SAIGEAI.EngineType.SEG;
                    break;
            }
            if (_saigeAI == null)
                _saigeAI = Global.Inst.InspStage.AIModule;
            _saigeAI.SetEngineType(_engineType);
        }
    }
}
