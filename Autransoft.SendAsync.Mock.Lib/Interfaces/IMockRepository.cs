using System;

namespace Autransoft.SendAsync.Mock.Lib.interfaces
{
    internal interface IMockRepository
    {
        void Clean();
        object Get(Type type);
        void Add(Type type, object mockObject);
    }
}