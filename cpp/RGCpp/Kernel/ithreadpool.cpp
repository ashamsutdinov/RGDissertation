#include "ithreadpool.h"

IThreadPool::IThreadPool(QObject* parent) :
  IService(parent)
{
}

IThreadPool::~IThreadPool()
{
}

ThreadPool::ThreadPool(QObject* parent)
  : IThreadPool(parent)
{
}

ThreadPool::~ThreadPool()
{
}

void ThreadPool::initializeInternal()
{
}
