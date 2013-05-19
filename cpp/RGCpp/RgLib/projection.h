#ifndef PROJECTION_H
#define PROJECTION_H

#include "rg.h"
#include "bitmask.h"

enum CProjection
{
  ProjC0C1  = Bit0,
  ProjC1C2  = Bit1,
  ProjC0C2  = Bit2,
  ProjUp    = Bit4,
  ProjDown  = Bit5,
  ProjUpC0C1  = ProjC0C1 | ProjUp,
  ProjUpC1C2  = ProjC1C2 | ProjUp,
  ProjUpC0C2  = ProjC0C2 | ProjUp,
  ProjDownC0C1  = ProjC0C1 | ProjDown,
  ProjDownC1C2  = ProjC1C2 | ProjDown,
  ProjDownC0C2  = ProjC0C2 | ProjDown
};

#endif // PROJECTION_H
