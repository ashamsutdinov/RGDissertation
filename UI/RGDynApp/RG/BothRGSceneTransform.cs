using System.Drawing;

namespace RGDynApp
{
    public abstract class BothRGSceneTransform : RGSceneTransform
    {
        public override Bitmap GetLayer(RectangleF rect, Size frameRect, RGScene scene, RGProcessor processor)
        {
            var bmp = scene.CreateBackgroundBitmap(frameRect);
            for (var i = 0; i < bmp.Width; i++)
            {
                for (var j = 0; j < bmp.Height; j++)
                {
                    var pt = scene.MapToRGFrame(new Point(i, j));
                    var clr = GetPixelColor(bmp, pt, scene, processor);
                    if (clr != RGScene.BackgroundColr)
                    {
                        bmp.SetPixel(i, j, clr);
                    }

                }
            }
            var gr = Graphics.FromImage(bmp);
            ApplyMarkup(bmp, gr, scene, processor);
            gr.Save();
            return bmp;
        }

        protected abstract Color GetPixelColor(Bitmap bmp, PointF ptF, RGScene scene, RGProcessor processor);

        protected abstract void ApplyMarkup(Bitmap bmp, Graphics gr, RGScene scene, RGProcessor processor);
    }
}