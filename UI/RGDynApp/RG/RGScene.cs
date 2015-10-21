using System;
using System.Drawing;

namespace RGDynApp.RG
{
    public class RGScene : 
        IDisposable
    {
        private RectangleF _rgFrame;

        private Size _uiFrame;

        private SizeF _onePxSize;

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
            _rgFrame = rgFrameSize;
            _uiFrame = uiFrameSize;
            var onePxWidth = rgFrameSize.Width / uiFrameSize.Width;
            var onePxHeight = rgFrameSize.Height / uiFrameSize.Height;
            _onePxSize = new SizeF(onePxWidth, onePxHeight);
            OriginalImage = CreateBackgroundBitmap(uiFrameSize);
            ResultedImage = new Bitmap(OriginalImage);
        }

        public PointF MapToRGFrame(Point uiPoint)
        {
            var xOffset = uiPoint.X * _onePxSize.Width;
            var x = _rgFrame.X + xOffset;
            var yOffset = (_uiFrame.Height - uiPoint.Y) * _onePxSize.Height;
            var y = _rgFrame.Y + yOffset;
            var pt = new PointF(x, y);
            return pt;
        }

        public Point MapToUIFrame(PointF rgPoint)
        {
            var xOffset = Math.Abs(rgPoint.X - _rgFrame.X);
            var xUiOffset = xOffset / _onePxSize.Width;
            var x = xUiOffset;
            var yOffset = Math.Abs(rgPoint.Y - _rgFrame.Y);
            var yUiOffset = yOffset / _onePxSize.Height;
            var y = _uiFrame.Height - yUiOffset;
            var pt = new Point((int)x, (int)y);
            return pt;
        }

        public void ApplyTransformation(RGSceneTransform transform, RGProcessor processor)
        {
            lock (OriginalImage)
            {
                var tImage = new Bitmap(OriginalImage);
                using (var layer = transform.GetLayer(_rgFrame, _uiFrame, this, processor))
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