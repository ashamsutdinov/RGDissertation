namespace RGUI
{
  partial class TrackPointSlide
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
      this.txtTo = new System.Windows.Forms.TextBox();
      this.slideCurrent = new System.Windows.Forms.TrackBar();
      this.chShowFullTrack = new System.Windows.Forms.CheckBox();
      ((System.ComponentModel.ISupportInitialize)(this.slideCurrent)).BeginInit();
      this.SuspendLayout();
      // 
      // txtTo
      // 
      this.txtTo.Location = new System.Drawing.Point(411, 12);
      this.txtTo.Name = "txtTo";
      this.txtTo.Size = new System.Drawing.Size(100, 20);
      this.txtTo.TabIndex = 1;
      // 
      // slideCurrent
      // 
      this.slideCurrent.Location = new System.Drawing.Point(12, 12);
      this.slideCurrent.Name = "slideCurrent";
      this.slideCurrent.Size = new System.Drawing.Size(393, 45);
      this.slideCurrent.TabIndex = 2;
      // 
      // chShowFullTrack
      // 
      this.chShowFullTrack.AutoSize = true;
      this.chShowFullTrack.Location = new System.Drawing.Point(12, 51);
      this.chShowFullTrack.Name = "chShowFullTrack";
      this.chShowFullTrack.Size = new System.Drawing.Size(160, 17);
      this.chShowFullTrack.TabIndex = 3;
      this.chShowFullTrack.Text = "Отобразить весь маршрут";
      this.chShowFullTrack.UseVisualStyleBackColor = true;
      // 
      // TrackPointSlide
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(523, 80);
      this.Controls.Add(this.chShowFullTrack);
      this.Controls.Add(this.slideCurrent);
      this.Controls.Add(this.txtTo);
      this.Name = "TrackPointSlide";
      this.Text = "TrackPointSlide";
      ((System.ComponentModel.ISupportInitialize)(this.slideCurrent)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtTo;
    private System.Windows.Forms.TrackBar slideCurrent;
    private System.Windows.Forms.CheckBox chShowFullTrack;
  }
}