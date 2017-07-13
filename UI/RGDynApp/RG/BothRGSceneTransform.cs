using System.CodeDom;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;

namespace RGDynApp.RG
{
    public abstract class BothRGSceneTransform :
        RGSceneTransform
    {
        public override Bitmap GetLayer(RectangleF rect, Size frameRect, RGScene scene, RGProcessor processor)
        {
            var bmp = scene.CreateBackgroundBitmap(frameRect);
            var colors = new Color[bmp.Width, bmp.Height];
            for (var i1 = 0; i1 < bmp.Width; i1++)
            {
                var i = i1;
                Parallel.For(0, bmp.Height, j =>
                {
                    var pt = scene.MapToRGFrame(new Point(i, j));
                    var clr = GetPixelColor(pt, scene, processor);
                    if (clr != RGScene.BackgroundColr)
                    {
                        //bmp.SetPixel(i, j, clr);
                        colors[i, j] = clr;
                    }
                });
                /*
                for (var j = 0; j < bmp.Height; j++)
                {
                    

                }*/
            }
            for (var i = 0; i < bmp.Width; i++)
            {
                for (var j = 0; j < bmp.Height; j++)
                {
                    bmp.SetPixel(i, j, colors[i, j]);
                }
            }
            var gr = Graphics.FromImage(bmp);
            ApplyMarkup(bmp, gr, scene, processor);
            gr.Save();
            return bmp;
        }

        protected abstract void ApplyMarkup(Bitmap bmp, Graphics gr, RGScene scene, RGProcessor processor);
    }
}