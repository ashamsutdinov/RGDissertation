namespace RGDynApp.UI
{
    partial class Mode
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
            this.pnlMode = new System.Windows.Forms.Panel();
            this.rbBoundaryAnalisysMode = new System.Windows.Forms.RadioButton();
            this.rbZoomMode = new System.Windows.Forms.RadioButton();
            this.btnChangeMode = new System.Windows.Forms.Button();
            this.rbTrackPointMode = new System.Windows.Forms.RadioButton();
            this.pnlMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMode
            // 
            this.pnlMode.Controls.Add(this.rbTrackPointMode);
            this.pnlMode.Controls.Add(this.rbBoundaryAnalisysMode);
            this.pnlMode.Controls.Add(this.rbZoomMode);
            this.pnlMode.Location = new System.Drawing.Point(9, 11);
            this.pnlMode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlMode.Name = "pnlMode";
            this.pnlMode.Size = new System.Drawing.Size(265, 98);
            this.pnlMode.TabIndex = 0;
            // 
            // rbBoundaryAnalisysMode
            // 
            this.rbBoundaryAnalisysMode.AutoSize = true;
            this.rbBoundaryAnalisysMode.Location = new System.Drawing.Point(12, 35);
            this.rbBoundaryAnalisysMode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbBoundaryAnalisysMode.Name = "rbBoundaryAnalisysMode";
            this.rbBoundaryAnalisysMode.Size = new System.Drawing.Size(101, 17);
            this.rbBoundaryAnalisysMode.TabIndex = 1;
            this.rbBoundaryAnalisysMode.TabStop = true;
            this.rbBoundaryAnalisysMode.Text = "Track Boundary";
            this.rbBoundaryAnalisysMode.UseVisualStyleBackColor = true;
            // 
            // rbZoomMode
            // 
            this.rbZoomMode.AutoSize = true;
            this.rbZoomMode.Location = new System.Drawing.Point(12, 12);
            this.rbZoomMode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbZoomMode.Name = "rbZoomMode";
            this.rbZoomMode.Size = new System.Drawing.Size(52, 17);
            this.rbZoomMode.TabIndex = 0;
            this.rbZoomMode.TabStop = true;
            this.rbZoomMode.Text = "Zoom";
            this.rbZoomMode.UseVisualStyleBackColor = true;
            // 
            // btnChangeMode
            // 
            this.btnChangeMode.Location = new System.Drawing.Point(10, 133);
            this.btnChangeMode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnChangeMode.Name = "btnChangeMode";
            this.btnChangeMode.Size = new System.Drawing.Size(264, 19);
            this.btnChangeMode.TabIndex = 1;
            this.btnChangeMode.Text = "Change mode";
            this.btnChangeMode.UseVisualStyleBackColor = true;
            this.btnChangeMode.Click += new System.EventHandler(this.OnChangeMode);
            // 
            // rbTrackPointMode
            // 
            this.rbTrackPointMode.AutoSize = true;
            this.rbTrackPointMode.Location = new System.Drawing.Point(12, 57);
            this.rbTrackPointMode.Name = "rbTrackPointMode";
            this.rbTrackPointMode.Size = new System.Drawing.Size(80, 17);
            this.rbTrackPointMode.TabIndex = 2;
            this.rbTrackPointMode.TabStop = true;
            this.rbTrackPointMode.Text = "Track Point";
            this.rbTrackPointMode.UseVisualStyleBackColor = true;
            // 
            // Mode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 176);
            this.Controls.Add(this.btnChangeMode);
            this.Controls.Add(this.pnlMode);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Mode";
            this.Text = "Mode";
            this.pnlMode.ResumeLayout(false);
            this.pnlMode.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMode;
        private System.Windows.Forms.Button btnChangeMode;
        private System.Windows.Forms.RadioButton rbBoundaryAnalisysMode;
        private System.Windows.Forms.RadioButton rbZoomMode;
        private System.Windows.Forms.RadioButton rbTrackPointMode;
    }
}