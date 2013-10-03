using System.Drawing;

namespace RGDynApp
{
    public abstract class RGSceneTransform
    {
        public abstract Bitmap GetLayer(RectangleF rect, Size frameRect, RGScene scene, RGProcessor processor);
    }
}