using System;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using Autransoft.SendAsync.Mock.Lib.Entities;
using Autransoft.SendAsync.Mock.Lib.Extensions;
using Autransoft.SendAsync.Mock.Lib.Helpers;
using Autransoft.SendAsync.Mock.Lib.interfaces;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using AutransoftRepository = Autransoft.SendAsync.Mock.Lib.Repositories;

namespace Autransoft.SendAsync.Mock.Lib.Mocks
{
    internal class GenericMethodMock<INTERFACE, CLASS> : HttpMessageHandlerMock, IGenericMethodMock
        where CLASS : class, INTERFACE
        where INTERFACE : class
    {
        private readonly AutransoftRepository.RequestResponseRepository _requestResponseRepository;
        private readonly Expression<Func<CLASS, HttpClient>> _expression;
        private readonly IMockRepository _mockRepository;
        private Mock<CLASS> _mock;

        public Func<HttpMethod, HttpRequestHeaders, string, string, string, ResponseMockEntity> ConfigureResponseFunc { get; set; }
        public AutransoftRepository.RequestResponseRepository RequestResponseRepository { get => _requestResponseRepository; }
        public Type InterfaceType { get => typeof(INTERFACE); }
        public Type ClassType { get => typeof(CLASS); }

        public GenericMethodMock(IMockRepository mockRepository, Expression<Func<CLASS, HttpClient>> expression)
        {
            _requestResponseRepository = new AutransoftRepository.RequestResponseRepository();
            _mockRepository = mockRepository;
            _expression = expression;
        }

        public void CreateMock(IServiceCollection serviceCollection)
        {
            _mock = new Mock<CLASS>(ConstructorHelper.GetConstructorParams<CLASS>(serviceCollection, _mockRepository));
            _mock.Setup(_expression).Returns(AddHttpMessageHandlerMock());
        }

        public void AddToDependencyInjection(IServiceCollection serviceCollection)
        {
            _mockRepository.Add(typeof(INTERFACE), _mock.Object);
            serviceCollection.ReplaceTransient<INTERFACE, CLASS>(_mock.Object);
        }

        public override ResponseMockEntity ConfigureResponseMock(HttpMethod httpMethod, HttpRequestHeaders httpRequestHeaders, string absolutePath, string query, string json)
        {
            var requestResponsePair = _requestResponseRepository.Get(httpMethod, absolutePath, query);
            if(requestResponsePair.Key != null && requestResponsePair.Value != null)
                return new ResponseMockEntity
                {
                    HttpStatusCode = requestResponsePair.Value.HttpStatusCode,
                    Obj = requestResponsePair.Value.ResponseObject ?? requestResponsePair.Value.ResponseFunc()
                };

            if(ConfigureResponseFunc != null)
                return ConfigureResponseFunc(httpMethod, httpRequestHeaders, absolutePath, query, json);

            return null;
        }

        public void Clean()
        {
            _requestResponseRepository.Clean();
            _mockRepository.Clean();
        }
    }
}