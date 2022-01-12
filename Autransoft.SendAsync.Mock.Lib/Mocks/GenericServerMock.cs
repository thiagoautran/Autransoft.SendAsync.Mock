using System;
using System.Linq.Expressions;
using System.Net.Http;
using Autransoft.SendAsync.Mock.Lib.Extensions;
using Autransoft.SendAsync.Mock.Lib.Helpers;
using Autransoft.SendAsync.Mock.Lib.interfaces;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Autransoft.SendAsync.Mock.Lib.Mocks
{
    public class GenericServerMock<INTERFACE, CLASS> : HttpMessageHandlerMock, IGenericServerMock
        where CLASS : class, INTERFACE
        where INTERFACE : class
    {
        internal static IMockRepository _mockRepository;

        internal static IMockRepository MockRepository 
        { 
            get  
            {
                if(_mockRepository == null)
                    _mockRepository = new Autransoft.SendAsync.Mock.Lib.Repositories.ServerMockRepository();

                return _mockRepository;
            }
        }

        public void AddToDependencyInjection(IServiceCollection serviceCollection)
        {
            var mock = new Mock<CLASS>(ConstructorHelper.GetConstructorParams<CLASS>(serviceCollection, MockRepository));

            MockInitialize();

            mock.Setup(HttpClientMethod()).Returns(AddHttpMessageHandlerMock());

            MockRepository.Add(typeof(INTERFACE), mock.Object);

            serviceCollection.ReplaceTransient<INTERFACE, CLASS>(mock.Object);
        }

        public virtual Expression<Func<CLASS, HttpClient>> HttpClientMethod() => default;

        public virtual void MockInitialize() { }
    }
}