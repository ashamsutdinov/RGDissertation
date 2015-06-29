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
            this.btnChangeMode = new System.Windows.Forms.Button();
            this.rbZoomMode = new System.Windows.Forms.RadioButton();
            this.rbBoundaryAnalisysMode = new System.Windows.Forms.RadioButton();
            this.pnlMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMode
            // 
            this.pnlMode.Controls.Add(this.rbBoundaryAnalisysMode);
            this.pnlMode.Controls.Add(this.rbZoomMode);
            this.pnlMode.Location = new System.Drawing.Point(12, 13);
            this.pnlMode.Name = "pnlMode";
            this.pnlMode.Size = new System.Drawing.Size(353, 120);
            this.pnlMode.TabIndex = 0;
            // 
            // btnChangeMode
            // 
            this.btnChangeMode.Location = new System.Drawing.Point(13, 164);
            this.btnChangeMode.Name = "btnChangeMode";
            this.btnChangeMode.Size = new System.Drawing.Size(352, 23);
            this.btnChangeMode.TabIndex = 1;
            this.btnChangeMode.Text = "Change mode";
            this.btnChangeMode.UseVisualStyleBackColor = true;
            this.btnChangeMode.Click += new System.EventHandler(this.OnChangeMode);
            // 
            // rbZoomMode
            // 
            this.rbZoomMode.AutoSize = true;
            this.rbZoomMode.Location = new System.Drawing.Point(16, 15);
            this.rbZoomMode.Name = "rbZoomMode";
            this.rbZoomMode.Size = new System.Drawing.Size(104, 21);
            this.rbZoomMode.TabIndex = 0;
            this.rbZoomMode.TabStop = true;
            this.rbZoomMode.Text = "Zoom Mode";
            this.rbZoomMode.UseVisualStyleBackColor = true;
            // 
            // rbBoundaryAnalisysMode
            // 
            this.rbBoundaryAnalisysMode.AutoSize = true;
            this.rbBoundaryAnalisysMode.Location = new System.Drawing.Point(16, 43);
            this.rbBoundaryAnalisysMode.Name = "rbBoundaryAnalisysMode";
            this.rbBoundaryAnalisysMode.Size = new System.Drawing.Size(185, 21);
            this.rbBoundaryAnalisysMode.TabIndex = 1;
            this.rbBoundaryAnalisysMode.TabStop = true;
            this.rbBoundaryAnalisysMode.Text = "Boundary Analisys mode";
            this.rbBoundaryAnalisysMode.UseVisualStyleBackColor = true;
            // 
            // Mode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 216);
            this.Controls.Add(this.btnChangeMode);
            this.Controls.Add(this.pnlMode);
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
    }
}