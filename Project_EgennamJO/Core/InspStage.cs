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

namespace Project_EgennamJO.Core
{
    public class InspStage : IDisposable
    {
        public static readonly int MAX_GRAB_BUF = 5;

        private ImageSpace _imageSpace = null;
        private GrabModel _grabManager = null;
        private CameraType _camType= CameraType.WebCam;
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

        public CameraType CamType
        {
            get => _camType;
            set => _camType = value;
        }
        SAIGEAI _saigeAI;

        BlobAlgorithm _blobAlgorithm = null;
        private PreviewImage _previewImage = null;

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
        public BlobAlgorithm BlobAlgorithm
        {
            get => _blobAlgorithm;
        }
        public PreviewImage Preview
        {
            get => _previewImage;
        }

        public bool Initialize()
        {
            _imageSpace = new ImageSpace();
            _blobAlgorithm = new BlobAlgorithm();
            _previewImage = new PreviewImage();
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
            UpdateProperty();
        }
        private void UpdateProperty()
        {
            if (BlobAlgorithm is null)
                return;

            PropertiesForm propertiesForm = MainForm.GetDockForm<PropertiesForm>();
            if (propertiesForm is null)
                return;

            propertiesForm.UpdateProperty(BlobAlgorithm);
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
        public void TryInspection()
        {
            if (_blobAlgorithm is null)
                return;

            Mat srcImage = Global.Inst.InspStage.GetMat();
            _blobAlgorithm.SetInspData(srcImage);

            _blobAlgorithm.InspRect = new Rect(0, 0, srcImage.Width, srcImage.Height);

            if (_blobAlgorithm.DoInspect())
            {
                DisplayResult();
            }
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

        private async void _multiGrab_TransferCompleted(object sender,object e)
        {
            int bufferIndex = (int)e;
            Console.WriteLine($"_multiGrab_TransferCompleted {bufferIndex}");
            _imageSpace.Split(bufferIndex);
            DisplayGrabImage(bufferIndex);
            if(_previewImage != null)
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

        public Bitmap GetCurrentImage()
        {
            Bitmap bitmap = null;
            var cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm != null)
            {
                bitmap = cameraForm.GetDisplayImage();
            }

            return bitmap;
        }
        public Bitmap GetBitmap(int bufferIndex = -1)
        {
            if (Global.Inst.InspStage.ImageSpace is null)
                return null;

            return Global.Inst.InspStage.ImageSpace.GetBitmap();
        }
        public Mat GetMat()
        {
            return Global.Inst.InspStage.ImageSpace.GetMat();
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