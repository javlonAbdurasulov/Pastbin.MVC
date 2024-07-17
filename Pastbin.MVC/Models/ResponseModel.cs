using System.Net;

namespace Pastbin.MVC.Models
{
    public class ResponseModel<T>
    {
        public ResponseModel(T result, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            Result = result;
            StatusCode = statusCode;
        }
        public ResponseModel(string? error, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            Error = error;
            StatusCode = statusCode;
        }
        public T Result { get; set; }
        public string? Error { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
