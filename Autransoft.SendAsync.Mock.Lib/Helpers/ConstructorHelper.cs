using System.Collections.Generic;
using Autransoft.SendAsync.Mock.Lib.Extensions;

namespace Autransoft.SendAsync.Mock.Lib.Helpers
{
    internal static class ConstructorHelper
    {
        internal static object[] GetConstructorParams<CLASS>()
        {
            var listConstructor = typeof(CLASS).GetConstructors();
            var listParams = new List<object>();

            foreach (var construtor in listConstructor)
            {
                if (construtor.IsPublic)
                {
                    foreach (var param in construtor.GetParameters())
                        listParams.Add(param.ParameterType.Default());

                    break;
                }
            }

            return listParams.ToArray();
        }
    }
}