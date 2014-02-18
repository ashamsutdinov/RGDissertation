namespace RGPlanParabolas
{
    partial class MainForm
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
            this._plot = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this._plot)).BeginInit();
            this.SuspendLayout();
            // 
            // _plot
            // 
            this._plot.Location = new System.Drawing.Point(12, 12);
            this._plot.Name = "_plot";
            this._plot.Size = new System.Drawing.Size(758, 729);
            this._plot.TabIndex = 0;
            this._plot.TabStop = false;
            this._plot.MouseClick += new System.Windows.Forms.MouseEventHandler(this._plot_MouseClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 753);
            this.Controls.Add(this._plot);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this._plot)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox _plot;
    }
}

