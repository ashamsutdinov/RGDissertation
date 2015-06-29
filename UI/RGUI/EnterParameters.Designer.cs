namespace RGUI
{
  partial class EnterParameters
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
      this.lblN = new System.Windows.Forms.Label();
      this.tbN = new System.Windows.Forms.TextBox();
      this.lblAlpha = new System.Windows.Forms.Label();
      this.tbAlpha = new System.Windows.Forms.TextBox();
      this.btnOk = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.cbSplit = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // lblN
      // 
      this.lblN.AutoSize = true;
      this.lblN.Location = new System.Drawing.Point(12, 9);
      this.lblN.Name = "lblN";
      this.lblN.Size = new System.Drawing.Size(18, 13);
      this.lblN.TabIndex = 6;
      this.lblN.Text = "N:";
      // 
      // tbN
      // 
      this.tbN.Location = new System.Drawing.Point(140, 9);
      this.tbN.Name = "tbN";
      this.tbN.Size = new System.Drawing.Size(100, 20);
      this.tbN.TabIndex = 7;
      this.tbN.Text = "2";
      // 
      // lblAlpha
      // 
      this.lblAlpha.AutoSize = true;
      this.lblAlpha.Location = new System.Drawing.Point(12, 35);
      this.lblAlpha.Name = "lblAlpha";
      this.lblAlpha.Size = new System.Drawing.Size(37, 13);
      this.lblAlpha.TabIndex = 8;
      this.lblAlpha.Text = "Alpha:";
      // 
      // tbAlpha
      // 
      this.tbAlpha.Location = new System.Drawing.Point(140, 35);
      this.tbAlpha.Name = "tbAlpha";
      this.tbAlpha.Size = new System.Drawing.Size(100, 20);
      this.tbAlpha.TabIndex = 9;
      this.tbAlpha.Text = "2";
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(12, 99);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(100, 23);
      this.btnOk.TabIndex = 10;
      this.btnOk.Text = "OK";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new System.EventHandler(this.BtnOkClick);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(140, 99);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(100, 23);
      this.btnCancel.TabIndex = 11;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
      // 
      // cbSplit
      // 
      this.cbSplit.AutoSize = true;
      this.cbSplit.Checked = true;
      this.cbSplit.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbSplit.Location = new System.Drawing.Point(15, 66);
      this.cbSplit.Name = "cbSplit";
      this.cbSplit.Size = new System.Drawing.Size(108, 17);
      this.cbSplit.TabIndex = 12;
      this.cbSplit.Text = "Split to half-plains";
      this.cbSplit.UseVisualStyleBackColor = true;
      // 
      // EnterParameters
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(252, 134);
      this.Controls.Add(this.cbSplit);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.tbAlpha);
      this.Controls.Add(this.lblAlpha);
      this.Controls.Add(this.tbN);
      this.Controls.Add(this.lblN);
      this.Name = "EnterParameters";
      this.Text = "EnterParameters";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblN;
    private System.Windows.Forms.TextBox tbN;
    private System.Windows.Forms.Label lblAlpha;
    private System.Windows.Forms.TextBox tbAlpha;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.CheckBox cbSplit;
  }
}