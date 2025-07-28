﻿namespace Project_EgennamJO
{
    partial class AIModuleProp
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnSelAIModel = new System.Windows.Forms.Button();
            this.btnLoadModel = new System.Windows.Forms.Button();
            this.btnInspAI = new System.Windows.Forms.Button();
            this.cbEngineType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(22, 76);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(442, 28);
            this.textBox1.TabIndex = 0;
            // 
            // btnSelAIModel
            // 
            this.btnSelAIModel.Location = new System.Drawing.Point(336, 126);
            this.btnSelAIModel.Name = "btnSelAIModel";
            this.btnSelAIModel.Size = new System.Drawing.Size(115, 46);
            this.btnSelAIModel.TabIndex = 1;
            this.btnSelAIModel.Text = "AI모델 선택";
            this.btnSelAIModel.UseVisualStyleBackColor = true;
            this.btnSelAIModel.Click += new System.EventHandler(this.btnSelAIModel_Click);
            // 
            // btnLoadModel
            // 
            this.btnLoadModel.Location = new System.Drawing.Point(336, 178);
            this.btnLoadModel.Name = "btnLoadModel";
            this.btnLoadModel.Size = new System.Drawing.Size(115, 41);
            this.btnLoadModel.TabIndex = 2;
            this.btnLoadModel.Text = "모델로딩";
            this.btnLoadModel.UseVisualStyleBackColor = true;
            this.btnLoadModel.Click += new System.EventHandler(this.btnLoadModel_Click);
            // 
            // btnInspAI
            // 
            this.btnInspAI.Location = new System.Drawing.Point(336, 242);
            this.btnInspAI.Name = "btnInspAI";
            this.btnInspAI.Size = new System.Drawing.Size(115, 40);
            this.btnInspAI.TabIndex = 3;
            this.btnInspAI.Text = "AI 검사";
            this.btnInspAI.UseVisualStyleBackColor = true;
            this.btnInspAI.Click += new System.EventHandler(this.btnInspAI_Click);
            // 
            // cbEngineType
            // 
            this.cbEngineType.FormattingEnabled = true;
            this.cbEngineType.Items.AddRange(new object[] {
            "IAD",
            "DET",
            "SGE"});
            this.cbEngineType.Location = new System.Drawing.Point(22, 23);
            this.cbEngineType.Name = "cbEngineType";
            this.cbEngineType.Size = new System.Drawing.Size(442, 26);
            this.cbEngineType.TabIndex = 4;
            // 
            // AIModuleProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbEngineType);
            this.Controls.Add(this.btnInspAI);
            this.Controls.Add(this.btnLoadModel);
            this.Controls.Add(this.btnSelAIModel);
            this.Controls.Add(this.textBox1);
            this.Name = "AIModuleProp";
            this.Size = new System.Drawing.Size(521, 469);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSelAIModel;
        private System.Windows.Forms.Button btnLoadModel;
        private System.Windows.Forms.Button btnInspAI;
        private System.Windows.Forms.ComboBox cbEngineType;
    }
}
