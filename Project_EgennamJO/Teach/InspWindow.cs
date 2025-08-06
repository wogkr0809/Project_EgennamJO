﻿using OpenCvSharp;
using Project_EgennamJO.Alogrithm;
using Project_EgennamJO.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_EgennamJO.Teach
{
    public class InspWindow
    {
        public InspWindowType InspWindowType { get; set; }

        public string Name { get; set; }
        public string UID { get; set; }

        public Rect WindowArea { get; set; }
        public Rect InspArea { get; set; }
        public bool IsTeach { get; set; } = false;
        public List<InspAlgorithm> AlgorithmList { get; set; } = new List<InspAlgorithm>();

        public List<Mat> _windowImages = new List<Mat>();
        public void AddWindowImage(Mat image)
        {
            if (image is null)
                return;

            _windowImages.Add(image.Clone());
        }
        public void ResetWindowImages()
        {
            _windowImages.Clear();
        }
        public void SetWindowImage(Mat image, int index)
        {
            if (image is null)
                return;
            if (index < 0 || index >= _windowImages.Count)
                return;
            _windowImages[index] = image.Clone();
        }
        public void DelWindowImage(int index)
        {
            if (index < 0 || index >= _windowImages.Count)
                return;
            _windowImages.RemoveAt(index);

            IsPatternLearn = false;
            PatternLearn();
        }
        public bool IsPatternLearn { get; set; } = false;

        public InspWindow()
        {

        }
        public InspWindow(InspWindowType windowType, string name)
        {
            InspWindowType = windowType;
            Name = name;
        }

        public InspWindow Clone(OpenCvSharp.Point offset, bool includeChildren = true)
        {
            InspWindow cloneWindow = InspWindowFactory.Inst.Create(this.InspWindowType, false);
            cloneWindow.WindowArea = this.WindowArea + offset;
            cloneWindow.IsTeach = false;

            foreach (InspAlgorithm algo in AlgorithmList)
            {
                var cloneAlgo = algo.Clone();
                cloneWindow.AlgorithmList.Add(cloneAlgo);
            }

            return cloneWindow;
        }
        public bool PatternLearn()
        {
            if (IsPatternLearn == true)
                return true;

            foreach (var algorithm in AlgorithmList)
            {
                if (algorithm.InspectType != InspectType.InspMatch)
                    continue;
                MatchAlgorithm matchAlgo = (MatchAlgorithm)algorithm;
                matchAlgo.ResetTemplateImages();

                for (int i = 0; i < _windowImages.Count; i++)
                {
                    Mat tempImage = _windowImages[i];
                    if (tempImage is null)
                        continue;

                    if (tempImage.Type() == MatType.CV_8UC3)
                    {
                        Mat grayImage = new Mat();
                        Cv2.CvtColor(tempImage, grayImage, ColorConversionCodes.BGR2GRAY);
                        matchAlgo.AddTemplateImage(grayImage);
                    }
                    else
                    {
                        matchAlgo.AddTemplateImage(tempImage);  
                    }
                }
            }

            IsPatternLearn = true;
            return true;
        }

        //#ABSTRACT ALGORITHM#10 타입에 따라 알고리즘을 추가하는 함수
        public bool AddInspAlgorithm(InspectType inspType)
        {
            InspAlgorithm inspAlgo = null;

            switch (inspType)
            {
                case InspectType.InspBinary:
                    inspAlgo = new BlobAlgorithm();
                    break;
                case InspectType.InspMatch:
                    inspAlgo = new MatchAlgorithm();
                    break;

            }

            if (inspAlgo is null)
                return false;

            AlgorithmList.Add(inspAlgo);

            return true;
        }

        //알고리즘을 리스트로 관리하므로, 필요한 타입의 알고리즘을 찾는 함수
        public InspAlgorithm FindInspAlgorithm(InspectType inspType)
        {
            return AlgorithmList.Find(algo => algo.InspectType == inspType);
        }

        //클래스 내에서, 인자로 입력된 타입의 알고리즘을 검사하거나,
        ///모든 알고리즘을 검사하는 옵션을 가지는 검사 함수
        public virtual bool DoInpsect(InspectType inspType)
        {
            foreach (var inspAlgo in AlgorithmList)
            {
                if (inspAlgo.InspectType == inspType || inspType == InspectType.InspNone)
                    inspAlgo.DoInspect();
            }

            return true;
        }

        public bool IsDefect()
        {
            foreach (InspAlgorithm algo in AlgorithmList)
            {
                if (!algo.IsInspected)
                    continue;

                if (algo.IsDefect)
                    return true;
            }
            return false;
        }

        public virtual bool OffsetMove(OpenCvSharp.Point offset)
        {
            Rect windowRect = WindowArea;
            windowRect.X += offset.X;
            windowRect.Y += offset.Y;
            WindowArea = windowRect;
            return true;
        }

        public bool SetInspOffset(OpenCvSharp.Point offset)
        {
            InspArea = WindowArea + offset;
            AlgorithmList.ForEach(algo => algo.InspRect = algo.TeachRect + offset);
            return true;
        }
    }
}



