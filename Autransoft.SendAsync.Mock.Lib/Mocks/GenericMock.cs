using System;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autransoft.SendAsync.Mock.Lib.Entities;
using Autransoft.SendAsync.Mock.Lib.Extensions;
using Autransoft.SendAsync.Mock.Lib.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace Autransoft.SendAsync.Mock.Lib.Mocks
{
    internal abstract class GenericMock : HttpMessageHandlerMock
    {
        internal void AddMock<INTERFACE, CLASS>(IServiceCollection services, Expression<Func<CLASS, HttpClient>> expression)
            where CLASS : class, INTERFACE
            where INTERFACE : class
        {
            var mock = new Mock<CLASS>(ConstructorHelper.GetConstructorParams<CLASS>());

            StartMock();

            mock.Setup(expression).Returns(AddHttpMessageHandlerMock());

            services.AddTransientMock<INTERFACE, CLASS>(mock.Object);
        }

        internal abstract void StartMock();
    }
}