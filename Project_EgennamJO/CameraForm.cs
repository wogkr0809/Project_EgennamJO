﻿using Project_EgennamJO.Core;
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
using Project_EgennamJO.Util;
using Project_EgennamJO.UIControl;

namespace Project_EgennamJO
{
    public partial class CameraForm : DockContent
    {
        eImageChannel _currentImageChannel = eImageChannel.Gray;
        public CameraForm()
        {
            InitializeComponent();

            this.FormClosed += CameraForm_FormClosed;

            imageViewer.DiagramEntityEvent += ImageViewCtrl_DiagramEntityEvent;

            mainViewToolbar.ButtonChanged += Toolbar_ButtonChanged;
        }
        private void ImageViewCtrl_DiagramEntityEvent(object sender, DiagramEntityEventArgs e)
        {
            SLogger.Write($"ImageViewer Action {e.ActionType.ToString()}");
            switch (e.ActionType)
            {
                case EntityActionType.Select:
                    Global.Inst.InspStage.SelectInspWindow(e.InspWindow);
                    imageViewer.Focus();
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
            imageViewer.LoadBitMap((Bitmap)bitmap);
        }
        private void CameraForm_Resize(object sender, EventArgs e)
        {
            int margin = 0;
            imageViewer.Width = this.Width - mainViewToolbar.Width - margin * 2;
            imageViewer.Height = this.Height - margin * 2;

            imageViewer.Location = new System.Drawing.Point(margin, margin);
        }
        public void UpdateDisplay(Bitmap bitmap = null)
        {
            if (bitmap == null)
            {
                //#6_INSP_STAGE#3 업데이트시 bitmap이 없다면 InspSpace에서 가져온다
                bitmap = Global.Inst.InspStage.GetBitmap(0, _currentImageChannel);
                if (bitmap == null)
                    return;
            }

            if (imageViewer != null)
                imageViewer.LoadBitMap(bitmap);
        
        }
        
        public Mat GetDisplayImage()
        {
            return Global.Inst.InspStage.ImageSpace.GetMat(0, _currentImageChannel);
        }
        
        public void UpdateImageViewer()
        {
            imageViewer.UpdateInspParam();
            imageViewer.Invalidate();
        }
        public void UpdateDiagramEntity()
        {
            imageViewer.ResetEntity();

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
                    EntityColor = imageViewer.GetWindowColor(window.InspWindowType),
                    IsHold = window.IsTeach
                };
                diagramEntityList.Add(entity);
            }

            imageViewer.SetDiagramEntityList(diagramEntityList);
        }
        public void SelectDiagramEntity(InspWindow window)
        {
            imageViewer.SelectDiagramEntity(window);
        }
        public void ResetDisplay()
        {
            imageViewer.ResetEntity();
        }
        public void AddRect(List<DrawInspectInfo> rectInfos)
        {
            imageViewer.AddRect(rectInfos);
        }
        public void AddRoi(InspWindowType inspWindowType)
        {
            imageViewer.NewRoi(inspWindowType);
        }
        public void SetInspResultCount(int totalArea, int okCnt, int ngCnt)
        {
            imageViewer.SetInspResultCount(new InspectResultCount(totalArea, okCnt, ngCnt));
        }
        public void SetWorkingState(WorkingState workingState)
        {
            string state = "";
            switch (workingState)
            {
                case WorkingState.INSPECT:
                    state = "INSPECT";
                    break;

                case WorkingState.LIVE:
                    state = "LIVE";
                    break;

                case WorkingState.ALARM:
                    state = "ALARM";
                    break;
            }

            imageViewer.WorkingState = state;
            imageViewer.Invalidate();
        }
        private void Toolbar_ButtonChanged(object sender, ToolbarEventArgs e)
        {
            switch (e.Button)
            {
                case ToolbarButton.ShowROI:
                    if (e.IsChecked)
                        UpdateDiagramEntity();
                    else
                        imageViewer.ResetEntity();
                    break;
                case ToolbarButton.ChannelColor:
                    _currentImageChannel = eImageChannel.Color;
                    UpdateDisplay();
                    break;
                case ToolbarButton.ChannelGray:
                    _currentImageChannel = eImageChannel.Gray;
                    UpdateDisplay();
                    break;
                case ToolbarButton.ChannelRed:
                    _currentImageChannel = eImageChannel.Red;
                    UpdateDisplay();
                    break;
                case ToolbarButton.ChannelGreen:
                    _currentImageChannel = eImageChannel.Green;
                    UpdateDisplay();
                    break;
                case ToolbarButton.ChannelBlue:
                    _currentImageChannel = eImageChannel.Blue;
                    UpdateDisplay();
                    break;
            }
        }
        public void SetImageChannel(eImageChannel channel)
        {
            mainViewToolbar.SetSelectButton(channel);
            UpdateDisplay();
        }
        private void CameraForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainViewToolbar.ButtonChanged -= Toolbar_ButtonChanged;

            imageViewer.DiagramEntityEvent -= ImageViewCtrl_DiagramEntityEvent;

            this.FormClosed -= CameraForm_FormClosed;
        }
    }
}
