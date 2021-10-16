using System.Collections.Generic;

namespace Autransoft.SendAsync.Mock.Lib.interfaces
{
    internal interface IGenericMethodMockRepository
    {
        void Clean();
        IList<IGenericMethodMock> Get();
        void Add(IGenericMethodMock genericMethodMock);
    }
}