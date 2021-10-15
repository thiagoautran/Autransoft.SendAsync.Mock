using System.Collections.Generic;
using Autransoft.SendAsync.Mock.Lib.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Autransoft.SendAsync.Mock.Lib.Helpers
{
    internal static class ConstructorHelper
    {
        internal static object[] GetConstructorParams<CLASS>(IServiceCollection serviceCollection)
        {
            var listConstructor = typeof(CLASS).GetConstructors();
            var listParams = new List<object>();

            foreach (var construtor in listConstructor)
            {
                if (construtor.IsPublic)
                {
                    foreach (var param in construtor.GetParameters())
                        listParams.Add(param.ParameterType.Default(serviceCollection));

                    break;
                }
            }

            return listParams.ToArray();
        }
    }
}