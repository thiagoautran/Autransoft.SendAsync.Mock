using System.Net.Http;
using System.Net.Http.Headers;
using Autransoft.SendAsync.Mock.Lib.Entities;
using Autransoft.SendAsync.Mock.Lib.Mocks;

namespace Autransoft.SendAsync.Mock.Lib.Base
{
    public abstract class SendAsyncServerMock<INTERFACE, CLASS> : GenericMock<INTERFACE, CLASS> 
        where CLASS : class, INTERFACE
        where INTERFACE : class
    {
        public override void MockInitialize() { }

        public abstract override ResponseMockEntity ConfigureResponseMock(HttpMethod httpMethod, HttpRequestHeaders httpRequestHeaders, string absolutePath, string query, string json);
    }
}