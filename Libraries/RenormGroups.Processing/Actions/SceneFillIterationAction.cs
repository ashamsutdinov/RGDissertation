using System;
using System.Collections.Generic;
using System.Drawing;
using RenormGroups.Configuration;

namespace RenormGroups.Processing
{
    public abstract class SceneFillIterationAction : SceneIterationAction
    {
        public bool Split { get; set; }

        public const int MaxChannelValue = 255;

        protected CPoint PositiveOrientir;

        protected CPoint NegativeOrientir;

        protected abstract CPoint PositiveAlphaOrientir { get; }

        protected abstract CPoint NegativeAlphaOrientir { get; }

        public CPoint Orientir(double alpha)
        {
            if (alpha > 1)
                return PositiveAlphaOrientir;
            if (alpha < 1)
                return NegativeAlphaOrientir;
            throw new NotSupportedException();
        }

        protected Func<CPoint, CPoint> SingleIterationInternal;

        public abstract Func<CPoint, CPoint> SingleIteration { get; set; }

        protected Func<CPoint, CPoint, bool> OrientationCriteriaInternal;

        public abstract Func<CPoint, CPoint, bool> OrientationCriteria { get; set; }

        protected abstract int GetRedChannel(IterationResult result);

        protected IterationResult ProcessIteration(CPoint pt)
        {
            var orient = Orientir(Alpha);
            var criticalIterationsCount = Config.CriticalIterationsCount;
            var iterationNum = 0;
            var limitAccuracy = Config.LimitAccuracy;
            var curPt = pt;
            var dist = double.MaxValue;
            var result = new IterationResult { Resolution = IterationResolution.Trivial, StartPoint = pt };
            var track = new List<CPoint> { curPt };
            while (dist > limitAccuracy)
            {
                curPt = SingleIteration(curPt);
                dist = curPt.DistanceTo(orient);
                track.Add(curPt);
                iterationNum++;
            }

            if (iterationNum < criticalIterationsCount)
            {
                result.Resolution = IterationResolution.Trivial;
            }

            result.EndPoint = track[track.Count - 1];
            result.Track = track;
            result.TrackSteps = iterationNum;
            return result;
        }

        public override void Apply<TStack>(TStack stack)
        {
            var frame = stack.CurrentFrame;
            var rect = frame.Rectangle;
            var image = frame.ProcessedImage;

            var i = 0;
            for (var x = rect.X; x <= rect.X + rect.Width; x += rect.AccuracyX)
            {
                var j = 0;
                for (var y = rect.Y; y <= rect.Y + rect.Height; y += rect.AccuracyY)
                {
                    lock (image)
                    {
                        if (i < image.Width && j < image.Height)
                        {
                            var pt = stack.GetCPoint(x, y);
                            if (pt.IsFake)
                            {
                                image.SetPixel(i, j, Config.BackgroundColor);
                            }
                            else
                            {
                                var rg = pt.RGPoint;
                                var iterated = ProcessIteration(pt);

                                var backColor = Split
                                              ? (rg.G < 0 ? Config.GNegativeFillColor : Config.GPositiveFillColor)
                                              : Config.GPositiveFillColor;
                                var redChannel = GetRedChannel(iterated);
                                var red = Color.FromArgb(redChannel);
                                var color = Color.FromArgb(backColor.A, red.R, backColor.G, backColor.B);
                                image.SetPixel(i, j, color);
                            }
                        }
                    }
                    j++;
                }
                i++;
            }

            frame.DrawedImage = image.Clone() as Bitmap;
        }
    }
}