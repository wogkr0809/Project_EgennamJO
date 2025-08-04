using Project_EgennamJO.Alogrithm;
using Project_EgennamJO.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_EgennamJO
{
    public partial class ImageViewCtrl : UserControl
    {
        private bool _isInitialized = false;
        private Bitmap _bitmapImage = null;
        private Bitmap Canvas = null;
        private RectangleF ImageRect = new RectangleF(0, 0, 0, 0);
        private float _curZoom = 1.0f;
        private float _zoomFactor = 1.1f;

        private float MinZoom = 1.0f;
        private const float MaxZoom = 100.0f;

        private List<DrawInspectInfo> _rectInfos = new List<DrawInspectInfo>();

        public ImageViewCtrl()
        {
            InitializeComponent();
            InitialIzeCanvas();

            MouseWheel += new MouseEventHandler(ImageViewCtrl_MouseWheel);

        }

        private void InitialIzeCanvas()
        {
            ResizeCanvas();

            DoubleBuffered = true;
        }
        public Bitmap GetCurBitmap()
        {
            return _bitmapImage;
        }

        private void ResizeCanvas()
        {
            if (Width <= 0 || Height <= 0 || _bitmapImage == null)
                return;
            Canvas = new Bitmap(Width, Height);
            if (Canvas == null)
                return;
            float virtualWidth = _bitmapImage.Width * _curZoom;
            float virtualHeight = _bitmapImage.Height * _curZoom;

            float offsetX = virtualWidth < Width ? (Width - virtualWidth) / 2f : 0f;
            float offsetY = virtualHeight < Height ? (Height - virtualHeight) / 2f : 0f;

            ImageRect = new RectangleF(offsetX, offsetY, virtualWidth, virtualHeight);
        }
        public void LoadBitMap(Bitmap bitmap)
        {
            if (_bitmapImage != null)
            {
                if (_bitmapImage.Width == bitmap.Width && _bitmapImage.Height == bitmap.Height)
                {
                    _bitmapImage = bitmap;
                    Invalidate();
                    return;
                }
                _bitmapImage.Dispose();
                _bitmapImage = null;
            }
            _bitmapImage = bitmap;

            if (_isInitialized == false)
            {
                _isInitialized = true;
                ResizeCanvas();
            }
            FitImageToScreen();
        }
        private void FitImageToScreen()
        {
            RecalcZoomRatio();

            float NewWidth = _bitmapImage.Width * _curZoom;
            float NewHeight = _bitmapImage.Height * _curZoom;

            ImageRect = new RectangleF(
                (Width - NewWidth) / 2f,
                (Height - NewHeight) / 2f,
                NewWidth,
                NewHeight
                );
            Invalidate();
        }
        private void RecalcZoomRatio()
        {
            if (_bitmapImage == null || Width <= 0 || Height <= 0)
                return;
            Size imageSize = new Size(_bitmapImage.Width, _bitmapImage.Height);
            float aspectRatio = (float)imageSize.Height / (float)imageSize.Width;
            float clientAspect = (float)Height / (float)Width;

            float ratio;
            if (aspectRatio <= clientAspect)
                ratio = (float)Width / (float)imageSize.Width;
            else
                ratio = (float)Height / (float)imageSize.Height;

            float minZoom = ratio;

            MinZoom = minZoom;

            _curZoom = Math.Max(MinZoom, Math.Min(MaxZoom, ratio));

            Invalidate();

        }
       protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_bitmapImage != null && Canvas != null)
            {
                using (Graphics g = Graphics.FromImage(Canvas))
                {
                    g.Clear(Color.Transparent);
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.DrawImage(_bitmapImage, ImageRect);

                    DrawDiagram(g);

                    e.Graphics.DrawImage(Canvas, 0, 0);
                }
            }
        }
        private void DrawDiagram(Graphics g)
        {
            // 이미지 좌표 → 화면 좌표 변환 후 사각형 그리기
            if (_rectInfos != null)
            {
                foreach (DrawInspectInfo rectInfo in _rectInfos)
                {
                    Color lineColor = Color.LightCoral;
                    if (rectInfo.decision == DecisionType.Defect)
                        lineColor = Color.Red;  // eyevision처럼 불량이면 빨간색
                    else if (rectInfo.decision == DecisionType.Good)
                        lineColor = Color.LightGreen;  //eyevision처럼 양호면 연두색.

                    Rectangle rect = new Rectangle(rectInfo.rect.X, rectInfo.rect.Y, rectInfo.rect.Width, rectInfo.rect.Height);
                    Rectangle screenRect = VirtualToScreen(rect);

                    using (Pen pen = new Pen(lineColor, 2))
                    {
                        if (rectInfo.UseRotatedRect)
                        {
                            PointF[] screenPoints = rectInfo.rotatedPoints
                                                    .Select(p => VirtualToScreen(new PointF(p.X, p.Y))) // 화면 좌표계로 변환
                                                    .ToArray();

                            if (screenPoints.Length == 4)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    g.DrawLine(pen, screenPoints[i], screenPoints[(i + 1) % 4]);
                                    // 시계방향으로 선 연결,  %4 연산은 나머지값. 01,12,23,34 인데 4의 나머지가 0이므로 연결된다.
                                }
                            }
                        }
                        else
                        {
                            g.DrawRectangle(pen, screenRect);
                        }
                    }

                    if (rectInfo.info != "")
                    {
                        float baseFontSize = 20.0f;

                        if (rectInfo.decision == DecisionType.Info)
                        {
                            baseFontSize = 3.0f;
                            lineColor = Color.LightBlue;
                        }

                        float fontSize = baseFontSize * _curZoom;

                        // 스코어 문자열 그리기 (우상단)
                        string infoText = rectInfo.info;
                        PointF textPos = new PointF(screenRect.Left, screenRect.Top); // 위로 약간 띄우기

                        if (rectInfo.inspectType == InspectType.InspBinary
                            && rectInfo.decision != DecisionType.Info)
                        {
                            textPos.Y = screenRect.Bottom - fontSize;
                        }

                        DrawText(g, infoText, textPos, fontSize, lineColor);
                    }
                }
            }
        }
        private void DrawText(Graphics g, string text, PointF position, float fontSize, Color color)
        {
            using (Font font = new Font("Arial", fontSize, FontStyle.Bold))
            // 테두리용 검정색 브러시
            using (Brush outlineBrush = new SolidBrush(Color.Black))
            // 본문용 노란색 브러시
            using (Brush textBrush = new SolidBrush(color))
            {
                // 테두리 효과를 위해 주변 8방향으로 그리기
                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        if (dx == 0 && dy == 0) continue; // 가운데는 제외
                        PointF borderPos = new PointF(position.X + dx, position.Y + dy);
                        g.DrawString(text, font, outlineBrush, borderPos);
                    }
                }

                // 본문 텍스트
                g.DrawString(text, font, textBrush, position);
            }
        }
        private void ImageViewCtrl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
                ZoomMove(_curZoom / _zoomFactor, e.Location);
            else
                ZoomMove(_curZoom * _zoomFactor, e.Location);
            if(_bitmapImage != null)
            {
                ImageRect.Width = _bitmapImage.Width * _curZoom;
                ImageRect.Height = _bitmapImage.Height * _curZoom;
            }
            Invalidate();
        }
        private void ZoomMove(float zoom, Point zoomOrigin)
        {
            PointF virtualOrigin = ScreenToVirtual(new PointF(zoomOrigin.X, zoomOrigin.Y));

            _curZoom = Math.Max(MinZoom, Math.Min(MaxZoom, zoom));
            if (_curZoom <= MinZoom)
                return;
            PointF zoomedOrigin = VirtualToScreen(virtualOrigin);

            float dx = zoomedOrigin.X - zoomOrigin.X;
            float dy = zoomedOrigin.Y - zoomOrigin.Y;

            ImageRect.X -= dx;
            ImageRect.Y -= dy;
        }
        #region 좌표계 변환
        private PointF GetScreenOffset()
        {
            return new PointF(ImageRect.X, ImageRect.Y);
        }
        private Rectangle ScreenToVirtual(Rectangle screenRect)
        {
            PointF offset = GetScreenOffset();
            return new Rectangle(
                (int)((screenRect.X - offset.X) * _curZoom + 0.5f),
                (int)((screenRect.Y - offset.Y) * _curZoom + 0.5f),
                (int)(screenRect.Width * _curZoom + 0.5f),
                (int)(screenRect.Height * _curZoom + 0.5f));
        }
        private Rectangle VirtualToScreen(Rectangle virtualRect)
        {
            PointF offset = GetScreenOffset();
            return new Rectangle(
                (int)((virtualRect.X * _curZoom) + offset.X + 0.5f),
                (int)((virtualRect.Y * _curZoom) + offset.Y + 0.5f),
                (int)(virtualRect.Width * _curZoom + 0.5f),
                (int)(virtualRect.Height * _curZoom + 0.5f));
        }
        private PointF ScreenToVirtual(PointF screenPos)
        {
            PointF offset = GetScreenOffset();
            return new PointF(
                (screenPos.X - offset.X) / _curZoom,
                (screenPos.Y - offset.Y) / _curZoom);
        }
        private PointF VirtualToScreen(PointF virtualPos)
        {
            PointF offset = GetScreenOffset();
            return new PointF(
                (virtualPos.X * _curZoom) + offset.X,
                (virtualPos.Y * _curZoom) + offset.Y);
        }
        #endregion
        private void ImageView_Resize(object sender, EventArgs e)
        {
            ResizeCanvas();
            Invalidate();
        }
        private void ImageViewCtrl_MouseDobuleClik(object sender, MouseEventArgs e)
        {
            FitImageToScreen();
        }
        public void AddRect(List<DrawInspectInfo> rectInfos)
        {
            _rectInfos.AddRange(rectInfos);
            Invalidate();
        }
        public void ResetEntity()
        {
            _rectInfos.Clear();
            Invalidate();
        }


    }
}








