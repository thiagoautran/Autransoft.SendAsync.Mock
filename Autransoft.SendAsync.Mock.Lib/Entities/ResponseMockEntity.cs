using Autransoft.SendAsync.Mock.Lib.Enums;
using System.Net;

namespace Autransoft.SendAsync.Mock.Lib.Entities
{
    public class ResponseMockEntity
    {
        public object Obj { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public SerializationType SerializationType { get; set; }
    }
}