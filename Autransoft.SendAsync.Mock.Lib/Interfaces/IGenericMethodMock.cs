using System;
using Microsoft.Extensions.DependencyInjection;

namespace Autransoft.SendAsync.Mock.Lib.interfaces
{
    public interface IGenericMethodMock : IHttpMessageHandlerMock
    {
        Type GetClassType();
        Type GetInterfaceType();
        void AddToDependencyInjection(IServiceCollection serviceCollection);
    }
}