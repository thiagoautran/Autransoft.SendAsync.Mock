using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autransoft.SendAsync.Mock.Lib.Entities;
using Autransoft.SendAsync.Mock.Lib.interfaces;
using Autransoft.SendAsync.Mock.Lib.Repositories;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace Autransoft.SendAsync.Mock.Lib.Mocks
{
    public abstract class HttpMessageHandlerMock : IHttpMessageHandlerMock
    {
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        public Func<HttpMethod, HttpRequestHeaders, string, string, string, ResponseMockEntity> ConfigureResponseFunc { get; set; }
        
        internal HttpClient AddHttpMessageHandlerMock()
        {
            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns((HttpRequestMessage httpRequestMessage, CancellationToken cncellationToken) => GetReturns(httpRequestMessage));

            return new HttpClient(_mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("http://www.sendasyncmock.com")
            };
        }

        private async Task<HttpResponseMessage> GetReturns(HttpRequestMessage httpRequestMessage)
        {
            var json = string.Empty;
            if(httpRequestMessage.Content != null)
                json = await httpRequestMessage.Content.ReadAsStringAsync().ConfigureAwait(true);

            var returnMock = ConfigureResponseMock(httpRequestMessage.Method, httpRequestMessage.Headers, httpRequestMessage.RequestUri.AbsolutePath, httpRequestMessage.RequestUri.Query, json);
            if (returnMock != null)
            {
                return new HttpResponseMessage
                {
                    StatusCode = returnMock.HttpStatusCode,
                    Content = new StringContent(JsonConvert.SerializeObject(returnMock.Obj), Encoding.UTF8, "application/json")
                };
            }

            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = null
            };
        }

        public virtual ResponseMockEntity ConfigureResponseMock(HttpMethod httpMethod, HttpRequestHeaders httpRequestHeaders, string absolutePath, string query, string json)
        {
            var requestResponsePair = RequestResponseRepository.Get(httpMethod, absolutePath, query);
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
    }
}