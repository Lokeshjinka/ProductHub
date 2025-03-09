using System;

namespace ProductHub.Common
{
    public class Response<T>
    {
        public int Status { get; set; }
        public T Data { get; set; }
        public string Error { get; set; }
    }
}
