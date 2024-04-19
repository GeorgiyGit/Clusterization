using System.Net;

namespace Domain.Exceptions
{
    [Serializable]
    public class HttpException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public HttpException() { }
        public HttpException(HttpStatusCode code)
        {
            this.StatusCode = code;
        }
        public HttpException(string message) : base(message) { }
        public HttpException(string message, HttpStatusCode code) : base(message)
        {
            this.StatusCode = code;
        }
        public HttpException(string message, HttpStatusCode code, Exception inner) : base(message, inner)
        {
            this.StatusCode = code;
        }
        protected HttpException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
