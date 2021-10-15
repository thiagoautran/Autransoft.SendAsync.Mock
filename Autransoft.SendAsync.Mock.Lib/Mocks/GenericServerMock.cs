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
        public void AddToDependencyInjection(IServiceCollection serviceCollection)
        {
            var mock = new Mock<CLASS>(ConstructorHelper.GetConstructorParams<CLASS>(serviceCollection));

            MockInitialize();

            mock.Setup(HttpClientMethod()).Returns(AddHttpMessageHandlerMock());

            Autransoft.SendAsync.Mock.Lib.Repositories.MockRepository.Add(typeof(INTERFACE), mock.Object);

            serviceCollection.AddTransientMock<INTERFACE, CLASS>(mock.Object);
        }

        public virtual Expression<Func<CLASS, HttpClient>> HttpClientMethod() => default;

        public virtual void MockInitialize() { }
    }
}