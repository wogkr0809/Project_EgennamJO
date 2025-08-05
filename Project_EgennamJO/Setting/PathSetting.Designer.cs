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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnSelModeDir = new System.Windows.Forms.Button();
            this.btnSelImageDir = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbModeDir
            // 
            this.lbModeDir.AutoSize = true;
            this.lbModeDir.Location = new System.Drawing.Point(22, 40);
            this.lbModeDir.Name = "lbModeDir";
            this.lbModeDir.Size = new System.Drawing.Size(86, 18);
            this.lbModeDir.TabIndex = 0;
            this.lbModeDir.Text = "모델 경로";
            // 
            // IbImageDir
            // 
            this.IbImageDir.AutoSize = true;
            this.IbImageDir.Location = new System.Drawing.Point(13, 79);
            this.IbImageDir.Name = "IbImageDir";
            this.IbImageDir.Size = new System.Drawing.Size(104, 18);
            this.IbImageDir.TabIndex = 1;
            this.IbImageDir.Text = "이미지 경로";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(123, 30);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(249, 28);
            this.textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(123, 73);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(249, 28);
            this.textBox2.TabIndex = 3;
            // 
            // btnSelModeDir
            // 
            this.btnSelModeDir.Location = new System.Drawing.Point(394, 28);
            this.btnSelModeDir.Name = "btnSelModeDir";
            this.btnSelModeDir.Size = new System.Drawing.Size(65, 28);
            this.btnSelModeDir.TabIndex = 4;
            this.btnSelModeDir.Text = "...";
            this.btnSelModeDir.UseVisualStyleBackColor = true;
            // 
            // btnSelImageDir
            // 
            this.btnSelImageDir.Location = new System.Drawing.Point(394, 73);
            this.btnSelImageDir.Name = "btnSelImageDir";
            this.btnSelImageDir.Size = new System.Drawing.Size(65, 30);
            this.btnSelImageDir.TabIndex = 5;
            this.btnSelImageDir.Text = "...";
            this.btnSelImageDir.UseVisualStyleBackColor = true;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(383, 122);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(89, 39);
            this.btnApply.TabIndex = 6;
            this.btnApply.Text = "적용";
            this.btnApply.UseVisualStyleBackColor = true;
            // 
            // PathSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnSelImageDir);
            this.Controls.Add(this.btnSelModeDir);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.IbImageDir);
            this.Controls.Add(this.lbModeDir);
            this.Name = "PathSetting";
            this.Size = new System.Drawing.Size(501, 186);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbModeDir;
        private System.Windows.Forms.Label IbImageDir;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btnSelModeDir;
        private System.Windows.Forms.Button btnSelImageDir;
        private System.Windows.Forms.Button btnApply;
    }
}
