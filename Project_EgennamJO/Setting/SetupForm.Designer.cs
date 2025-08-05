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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(588, 323);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage
            // 
            this.tabPage.Controls.Add(this.btnApply);
            this.tabPage.Controls.Add(this.cbCameralist);
            this.tabPage.Controls.Add(this.camera);
            this.tabPage.Location = new System.Drawing.Point(4, 28);
            this.tabPage.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage.Name = "tabPage";
            this.tabPage.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage.Size = new System.Drawing.Size(580, 291);
            this.tabPage.TabIndex = 0;
            this.tabPage.Text = "Camera";
            this.tabPage.UseVisualStyleBackColor = true;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(355, 172);
            this.btnApply.Margin = new System.Windows.Forms.Padding(4);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(132, 53);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "적용";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // cbCameralist
            // 
            this.cbCameralist.FormattingEnabled = true;
            this.cbCameralist.Location = new System.Drawing.Point(178, 55);
            this.cbCameralist.Margin = new System.Windows.Forms.Padding(4);
            this.cbCameralist.Name = "cbCameralist";
            this.cbCameralist.Size = new System.Drawing.Size(244, 26);
            this.cbCameralist.TabIndex = 1;
            // 
            // camera
            // 
            this.camera.AutoSize = true;
            this.camera.Location = new System.Drawing.Point(61, 59);
            this.camera.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.camera.Name = "camera";
            this.camera.Size = new System.Drawing.Size(104, 18);
            this.camera.TabIndex = 0;
            this.camera.Text = "카메라 종류";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(580, 291);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Path";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // SetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 323);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.TabPage tabPage2;
    }
}