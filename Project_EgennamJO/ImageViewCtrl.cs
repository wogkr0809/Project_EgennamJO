﻿using System;
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

                    e.Graphics.DrawImage(Canvas, 0, 0);
                }
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
        private void ImageView_Resize(object sender, EventArgs e)
        {
            ResizeCanvas();
            Invalidate();
        }
        private void ImageViewCtrl_MouseDobuleClik(object sender, MouseEventArgs e)
        {
            FitImageToScreen();
        }

        
    }
}








