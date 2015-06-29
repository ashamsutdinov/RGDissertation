using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using RenormGroups.Configuration;

namespace RenormGroups.Processing
{
  /// <summary>
  /// РГ-сцена
  /// </summary>
  public sealed class ProcessingScene
  {
    #region Private fields

    private static readonly Dictionary<string, ImageCodecInfo> ImageCodecsTable;

    #endregion

    #region Public static properties

    public static ImageCodecInfo DefaultImageCodecInfo { get; private set; }

    public static EncoderParameters DefaultEncoderParameters { get; private set; }

    public static ImageCodecInfo[] ImageEncoders { get; private set; }

    public static ImageCodecInfo[] ImageDecoders { get; private set; }

    public static readonly IEnumerable<CPointEx> FixedPoints = new[]
    {
      new CPointEx{C0=1,C1=0,C2=0, Color = Config.C0PointCircleBorderColor},
      new CPointEx{C0=-1,C1=0,C2=0, Color = Config.C0PointCircleBorderColor},
      new CPointEx{C2=1,C0=0,C1=0, Color = Config.C2PointCircleBorderColor},
      new CPointEx{C2=-1,C0=0,C1=0, Color = Config.C2PointCircleBorderColor}
    };

    public static readonly CIterator CInterator = new CIterator();

    #endregion

    #region Public properties

    public ProcessingStack StackC1C2 { get; private set; }

    public ProcessingStack StackC0C1 { get; private set; }

    public ProcessingStack StackC0C2 { get; private set; }

    public ClientGeometry Geometry { get; private set; }

    public ReadOnlyCollection<ProcessingStack> AllStacks { get; private set; }

    #endregion

    #region Constants

    #endregion

    #region Constructors

    static ProcessingScene()
    {
      ImageCodecsTable = new Dictionary<string, ImageCodecInfo>();
      DefaultEncoderParameters = new EncoderParameters(1);
      DefaultEncoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 97);
      ImageEncoders = ImageCodecInfo.GetImageEncoders();
      ImageDecoders = ImageCodecInfo.GetImageDecoders();
      DefaultImageCodecInfo = GetEncoderInfo(Config.DefaultImageMimeType);
    }

    public ProcessingScene()
    {
      StackC1C2 = new C1C2ProjectionProcessingStack();
      StackC0C1 = new C0C1ProjectionProcessingStack();
      StackC0C2 = new C0C2ProjectionProcessingStack();
      AllStacks = new ReadOnlyCollection<ProcessingStack>(new List<ProcessingStack> { StackC1C2, StackC0C1, StackC0C2 });
    }

    #endregion

    #region Private members

    private void CreateDefaultFirstFrame()
    {
      StackC1C2.Push(CreateNewFrame(Geometry.Rectangle));
      StackC0C1.Push(CreateNewFrame(Geometry.Rectangle));
      StackC0C2.Push(CreateNewFrame(Geometry.Rectangle));

      var font = new Font(FontFamily.GenericSansSerif, 10);
      var white = new SolidBrush(Config.BackgroundColor);
      var yellow = new SolidBrush(Config.GPositiveFillColor);
      var black = new SolidBrush(Config.LegendColor);

      var circleRect = new RectangleF((float)Geometry.ClientCircleX, (float)Geometry.ClientCircleY, (float)Geometry.ClientCircleRadius * 2, (float)Geometry.ClientCircleRadius * 2);

      DrawOnGraphics(g =>
      {
        g.FillRectangle(white, Geometry.Rectangle);
        g.FillEllipse(yellow, circleRect);
        g.DrawString("C1C2", font, black, 10, 10);
      }, StackC1C2.CurrentFrame.Image);

      DrawOnGraphics(g =>
      {
        g.FillRectangle(white, Geometry.Rectangle);
        g.FillEllipse(yellow, circleRect);
        g.DrawString("C0C1", font, black, 10, 10);
      }, StackC0C1.CurrentFrame.Image);

      DrawOnGraphics(g =>
      {
        g.FillRectangle(white, Geometry.Rectangle);
        g.FillEllipse(yellow, circleRect);
        g.DrawString("C0C2", font, black, 10, 10);
      }, StackC0C2.CurrentFrame.Image);

      DrawInitialFigureC1C2();
      DrawInitialFigureC0C1();
      DrawInitialFigureC0C2();

      foreach (var fixedPoint in FixedPoints)
      {
        SelectCPointArea(fixedPoint, 0.03, fixedPoint.Color);
      }
    }

    private void DrawInitialFigureC1C2()
    {
      var frame = StackC1C2.CurrentFrame;
      var rect = frame.Rectangle;

      var circleRadius = Config.CircleRadius;

      for (var x = -circleRadius; x <= circleRadius; x += rect.AccuracyX)
      {
        var i = -x / rect.AccuracyX + Geometry.ClientCircleX + Geometry.ClientCircleRadius;
        for (var y = -circleRadius; y <= circleRadius; y += rect.AccuracyY)
        {
          var j = -y / rect.AccuracyY + Geometry.ClientCircleY + Geometry.ClientCircleRadius;

          var c1 = x;
          var c2 = y;

          if (c1 * c1 + c2 * c2 > circleRadius)
            continue;

          var c0 = Math.Sqrt(1 - c1 * c1 - c2 * c2);
          var c = new CPoint { C0 = c0, C1 = c1, C2 = c2 };
          var rg = c.RGPoint;

          if (rg.G < 0)
          {
            frame.Image.SetPixel((int)i, (int)j, Config.GNegativeFillColor);
          }
        }
      }
    }

    private void DrawInitialFigureC0C1()
    {
      var frame = StackC0C1.CurrentFrame;
      var rect = frame.Rectangle;

      var circleRadius = Config.CircleRadius;

      for (var x = -circleRadius; x <= circleRadius; x += rect.AccuracyX)
      {
        var i = -x / rect.AccuracyX + Geometry.ClientCircleX + Geometry.ClientCircleRadius;
        for (var y = -circleRadius; y <= circleRadius; y += rect.AccuracyY)
        {
          var j = -y / rect.AccuracyY + Geometry.ClientCircleY + Geometry.ClientCircleRadius;

          var c0 = x;
          var c1 = y;

          if (c0 * c0 + c1 * c1 > circleRadius)
            continue;

          var c2 = Math.Sqrt(1 - c0 * c0 - c1 * c1);
          var c = new CPoint { C0 = c0, C1 = c1, C2 = c2 };
          var rg = c.RGPoint;

          if (rg.G < 0)
          {
            frame.Image.SetPixel((int)i, (int)j, Config.GNegativeFillColor);
          }
        }
      }
    }

    private void DrawInitialFigureC0C2()
    {
      var frame = StackC0C2.CurrentFrame;
      var rect = frame.Rectangle;

      var circleRadius = Config.CircleRadius;
      for (var x = -circleRadius; x <= circleRadius; x += rect.AccuracyX)
      {
        var i = -x / rect.AccuracyX + Geometry.ClientCircleX + Geometry.ClientCircleRadius;
        for (var y = -circleRadius; y <= circleRadius; y += rect.AccuracyY)
        {
          var j = -y / rect.AccuracyY + Geometry.ClientCircleY + Geometry.ClientCircleRadius;

          var c0 = x;
          var c2 = y;

          if (c0 * c0 + c2 * c2 > circleRadius)
            continue;

          var c1 = Math.Sqrt(1 - c0 * c0 - c2 * c2);
          var c = new CPoint { C0 = c0, C1 = c1, C2 = c2 };
          var rg = c.RGPoint;

          if (rg.G < 0)
          {
            frame.Image.SetPixel((int)i, (int)j, Config.GNegativeFillColor);
          }
        }
      }
    }

    private ProcessingFrame CreateNewFrame(Rectangle selectedRectangle, bool isNew = true)
    {
      var frame = new ProcessingFrame { Image = new Bitmap(Geometry.Rectangle.Width, Geometry.Rectangle.Height) };
      var resultedRectD = new RectangleD();
      if (isNew)
      {
        var centerX = selectedRectangle.Width * 1.0 / 2;
        var centerY = selectedRectangle.Height * 1.0 / 2;
        var clientRadius = Math.Min(centerX, centerY) * Config.CircleFillFactor;
        var onePixel = Config.CircleRadius / clientRadius;
        var x = -centerX * onePixel;
        var w = selectedRectangle.Width * onePixel;
        var y = -centerY * onePixel;
        var h = selectedRectangle.Height * onePixel;
        resultedRectD.X = x;
        resultedRectD.Y = y;
        resultedRectD.Width = w;
        resultedRectD.Height = h;
        Geometry.ClientCircleRadius = clientRadius;
        Geometry.ClientCircleX = centerX - clientRadius;
        Geometry.ClientCircleY = centerY - clientRadius;
        resultedRectD.AccuracyX = resultedRectD.Width / selectedRectangle.Width;
        resultedRectD.AccuracyY = resultedRectD.Height / selectedRectangle.Height;
      }
      else
      {
        throw new NotImplementedException();
      }
      frame.Rectangle = resultedRectD;
      frame.ClientRectangle = Geometry.Rectangle;
      return frame;
    }

    #endregion

    #region Public members

    public static Rectangle GetRectangle(Point p1, Point p2)
    {
      var x1 = Math.Min(p1.X, p2.X);
      var x2 = Math.Max(p1.X, p2.X);
      var y1 = Math.Min(p1.Y, p2.Y);
      var y2 = Math.Max(p1.Y, p2.Y);
      var rect = new Rectangle(x1, y1, (x2 - x1), (y2 - y1));
      return rect;
    }

    public static void DrawOnGraphics(Action<Graphics> drawer, Image image)
    {
      var graphics = Graphics.FromImage(image);
      drawer(graphics);
      graphics.Save();
    }

    public static ImageCodecInfo GetEncoderInfo(string smimeType)
    {
      var mimeType = string.Format("{0}", smimeType).Trim().ToLower();
      if (!ImageCodecsTable.ContainsKey(mimeType))
      {
        var codecInfo = ImageEncoders.FirstOrDefault(cI => cI.MimeType == mimeType);
        if (codecInfo != null)
        {
          ImageCodecsTable.Add(mimeType, codecInfo);
        }
      }
      return ImageCodecsTable.ContainsKey(mimeType) ? ImageCodecsTable[mimeType] : null;
    }

    public void Initialize(Rectangle clientRect)
    {
      Geometry = new ClientGeometry { Rectangle = clientRect };
      StackC1C2.Clear();
      CreateDefaultFirstFrame();
    }

    public void SelectCPointArea(CPoint point, double epsilon, Color color)
    {
      foreach (var stack in AllStacks)
      {
        stack.SelectCPointArea(point, epsilon, color);
      }
    }

    public void SelectCPoint(CPoint point, Color color)
    {
      foreach (var stack in AllStacks)
      {
        stack.SelectCPoint(point, color);
      }
    }

    public void DrawLine(CPoint point1, CPoint point2, Color color)
    {
      foreach (var stack in AllStacks)
      {
        stack.DrawLine(point1, point2, color);
      }
    }

    public void DrawPath(IEnumerable<CPoint> points, Color color)
    {
      var pointsList = points.ToList();
      for (var i = 0; i < pointsList.Count - 1; i++)
      {
        var p1 = pointsList[i];
        var p2 = pointsList[i + 1];
        DrawLine(p1, p2, color);
      }
    }

    #endregion
  }
}