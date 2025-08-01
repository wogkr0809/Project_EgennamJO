﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using Project_EgennamJO.Property;

namespace Project_EgennamJO.Core
{
    public class PreviewImage
    {
        private Mat _orinalImage = null;
        private Mat _previewImage = null;
        private bool _usePreview = true;

        public void SetImage(Mat image)
        {
            _orinalImage = image;
            _previewImage = new Mat();
        }

        public void SetBinary(int lowerValue, int uppeValue, bool invert, ShowBinaryMode showBinMode )
        {
            if (_usePreview == false)
                return;
            if (_orinalImage == null)
                return;

            var cameraForm = MainForm.GetDockForm<CameraForm>();
            if(cameraForm == null)
                return;
            Bitmap bmpImage;
            if(showBinMode == ShowBinaryMode.ShowBinaryNone)
            {
                bmpImage = BitmapConverter.ToBitmap(_orinalImage);
                cameraForm.UpdateDisplay(bmpImage);
                return;
            }
            Rect windowArea = new Rect(0, 0, _orinalImage.Width, _orinalImage.Height);

            Mat orgRoi = _orinalImage[windowArea];

            Mat grayImage = new Mat();
            if (orgRoi.Type() == MatType.CV_8UC3)
                Cv2.CvtColor(orgRoi, grayImage, ColorConversionCodes.BGR2GRAY);
            else
                grayImage = orgRoi;
            Mat binaryMask = new Mat();
            Cv2.InRange(grayImage, lowerValue, uppeValue, binaryMask);

            if (invert)
                binaryMask = ~binaryMask;

            Mat fullBinaryMask = Mat.Zeros(_orinalImage.Size(), MatType.CV_8UC1);
            binaryMask.CopyTo(new Mat(fullBinaryMask, windowArea));

            if(showBinMode == ShowBinaryMode.ShowBinaryOnly)
            {
                if(orgRoi.Type() == MatType.CV_8UC3)
                {
                    Mat colorBinary = new Mat();
                    Cv2.CvtColor(binaryMask, colorBinary, ColorConversionCodes.GRAY2BGR);
                    _previewImage = _orinalImage.Clone();
                    colorBinary.CopyTo(new Mat(_previewImage, windowArea));
                }
                else
                {
                    _previewImage = _orinalImage.Clone();
                    binaryMask.CopyTo(new Mat(_previewImage,windowArea));
                }
                bmpImage = BitmapConverter.ToBitmap(_previewImage);
                cameraForm.UpdateDisplay(bmpImage);
                return;
            }
            Scalar highlightColor;
            if (showBinMode == ShowBinaryMode.ShowBinaryHighlightRed)
                highlightColor = new Scalar(0, 0, 255);
            else if (showBinMode == ShowBinaryMode.ShowBinaryHighlightGreen)
                highlightColor = new Scalar(0, 255, 0);
            else 
                highlightColor = new Scalar(255,0,0);

            Mat overlayImage;
            if(_orinalImage.Type() == MatType.CV_8UC1)
            {
                overlayImage = new Mat();
                Cv2.CvtColor(_orinalImage, overlayImage, ColorConversionCodes.GRAY2BGR);

                Mat colorOrinal = overlayImage.Clone();
                
                overlayImage.SetTo(highlightColor, fullBinaryMask);
                Cv2.AddWeighted(colorOrinal, 0.7, overlayImage, 0.3, 0, _previewImage);
            }
            else
            {
                overlayImage = _orinalImage.Clone();
                overlayImage.SetTo(highlightColor, fullBinaryMask);

                Cv2.AddWeighted(_orinalImage, 0.7, overlayImage, 0.3, 0, _previewImage);
            }
            bmpImage = BitmapConverter.ToBitmap(_previewImage);
            cameraForm.UpdateDisplay(bmpImage);
        }
    }
}
