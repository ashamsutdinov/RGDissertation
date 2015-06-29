namespace RGUI
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.mainMenu = new System.Windows.Forms.MenuStrip();
      this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.initializeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.processingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.pointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.trackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.trackReserveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.lineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.trackDirectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.trackReverseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.areaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.trackDirectToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.trackReverseToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.fillSceneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.directToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.reverseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
      this.pbC1C2 = new System.Windows.Forms.PictureBox();
      this.pbContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.backToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.imageAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.pbC0C1 = new System.Windows.Forms.PictureBox();
      this.pbC0C2 = new System.Windows.Forms.PictureBox();
      this.statusBar = new System.Windows.Forms.StatusStrip();
      this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.convergenseDirectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.convergenseSpeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.convergenseDirectionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.convergenseSpeedToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.mainMenu.SuspendLayout();
      this.flowLayoutPanel.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbC1C2)).BeginInit();
      this.pbContextMenu.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbC0C1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pbC0C2)).BeginInit();
      this.statusBar.SuspendLayout();
      this.SuspendLayout();
      // 
      // mainMenu
      // 
      this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.processingToolStripMenuItem,
            this.helpToolStripMenuItem});
      resources.ApplyResources(this.mainMenu, "mainMenu");
      this.mainMenu.Name = "mainMenu";
      // 
      // fileToolStripMenuItem
      // 
      this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.initializeToolStripMenuItem,
            this.exitToolStripMenuItem});
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
      // 
      // initializeToolStripMenuItem
      // 
      this.initializeToolStripMenuItem.Name = "initializeToolStripMenuItem";
      resources.ApplyResources(this.initializeToolStripMenuItem, "initializeToolStripMenuItem");
      this.initializeToolStripMenuItem.Click += new System.EventHandler(this.OnInitializeImage);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.OnExit);
      // 
      // processingToolStripMenuItem
      // 
      this.processingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fillSceneToolStripMenuItem,
            this.pointToolStripMenuItem,
            this.lineToolStripMenuItem,
            this.areaToolStripMenuItem});
      this.processingToolStripMenuItem.Name = "processingToolStripMenuItem";
      resources.ApplyResources(this.processingToolStripMenuItem, "processingToolStripMenuItem");
      // 
      // pointToolStripMenuItem
      // 
      this.pointToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trackToolStripMenuItem,
            this.trackReserveToolStripMenuItem});
      this.pointToolStripMenuItem.Name = "pointToolStripMenuItem";
      resources.ApplyResources(this.pointToolStripMenuItem, "pointToolStripMenuItem");
      // 
      // trackToolStripMenuItem
      // 
      this.trackToolStripMenuItem.Name = "trackToolStripMenuItem";
      resources.ApplyResources(this.trackToolStripMenuItem, "trackToolStripMenuItem");
      this.trackToolStripMenuItem.Click += new System.EventHandler(this.OnTrackPointDirect);
      // 
      // trackReserveToolStripMenuItem
      // 
      this.trackReserveToolStripMenuItem.Name = "trackReserveToolStripMenuItem";
      resources.ApplyResources(this.trackReserveToolStripMenuItem, "trackReserveToolStripMenuItem");
      this.trackReserveToolStripMenuItem.Click += new System.EventHandler(this.OnTrackPointReverse);
      // 
      // lineToolStripMenuItem
      // 
      this.lineToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trackDirectToolStripMenuItem,
            this.trackReverseToolStripMenuItem});
      this.lineToolStripMenuItem.Name = "lineToolStripMenuItem";
      resources.ApplyResources(this.lineToolStripMenuItem, "lineToolStripMenuItem");
      // 
      // trackDirectToolStripMenuItem
      // 
      this.trackDirectToolStripMenuItem.Name = "trackDirectToolStripMenuItem";
      resources.ApplyResources(this.trackDirectToolStripMenuItem, "trackDirectToolStripMenuItem");
      // 
      // trackReverseToolStripMenuItem
      // 
      this.trackReverseToolStripMenuItem.Name = "trackReverseToolStripMenuItem";
      resources.ApplyResources(this.trackReverseToolStripMenuItem, "trackReverseToolStripMenuItem");
      // 
      // areaToolStripMenuItem
      // 
      this.areaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trackDirectToolStripMenuItem1,
            this.trackReverseToolStripMenuItem1});
      this.areaToolStripMenuItem.Name = "areaToolStripMenuItem";
      resources.ApplyResources(this.areaToolStripMenuItem, "areaToolStripMenuItem");
      // 
      // trackDirectToolStripMenuItem1
      // 
      this.trackDirectToolStripMenuItem1.Name = "trackDirectToolStripMenuItem1";
      resources.ApplyResources(this.trackDirectToolStripMenuItem1, "trackDirectToolStripMenuItem1");
      // 
      // trackReverseToolStripMenuItem1
      // 
      this.trackReverseToolStripMenuItem1.Name = "trackReverseToolStripMenuItem1";
      resources.ApplyResources(this.trackReverseToolStripMenuItem1, "trackReverseToolStripMenuItem1");
      // 
      // fillSceneToolStripMenuItem
      // 
      this.fillSceneToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.directToolStripMenuItem,
            this.reverseToolStripMenuItem});
      this.fillSceneToolStripMenuItem.Name = "fillSceneToolStripMenuItem";
      resources.ApplyResources(this.fillSceneToolStripMenuItem, "fillSceneToolStripMenuItem");
      // 
      // directToolStripMenuItem
      // 
      this.directToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convergenseDirectionToolStripMenuItem,
            this.convergenseSpeedToolStripMenuItem});
      this.directToolStripMenuItem.Name = "directToolStripMenuItem";
      resources.ApplyResources(this.directToolStripMenuItem, "directToolStripMenuItem");
      // 
      // reverseToolStripMenuItem
      // 
      this.reverseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convergenseDirectionToolStripMenuItem1,
            this.convergenseSpeedToolStripMenuItem1});
      this.reverseToolStripMenuItem.Name = "reverseToolStripMenuItem";
      resources.ApplyResources(this.reverseToolStripMenuItem, "reverseToolStripMenuItem");
      // 
      // helpToolStripMenuItem
      // 
      this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
      // 
      // aboutToolStripMenuItem
      // 
      this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
      resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
      this.aboutToolStripMenuItem.Click += new System.EventHandler(this.OnAbout);
      // 
      // flowLayoutPanel
      // 
      this.flowLayoutPanel.Controls.Add(this.pbC1C2);
      this.flowLayoutPanel.Controls.Add(this.pbC0C1);
      this.flowLayoutPanel.Controls.Add(this.pbC0C2);
      resources.ApplyResources(this.flowLayoutPanel, "flowLayoutPanel");
      this.flowLayoutPanel.Name = "flowLayoutPanel";
      // 
      // pbC1C2
      // 
      this.pbC1C2.ContextMenuStrip = this.pbContextMenu;
      this.pbC1C2.Cursor = System.Windows.Forms.Cursors.Default;
      resources.ApplyResources(this.pbC1C2, "pbC1C2");
      this.pbC1C2.Name = "pbC1C2";
      this.pbC1C2.TabStop = false;
      this.pbC1C2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnPictureBoxMouseDown);
      this.pbC1C2.MouseEnter += new System.EventHandler(this.OnPictureBoxMouseEnter);
      this.pbC1C2.MouseLeave += new System.EventHandler(this.OnPictureBoxMouseLeave);
      this.pbC1C2.MouseHover += new System.EventHandler(this.OnPictureBoxMouseHover);
      this.pbC1C2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnPictureBoxMouseMove);
      this.pbC1C2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnPictureBoxMouseUp);
      // 
      // pbContextMenu
      // 
      this.pbContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backToolStripMenuItem,
            this.saveAsToolStripMenuItem});
      this.pbContextMenu.Name = "pbContextMenu";
      resources.ApplyResources(this.pbContextMenu, "pbContextMenu");
      // 
      // backToolStripMenuItem
      // 
      this.backToolStripMenuItem.Name = "backToolStripMenuItem";
      resources.ApplyResources(this.backToolStripMenuItem, "backToolStripMenuItem");
      this.backToolStripMenuItem.Click += new System.EventHandler(this.OnPictureBoxZoomOut);
      // 
      // saveAsToolStripMenuItem
      // 
      this.saveAsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imageAsToolStripMenuItem});
      this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
      resources.ApplyResources(this.saveAsToolStripMenuItem, "saveAsToolStripMenuItem");
      // 
      // imageAsToolStripMenuItem
      // 
      this.imageAsToolStripMenuItem.Name = "imageAsToolStripMenuItem";
      resources.ApplyResources(this.imageAsToolStripMenuItem, "imageAsToolStripMenuItem");
      this.imageAsToolStripMenuItem.Click += new System.EventHandler(this.OnPictureBoxSaveImageAs);
      // 
      // pbC0C1
      // 
      this.pbC0C1.ContextMenuStrip = this.pbContextMenu;
      this.pbC0C1.Cursor = System.Windows.Forms.Cursors.Default;
      resources.ApplyResources(this.pbC0C1, "pbC0C1");
      this.pbC0C1.Name = "pbC0C1";
      this.pbC0C1.TabStop = false;
      this.pbC0C1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnPictureBoxMouseDown);
      this.pbC0C1.MouseEnter += new System.EventHandler(this.OnPictureBoxMouseEnter);
      this.pbC0C1.MouseLeave += new System.EventHandler(this.OnPictureBoxMouseLeave);
      this.pbC0C1.MouseHover += new System.EventHandler(this.OnPictureBoxMouseHover);
      this.pbC0C1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnPictureBoxMouseMove);
      this.pbC0C1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnPictureBoxMouseUp);
      // 
      // pbC0C2
      // 
      this.pbC0C2.ContextMenuStrip = this.pbContextMenu;
      this.pbC0C2.Cursor = System.Windows.Forms.Cursors.Default;
      resources.ApplyResources(this.pbC0C2, "pbC0C2");
      this.pbC0C2.Name = "pbC0C2";
      this.pbC0C2.TabStop = false;
      this.pbC0C2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnPictureBoxMouseDown);
      this.pbC0C2.MouseEnter += new System.EventHandler(this.OnPictureBoxMouseEnter);
      this.pbC0C2.MouseLeave += new System.EventHandler(this.OnPictureBoxMouseLeave);
      this.pbC0C2.MouseHover += new System.EventHandler(this.OnPictureBoxMouseHover);
      this.pbC0C2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnPictureBoxMouseMove);
      this.pbC0C2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnPictureBoxMouseUp);
      // 
      // statusBar
      // 
      this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
      resources.ApplyResources(this.statusBar, "statusBar");
      this.statusBar.Name = "statusBar";
      // 
      // toolStripStatusLabel
      // 
      this.toolStripStatusLabel.Name = "toolStripStatusLabel";
      resources.ApplyResources(this.toolStripStatusLabel, "toolStripStatusLabel");
      // 
      // convergenseDirectionToolStripMenuItem
      // 
      this.convergenseDirectionToolStripMenuItem.Name = "convergenseDirectionToolStripMenuItem";
      resources.ApplyResources(this.convergenseDirectionToolStripMenuItem, "convergenseDirectionToolStripMenuItem");
      this.convergenseDirectionToolStripMenuItem.Click += new System.EventHandler(this.OnFillSceneDirectionDirect);
      // 
      // convergenseSpeedToolStripMenuItem
      // 
      this.convergenseSpeedToolStripMenuItem.Name = "convergenseSpeedToolStripMenuItem";
      resources.ApplyResources(this.convergenseSpeedToolStripMenuItem, "convergenseSpeedToolStripMenuItem");
      this.convergenseSpeedToolStripMenuItem.Click += new System.EventHandler(this.OnFillSceneDirect);
      // 
      // convergenseDirectionToolStripMenuItem1
      // 
      this.convergenseDirectionToolStripMenuItem1.Name = "convergenseDirectionToolStripMenuItem1";
      resources.ApplyResources(this.convergenseDirectionToolStripMenuItem1, "convergenseDirectionToolStripMenuItem1");
      this.convergenseDirectionToolStripMenuItem1.Click += new System.EventHandler(this.OnFillSceneDirectionReverse);
      // 
      // convergenseSpeedToolStripMenuItem1
      // 
      this.convergenseSpeedToolStripMenuItem1.Name = "convergenseSpeedToolStripMenuItem1";
      resources.ApplyResources(this.convergenseSpeedToolStripMenuItem1, "convergenseSpeedToolStripMenuItem1");
      this.convergenseSpeedToolStripMenuItem1.Click += new System.EventHandler(this.OnFillSceneReverse);
      // 
      // MainForm
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.statusBar);
      this.Controls.Add(this.flowLayoutPanel);
      this.Controls.Add(this.mainMenu);
      this.MainMenuStrip = this.mainMenu;
      this.Name = "MainForm";
      this.mainMenu.ResumeLayout(false);
      this.mainMenu.PerformLayout();
      this.flowLayoutPanel.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pbC1C2)).EndInit();
      this.pbContextMenu.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pbC0C1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pbC0C2)).EndInit();
      this.statusBar.ResumeLayout(false);
      this.statusBar.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mainMenu;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
    private System.Windows.Forms.StatusStrip statusBar;
    private System.Windows.Forms.PictureBox pbC1C2;
    private System.Windows.Forms.ToolStripMenuItem initializeToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem processingToolStripMenuItem;
    private System.Windows.Forms.PictureBox pbC0C1;
    private System.Windows.Forms.PictureBox pbC0C2;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
    private System.Windows.Forms.ContextMenuStrip pbContextMenu;
    private System.Windows.Forms.ToolStripMenuItem backToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem imageAsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem pointToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem trackToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem trackReserveToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem lineToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem trackDirectToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem trackReverseToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem areaToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem trackDirectToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem trackReverseToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem fillSceneToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem directToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem reverseToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem convergenseDirectionToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem convergenseSpeedToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem convergenseDirectionToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem convergenseSpeedToolStripMenuItem1;


  }
}

