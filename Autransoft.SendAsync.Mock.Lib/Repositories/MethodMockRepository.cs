using System;
using System.Collections.Generic;
using System.Linq;
using Autransoft.SendAsync.Mock.Lib.interfaces;

namespace Autransoft.SendAsync.Mock.Lib.Repositories
{
    internal class MethodMockRepository : IMockRepository
    {
        internal Dictionary<Type, object> _mocks;

        internal Dictionary<Type, object> Mocks
        {
            get
            {
                if(_mocks == null)
                    _mocks = new Dictionary<Type, object>();

                return _mocks;
            }
        }

        public void Clean() => _mocks = null;

        public void Add(Type type, object mockObject)
        {
            var item = Mocks.Where(x => x.Key == type).Select(x => x.Value).FirstOrDefault();
            if(item != null)
                Mocks.Remove(type);
            
            Mocks.Add(type, mockObject);
        }

        public object Get(Type type) =>
            Mocks.Where(x => x.Key == type).Select(x => x.Value).FirstOrDefault();
    }
}