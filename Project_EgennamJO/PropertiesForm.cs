﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using WeifenLuo.WinFormsUI.ThemeVS2015;
using Project_EgennamJO.Property;
using Project_EgennamJO.Alogrithm;
using Project_EgennamJO.Core;
using Project_EgennamJO.Teach;

namespace Project_EgennamJO
{

    public partial class PropertiesForm : DockContent
    {
        private Dictionary<string, TabPage> _allTabs = new Dictionary<string, TabPage>();

        public PropertiesForm()
        {
            InitializeComponent();

        }
        private void LoadOptionControl(InspectType propType)
        {
            string tabName = propType.ToString();

            foreach (TabPage tabPage in tabPropControl.TabPages)
            {
                if (tabPage.Text == tabName)
                    return;

            }

            if (_allTabs.TryGetValue(tabName, out TabPage page))
            {
                tabPropControl.TabPages.Add(page);
                return;
            }

            UserControl _inspProp = CreateUserControl(propType);
            if (_inspProp == null)
                return;

            TabPage newTab = new TabPage(tabName)
            {
                Dock = DockStyle.Fill
            };
            _inspProp.Dock = DockStyle.Fill;
            newTab.Controls.Add(_inspProp);
            tabPropControl.TabPages.Add(newTab);
            tabPropControl.SelectedTab = newTab;

            _allTabs[tabName] = newTab;
        }
        private UserControl CreateUserControl(InspectType inspPropType)
        {
            UserControl curProp = null;
            switch (inspPropType)
            {
                case InspectType.InspBinary:
                    BinaryProp blobProp = new BinaryProp();

                    //#7_BINARY_PREVIEW#8 이진화 속성 변경시 발생하는 이벤트 추가
                    blobProp.RangeChanged += RangeSlider_RangeChanged;

                    //#18_IMAGE_CHANNEL#13 이미지 채널 변경시 이벤트 추가
                    blobProp.ImageChannelChanged += ImageChannelChanged;
                    curProp = blobProp;
                    break;
                //#11_MATCHING#5 패턴매칭 속성창 추가
                case InspectType.InspMatch:
                    MatchInspProp matchProp = new MatchInspProp();
                    curProp = matchProp;
                    break;
                case InspectType.InspFilter:
                    ImageFilterProp filterProp = new ImageFilterProp();
                    curProp = filterProp;
                    break;
                case InspectType.InspAIModule:
                    AIModuleProp aiModuleProp = new AIModuleProp();
                    curProp = aiModuleProp;
                    break;
                default:
                    MessageBox.Show("유효하지 않은 옵션입니다.");
                    return null;
            }
            return curProp;
        }

        public void ShowProperty(InspWindow window)
        {
            foreach (InspAlgorithm algo in window.AlgorithmList)
            {
                LoadOptionControl(algo.InspectType);
            }
        }
        public void ResetProperty()
        {
            tabPropControl.TabPages.Clear();
        }

        public void UpdateProperty(InspWindow window)
        {
            if (window is null)
                return;

            foreach (TabPage tabPage in tabPropControl.TabPages)
            {
                if (tabPage.Controls.Count > 0)
                {
                    UserControl uc = tabPage.Controls[0] as UserControl;

                    if (uc is BinaryProp binaryProp)
                    {
                        BlobAlgorithm blobAlgo = (BlobAlgorithm)window.FindInspAlgorithm(InspectType.InspBinary);
                        if (blobAlgo is null)
                            continue;

                        binaryProp.SetAlgorithm(blobAlgo);
                    }
                    else if (uc is MatchInspProp matchProp)
                    {
                        MatchAlgorithm matchAlgo = (MatchAlgorithm)window.FindInspAlgorithm(InspectType.InspMatch);
                        if (matchAlgo is null)
                            continue;

                        window.PatternLearn();

                        matchProp.SetAlgorithm(matchAlgo);
                    }
                }
            }
        }
        private void RangeSlider_RangeChanged(object sender, RangeChangedEventArgs e)
        {
            int lowerValue = e.LowerValue;
            int upperValue = e.UpperValue;
            bool invert = e.Invert;
            ShowBinaryMode showBinMode = e.ShowBinMode;
            Global.Inst.InspStage.PreView?.SetBinary(lowerValue, upperValue, invert, showBinMode);
        }
        private void ImageChannelChanged(object sender, ImageChannelEventArgs e)
        {
            Global.Inst.InspStage.SetPreviewImage(e.Channel);
        }

    }
}


