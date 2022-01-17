using System;
using System.Linq.Expressions;
using System.Net.Http;
using Autransoft.SendAsync.Mock.Lib.Helpers;
using Autransoft.SendAsync.Mock.Lib.interfaces;
using Autransoft.SendAsync.Mock.Lib.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Autransoft.SendAsync.Mock.Lib.Servers
{
    public class SendAsyncMethodMock : IDisposable
    {
        internal readonly static IGenericMethodMockRepository _genericMethodMockRepository;
        internal readonly static IMockRepository _mockRepository;

        static SendAsyncMethodMock()
        {
            _genericMethodMockRepository = new GenericMethodMockRepository();
            _mockRepository = new MethodMockRepository();
        }

        public GenericMethodHelper<INTERFACE, CLASS> CreateMock<INTERFACE, CLASS>(Expression<Func<CLASS, HttpClient>> expression)
            where CLASS : class, INTERFACE
            where INTERFACE : class 
        {
            var genericMethodHelper = new GenericMethodHelper<INTERFACE, CLASS>(_mockRepository, expression);

            _genericMethodMockRepository.Add(genericMethodHelper.GenericMethodMock);

            return genericMethodHelper;
        }

        public IServiceCollection AddToDependencyInjection(IServiceCollection serviceCollection)
        {
            foreach (var genericMock in _genericMethodMockRepository.Get())
            {
                genericMock.CreateMock(serviceCollection);
                genericMock.AddToDependencyInjection(serviceCollection);
            }

            return serviceCollection;
        }

        public void Dispose()
        {
            _genericMethodMockRepository.Clean();
            _mockRepository.Clean();
        }
    }
}