using Project_EgennamJO.Core;
using Project_EgennamJO.Grab;
using Project_EgennamJO.Util;
using System;
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
    public partial class CameraSetting : UserControl
    {
        public CameraSetting()
        {
            InitializeComponent();

            LoadSetting();
        }
        private void LoadSetting()
        {
            cbCameraType.DataSource = Enum.GetValues(typeof(CameraType)).Cast<CameraType>().ToList();

            cbCameraType.SelectedIndex = (int)SettingXml.Inst.CamType;
        }

        private void SaveSetting()
        {
            CameraType selectedType = (CameraType)cbCameraType.SelectedIndex;

            SettingXml.Inst.CamType = selectedType;
            SettingXml.Save();

            SLogger.Write($"카메라 설정 저장");
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            SaveSetting();
            CameraType selectedType = (CameraType)cbCameraType.SelectedItem;
            string message = "";

            switch (selectedType)
            {
                case CameraType.None:
                    message = "카메라가 선택되지 않았습니다.";
                    break;
                case CameraType.WebCam:
                    message = "WebCam이 선택되었습니다.";
                    break;
                case CameraType.HikRobotCam:
                    message = "HikRobotCam이 선택되었습니다.";
                    break;
                default:
                    message = "알 수 없는 카메라 타입이 선택되었습니다.";
                    break;
            }

            MessageBox.Show(message, "카메라 설정", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
