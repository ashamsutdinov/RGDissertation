using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using RGUI.Resources;
using RenormGroups.Configuration;
using RenormGroups.Processing;

namespace RGUI
{
  public partial class MainForm : Form
  {
    private readonly ProcessingScene _scene = new ProcessingScene();

    private MouseMovingState _mouseMovingState;

    private TrackPoint _trackPointDirect;

    private TrackPoint _trackPointReverse;

    private readonly Dictionary<object, ProcessingStack> _stacks = new Dictionary<object, ProcessingStack>();

    public MainForm()
    {
      InitializeComponent();
      InitializeResources();
      var trackW = new TrackPointSlide(100);
      trackW.Show();
    }

    private void InitializeResources()
    {
      fileToolStripMenuItem.Text = str.File;
      initializeToolStripMenuItem.Text = str.Initialize;
      exitToolStripMenuItem.Text = str.Exit;
      helpToolStripMenuItem.Text = str.Help;
      aboutToolStripMenuItem.Text = str.AboutTitle;
      processingToolStripMenuItem.Text = str.Processing;
      backToolStripMenuItem.Text = str.ZoomOut;
      saveAsToolStripMenuItem.Text = str.Save;
      imageAsToolStripMenuItem.Text = str.ImageAs;
      pointToolStripMenuItem.Text = str.Point;
      trackToolStripMenuItem.Text = str.ToTrackDirect;
      trackDirectToolStripMenuItem.Text = str.ToTrackDirect;
      trackDirectToolStripMenuItem1.Text = str.ToTrackDirect;
      trackReserveToolStripMenuItem.Text = str.ToTrackReverse;
      trackReverseToolStripMenuItem.Text = str.ToTrackReverse;
      trackReverseToolStripMenuItem1.Text = str.ToTrackReverse;
      lineToolStripMenuItem.Text = str.Line;
      areaToolStripMenuItem.Text = str.Area;
      fillSceneToolStripMenuItem.Text = str.FillScene;
      directToolStripMenuItem.Text = str.FillSceneDirect;
      reverseToolStripMenuItem.Text = str.FillSceneReverse;
      convergenseDirectionToolStripMenuItem.Text = str.ConvergenseDirection;
      convergenseDirectionToolStripMenuItem1.Text = str.ConvergenseDirection;
      convergenseSpeedToolStripMenuItem.Text = str.ConvergenseSpeed;
      convergenseSpeedToolStripMenuItem1.Text = str.ConvergenseSpeed;

      var image = new Bitmap(pbC0C1.Width, pbC0C2.Height);
      var gr = Graphics.FromImage(image);
      gr.FillRectangle(new SolidBrush(Config.BackgroundColor), 0, 0, pbC0C1.Width, pbC0C1.Height);
      gr.Save();

      pbC0C1.Image = image;
      pbC0C2.Image = image;
      pbC1C2.Image = image;
    }

    #region Common menu actions

    private void OnExit(object sender, EventArgs eargs)
    {
      Application.Exit();
    }

    private void OnAbout(object sender, EventArgs eargs)
    {
      MessageBox.Show(str.AboutBody);
    }

    #endregion

    #region Drawing

    private void OnInitializeImage(object sender, EventArgs eargs)
    {
      _scene.Initialize(pbC1C2.DisplayRectangle);
      pbC1C2.Image = _scene.StackC1C2.CurrentFrame.ActualImage;
      pbC0C1.Image = _scene.StackC0C1.CurrentFrame.ActualImage;
      pbC0C2.Image = _scene.StackC0C2.CurrentFrame.ActualImage;
      flowLayoutPanel.Update();
      _stacks.Clear();
      _stacks.Add(pbC0C1, _scene.StackC0C1);
      _stacks.Add(pbC0C2, _scene.StackC0C2);
      _stacks.Add(pbC1C2, _scene.StackC1C2);
    }

    #endregion

    #region Mouse moving

    private void OnPictureBoxMouseDown(object sender, MouseEventArgs eargs)
    {
      if (_trackPointDirect != null)
        return;

      if (_trackPointReverse != null)
        return;

      if (eargs.Button != MouseButtons.Left)
        return;

      var currentPoint = new Point(eargs.X, eargs.Y);

      if (_mouseMovingState != null)
      {
        _mouseMovingState = null;
        return;
      }

      if (!_stacks.ContainsKey(sender))
        return;

      _mouseMovingState = new MouseMovingState { Current = currentPoint, Start = currentPoint };
    }

    private PictureBox CheckCanDrawRect(object sender, MouseEventArgs eargs)
    {
      if (eargs.Button != MouseButtons.Left)
        return null;

      if (_mouseMovingState == null)
        return null;

      if (!_stacks.ContainsKey(sender))
        return null;

      var pb = sender as PictureBox;
      return pb;
    }

    private void OnPictureBoxMouseMove(object sender, MouseEventArgs eargs)
    {
      var currentPoint = new Point(eargs.X, eargs.Y);
      ProcessingStack stack = null;

      if (_stacks.ContainsKey(sender))
      {
        stack = _stacks[sender];
        var pt = stack.GetCPoint(currentPoint);
        toolStripStatusLabel.Text = pt.ToString();
      }

      var pb = CheckCanDrawRect(sender, eargs);
      if (pb == null)
        return;

      _mouseMovingState.Current = currentPoint;

      var rect = ProcessingScene.GetRectangle(_mouseMovingState.Start, _mouseMovingState.Current);
      if (stack != null)
      {
        var image = stack.DrawRectangle(rect, Config.AreaBorderColor);
        pb.Image = image;
      }
      flowLayoutPanel.Update();
    }

    private void OnPictureBoxMouseUp(object sender, MouseEventArgs eargs)
    {
      var currentPoint = new Point(eargs.X, eargs.Y);
      ProcessingStack stack;

      #region Track point

      #region Track direct

      if (_trackPointDirect != null)
      {
        if (_stacks.ContainsKey(sender))
        {
          stack = _stacks[sender];
          var pt = stack.GetCPoint(currentPoint);
          //_scene.TrackPointDirect(pt, _trackPointDirect.Iterations, _trackPointDirect.N, _trackPointDirect.Alpha, Config.TrackColor, Config.PointColor);
          //pbC1C2.Image = _scene.StackC1C2.CurrentFrame.ActualImage;
          pbC1C2.Cursor = Cursors.Default;
          //pbC0C1.Image = _scene.StackC0C1.CurrentFrame.ActualImage;
          pbC0C1.Cursor = Cursors.Default;
          //pbC0C2.Image = _scene.StackC0C2.CurrentFrame.ActualImage;
          pbC0C2.Cursor = Cursors.Default;
          var act = new SceneTrackPointDirectAction
          {
            Alpha = _trackPointDirect.Alpha,
            N = _trackPointDirect.N,
            InitialPoint = pt,
            IterationsCount = _trackPointDirect.Iterations
          };
          ActionBodySimple(act);
          flowLayoutPanel.Update();
        }
        _trackPointDirect = null;
        return;
      }

      #endregion

      #region Track reserse

      if (_trackPointReverse != null)
      {
        if (_stacks.ContainsKey(sender))
        {
          stack = _stacks[sender];
          var pt = stack.GetCPoint(currentPoint);
          //_scene.TrackPointReverse(pt, _trackPointReverse.Iterations, _trackPointReverse.N, _trackPointReverse.Alpha, Config.TrackColor.Interted(), Config.PointColor.Interted());
          //pbC1C2.Image = _scene.StackC1C2.CurrentFrame.ActualImage;
          pbC1C2.Cursor = Cursors.Default;
          //pbC0C1.Image = _scene.StackC0C1.CurrentFrame.ActualImage;
          pbC0C1.Cursor = Cursors.Default;
          //pbC0C2.Image = _scene.StackC0C2.CurrentFrame.ActualImage;
          pbC0C2.Cursor = Cursors.Default;
          var act = new SceneTrackPointReverseAction()
          {
            Alpha = _trackPointReverse.Alpha,
            N = _trackPointReverse.N,
            InitialPoint = pt,
            IterationsCount = _trackPointReverse.Iterations
          };
          ActionBodySimple(act);
          flowLayoutPanel.Update();
        }
        _trackPointReverse = null;
        return;
      }

      #endregion

      #endregion

      var pb = CheckCanDrawRect(sender, eargs);
      if (pb == null)
        return;

      _mouseMovingState.End = currentPoint;

      stack = _stacks[sender];
      var rect = ProcessingScene.GetRectangle(_mouseMovingState.Start, _mouseMovingState.End);
      if (rect.Width < 2 || rect.Height < 2)
      {
        _mouseMovingState = null;
        return;
      }

      var image = stack.DrawRectangle(rect, Config.AreaBorderColor);
      pb.Image = image;
      flowLayoutPanel.Update();

      stack.ZoomRectangle(rect);
      pb.Image = stack.CurrentFrame.ActualImage;
      flowLayoutPanel.Update();

      _mouseMovingState = null;
    }

    private void OnPictureBoxMouseLeave(object sender, EventArgs eargs)
    {

    }

    private void OnPictureBoxMouseEnter(object sender, EventArgs eargs)
    {

    }

    private void OnPictureBoxMouseHover(object sender, EventArgs eargs)
    {

    }

    #endregion

    #region Context menu actions

    private void OnPictureBoxZoomOut(object sender, EventArgs eargs)
    {
      var ctrl = pbContextMenu.SourceControl as PictureBox;
      if (ctrl == null || !_stacks.ContainsKey(ctrl))
        return;

      var stack = _stacks[ctrl];
      var frame = stack.ZoomOut();
      if (frame == null)
        return;

      ctrl.Image = frame.ActualImage;
      flowLayoutPanel.Update();
    }

    private void OnPictureBoxSaveImageAs(object sender, EventArgs eargs)
    {

    }

    #endregion

    #region Point

    private void OnTrackPointDirect(object sender, EventArgs eargs)
    {
      _trackPointDirect = new TrackPoint();
      var res = _trackPointDirect.ShowDialog();
      if (res != DialogResult.OK) 
        return;

      if (!_trackPointDirect.SelectManually)
      {
        var act = new SceneTrackPointDirectAction
          {
            Alpha = _trackPointDirect.Alpha,
            N = _trackPointDirect.N,
            InitialPoint = _trackPointDirect.CPoint,
            IterationsCount = _trackPointDirect.Iterations
          };
        ActionBody(act);
        //_scene.TrackPointDirect(_trackPointDirect.CPoint, _trackPointDirect.Iterations, _trackPointDirect.N, _trackPointDirect.Alpha, Config.TrackColor, Config.PointColor);
        //pbC1C2.Image = _scene.StackC1C2.CurrentFrame.ActualImage;
        //pbC0C1.Image = _scene.StackC0C1.CurrentFrame.ActualImage;
        //pbC0C2.Image = _scene.StackC0C2.CurrentFrame.ActualImage;
        _trackPointDirect = null;
      }
      else
      {
        pbC0C1.Cursor = Cursors.Cross;
        pbC0C2.Cursor = Cursors.Cross;
        pbC1C2.Cursor = Cursors.Cross;
      }
    }

    private void OnTrackPointReverse(object sender, EventArgs eargs)
    {
      _trackPointReverse = new TrackPoint();
      var res = _trackPointReverse.ShowDialog();
      if (res != DialogResult.OK)
        return;

      if (!_trackPointReverse.SelectManually)
      {
        _scene.TrackPointReverse(_trackPointReverse.CPoint, _trackPointReverse.Iterations, _trackPointReverse.N, _trackPointReverse.Alpha, Config.TrackColor.Interted(), Config.PointColor.Interted());
        pbC1C2.Image = _scene.StackC1C2.CurrentFrame.ActualImage;
        pbC0C1.Image = _scene.StackC0C1.CurrentFrame.ActualImage;
        pbC0C2.Image = _scene.StackC0C2.CurrentFrame.ActualImage;
        _trackPointReverse = null;
      }
      else
      {
        pbC0C1.Cursor = Cursors.Cross;
        pbC0C2.Cursor = Cursors.Cross;
        pbC1C2.Cursor = Cursors.Cross;
      }
    }

    #endregion

    #region Fill Scene

    private void OnFillSceneDirect(object sender, EventArgs eargs)
    {
      var dlg = new EnterParameters();
      var res = dlg.ShowDialog();
      if (res != DialogResult.OK) 
        return;

      var act = new SceneDirectIterationAction { N = dlg.N, Alpha = dlg.Alpha, Split = dlg.Split };
      ActionBody(act);
    }

    private void OnFillSceneReverse(object sender, EventArgs eargs)
    {
      var dlg = new EnterParameters();
      var res = dlg.ShowDialog();
      if (res != DialogResult.OK) 
        return;

      var act = new SceneReverseIterationAction { N = dlg.N, Alpha = dlg.Alpha, Split = dlg.Split };
      ActionBody(act);
    }

    private void OnFillSceneDirectionDirect(object sender, EventArgs eargs)
    {
      var dlg = new EnterParameters();
      var res = dlg.ShowDialog();
      if (res != DialogResult.OK) 
        return;

      var act = new SceneDirectionDirectIterationAction { N = dlg.N, Alpha = dlg.Alpha, Split = dlg.Split };
      ActionBody(act);
    }

    private void OnFillSceneDirectionReverse(object sender, EventArgs eargs)
    {
      var dlg = new EnterParameters();
      var res = dlg.ShowDialog();
      if (res != DialogResult.OK) 
        return;

      var act = new SceneDirectionReverseIterationAction { N = dlg.N, Alpha = dlg.Alpha, Split = dlg.Split };
      ActionBody(act);
    }

    private void ActionBodySimple(ProcessingAction act)
    {
      new Thread(() =>
      {
        var s = _scene.StackC1C2;
        var f = s.CurrentFrame;
        s.ProcessedActions.Add(act);
        s.DoAllSimpleActions();
        pbC1C2.Image = f.ActualImage;
      }).Start();

      new Thread(() =>
      {
        var s = _scene.StackC0C1;
        var f = s.CurrentFrame;
        s.ProcessedActions.Add(act);
        s.DoAllSimpleActions();
        pbC0C1.Image = f.ActualImage;
      }).Start();

      new Thread(() =>
      {
        var s = _scene.StackC0C2;
        var f = s.CurrentFrame;
        s.ProcessedActions.Add(act);
        s.DoAllSimpleActions();
        pbC0C2.Image = f.ActualImage;
      }).Start();
    }

    private void ActionBody(ProcessingAction act)
    {
      new Thread(() =>
      {
        var s = _scene.StackC1C2;
        var f = s.CurrentFrame;
        var rect = f.ClientRectangle;
        s.ProcessedActions.Add(act);
        s.ZoomRectangle(rect);
        f = s.CurrentFrame;
        pbC1C2.Image = f.ActualImage;
      }).Start();

      new Thread(() =>
      {
        var s = _scene.StackC0C1;
        var f = s.CurrentFrame;
        var rect = f.ClientRectangle;
        s.ProcessedActions.Add(act);
        s.ZoomRectangle(rect);
        f = s.CurrentFrame;
        pbC0C1.Image = f.ActualImage;
      }).Start();

      new Thread(() =>
      {
        var s = _scene.StackC0C2;
        var f = s.CurrentFrame;
        var rect = f.ClientRectangle;
        s.ProcessedActions.Add(act);
        s.ZoomRectangle(rect);
        f = s.CurrentFrame;
        pbC0C2.Image = f.ActualImage;
      }).Start();
    }

    #endregion


  }
}
