using System;
using Microsoft.Extensions.DependencyInjection;

namespace Autransoft.SendAsync.Mock.Lib.interfaces
{
    public interface IGenericServerMock : IHttpMessageHandlerMock
    {
        void MockInitialize();
        void AddToDependencyInjection(IServiceCollection serviceCollection);
    }
}