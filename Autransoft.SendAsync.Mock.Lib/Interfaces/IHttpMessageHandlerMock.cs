using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Autransoft.SendAsync.Mock.Lib.Entities;

namespace Autransoft.SendAsync.Mock.Lib.interfaces
{
    internal interface IHttpMessageHandlerMock
    {
        ResponseMockEntity ConfigureResponseMock(HttpMethod httpMethod, HttpRequestHeaders httpRequestHeaders, string absolutePath, string query, string json);
    }
}