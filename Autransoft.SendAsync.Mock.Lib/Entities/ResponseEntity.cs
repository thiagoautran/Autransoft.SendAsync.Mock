using System.Net;

namespace Autransoft.SendAsync.Mock.Lib.Entities
{
    internal class ResponseEntity
    {
        public object Obj { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}