using Project_EgennamJO.Alogrithm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_EgennamJO.Property
{
    public enum ShowBinaryMode : int
    {
        ShowBinaryNone = 0,
        ShowBinaryHighlightRed,
        ShowBinaryHighlightGreen,
        ShowBinaryHighlightBlue,
        ShowBinaryOnly

    }
    public partial class BinaryProp : UserControl
    {
        public event EventHandler<EventArgs> PropertyChanged;

        public event EventHandler<RangeChangedEventArgs> RangeChange;

        BlobAlgorithm _blobAlgo = null;

        public int LeftValue => binRangeTrackbar.ValueLeft;
        public int RightValue => binRangeTrackbar.ValueRight;


        public BinaryProp()
        {
            InitializeComponent();

            binRangeTrackbar.RangeChanged += Range_RangeChanged;

            binRangeTrackbar.ValueLeft = 0;
            binRangeTrackbar.ValueRight = 128;

            cbHightlight.Items.Add("사용안함");
            cbHightlight.Items.Add("빨간색");
            cbHightlight.Items.Add("녹색");
            cbHightlight.Items.Add("파란색");
            cbHightlight.Items.Add("흑백");
            cbHightlight.SelectedIndex = 0;
        }
        public void SetAlgorithm(BlobAlgorithm bloAlgo)
        {
            _blobAlgo = bloAlgo;
            SetProperty();
        }
        public void SetProperty()
        {
            if (_blobAlgo is null)
                return;
            chkUse.Checked = _blobAlgo.IsUse;
            BinaryThreshold threshold = _blobAlgo.BinThreshold;

            if (threshold.invert)
            {
                binRangeTrackbar.SetThreshold(threshold.upper, threshold.lower);

            }
            else
            {
                binRangeTrackbar.SetThreshold(threshold.lower, threshold.upper);

            }
        }
        public void GetProperty()
        {
            if (_blobAlgo is null)
                return;
            _blobAlgo.IsUse = chkUse.Checked;

            BinaryThreshold threshold = new BinaryThreshold();
            int leftValue = LeftValue;
            int rightValue = RightValue;
            if (LeftValue < RightValue)
            {
                threshold.lower = leftValue;
                threshold.upper = rightValue;
                threshold.invert = false;
            }
            else
            {
                threshold.lower = rightValue;
                threshold.upper = leftValue;
                threshold.invert = true;
            }
            _blobAlgo.BinThreshold = threshold;
        }
        private void UpdateBinary()
        {
            GetProperty();
            int leftValue = LeftValue;
            int rightValue = RightValue;
            bool invert = false;

            if (leftValue > rightValue)
            {
                leftValue = RightValue;
                rightValue = LeftValue;
                invert = true;
            }
            ShowBinaryMode showBinaryMode = (ShowBinaryMode)cbHightlight.SelectedIndex;
            RangeChange?.Invoke(this, new RangeChangedEventArgs(leftValue, rightValue, invert, showBinaryMode));
        }
        private void Range_RangeChanged(object sender, EventArgs e)
        {
            UpdateBinary();
        }
        private void chkUse_CheckedChanged(object sender, EventArgs e)
        {
            bool useBinary = chkUse.Checked;
            grpBinary.Enabled = useBinary;

            GetProperty();
        }
        private void cbHighlight_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateBinary();
        }
    }
    public class RangeChangedEventArgs : EventArgs
    {
        public int LowerValue { get; }
        public int UpperValue { get; }
        public bool Invert { get; }
        public ShowBinaryMode ShowBinMode { get; }

        public RangeChangedEventArgs(int lowerValue, int upperValue, bool invert, ShowBinaryMode showBinMode)
        {
            LowerValue = lowerValue;
            UpperValue = upperValue;
            Invert = invert;
            ShowBinMode = showBinMode;
        }
    }

}
