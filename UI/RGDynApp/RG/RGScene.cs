using System;
using System.Drawing;

namespace RGDynApp
{
    public class RGScene : IDisposable
    {
        public RectangleF RGFrame;

        public Size UIFrame;

        public SizeF OnePxSize;

        public Image ResultedImage;

        public Image OriginalImage;

        public static readonly Color BackgroundColr = Color.FromArgb(255, 255, 255, 255);

        public static readonly Color UndefinedColor = Color.Black;

        public static readonly Color NegativeColor = Color.LightGray;

        public static readonly Color NegativeDynamicsLeftColor = Color.LightSlateGray;

        public static readonly Color NegativeDynamicsRightColor = Color.LightGray;

        public static readonly Color PositiveColor = Color.Gray;

        public static readonly Color PositiveDynamicsLeftColor = Color.FromArgb(255, 75, 75, 75);

        public static readonly Color PositiveDynamicsRightColor = Color.Gray;

        public static readonly Color MarkupColor = Color.Black;

        public static readonly Color LabelsColor = Color.White;

        public static readonly Color MarkupVColor = Color.Yellow;

        public static readonly Color MarkupHColor = Color.YellowGreen;

        public static readonly Color Line1Color = Color.Blue;

        public static readonly Color Line2Color = Color.Red;

        public RGScene(RectangleF rgFrameSize, Size uiFrameSize)
        {
            RGFrame = rgFrameSize;
            UIFrame = uiFrameSize;
            var onePxWidth = rgFrameSize.Width / uiFrameSize.Width;
            var onePxHeight = rgFrameSize.Height / uiFrameSize.Height;
            OnePxSize = new SizeF(onePxWidth, onePxHeight);
            OriginalImage = CreateBackgroundBitmap(uiFrameSize);
            ResultedImage = new Bitmap(OriginalImage);
        }

        public PointF MapToRGFrame(Point uiPoint)
        {
            var xOffset = uiPoint.X * OnePxSize.Width;
            var x = RGFrame.X + xOffset;
            var yOffset = (UIFrame.Height - uiPoint.Y) * OnePxSize.Height;
            var y = RGFrame.Y + yOffset;
            var pt = new PointF(x, y);
            return pt;
        }

        public Point MapToUIFrame(PointF rgPoint)
        {
            var xOffset = Math.Abs(rgPoint.X - RGFrame.X);
            var xUiOffset = xOffset / OnePxSize.Width;
            var x = xUiOffset;
            var yOffset = Math.Abs(rgPoint.Y - RGFrame.Y);
            var yUiOffset = yOffset / OnePxSize.Height;
            var y = UIFrame.Height - yUiOffset;
            var pt = new Point((int)x, (int)y);
            return pt;
        }

        public void ApplyTransformation(RGSceneTransform transform, RGProcessor processor)
        {
            lock (OriginalImage)
            {
                var tImage = new Bitmap(OriginalImage);
                using (var layer = transform.GetLayer(RGFrame, UIFrame, this, processor))
                {
                    for (var i = 0; i < tImage.Width; i++)
                    {
                        for (var j = 0; j < tImage.Height; j++)
                        {
                            var clr = layer.GetPixel(i, j);
                            if (clr != BackgroundColr)
                            {
                                tImage.SetPixel(i, j, clr);
                            }
                        }
                    }
                    OriginalImage = tImage;
                    ResultedImage = new Bitmap(OriginalImage);
                }
            }
        }

        public Bitmap CreateBackgroundBitmap(Size size)
        {
            var bmp = new Bitmap(size.Width, size.Height);
            var bgColor = BackgroundColr;
            var gr = Graphics.FromImage(bmp);
            gr.FillRectangle(new SolidBrush(bgColor), 0, 0, size.Width, size.Height);
            gr.Save();
            return bmp;
        }

        public void Dispose()
        {
            if (OriginalImage != null)
                OriginalImage.Dispose();
            if (ResultedImage != null)
                ResultedImage.Dispose();
        }
    }
}