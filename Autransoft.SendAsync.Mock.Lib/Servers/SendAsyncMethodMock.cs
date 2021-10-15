using System;
using System.Linq.Expressions;
using System.Net.Http;
using Autransoft.SendAsync.Mock.Lib.Mocks;
using Autransoft.SendAsync.Mock.Lib.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Autransoft.SendAsync.Mock.Lib.Base
{
    public static class SendAsyncMethodMock
    {
        public static void CreateMock<INTERFACE, CLASS>(IServiceCollection serviceCollection, Expression<Func<CLASS, HttpClient>> expression)
            where CLASS : class, INTERFACE
            where INTERFACE : class
        {
            var genericMock = new GenericMethodMock<INTERFACE, CLASS>();
            genericMock.CreateMock(serviceCollection, expression);

            GenericMockRepository.Add(genericMock);
        }

        public static IServiceCollection AddToDependencyInjection(IServiceCollection serviceCollection)
        {
            foreach (var genericMock in GenericMockRepository.Get())
                genericMock.AddToDependencyInjection(serviceCollection);

            return serviceCollection;
        }

        public static void Clean()
        {
            GenericMockRepository.Clean();
            RequestResponseRepository.Clean();
        }
    }
}