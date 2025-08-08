namespace Project_EgennamJO
{
    partial class RunForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RunForm));
            this.btnGrab = new System.Windows.Forms.Button();
            this.runImageList = new System.Windows.Forms.ImageList(this.components);
            this.btnLive = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGrab
            // 
            this.btnGrab.ImageIndex = 0;
            this.btnGrab.ImageList = this.runImageList;
            this.btnGrab.Location = new System.Drawing.Point(11, 46);
            this.btnGrab.Margin = new System.Windows.Forms.Padding(2);
            this.btnGrab.Name = "btnGrab";
            this.btnGrab.Size = new System.Drawing.Size(110, 80);
            this.btnGrab.TabIndex = 0;
            this.btnGrab.Text = "촬상";
            this.btnGrab.UseVisualStyleBackColor = true;
            this.btnGrab.Click += new System.EventHandler(this.btnGrab_Click);
            // 
            // runImageList
            // 
            this.runImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("runImageList.ImageStream")));
            this.runImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.runImageList.Images.SetKeyName(0, "스크린샷 2025-08-08 122042.png");
            this.runImageList.Images.SetKeyName(1, "스크린샷 2025-08-08 122105.png");
            this.runImageList.Images.SetKeyName(2, "스크린샷 2025-08-08 122303.png");
            this.runImageList.Images.SetKeyName(3, "정지.jpg");
            // 
            // btnLive
            // 
            this.btnLive.ImageIndex = 1;
            this.btnLive.ImageList = this.runImageList;
            this.btnLive.Location = new System.Drawing.Point(127, 46);
            this.btnLive.Margin = new System.Windows.Forms.Padding(4);
            this.btnLive.Name = "btnLive";
            this.btnLive.Size = new System.Drawing.Size(103, 80);
            this.btnLive.TabIndex = 1;
            this.btnLive.Text = "LiveMode";
            this.btnLive.UseVisualStyleBackColor = true;
            this.btnLive.Click += new System.EventHandler(this.btnLive_Click);
            // 
            // btnStart
            // 
            this.btnStart.ImageKey = "스크린샷 2025-08-08 122303.png";
            this.btnStart.ImageList = this.runImageList;
            this.btnStart.Location = new System.Drawing.Point(237, 46);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(96, 80);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "검사";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.ImageKey = "정지.jpg";
            this.btnStop.ImageList = this.runImageList;
            this.btnStop.Location = new System.Drawing.Point(351, 46);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(94, 80);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "검사";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // RunForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 170);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnLive);
            this.Controls.Add(this.btnGrab);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "RunForm";
            this.Text = "RunForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGrab;
        private System.Windows.Forms.Button btnLive;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ImageList runImageList;
        private System.Windows.Forms.Button btnStop;
    }
}