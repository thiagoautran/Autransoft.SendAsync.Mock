using System.Collections.Generic;
using System.Linq;
using Autransoft.SendAsync.Mock.Lib.interfaces;

namespace Autransoft.SendAsync.Mock.Lib.Repositories
{
    internal static class GenericMockRepository
    {
        private static IList<IGenericMethodMock> _genericMethodMocks;

        private static IList<IGenericMethodMock> GenericMethodMocks
        {
            get
            {
                if(_genericMethodMocks == null)
                    _genericMethodMocks = new List<IGenericMethodMock>();

                return _genericMethodMocks;
            }
        }

        internal static void Clean() => _genericMethodMocks = null;

        internal static void Add(IGenericMethodMock genericMock)
        {
            var item = GenericMethodMocks.Where(x => x.GetClassType() == genericMock.GetClassType() && x.GetInterfaceType() == genericMock.GetInterfaceType()).FirstOrDefault();
            if(item != null)
                GenericMethodMocks.Remove(item);

            GenericMethodMocks.Add(genericMock);
        }

        internal static IList<IGenericMethodMock> Get() => GenericMethodMocks;
    }
}