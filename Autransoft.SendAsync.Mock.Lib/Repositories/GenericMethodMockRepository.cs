using System;
using System.Collections.Generic;
using System.Linq;
using Autransoft.SendAsync.Mock.Lib.interfaces;

namespace Autransoft.SendAsync.Mock.Lib.Repositories
{
    internal class GenericMethodMockRepository : IGenericMethodMockRepository
    {
        internal IList<IGenericMethodMock> _genericMethodMocks;

        internal IList<IGenericMethodMock> GenericMethodMocks
        {
            get
            {
                if(_genericMethodMocks == null)
                    _genericMethodMocks = new List<IGenericMethodMock>();

                return _genericMethodMocks;
            }
        }

        public void Clean() 
        {
            foreach (var genericMethodMock in GenericMethodMocks)
                genericMethodMock.Clean();

            _genericMethodMocks = null;
        }

        public void Add(IGenericMethodMock genericMethodMock)
        {
            var item = GenericMethodMocks.Where(x => x.ClassType == genericMethodMock.ClassType && x.InterfaceType == genericMethodMock.InterfaceType).FirstOrDefault();
            if(item != null)
                GenericMethodMocks.Remove(item);

            GenericMethodMocks.Add(genericMethodMock);
        }

        public IList<IGenericMethodMock> Get() => GenericMethodMocks;

        public IGenericMethodMock Get(Type interfac) => GenericMethodMocks.FirstOrDefault(genericMethodMock => genericMethodMock.InterfaceType == interfac);
    }
}