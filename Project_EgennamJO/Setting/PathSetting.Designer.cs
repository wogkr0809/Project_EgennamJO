namespace Project_EgennamJO.Setting
{
    partial class PathSetting
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
            this.lbModeDir = new System.Windows.Forms.Label();
            this.IbImageDir = new System.Windows.Forms.Label();
            this.txtModelDir = new System.Windows.Forms.TextBox();
            this.txtImageDir = new System.Windows.Forms.TextBox();
            this.btnSelModeDir = new System.Windows.Forms.Button();
            this.btnSelImageDir = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbModeDir
            // 
            this.lbModeDir.AutoSize = true;
            this.lbModeDir.Location = new System.Drawing.Point(18, 33);
            this.lbModeDir.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbModeDir.Name = "lbModeDir";
            this.lbModeDir.Size = new System.Drawing.Size(72, 15);
            this.lbModeDir.TabIndex = 0;
            this.lbModeDir.Text = "모델 경로";
            // 
            // IbImageDir
            // 
            this.IbImageDir.AutoSize = true;
            this.IbImageDir.Location = new System.Drawing.Point(10, 66);
            this.IbImageDir.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.IbImageDir.Name = "IbImageDir";
            this.IbImageDir.Size = new System.Drawing.Size(87, 15);
            this.IbImageDir.TabIndex = 1;
            this.IbImageDir.Text = "이미지 경로";
            // 
            // txtModelDir
            // 
            this.txtModelDir.Location = new System.Drawing.Point(98, 25);
            this.txtModelDir.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtModelDir.Name = "txtModelDir";
            this.txtModelDir.Size = new System.Drawing.Size(200, 25);
            this.txtModelDir.TabIndex = 2;
            // 
            // txtImageDir
            // 
            this.txtImageDir.Location = new System.Drawing.Point(98, 61);
            this.txtImageDir.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtImageDir.Name = "txtImageDir";
            this.txtImageDir.Size = new System.Drawing.Size(200, 25);
            this.txtImageDir.TabIndex = 3;
            // 
            // btnSelModeDir
            // 
            this.btnSelModeDir.Location = new System.Drawing.Point(315, 23);
            this.btnSelModeDir.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSelModeDir.Name = "btnSelModeDir";
            this.btnSelModeDir.Size = new System.Drawing.Size(52, 23);
            this.btnSelModeDir.TabIndex = 4;
            this.btnSelModeDir.Text = "...";
            this.btnSelModeDir.UseVisualStyleBackColor = true;
            this.btnSelModeDir.Click += new System.EventHandler(this.btnSelModeDir_Click);
            // 
            // btnSelImageDir
            // 
            this.btnSelImageDir.Location = new System.Drawing.Point(315, 61);
            this.btnSelImageDir.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSelImageDir.Name = "btnSelImageDir";
            this.btnSelImageDir.Size = new System.Drawing.Size(52, 25);
            this.btnSelImageDir.TabIndex = 5;
            this.btnSelImageDir.Text = "...";
            this.btnSelImageDir.UseVisualStyleBackColor = true;
            this.btnSelImageDir.Click += new System.EventHandler(this.btnSelImageDir_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(306, 102);
            this.btnApply.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(71, 32);
            this.btnApply.TabIndex = 6;
            this.btnApply.Text = "적용";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // PathSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnSelImageDir);
            this.Controls.Add(this.btnSelModeDir);
            this.Controls.Add(this.txtImageDir);
            this.Controls.Add(this.txtModelDir);
            this.Controls.Add(this.IbImageDir);
            this.Controls.Add(this.lbModeDir);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "PathSetting";
            this.Size = new System.Drawing.Size(401, 155);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbModeDir;
        private System.Windows.Forms.Label IbImageDir;
        private System.Windows.Forms.TextBox txtModelDir;
        private System.Windows.Forms.TextBox txtImageDir;
        private System.Windows.Forms.Button btnSelModeDir;
        private System.Windows.Forms.Button btnSelImageDir;
        private System.Windows.Forms.Button btnApply;
    }
}
