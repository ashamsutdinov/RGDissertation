namespace RGLines
{
    partial class RGLines
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
      this.pictureBox = new System.Windows.Forms.PictureBox();
      this.rgParams = new System.Windows.Forms.GroupBox();
      this.bnApplyRSettings = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.txtAcc = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.txtAlpha = new System.Windows.Forms.TextBox();
      this.txtN = new System.Windows.Forms.TextBox();
      this.lineParams = new System.Windows.Forms.GroupBox();
      this.btnAppyLineSettings = new System.Windows.Forms.Button();
      this.label8 = new System.Windows.Forms.Label();
      this.txtRStep = new System.Windows.Forms.TextBox();
      this.btnDefB = new System.Windows.Forms.Button();
      this.label7 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.txtRMax = new System.Windows.Forms.TextBox();
      this.txtRMin = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.txtLineB = new System.Windows.Forms.TextBox();
      this.txtLineA = new System.Windows.Forms.TextBox();
      this.trackLine = new System.Windows.Forms.TrackBar();
      this.txtStepR = new System.Windows.Forms.TextBox();
      this.btnBack = new System.Windows.Forms.Button();
      this.btnNext = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
      this.rgParams.SuspendLayout();
      this.lineParams.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trackLine)).BeginInit();
      this.SuspendLayout();
      // 
      // pictureBox
      // 
      this.pictureBox.Location = new System.Drawing.Point(12, 12);
      this.pictureBox.Name = "pictureBox";
      this.pictureBox.Size = new System.Drawing.Size(900, 900);
      this.pictureBox.TabIndex = 0;
      this.pictureBox.TabStop = false;
      // 
      // rgParams
      // 
      this.rgParams.Controls.Add(this.bnApplyRSettings);
      this.rgParams.Controls.Add(this.label3);
      this.rgParams.Controls.Add(this.txtAcc);
      this.rgParams.Controls.Add(this.label2);
      this.rgParams.Controls.Add(this.label1);
      this.rgParams.Controls.Add(this.txtAlpha);
      this.rgParams.Controls.Add(this.txtN);
      this.rgParams.Location = new System.Drawing.Point(980, 12);
      this.rgParams.Name = "rgParams";
      this.rgParams.Size = new System.Drawing.Size(272, 150);
      this.rgParams.TabIndex = 1;
      this.rgParams.TabStop = false;
      this.rgParams.Text = "RG Params";
      // 
      // bnApplyRSettings
      // 
      this.bnApplyRSettings.Location = new System.Drawing.Point(9, 121);
      this.bnApplyRSettings.Name = "bnApplyRSettings";
      this.bnApplyRSettings.Size = new System.Drawing.Size(257, 23);
      this.bnApplyRSettings.TabIndex = 6;
      this.bnApplyRSettings.Text = "Apply";
      this.bnApplyRSettings.UseVisualStyleBackColor = true;
      this.bnApplyRSettings.Click += new System.EventHandler(this.BnApplyClick);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(6, 78);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(26, 13);
      this.label3.TabIndex = 5;
      this.label3.Text = "Acc";
      // 
      // txtAcc
      // 
      this.txtAcc.Location = new System.Drawing.Point(56, 71);
      this.txtAcc.Name = "txtAcc";
      this.txtAcc.Size = new System.Drawing.Size(210, 20);
      this.txtAcc.TabIndex = 4;
      this.txtAcc.Text = "0,00000001";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 52);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(34, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "Alpha";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(6, 26);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(15, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "N";
      // 
      // txtAlpha
      // 
      this.txtAlpha.Location = new System.Drawing.Point(56, 45);
      this.txtAlpha.Name = "txtAlpha";
      this.txtAlpha.Size = new System.Drawing.Size(210, 20);
      this.txtAlpha.TabIndex = 1;
      this.txtAlpha.Text = "1,7";
      // 
      // txtN
      // 
      this.txtN.Location = new System.Drawing.Point(56, 19);
      this.txtN.Name = "txtN";
      this.txtN.Size = new System.Drawing.Size(210, 20);
      this.txtN.TabIndex = 0;
      this.txtN.Text = "2";
      // 
      // lineParams
      // 
      this.lineParams.Controls.Add(this.btnAppyLineSettings);
      this.lineParams.Controls.Add(this.label8);
      this.lineParams.Controls.Add(this.txtRStep);
      this.lineParams.Controls.Add(this.btnDefB);
      this.lineParams.Controls.Add(this.label7);
      this.lineParams.Controls.Add(this.label4);
      this.lineParams.Controls.Add(this.txtRMax);
      this.lineParams.Controls.Add(this.txtRMin);
      this.lineParams.Controls.Add(this.label5);
      this.lineParams.Controls.Add(this.label6);
      this.lineParams.Controls.Add(this.txtLineB);
      this.lineParams.Controls.Add(this.txtLineA);
      this.lineParams.Location = new System.Drawing.Point(980, 168);
      this.lineParams.Name = "lineParams";
      this.lineParams.Size = new System.Drawing.Size(272, 244);
      this.lineParams.TabIndex = 7;
      this.lineParams.TabStop = false;
      this.lineParams.Text = "Line Params";
      // 
      // btnAppyLineSettings
      // 
      this.btnAppyLineSettings.Location = new System.Drawing.Point(9, 215);
      this.btnAppyLineSettings.Name = "btnAppyLineSettings";
      this.btnAppyLineSettings.Size = new System.Drawing.Size(257, 23);
      this.btnAppyLineSettings.TabIndex = 7;
      this.btnAppyLineSettings.Text = "Apply";
      this.btnAppyLineSettings.UseVisualStyleBackColor = true;
      this.btnAppyLineSettings.Click += new System.EventHandler(this.BtnAppyLineSettingsClick);
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(6, 176);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(37, 13);
      this.label8.TabIndex = 10;
      this.label8.Text = "RStep";
      // 
      // txtRStep
      // 
      this.txtRStep.Location = new System.Drawing.Point(56, 169);
      this.txtRStep.Name = "txtRStep";
      this.txtRStep.Size = new System.Drawing.Size(210, 20);
      this.txtRStep.TabIndex = 9;
      this.txtRStep.Text = "0,01";
      // 
      // btnDefB
      // 
      this.btnDefB.Location = new System.Drawing.Point(56, 71);
      this.btnDefB.Name = "btnDefB";
      this.btnDefB.Size = new System.Drawing.Size(210, 23);
      this.btnDefB.TabIndex = 8;
      this.btnDefB.Text = "Default \'B\' value";
      this.btnDefB.UseVisualStyleBackColor = true;
      this.btnDefB.Click += new System.EventHandler(this.BtnDefBClick);
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(6, 150);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(35, 13);
      this.label7.TabIndex = 7;
      this.label7.Text = "RMax";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(6, 124);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(32, 13);
      this.label4.TabIndex = 6;
      this.label4.Text = "RMin";
      // 
      // txtRMax
      // 
      this.txtRMax.Location = new System.Drawing.Point(56, 143);
      this.txtRMax.Name = "txtRMax";
      this.txtRMax.Size = new System.Drawing.Size(210, 20);
      this.txtRMax.TabIndex = 5;
      this.txtRMax.Text = "100";
      // 
      // txtRMin
      // 
      this.txtRMin.Location = new System.Drawing.Point(56, 117);
      this.txtRMin.Name = "txtRMin";
      this.txtRMin.Size = new System.Drawing.Size(210, 20);
      this.txtRMin.TabIndex = 4;
      this.txtRMin.Text = "-100";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(6, 52);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(14, 13);
      this.label5.TabIndex = 3;
      this.label5.Text = "B";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(6, 26);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(14, 13);
      this.label6.TabIndex = 2;
      this.label6.Text = "A";
      // 
      // txtLineB
      // 
      this.txtLineB.Location = new System.Drawing.Point(56, 45);
      this.txtLineB.Name = "txtLineB";
      this.txtLineB.Size = new System.Drawing.Size(210, 20);
      this.txtLineB.TabIndex = 1;
      this.txtLineB.Text = "0";
      // 
      // txtLineA
      // 
      this.txtLineA.Location = new System.Drawing.Point(56, 19);
      this.txtLineA.Name = "txtLineA";
      this.txtLineA.Size = new System.Drawing.Size(210, 20);
      this.txtLineA.TabIndex = 0;
      this.txtLineA.Text = "0";
      // 
      // trackLine
      // 
      this.trackLine.Location = new System.Drawing.Point(980, 468);
      this.trackLine.Name = "trackLine";
      this.trackLine.Size = new System.Drawing.Size(272, 45);
      this.trackLine.TabIndex = 8;
      this.trackLine.ValueChanged += new System.EventHandler(this.TrackLineValueChanged);
      // 
      // txtStepR
      // 
      this.txtStepR.Enabled = false;
      this.txtStepR.Location = new System.Drawing.Point(980, 418);
      this.txtStepR.Name = "txtStepR";
      this.txtStepR.Size = new System.Drawing.Size(135, 20);
      this.txtStepR.TabIndex = 9;
      // 
      // btnBack
      // 
      this.btnBack.Location = new System.Drawing.Point(1121, 415);
      this.btnBack.Name = "btnBack";
      this.btnBack.Size = new System.Drawing.Size(50, 23);
      this.btnBack.TabIndex = 10;
      this.btnBack.Text = "<-";
      this.btnBack.UseVisualStyleBackColor = true;
      this.btnBack.Click += new System.EventHandler(this.BtnBackClick);
      // 
      // btnNext
      // 
      this.btnNext.Location = new System.Drawing.Point(1177, 415);
      this.btnNext.Name = "btnNext";
      this.btnNext.Size = new System.Drawing.Size(50, 23);
      this.btnNext.TabIndex = 11;
      this.btnNext.Text = "->";
      this.btnNext.UseVisualStyleBackColor = true;
      this.btnNext.Click += new System.EventHandler(this.BtnNextClick);
      // 
      // RGLines
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1264, 932);
      this.Controls.Add(this.btnNext);
      this.Controls.Add(this.btnBack);
      this.Controls.Add(this.txtStepR);
      this.Controls.Add(this.trackLine);
      this.Controls.Add(this.lineParams);
      this.Controls.Add(this.rgParams);
      this.Controls.Add(this.pictureBox);
      this.MaximizeBox = false;
      this.Name = "RGLines";
      this.Text = "RG Lines";
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
      this.rgParams.ResumeLayout(false);
      this.rgParams.PerformLayout();
      this.lineParams.ResumeLayout(false);
      this.lineParams.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.trackLine)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.GroupBox rgParams;
        private System.Windows.Forms.Button bnApplyRSettings;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAcc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAlpha;
        private System.Windows.Forms.TextBox txtN;
        private System.Windows.Forms.GroupBox lineParams;
        private System.Windows.Forms.Button btnDefB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRMax;
        private System.Windows.Forms.TextBox txtRMin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLineB;
        private System.Windows.Forms.TextBox txtLineA;
        private System.Windows.Forms.Button btnAppyLineSettings;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtRStep;
        private System.Windows.Forms.TrackBar trackLine;
        private System.Windows.Forms.TextBox txtStepR;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnNext;
    }
}

