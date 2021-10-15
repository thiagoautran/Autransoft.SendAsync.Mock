using System;
using System.Net;

namespace Autransoft.SendAsync.Mock.Lib.Entities
{
    public class ResponseEntity
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public object ResponseObject { get; set; }
        public Func<object> ResponseFunc { get; set; }
    }
}