using Project_EgennamJO.Alogrithm;
using Project_EgennamJO.Grab;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using Project_EgennamJO.Teach;
using Project_EgennamJO.Setting;

namespace Project_EgennamJO.Core
{
    public class InspStage : IDisposable
    {
        public static readonly int MAX_GRAB_BUF = 5;

        private ImageSpace _imageSpace = null;

        private GrabModel _grabManager = null;
        private CameraType _camType = CameraType.WebCam;

        SAIGEAI _saigeAI;

        private PreviewImage _previewImage = null;

        BlobAlgorithm _blobAlgorithm = null;

        private Model _model = null;

        private InspWindow _selectedInspWindow = null;

        public bool LiveMode { get; set; } = false;

        public void ToggleLiveMode()
        {
            LiveMode = !LiveMode;

        }

        public static class SLogger
        {
            public static void Write(string msg)
            {
                Console.WriteLine(msg);
            }
        }
        public InspStage() { }

        public ImageSpace ImageSpace
        {
            get => _imageSpace;
        }

        public SAIGEAI AIModule
        {
            get
            {
                if (_saigeAI is null)
                    _saigeAI = new SAIGEAI();
                return _saigeAI;
            }
        }
        public PreviewImage PreView
        {
            get => _previewImage;
        }

        public Model CurModel
        {
            get => _model;
        }
        public CameraType GetCurrentCameraType()
        {
            return _camType;
        }

        public void SetCameraType(CameraType camType)
        {
            if (_camType == camType)
                return;

            _camType = camType;

            _grabManager?.Dispose();
            _grabManager = null;

            switch (_camType)
            {
                case CameraType.WebCam:
                    _grabManager = new WebCam();
                    break;
                case CameraType.HikRobotCam:
                    _grabManager = new HikRobotCam();
                    break;
                case CameraType.None:
                    return;
            }

            if (_grabManager.InitGrab())
            {
                _grabManager.TransferCompleted += _multiGrab_TransferCompleted;
                InitModerGrab(MAX_GRAB_BUF);
            }
        }
        public bool Initialize()
        {
            _imageSpace = new ImageSpace();
            _previewImage = new PreviewImage();

            _model = new Model();

            LoadSetting();

            switch (_camType)
            {
                case CameraType.WebCam:
                    {
                        _grabManager = new WebCam();
                        break;
                    }
                case CameraType.HikRobotCam:
                    {
                        _grabManager = new HikRobotCam();
                        break;
                    }
            }
            if (_grabManager != null && _grabManager.InitGrab() == true)
            {
                _grabManager.TransferCompleted -= _multiGrab_TransferCompleted;
                _grabManager.TransferCompleted += _multiGrab_TransferCompleted;
                InitModerGrab(MAX_GRAB_BUF);
                return true;
            }
            return false;
        }
        private void LoadSetting()
        {
            //카메라 설정 타입 얻기
            _camType = SettingXml.Inst.CamType;
        }
        public void InitModerGrab(int bufferCount)
        {
            if (_grabManager == null)
                return;

            int pixelBpp = 8;
            _grabManager.GetPixelBpp(out pixelBpp);

            int inspectionWidth;
            int inspectionHeight; ;
            int inspectionStride;
            _grabManager.GetResolution(out inspectionWidth, out inspectionHeight, out inspectionStride);

            if (_imageSpace != null)
            {
                _imageSpace.SetImageInfo(pixelBpp, inspectionWidth, inspectionHeight, inspectionStride);

            }
            SetBuffer(bufferCount);

        }
        private void UpdateProperty(InspWindow inspWindow)
        {
            if (inspWindow is null)
                return;

            PropertiesForm propertiesForm = MainForm.GetDockForm<PropertiesForm>();
            if (propertiesForm is null)
                return;

            propertiesForm.UpdateProperty(inspWindow);
        }
        public void UpdateTeachingImage(int index)
        {
            if (_selectedInspWindow is null)
                return;
            SetTeachingImage(_selectedInspWindow, index);
        }
        public void DelTeachingImage(int index)
        {
            if (_selectedInspWindow is null)
                return;
            InspWindow inspWindow = _selectedInspWindow;

            inspWindow.DelWindowImage(index); ;

            MatchAlgorithm matchAlgo = (MatchAlgorithm)inspWindow.FindInspAlgorithm(InspectType.InspMatch);
            if (matchAlgo != null)
            {
                UpdateProperty(inspWindow);
            }
        }
        public void SetTeachingImage(InspWindow inspWindow, int index = -1)
        {
            if (inspWindow is null)
                return;

            CameraForm cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm is null)
                return;

            Mat curImage = cameraForm.GetDisplayImage();
            if (curImage is null)
                return;

            if (inspWindow.WindowArea.Right >= curImage.Width ||
                inspWindow.WindowArea.Bottom >= curImage.Height)
            {
                Console.Write("Roi 영역이 잘못되었습니다!");
                return;
            }
            Mat windowImage = curImage[inspWindow.WindowArea];

            if (index < 0)
                inspWindow.AddWindowImage(windowImage);
            else
                inspWindow.SetWindowImage(windowImage, index);

            inspWindow.IsPatternLearn = false;

            MatchAlgorithm matchAlgo = (MatchAlgorithm)inspWindow.FindInspAlgorithm(InspectType.InspMatch);
            if(matchAlgo != null)
            {
                UpdateProperty(inspWindow);
            }    
        }

        public void SetBuffer(int bufferCount)
        {
            if (_grabManager == null)
                return;
            if (_imageSpace.BufferCount == bufferCount)
                return;
            _imageSpace.InitImageSpace(bufferCount);
            _grabManager.InitBuffer(bufferCount);

            for (int i = 0; i < bufferCount; i++)
            {
                _grabManager.SetBuffer(
                    _imageSpace.GetInspectionBuffer(i),
                    _imageSpace.GetInspectionBufferPtr(i),
                    _imageSpace.GetInspectionBufferHandle(i), i);
            }
        }
        public void TryInspection(InspWindow inspWindow = null)
        {
            if (inspWindow is null)
            {
                if (_selectedInspWindow is null)
                    return;
                inspWindow = _selectedInspWindow;
            }
            UpdateDiagramEntity();

            List<DrawInspectInfo> totalArea = new List<DrawInspectInfo>();

            Rect windowArea = inspWindow.WindowArea;

            foreach (var inspAlgo in inspWindow.AlgorithmList)
            {
                inspAlgo.TeachRect = windowArea;
                inspAlgo.InspRect = windowArea;

                InspectType inspType = inspAlgo.InspectType;

                switch (inspType)
                {
                    case InspectType.InspBinary:
                        {
                            BlobAlgorithm blobAlgo = (BlobAlgorithm)inspAlgo;

                            Mat srcImage = Global.Inst.InspStage.GetMat();
                            blobAlgo.SetInspData(srcImage);

                            if (blobAlgo.DoInspect())
                            {
                                List<DrawInspectInfo> resultArea = new List<DrawInspectInfo>();
                                int resultCnt = blobAlgo.GetResultRect(out resultArea);
                                if (resultCnt > 0)
                                {
                                    totalArea.AddRange(resultArea);
                                }
                            }
                            break;
                        }
                }
                if (inspAlgo.DoInspect())
                {
                    List<DrawInspectInfo> resultArea = new List<DrawInspectInfo>();
                    int resultCnt = inspAlgo.GetResultRect(out resultArea);
                    if (resultCnt > 0)
                    {
                        totalArea.AddRange(resultArea);
                    }
                }
            }
            if (totalArea.Count > 0)
            {
                //찾은 위치를 이미지상에서 표시
                var cameraForm = MainForm.GetDockForm<CameraForm>();
                if (cameraForm != null)
                {
                    cameraForm.AddRect(totalArea);
                }
            }
        }
        public void SelectInspWindow(InspWindow inspWindow)
        {
            _selectedInspWindow = inspWindow;

            var propForm = MainForm.GetDockForm<PropertiesForm>();
            if (propForm != null)
            {
                if (inspWindow is null)
                {
                    propForm.ResetProperty();
                    return;
                }
                propForm.ShowProperty(inspWindow);
            }
            UpdateProperty(inspWindow);

            Global.Inst.InspStage.PreView.SetInspWindow(inspWindow);
        }
        public void AddInspWindow(InspWindowType windowType, Rect rect)
        {
            InspWindow inspWindow = _model.AddInspWindow(windowType);
            if (inspWindow is null)
                return;

            inspWindow.WindowArea = rect;
            inspWindow.IsTeach = false;

            SetTeachingImage(inspWindow);
            UpdateProperty(inspWindow);
            UpdateDiagramEntity();

            CameraForm cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm != null)
            {
                cameraForm.SelectDiagramEntity(inspWindow);
                SelectInspWindow(inspWindow);
            }
        }
        public bool AddInspWindow(InspWindow sourceWindow, OpenCvSharp.Point offset)
        {
            InspWindow cloneWindow = sourceWindow.Clone(offset);
            if (cloneWindow is null)
                return false;

            if (!_model.AddInspWindow(cloneWindow))
                return false;

            UpdateProperty(cloneWindow);
            UpdateDiagramEntity();

            CameraForm cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm != null)
            {
                cameraForm.SelectDiagramEntity(cloneWindow);
                SelectInspWindow(cloneWindow);
            }

            return true;
        }
        public void MoveInspWindow(InspWindow inspWindow, OpenCvSharp.Point offset)
        {
            if (inspWindow == null)
                return;

            inspWindow.OffsetMove(offset);
            UpdateProperty(inspWindow);
        }
        public void ModifyInspWindow(InspWindow inspWindow, Rect rect)
        {
            if (inspWindow == null)
                return;

            inspWindow.WindowArea = rect;
            inspWindow.IsTeach = false;

            UpdateProperty(inspWindow);
        }
        public void DelInspWindow(InspWindow inspWindow)
        {
            _model.DelInspWindow(inspWindow);
            UpdateDiagramEntity();
        }
        public void DelInspWindow(List<InspWindow> inspWindowList)
        {
            _model.DelInspWindowList(inspWindowList);
            UpdateDiagramEntity();
        }

        private bool DisplayResult()
        {
            if (_blobAlgorithm is null)
                return false;

            List<DrawInspectInfo> resultArea = new List<DrawInspectInfo>();
            int resultCnt = _blobAlgorithm.GetResultRect(out resultArea);
            if (resultCnt > 0)
            {
                //찾은 위치를 이미지상에서 표시
                var cameraForm = MainForm.GetDockForm<CameraForm>();
                if (cameraForm != null)
                {
                    cameraForm.ResetDisplay();
                    cameraForm.AddRect(resultArea);
                }
            }

            return true;
        }
        public void Grab(int bufferIndex)
        {
            if (_grabManager == null)
                return;

            _grabManager.Grab(bufferIndex, true);
        }

        private async void _multiGrab_TransferCompleted(object sender, object e)
        {
            int bufferIndex = (int)e;
            Console.WriteLine($"_multiGrab_TransferCompleted {bufferIndex}");
            _imageSpace.Split(bufferIndex);
            DisplayGrabImage(bufferIndex);
            if (_previewImage != null)
            {
                Bitmap bitmap = ImageSpace.GetBitmap(0);
                _previewImage.SetImage(BitmapConverter.ToMat(bitmap));
            }

            if (LiveMode)
            {
                SLogger.Write("Grab");
                await Task.Delay(100); // FPS 조절 (여기선 약 33fps)
                _grabManager.Grab(bufferIndex, true); // 다음 프레임 촬영 요청
            }
        }
        private void DisplayGrabImage(int bufferIndex)
        {
            var cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm != null)
            {
                cameraForm.UpdateDisplay();
            }
        }
        public void UpdateDisplay(Bitmap bitmap)
        {

            var cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm != null)
            {
                cameraForm.UpdateDisplay(bitmap);
            }
        }
        
        public Bitmap GetBitmap(int bufferIndex = -1)
        {
            if (Global.Inst.InspStage.ImageSpace is null)
                return null;

            return Global.Inst.InspStage.ImageSpace.GetBitmap();
        }
        public Mat GetMat(int bufferIndex = -1, eImageChannel imageChannel = eImageChannel.None)
        {
            return Global.Inst.InspStage.ImageSpace.GetMat();
        }
        public void UpdateDiagramEntity()
        {
            CameraForm cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm != null)
            {
                cameraForm.UpdateDiagramEntity();
            }

            ModelTreeForm modelTreeForm = MainForm.GetDockForm<ModelTreeForm>();
            if (modelTreeForm != null)
            {
                modelTreeForm.UpdateDiagramEntity();
            }
        }
        public void RedrawMainView()
        {
            CameraForm cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm != null)
            {
                cameraForm.UpdateImageViewer();
            }
        }
        #region Disposable

        private bool disposed = false; // to detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (_saigeAI != null)
                    {
                        _saigeAI.Dispose();
                        _saigeAI = null;
                    }
                    if (_grabManager != null)
                    {
                        _grabManager.Dispose();
                        _grabManager = null;
                    }
                }



                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
#endregion