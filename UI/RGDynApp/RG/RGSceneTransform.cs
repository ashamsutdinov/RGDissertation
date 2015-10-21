using System;
using System.Drawing;

namespace RGDynApp.RG
{
    public abstract class RGSceneTransform
    {
        public abstract Bitmap GetLayer(RectangleF rect, Size frameRect, RGScene scene, RGProcessor processor);

        public abstract void ApplyMarkupDynamics(int step, Bitmap bmp, Graphics gr, RGScene scene, RGProcessor processor);

        public abstract void ApplyBoundaryPointDynamics(int step, Bitmap bmp, Graphics gr, Tuple<Color, PointF, CPoint> left, Tuple<Color, PointF, CPoint> right, RGScene scene, RGProcessor processor);

        public abstract void ApplyTrackPointDynamics(int stem, Bitmap bmp, Graphics gr, Tuple<Color, PointF, CPoint> point, RGScene scene, RGProcessor processor);

        public abstract Color GetPixelColor(PointF ptF, RGScene scene, RGProcessor processor);

    }
}