using System;

namespace Autransoft.SendAsync.Mock.Lib.Entities
{
    public class ResponseEntity
    {
        public object ResponseObject { get; set; }
        public Func<object> ResponseFunc { get; set; }
    }
}