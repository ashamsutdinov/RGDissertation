﻿namespace RgLib
{
  partial class TrackPoint
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
      this.trackBar = new System.Windows.Forms.TrackBar();
      ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
      this.SuspendLayout();
      // 
      // trackBar
      // 
      this.trackBar.Location = new System.Drawing.Point(12, 12);
      this.trackBar.Name = "trackBar";
      this.trackBar.Size = new System.Drawing.Size(485, 45);
      this.trackBar.TabIndex = 0;
      this.trackBar.ValueChanged += new System.EventHandler(this.TrackBarValueChanged);
      // 
      // TrackPoint
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(509, 63);
      this.Controls.Add(this.trackBar);
      this.Name = "TrackPoint";
      this.Text = "TrackPoint";
      ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TrackBar trackBar;
  }
}