using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Autransoft.SendAsync.Mock.Lib.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Autransoft.SendAsync.Mock.Lib.interfaces
{
    internal interface IGenericMethodMock : IHttpMessageHandlerMock
    {
        Autransoft.SendAsync.Mock.Lib.Repositories.RequestResponseRepository RequestResponseRepository { get; }
        Type InterfaceType { get; }
        Type ClassType { get; }
        
        Func<HttpMethod, HttpRequestHeaders, string, string, string, ResponseMockEntity> ConfigureResponseFunc { get; set; }
        void AddToDependencyInjection(IServiceCollection serviceCollection);
        void CreateMock(IServiceCollection serviceCollection);
        void Clean();
    }
}