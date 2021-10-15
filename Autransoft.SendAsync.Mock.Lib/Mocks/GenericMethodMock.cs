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
    internal class GenericMethodMock<INTERFACE, CLASS> : HttpMessageHandlerMock, IGenericMethodMock
        where CLASS : class, INTERFACE
        where INTERFACE : class
    {
        private Mock<CLASS> _mock;

        public void CreateMock(IServiceCollection serviceCollection, Expression<Func<CLASS, HttpClient>> expression)
        {
            _mock = new Mock<CLASS>(ConstructorHelper.GetConstructorParams<CLASS>(serviceCollection));
            _mock.Setup(expression).Returns(AddHttpMessageHandlerMock());
        }

        public void AddToDependencyInjection(IServiceCollection serviceCollection)
        {
            Autransoft.SendAsync.Mock.Lib.Repositories.MockRepository.Add(typeof(INTERFACE), _mock.Object);
            serviceCollection.AddTransientMock<INTERFACE, CLASS>(_mock.Object);
        }

        public Type GetInterfaceType() => typeof(INTERFACE);

        public Type GetClassType() => typeof(CLASS);
    }
}