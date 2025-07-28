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
using SaigeVision.Net.V2.Detection;
using SaigeVision.Net.V2.Segmentation;

namespace Project_EgennamJO
{
    internal class SAIGEAI : IDisposable
    {
        IADEngine _IADEngine = null;
        IADResult _IADResult = null;

        Bitmap _InspImage = null;

        DetectionEngine _DetEngine = null;
        DetectionResult _DetResult = null;



        public void LoadEngine(string modelPath)
        {
            /*if (this._IADEngine != null)
                this._IADEngine.Dispose();
            _IADEngine = new IADEngine(modelPath, 0);

            IADOption option = _IADEngine.GetInferenceOption();

            option.CalcScoremap = false;
            option.CalcHeatmap = false;
            option.CalcMask = false;
            option.CalcObject = true;
            option.CalcObjectAreaAndApplyThreshold = true;
            option.CalcObjectAreaAndApplyThreshold = true;
            option.CalcTime = true;
            _IADEngine.SetInferenceOption(option);
            */

        }
        public bool InspIAD(Bitmap bmpImage)
        {
            if (_IADEngine == null)
            {
                MessageBox.Show("엔진이 초기화되지 않았습니다.LoadEngine 메서드를 호출하여 엔진을 초기화하세요.");
                return false;
            }
            _InspImage = bmpImage;

            SrImage srImage = new SrImage(bmpImage);

            Stopwatch sw = Stopwatch.StartNew();

            _IADResult = _IADEngine.Inspection(srImage);

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
                    if(_IADEngine != null)
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
