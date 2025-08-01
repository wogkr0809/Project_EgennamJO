using Project_EgennamJO.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using OpenCvSharp;

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
           //picMainview.Image = Image.FromFile(filPath);

            Image bitmap = Image.FromFile(filPath);
            imageViewCtrl.LoadBitMap((Bitmap)bitmap);
        }
        private void CameraForm_Resize(object sender, EventArgs e)
        {
            int margin = 0;
            imageViewCtrl.Width = this.Width - margin * 2;
            imageViewCtrl.Height = this.Height - margin * 2;

            imageViewCtrl.Location = new System.Drawing.Point(margin, margin);
        }
        public void UpdateDisplay(Bitmap bitmap = null)
        {
            if (bitmap == null)
            {
                //#6_INSP_STAGE#3 업데이트시 bitmap이 없다면 InspSpace에서 가져온다
                bitmap = Global.Inst.InspStage.GetBitmap(0);
                if (bitmap == null)
                    return;
            }

            if (imageViewCtrl != null)
                imageViewCtrl.LoadBitMap(bitmap);
            Mat curImage = Global.Inst.InspStage.GetMat();
            Global.Inst.InspStage.Preview.SetImage(curImage);
        }
        public Bitmap GetDisplayImage()
        {
            Bitmap curImage = null;

            if (imageViewCtrl != null)
                curImage = imageViewCtrl.GetCurBitmap();

            return curImage;
        }
        public void UpdateImageViewer()
        {
            imageViewCtrl.Invalidate();
        }
       
    }
}
