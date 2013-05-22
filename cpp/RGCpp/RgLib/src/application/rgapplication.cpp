#include "rgapplication.h"
#include "irgdirecttransformmap.h"
#include "irgreversetransformmap.h"

RgApplication::RgApplication(int argc, char* argv[]) :
  KernelApplication(argc, argv)
{
}

RgApplication::~RgApplication()
{
}

void RgApplication::initializeInternal()
{
  auto ioc = IoCContainer::instance();
  ioc->registerService<IRgDirectTransformMap,RgDirectTransformMap>();
  ioc->registerService<IRgReverseTransformMap,RgReverseTransformMap>();
}
