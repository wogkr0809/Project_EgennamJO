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
            this.imageViewCtrl = new Project_EgennamJO.ImageViewCtrl();
            this.SuspendLayout();
            // 
            // imageViewCtrl
            // 
            this.imageViewCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageViewCtrl.Location = new System.Drawing.Point(0, 0);
            this.imageViewCtrl.Name = "imageViewCtrl";
            this.imageViewCtrl.Size = new System.Drawing.Size(1040, 617);
            this.imageViewCtrl.TabIndex = 0;
            // 
            // CameraForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 617);
            this.Controls.Add(this.imageViewCtrl);
            this.Name = "CameraForm";
            this.Text = "CameraForm";
            this.ResumeLayout(false);

        }

        #endregion

        private ImageViewCtrl imageViewCtrl;
    }
}