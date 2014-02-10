namespace RgLib
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
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.backToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.setPointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.cleanPointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.pictureBox = new System.Windows.Forms.PictureBox();
      this.track = new System.Windows.Forms.TextBox();
      this.menuStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.imageToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(1276, 24);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "mainMenu";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileToolStripMenuItem.Text = "File";
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(89, 6);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
      this.exitToolStripMenuItem.Text = "Exit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
      // 
      // imageToolStripMenuItem
      // 
      this.imageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backToolStripMenuItem,
            this.setPointToolStripMenuItem,
            this.cleanPointToolStripMenuItem});
      this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
      this.imageToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
      this.imageToolStripMenuItem.Text = "Image";
      // 
      // backToolStripMenuItem
      // 
      this.backToolStripMenuItem.Name = "backToolStripMenuItem";
      this.backToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
      this.backToolStripMenuItem.Text = "Back";
      this.backToolStripMenuItem.Click += new System.EventHandler(this.BackToolStripMenuItemClick);
      // 
      // setPointToolStripMenuItem
      // 
      this.setPointToolStripMenuItem.Name = "setPointToolStripMenuItem";
      this.setPointToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
      this.setPointToolStripMenuItem.Text = "Set point";
      this.setPointToolStripMenuItem.Click += new System.EventHandler(this.SetPointToolStripMenuItemClick);
      // 
      // cleanPointToolStripMenuItem
      // 
      this.cleanPointToolStripMenuItem.Name = "cleanPointToolStripMenuItem";
      this.cleanPointToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
      this.cleanPointToolStripMenuItem.Text = "Clean point";
      this.cleanPointToolStripMenuItem.Click += new System.EventHandler(this.CleanPoint);
      // 
      // statusStrip1
      // 
      this.statusStrip1.Location = new System.Drawing.Point(0, 780);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(1276, 22);
      this.statusStrip1.TabIndex = 1;
      this.statusStrip1.Text = "statusBar";
      // 
      // pictureBox
      // 
      this.pictureBox.Location = new System.Drawing.Point(12, 27);
      this.pictureBox.Name = "pictureBox";
      this.pictureBox.Size = new System.Drawing.Size(750, 750);
      this.pictureBox.TabIndex = 2;
      this.pictureBox.TabStop = false;
      this.pictureBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PictureBoxMouseDoubleClick);
      this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBoxMouseMove);
      // 
      // track
      // 
      this.track.Enabled = false;
      this.track.Location = new System.Drawing.Point(768, 27);
      this.track.Multiline = true;
      this.track.Name = "track";
      this.track.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.track.Size = new System.Drawing.Size(496, 750);
      this.track.TabIndex = 3;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1276, 802);
      this.Controls.Add(this.track);
      this.Controls.Add(this.pictureBox);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.menuStrip1);
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "MainForm";
      this.Text = "Revers RG-Transformation";
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.PictureBox pictureBox;
    private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem backToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem setPointToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem cleanPointToolStripMenuItem;
    private System.Windows.Forms.TextBox track;
  }
}

