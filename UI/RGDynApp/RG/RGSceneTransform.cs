using System.Drawing;

namespace RGDynApp
{
    public abstract class RGSceneTransform
    {
        public abstract Bitmap GetLayer(RectangleF rect, Size frameRect, RGScene scene, RGProcessor processor);

        public abstract void ApplyMarkupDynamics(int step, Bitmap bmp, Graphics gr, RGScene scene, RGProcessor processor);

    }
}