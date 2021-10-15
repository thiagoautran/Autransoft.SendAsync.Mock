using System;
using System.Collections.Generic;
using System.Linq;

namespace Autransoft.SendAsync.Mock.Lib.Repositories
{
    internal static class MockRepository
    {
        private static Dictionary<Type, object> _mocks;

        private static Dictionary<Type, object> Mocks
        {
            get
            {
                if(_mocks == null)
                    _mocks = new Dictionary<Type, object>();

                return _mocks;
            }
        }

        internal static void Clean() => _mocks = null;

        internal static void Add(Type type, object mockObject)
        {
            var item = Mocks.Where(x => x.Key == type).Select(x => x.Value).FirstOrDefault();
            if(item != null)
                Mocks.Remove(type);
            
            Mocks.Add(type, mockObject);
        }

        internal static object Get(Type type) =>
            Mocks.Where(x => x.Key == type).Select(x => x.Value).FirstOrDefault();
    }
}