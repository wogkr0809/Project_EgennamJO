using Project_EgennamJO.Core;
using Project_EgennamJO.Grab;
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
        public SetupForm()
        {
            InitializeComponent();

            cbCameralist.Items.Add("None");
            cbCameralist.Items.Add("WebCam");
            cbCameralist.Items.Add("HikRobotCam");

            cbCameralist.SelectedIndex = 0;


        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            CameraType selectedType = CameraType.None;
            switch (cbCameralist.SelectedItem.ToString())
            {
                case "None": selectedType = CameraType.None; break;
                case "WebCam": selectedType = CameraType.WebCam; break;
                case "HikRobotCam": selectedType = CameraType.HikRobotCam; break;
            }

            // InspStage에 설정된 카메라 타입 반영
            Global.Inst.InspStage.CamType = selectedType;

            // 설정된 카메라로 다시 초기화
            Global.Inst.InspStage.Initialize();

            MessageBox.Show("카메라 설정 완료");
            this.Close();
        }
    }
}
