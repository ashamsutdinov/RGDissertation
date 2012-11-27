namespace RGUI
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
      this.tbC0 = new System.Windows.Forms.TextBox();
      this.tbC2 = new System.Windows.Forms.TextBox();
      this.tbC1 = new System.Windows.Forms.TextBox();
      this.cbSelectPoint = new System.Windows.Forms.CheckBox();
      this.lblIterations = new System.Windows.Forms.Label();
      this.tbIterations = new System.Windows.Forms.TextBox();
      this.lblN = new System.Windows.Forms.Label();
      this.tbN = new System.Windows.Forms.TextBox();
      this.lblAlpha = new System.Windows.Forms.Label();
      this.tbAlpha = new System.Windows.Forms.TextBox();
      this.btnOk = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // tbC0
      // 
      this.tbC0.Location = new System.Drawing.Point(12, 22);
      this.tbC0.Name = "tbC0";
      this.tbC0.Size = new System.Drawing.Size(100, 20);
      this.tbC0.TabIndex = 0;
      this.tbC0.Text = "0";
      // 
      // tbC2
      // 
      this.tbC2.Location = new System.Drawing.Point(224, 22);
      this.tbC2.Name = "tbC2";
      this.tbC2.Size = new System.Drawing.Size(101, 20);
      this.tbC2.TabIndex = 1;
      this.tbC2.Text = "0";
      // 
      // tbC1
      // 
      this.tbC1.Location = new System.Drawing.Point(118, 22);
      this.tbC1.Name = "tbC1";
      this.tbC1.Size = new System.Drawing.Size(100, 20);
      this.tbC1.TabIndex = 2;
      this.tbC1.Text = "-1";
      // 
      // cbSelectPoint
      // 
      this.cbSelectPoint.AutoSize = true;
      this.cbSelectPoint.Location = new System.Drawing.Point(12, 48);
      this.cbSelectPoint.Name = "cbSelectPoint";
      this.cbSelectPoint.Size = new System.Drawing.Size(126, 17);
      this.cbSelectPoint.TabIndex = 3;
      this.cbSelectPoint.Text = "Select point manually";
      this.cbSelectPoint.UseVisualStyleBackColor = true;
      this.cbSelectPoint.CheckedChanged += new System.EventHandler(this.CbSelectPointCheckedChanged);
      // 
      // lblIterations
      // 
      this.lblIterations.AutoSize = true;
      this.lblIterations.Location = new System.Drawing.Point(12, 86);
      this.lblIterations.Name = "lblIterations";
      this.lblIterations.Size = new System.Drawing.Size(53, 13);
      this.lblIterations.TabIndex = 4;
      this.lblIterations.Text = "Iterations:";
      // 
      // tbIterations
      // 
      this.tbIterations.Location = new System.Drawing.Point(118, 83);
      this.tbIterations.Name = "tbIterations";
      this.tbIterations.Size = new System.Drawing.Size(100, 20);
      this.tbIterations.TabIndex = 5;
      this.tbIterations.Text = "25";
      // 
      // lblN
      // 
      this.lblN.AutoSize = true;
      this.lblN.Location = new System.Drawing.Point(12, 112);
      this.lblN.Name = "lblN";
      this.lblN.Size = new System.Drawing.Size(18, 13);
      this.lblN.TabIndex = 6;
      this.lblN.Text = "N:";
      // 
      // tbN
      // 
      this.tbN.Location = new System.Drawing.Point(118, 109);
      this.tbN.Name = "tbN";
      this.tbN.Size = new System.Drawing.Size(100, 20);
      this.tbN.TabIndex = 7;
      this.tbN.Text = "2";
      // 
      // lblAlpha
      // 
      this.lblAlpha.AutoSize = true;
      this.lblAlpha.Location = new System.Drawing.Point(12, 138);
      this.lblAlpha.Name = "lblAlpha";
      this.lblAlpha.Size = new System.Drawing.Size(37, 13);
      this.lblAlpha.TabIndex = 8;
      this.lblAlpha.Text = "Alpha:";
      // 
      // tbAlpha
      // 
      this.tbAlpha.Location = new System.Drawing.Point(118, 135);
      this.tbAlpha.Name = "tbAlpha";
      this.tbAlpha.Size = new System.Drawing.Size(100, 20);
      this.tbAlpha.TabIndex = 9;
      this.tbAlpha.Text = "2";
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(12, 188);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(100, 23);
      this.btnOk.TabIndex = 10;
      this.btnOk.Text = "OK";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new System.EventHandler(this.BtnOkClick);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(118, 188);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(100, 23);
      this.btnCancel.TabIndex = 11;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
      // 
      // TrackPoint
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(337, 223);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.tbAlpha);
      this.Controls.Add(this.lblAlpha);
      this.Controls.Add(this.tbN);
      this.Controls.Add(this.lblN);
      this.Controls.Add(this.tbIterations);
      this.Controls.Add(this.lblIterations);
      this.Controls.Add(this.cbSelectPoint);
      this.Controls.Add(this.tbC1);
      this.Controls.Add(this.tbC2);
      this.Controls.Add(this.tbC0);
      this.Name = "TrackPoint";
      this.Text = "TrackPoint";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox tbC0;
    private System.Windows.Forms.TextBox tbC2;
    private System.Windows.Forms.TextBox tbC1;
    private System.Windows.Forms.CheckBox cbSelectPoint;
    private System.Windows.Forms.Label lblIterations;
    private System.Windows.Forms.TextBox tbIterations;
    private System.Windows.Forms.Label lblN;
    private System.Windows.Forms.TextBox tbN;
    private System.Windows.Forms.Label lblAlpha;
    private System.Windows.Forms.TextBox tbAlpha;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Button btnCancel;
  }
}