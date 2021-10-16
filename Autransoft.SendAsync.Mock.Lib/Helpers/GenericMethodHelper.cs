using System;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Autransoft.SendAsync.Mock.Lib.Entities;
using Autransoft.SendAsync.Mock.Lib.interfaces;
using Autransoft.SendAsync.Mock.Lib.Mocks;
using Microsoft.Extensions.DependencyInjection;

namespace Autransoft.SendAsync.Mock.Lib.Helpers
{
    public class GenericMethodHelper<INTERFACE, CLASS>
        where CLASS : class, INTERFACE
        where INTERFACE : class
    {
        internal readonly Expression<Func<CLASS, HttpClient>> _expression;
        internal readonly IMockRepository _mockRepository;

        internal GenericMethodMock<INTERFACE, CLASS> _genericMethodMock;

        internal GenericMethodMock<INTERFACE, CLASS> GenericMethodMock { get => _genericMethodMock; }

        internal GenericMethodHelper(IMockRepository mockRepository, Expression<Func<CLASS, HttpClient>> expression)
        {
            _genericMethodMock = new GenericMethodMock<INTERFACE, CLASS>(mockRepository, expression);

            _mockRepository = mockRepository;
            _expression = expression;
        }

        public GenericMethodHelper<INTERFACE, CLASS> Setup(HttpMethod requestHttpMethod, string requestAbsolutePath, string requestQuery, HttpStatusCode responseHttpStatusCode, Func<object> responseResponseFunc)
        {
            _genericMethodMock.RequestResponseRepository.Add
            (
                new RequestEntity
                {
                    HttpMethod = requestHttpMethod,
                    AbsolutePath = requestAbsolutePath,
                    Query = requestQuery
                },
                new ResponseEntity
                {
                    HttpStatusCode = responseHttpStatusCode,
                    ResponseFunc = responseResponseFunc,
                    ResponseObject = null
                }
            );

            return this;
        }

        public GenericMethodHelper<INTERFACE, CLASS> Setup(HttpMethod requestHttpMethod, string requestAbsolutePath, string requestQuery, HttpStatusCode responseHttpStatusCode, object responseResponseObject)
        {
            _genericMethodMock.RequestResponseRepository.Add
            (
                new RequestEntity
                {
                    HttpMethod = requestHttpMethod,
                    AbsolutePath = requestAbsolutePath,
                    Query = requestQuery
                },
                new ResponseEntity
                {
                    HttpStatusCode = responseHttpStatusCode,
                    ResponseObject = responseResponseObject,
                    ResponseFunc = null
                }
            );

            return this;
        }

        public GenericMethodHelper<INTERFACE, CLASS> Setup(RequestEntity request, ResponseEntity response)
        {
            _genericMethodMock.RequestResponseRepository.Add(request, response);

            return this;
        }

        public GenericMethodHelper<INTERFACE, CLASS> Setup(Func<HttpMethod, HttpRequestHeaders, string, string, string, ResponseMockEntity> configureResponseFunc)
        {
            _genericMethodMock.ConfigureResponseFunc = configureResponseFunc;

            return this;
        }

        public void AddToDependencyInjection(IServiceCollection serviceCollection) 
        { 
            _genericMethodMock.CreateMock(serviceCollection);
            _genericMethodMock.AddToDependencyInjection(serviceCollection);
        }
    }
}