using System;
using Microsoft.Extensions.DependencyInjection;

namespace Autransoft.SendAsync.Mock.Lib.interfaces
{
    internal interface IGenericMethodMock : IHttpMessageHandlerMock
    {
        Type GetClassType();
        Type GetInterfaceType();
        void AddToDependencyInjection(IServiceCollection serviceCollection);
    }
}