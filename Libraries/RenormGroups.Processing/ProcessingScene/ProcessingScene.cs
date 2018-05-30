using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
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
      StackC1C2.CreateNewFrame(Geometry.Rectangle);
      StackC0C1.CreateNewFrame(Geometry.Rectangle);
      StackC0C2.CreateNewFrame(Geometry.Rectangle);

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

      Parallel.ForEach(AllStacks, stack => stack.DrawInitialFigure());

      foreach (var fixedPoint in FixedPoints)
      {
        SelectCPointArea(fixedPoint, 0.03, fixedPoint.Color);
      }
    }

    #endregion

    #region Public members

    public static Rectangle GetRectangle(Point p1, Point p2)
    {
      var x1 = Math.Min(p1.X, p2.X);
      var x2 = Math.Max(p1.X, p2.X);
      var y1 = Math.Min(p1.Y, p2.Y);
      var y2 = Math.Max(p1.Y, p2.Y);
      var rect = new Rectangle(x1, y1, x2 - x1, y2 - y1);
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
      Parallel.ForEach(AllStacks, stack =>
      {
        stack.Clear();
        stack.Geometry = Geometry;
      });
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

    public void DrawLine(CPoint point1, CPoint point2, Color colorLine, Color colorPoint)
    {
      foreach (var stack in AllStacks)
      {
        stack.DrawLine(point1, point2, colorLine, colorPoint);
      }
    }

    public void DrawPath(IEnumerable<CPoint> points, Color colorLine, Color colorPoint)
    {
      var pointsList = points.ToList();
      for (var i = 0; i < pointsList.Count - 1; i++)
      {
        var p1 = pointsList[i];
        var p2 = pointsList[i + 1];
        DrawLine(p1, p2, colorLine, colorPoint);
      }
    }

    public void TrackPointDirect(CPoint point, int iterations, double n, double alpha, Color colorLine, Color colorPoint)
    {
      var track = CInterator.TrackPointDirect(point, iterations, n, alpha);
      DrawPath(track, colorLine, colorPoint);
    }

    public void TrackPointReverse(CPoint point, int iterations, double n, double alpha, Color colorLine, Color colorPoint)
    {
      var track = CInterator.TrackPointReverse(point, iterations, n, alpha);
      DrawPath(track, colorLine, colorPoint);
    }

    public void TrackSet(IEnumerable<CPoint> points, int iterations, double n, double alpha, Color colorLine, Color colorPoint)
    {
      foreach (var cPoint in points)
      {
        TrackPointDirect(cPoint, iterations, n, alpha, colorLine, colorPoint);
      }
    }

    public void TrackArea(CPoint center, double radius, double accuracy, int iterations, double n, double alpha, Color colorLine, Color colorPoint)
    {
      var points = new List<CPoint>();
      var c0From = center.C0 - radius;
      var c0To = center.C0 + radius;
      var c1From = center.C1 - radius;
      var c1To = center.C1 + radius;
      var c2From = center.C2 - radius;
      var c2To = center.C2 + radius;

      for (var c0 = c0From; c0 <= c0To; c0 += accuracy)
      {
        for (var c1 = c1From; c1 <= c1To; c1 += accuracy)
        {
          for (var c2 = c2From; c2 < c2To; c2 += accuracy)
          {
            var cpt = new CPoint { C0 = c0, C1 = c1, C2 = c2 }.Normalized as CPoint;
            if (cpt != null)
            {
              points.Add(cpt);
            }
          }
        }
      }

      TrackSet(points, iterations, n, alpha, colorLine, colorPoint);
    }

    #endregion
  }
}