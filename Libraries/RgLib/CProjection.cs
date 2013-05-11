using System;

namespace ReverseTransform
{
  [Flags]
  public enum CProjection
  {
    C0C1  = 0x1,
    C0C2  = 0x2,
    C1C2  = 0x4,
    Up    = 0x10,
    Down  = 0x20,
    UpC0C1  = C0C1 | Up,
    UpC0C2  = C0C2 | Up,
    UpC1C2  = C1C2 | Up,
    DownC0C1 = C0C1 | Down,
    DownC0C2 = C0C2 | Down,
    DownC1C2 = C1C2 | Down
  }
}