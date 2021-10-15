using System;
using System.Linq.Expressions;
using System.Net.Http;
using Autransoft.SendAsync.Mock.Lib.Extensions;
using Autransoft.SendAsync.Mock.Lib.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Autransoft.SendAsync.Mock.Lib.Mocks
{
    public abstract class GenericMock<INTERFACE, CLASS> : HttpMessageHandlerMock<INTERFACE, CLASS>
        where CLASS : class, INTERFACE
        where INTERFACE : class
    {
        public void AddMock(IServiceCollection serviceCollection, Expression<Func<CLASS, HttpClient>> expression)
        {
            var mock = new Mock<CLASS>(ConstructorHelper.GetConstructorParams<CLASS>(serviceCollection));

            MockInitialize();

            mock.Setup(expression).Returns(AddHttpMessageHandlerMock());

            Autransoft.SendAsync.Mock.Lib.Repositories.MockRepository.Add(typeof(INTERFACE), mock.Object);
            serviceCollection.AddTransientMock<INTERFACE, CLASS>(mock.Object);
        }

        public abstract void MockInitialize();
    }
}