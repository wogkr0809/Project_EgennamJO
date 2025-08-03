namespace Project_EgennamJO
{
    partial class SetupForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage = new System.Windows.Forms.TabPage();
            this.btnApply = new System.Windows.Forms.Button();
            this.cbCameralist = new System.Windows.Forms.ComboBox();
            this.camera = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(470, 269);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage
            // 
            this.tabPage.Controls.Add(this.btnApply);
            this.tabPage.Controls.Add(this.cbCameralist);
            this.tabPage.Controls.Add(this.camera);
            this.tabPage.Location = new System.Drawing.Point(4, 25);
            this.tabPage.Name = "tabPage";
            this.tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage.Size = new System.Drawing.Size(462, 240);
            this.tabPage.TabIndex = 0;
            this.tabPage.Text = "Camera";
            this.tabPage.UseVisualStyleBackColor = true;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(284, 143);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(106, 44);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "적용";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // cbCameralist
            // 
            this.cbCameralist.FormattingEnabled = true;
            this.cbCameralist.Location = new System.Drawing.Point(142, 46);
            this.cbCameralist.Name = "cbCameralist";
            this.cbCameralist.Size = new System.Drawing.Size(196, 23);
            this.cbCameralist.TabIndex = 1;
            // 
            // camera
            // 
            this.camera.AutoSize = true;
            this.camera.Location = new System.Drawing.Point(49, 49);
            this.camera.Name = "camera";
            this.camera.Size = new System.Drawing.Size(87, 15);
            this.camera.TabIndex = 0;
            this.camera.Text = "카메라 종류";
            // 
            // SetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 269);
            this.Controls.Add(this.tabControl1);
            this.Name = "SetupForm";
            this.Text = "SetupForm";
            this.tabControl1.ResumeLayout(false);
            this.tabPage.ResumeLayout(false);
            this.tabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.ComboBox cbCameralist;
        private System.Windows.Forms.Label camera;
    }
}