using System.Windows.Forms;

namespace Project_EgennamJO
{
    partial class CameraForm
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
            this.mainViewToolbar = new Project_EgennamJO.UIControl.MainViewToolbar();
            this.imageViewer = new Project_EgennamJO.ImageViewCtrl();
            this.SuspendLayout();
            // 
            // mainViewToolbar
            // 
            this.mainViewToolbar.Dock = System.Windows.Forms.DockStyle.Right;
            this.mainViewToolbar.Location = new System.Drawing.Point(910, 0);
            this.mainViewToolbar.Name = "mainViewToolbar";
            this.mainViewToolbar.Size = new System.Drawing.Size(130, 617);
            this.mainViewToolbar.TabIndex = 1;
            // 
            // imageViewer
            // 
            this.imageViewer.Dock = System.Windows.Forms.DockStyle.Left;
            this.imageViewer.Location = new System.Drawing.Point(0, 0);
            this.imageViewer.Name = "imageViewer";
            this.imageViewer.Size = new System.Drawing.Size(1040, 617);
            this.imageViewer.TabIndex = 0;
            this.imageViewer.WorkingState = "";
            // 
            // CameraForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 617);
            this.Controls.Add(this.mainViewToolbar);
            this.Controls.Add(this.imageViewer);
            this.Name = "CameraForm";
            this.Text = "CameraForm";
            this.Resize += new System.EventHandler(this.CameraForm_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private ImageViewCtrl imageViewer;
        private UIControl.MainViewToolbar mainViewToolbar;
    }
}