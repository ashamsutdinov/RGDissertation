using System.Drawing;

namespace RGDynApp
{
    public class C1C2InitialProjection :
        RGSceneC1C2Transform
    {
        protected override Color GetC1C2Color(Bitmap bmp, CPoint cpt, RGPoint rg, RGScene scene, RGProcessor processor)
        {
            return rg.G > 0 ? RGScene.PositiveColor : RGScene.NegativeColor;
        }
    }
}