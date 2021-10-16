using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autransoft.SendAsync.Mock.Lib.Entities;
using Autransoft.SendAsync.Mock.Lib.interfaces;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace Autransoft.SendAsync.Mock.Lib.Mocks
{
    public abstract class HttpMessageHandlerMock : IHttpMessageHandlerMock
    {
        internal readonly Mock<HttpMessageHandler> _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        
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

        internal async Task<HttpResponseMessage> GetReturns(HttpRequestMessage httpRequestMessage)
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

        public virtual ResponseMockEntity ConfigureResponseMock(HttpMethod httpMethod, HttpRequestHeaders httpRequestHeaders, string absolutePath, string query, string json) => default;
    }
}