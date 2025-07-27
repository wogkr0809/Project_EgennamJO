using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaigeVision.Net.V2;
using SaigeVision.Net.V2.IAD;
using System.Windows.Forms;
using System.Diagnostics;
using SaigeVision.Net.V2.Segmentation;
using SaigeVision.Net.V2.Detection;
using System.Runtime.InteropServices;

namespace Project_EgennamJO
{
    public class SAIGEAI : IDisposable
    {
        public enum EngineType { IAD, SEG, DET };
        public EngineType _engineType;

        private Dictionary<string, SegmentationResult> _SegmentationResults;
        private Dictionary<string, IADResult> _IADResults;
        private Dictionary<string, DetectionResult> _DetectionResults;

        IADEngine _IADEngine = null;
        IADResult _IADResult = null;
        SegmentationEngine _SEGEngine = null;
        SegmentationResult _SEGResult = null;
        DetectionEngine _DetEngine = null;
        DetectionResult _DetResult = null;

        Bitmap _InspImage = null;

        public SAIGEAI()
        {
            _SegmentationResults = new Dictionary<string, SegmentationResult>();
            _IADResults = new Dictionary<string, IADResult>();
            _DetectionResults = new Dictionary<string, DetectionResult>();
        }
        public void SetEngineType(EngineType engineType)
        {
            _engineType = engineType;
        }


        public void LoadEngine(string modelPath)
        {
            _IADEngine?.Dispose();
            // ?. => null 조건 연산자 IADEngine에 값이 들어오면 실행하고 null인경우 Dispose 실행
            _SEGEngine?.Dispose();
            _DetEngine?.Dispose();

            switch (_engineType)
            {
                case EngineType.IAD:
                    _IADEngine = new IADEngine(modelPath, 0);
                    var IADOption = _IADEngine.GetInferenceOption();
                    IADOption.CalcScoremap = false;
                    IADOption.CalcHeatmap = false;
                    IADOption.CalcMask = false;
                    IADOption.CalcObject = true;
                    IADOption.CalcObjectAreaAndApplyThreshold = true;
                    IADOption.CalcObjectScoreAndApplyThreshold = true;
                    IADOption.CalcTime = true;
                    _IADEngine.SetInferenceOption(IADOption);
                    break;
                case EngineType.SEG:
                    _SEGEngine = new SegmentationEngine(modelPath, 0);
                    var SegOption = _SEGEngine.GetInferenceOption();
                    SegOption.CalcTime = true;
                    SegOption.CalcObject = true;
                    SegOption.CalcScoremap = false;
                    SegOption.CalcMask = false;
                    SegOption.CalcObjectScoreAndApplyThreshold = true;
                    SegOption.CalcObjectAreaAndApplyThreshold = true;
                    _SEGEngine.SetInferenceOption(SegOption);
                    break;
                case EngineType.DET:
                    _DetEngine = new DetectionEngine(modelPath, 0);
                    var DetOption = _DetEngine.GetInferenceOption();
                    DetOption.CalcTime = true;
                    _DetEngine.SetInferenceOption(DetOption);
                    break;

            }

        }
        public bool Inspect(Bitmap bmpImage)
        {
            if (bmpImage == null)
            {
                MessageBox.Show("검사할 이미지가 없습니다.");
                return false;
            }
            _InspImage = bmpImage;

            SrImage srImage = new SrImage(bmpImage);
            Stopwatch sw = Stopwatch.StartNew();

            switch (_engineType)
            {
                case EngineType.IAD:
                    if (_IADEngine == null)
                    {
                        MessageBox.Show("IAD 엔진이 초기화되지않았습니다.");
                    }
                    _IADResult = _IADEngine.Inspection(srImage);
                    break;
                case EngineType.DET:
                    if (_DetEngine == null)
                    {
                        MessageBox.Show("DET 엔진이 초기화되지않았습니다.");
                    }
                    _DetResult = _DetEngine.Inspection(srImage);
                    break;
                case EngineType.SEG:
                    if (_SEGEngine == null)
                    {
                        MessageBox.Show("SEG 엔진이 초기화되지않았습니다.");
                    }
                    _SEGResult = _SEGEngine.Inspection(srImage);
                    break;

                default:
                    MessageBox.Show("지원하지 않는 엔진 타입입니다.");
                    return false;
            }
            sw.Stop();
            return true;
           

        }

        private void DrawIADResult(IADResult result, Bitmap bmp)
        {
            Graphics g = Graphics.FromImage(bmp);
            int step = 10;

            foreach (var prediction in result.SegmentedObjects)
            {
                SolidBrush brush = new SolidBrush(Color.FromArgb(127, prediction.ClassInfo.Color));
                using (GraphicsPath gp = new GraphicsPath())
                {
                    if (prediction.Contour.Value.Count < 3) continue;
                    gp.AddPolygon(prediction.Contour.Value.ToArray());
                    foreach (var innerValue in prediction.Contour.InnerValue)
                    {
                        gp.AddPolygon(innerValue.ToArray());
                    }
                    g.FillPath(brush, gp);
                }
                step += 50;
            }
        }
        public Bitmap GetResultImage()
        {
            if (_IADResult == null || _InspImage == null)
                return null;
            Bitmap resultImage = _InspImage.Clone(new Rectangle(0, 0, _InspImage.Width, _InspImage.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            DrawIADResult(_IADResult, resultImage);
            return resultImage;
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (_IADEngine != null)
                        _IADEngine.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
