namespace Project_EgennamJO.Property
{
    partial class BinaryProp
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpBinary = new System.Windows.Forms.GroupBox();
            this.binRangeTrackbar = new Project_EgennamJO.UIControl.RangeTrackbar();
            this.cbHightlight = new System.Windows.Forms.ComboBox();
            this.lbHighlight = new System.Windows.Forms.Label();
            this.chkUse = new System.Windows.Forms.CheckBox();
            this.grpBinary.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBinary
            // 
            this.grpBinary.Controls.Add(this.binRangeTrackbar);
            this.grpBinary.Controls.Add(this.cbHightlight);
            this.grpBinary.Controls.Add(this.lbHighlight);
            this.grpBinary.Location = new System.Drawing.Point(29, 60);
            this.grpBinary.Name = "grpBinary";
            this.grpBinary.Size = new System.Drawing.Size(371, 142);
            this.grpBinary.TabIndex = 0;
            this.grpBinary.TabStop = false;
            this.grpBinary.Text = "이진화";
            // 
            // binRangeTrackbar
            // 
            this.binRangeTrackbar.Location = new System.Drawing.Point(19, 27);
            this.binRangeTrackbar.Name = "binRangeTrackbar";
            this.binRangeTrackbar.Size = new System.Drawing.Size(339, 54);
            this.binRangeTrackbar.TabIndex = 2;
            this.binRangeTrackbar.ValueLeft = 80;
            this.binRangeTrackbar.ValueRight = 200;
            this.binRangeTrackbar.RangeChanged += new System.EventHandler(this.Range_RangeChanged);
            // 
            // cbHightlight
            // 
            this.cbHightlight.FormattingEnabled = true;
            this.cbHightlight.Location = new System.Drawing.Point(135, 107);
            this.cbHightlight.Name = "cbHightlight";
            this.cbHightlight.Size = new System.Drawing.Size(211, 26);
            this.cbHightlight.TabIndex = 1;
            this.cbHightlight.SelectedIndexChanged += new System.EventHandler(this.cbHighlight_SelectedIndexChanged);
            // 
            // lbHighlight
            // 
            this.lbHighlight.AutoSize = true;
            this.lbHighlight.Location = new System.Drawing.Point(16, 110);
            this.lbHighlight.Name = "lbHighlight";
            this.lbHighlight.Size = new System.Drawing.Size(98, 18);
            this.lbHighlight.TabIndex = 0;
            this.lbHighlight.Text = "하이라이트";
            // 
            // chkUse
            // 
            this.chkUse.AutoSize = true;
            this.chkUse.Checked = true;
            this.chkUse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUse.Location = new System.Drawing.Point(29, 22);
            this.chkUse.Name = "chkUse";
            this.chkUse.Size = new System.Drawing.Size(70, 22);
            this.chkUse.TabIndex = 1;
            this.chkUse.Text = "검사";
            this.chkUse.UseVisualStyleBackColor = true;
            this.chkUse.CheckedChanged += new System.EventHandler(this.chkUse_CheckedChanged);
            // 
            // BinaryProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkUse);
            this.Controls.Add(this.grpBinary);
            this.Name = "BinaryProp";
            this.Size = new System.Drawing.Size(434, 557);
            this.grpBinary.ResumeLayout(false);
            this.grpBinary.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBinary;
        private System.Windows.Forms.Label lbHighlight;
        private System.Windows.Forms.CheckBox chkUse;
        private System.Windows.Forms.ComboBox cbHightlight;
        private UIControl.RangeTrackbar binRangeTrackbar;
    }
}
