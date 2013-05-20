#include "idatabase.h"

IDatabase::IDatabase(QObject* parent) :
  IService(parent)
{
}

IDatabase::~IDatabase()
{
}

Database::Database(QObject* parent) :
  IDatabase(parent)
{
}

Database::~Database()
{
}

void Database::initializeInternal()
{
}
