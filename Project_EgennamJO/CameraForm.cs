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
using Project_EgennamJO.Alogrithm;
using Project_EgennamJO.Teach;

namespace Project_EgennamJO
{
    public partial class CameraForm : DockContent
    {
        public CameraForm()
        {
            InitializeComponent();
            imageViewCtrl.DiagramEntityEvent += ImageViewCtrl_DiagramEntityEvent;
        }
        private void ImageViewCtrl_DiagramEntityEvent(object sender, DiagramEntityEventArgs e)
        {
            switch (e.ActionType)
            {
                case EntityActionType.Select:
                    Global.Inst.InspStage.SelectInspWindow(e.InspWindow);
                    imageViewCtrl.Focus();
                    break;
                case EntityActionType.Inspect:
                    UpdateDiagramEntity();
                    Global.Inst.InspStage.TryInspection(e.InspWindow);
                    break;
                case EntityActionType.Add:
                    Global.Inst.InspStage.AddInspWindow(e.WindowType, e.Rect);
                    break;
                case EntityActionType.Copy:
                    Global.Inst.InspStage.AddInspWindow(e.InspWindow, e.OffsetMove);
                    break;
                case EntityActionType.Move:
                    Global.Inst.InspStage.MoveInspWindow(e.InspWindow, e.OffsetMove);
                    break;
                case EntityActionType.Resize:
                    Global.Inst.InspStage.ModifyInspWindow(e.InspWindow, e.Rect);
                    break;
                case EntityActionType.Delete:
                    Global.Inst.InspStage.DelInspWindow(e.InspWindow);
                    break;
                case EntityActionType.DeleteList:
                    Global.Inst.InspStage.DelInspWindow(e.InspWindowList);
                    break;
            }
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
            Global.Inst.InspStage.PreView.SetImage(curImage);
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
        public void UpdateDiagramEntity()
        {
            imageViewCtrl.ResetEntity();

            Model model = Global.Inst.InspStage.CurModel;
            List<DiagramEntity> diagramEntityList = new List<DiagramEntity>();

            foreach (InspWindow window in model.InspWindowList)
            {
                if (window is null)
                    continue;

                DiagramEntity entity = new DiagramEntity()
                {
                    LinkedWindow = window,
                    EntityROI = new Rectangle(
                        window.WindowArea.X, window.WindowArea.Y,
                            window.WindowArea.Width, window.WindowArea.Height),
                    EntityColor = imageViewCtrl.GetWindowColor(window.InspWindowType),
                    IsHold = window.IsTeach
                };
                diagramEntityList.Add(entity);
            }

            imageViewCtrl.SetDiagramEntityList(diagramEntityList);
        }
        public void SelectDiagramEntity(InspWindow window)
        {
            imageViewCtrl.SelectDiagramEntity(window);
        }
        public void ResetDisplay()
        {
            imageViewCtrl.ResetEntity();
        }
        public void AddRect(List<DrawInspectInfo> rectInfos)
        {
            imageViewCtrl.AddRect(rectInfos);
        }
        public void AddRoi(InspWindowType inspWindowType)
        {
            imageViewCtrl.NewRoi(inspWindowType);
        }

    }
}
