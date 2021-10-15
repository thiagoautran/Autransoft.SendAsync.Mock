using System.Net.Http;

namespace Autransoft.SendAsync.Mock.Lib.Entities
{
    public class RequestEntity
    {
        public HttpMethod HttpMethod { get; set; }
        public string AbsolutePath { get; set; }
        public string Query { get; set; }
    }
}