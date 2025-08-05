using Project_EgennamJO.Core;
using Project_EgennamJO.Grab;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Project_EgennamJO
{
    public partial class RunForm : DockContent
    {
        public RunForm()
        {
            InitializeComponent();
        }
        private void btnGrab_Click(object sender, EventArgs e)
        {
            var stage = Global.Inst.InspStage;

            Console.WriteLine($"[RunForm] 촬상 클릭됨. CameraType: {stage.GetCurrentCameraType()}");

            if (stage.GetCurrentCameraType() == CameraType.None)
            {
                MessageBox.Show("현재 선택된 카메라가 없습니다.");
                return;
            }

            stage.Grab(0);
        }

        private void btnLive_Click(object sender, EventArgs e)
        {
            Global.Inst.InspStage.ToggleLiveMode();
            if (Global.Inst.InspStage.LiveMode)
            {
                btnLive.Text = "Live stop";
                Global.Inst.InspStage.Grab(0);
            }
            else
            {
                btnLive.Text = "Live start";
            }
        }
        private void btnStart_Click(object sender, EventArgs e)
        {

            Global.Inst.InspStage.TryInspection();
        }
    }
}

