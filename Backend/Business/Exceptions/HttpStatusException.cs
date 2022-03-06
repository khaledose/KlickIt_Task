using System.Net;
using System.Runtime.Serialization;

namespace Business.Exceptions;

public class HttpStatusException : Exception
{
    public HttpStatusCode StatusCode { get; set; }

    public HttpStatusException(HttpStatusCode status)
    {
        StatusCode = status;
    }

    public HttpStatusException(string? message, HttpStatusCode status) : base(message)
    {
        StatusCode = status;
    }

    public HttpStatusException(string? message, Exception? innerException, HttpStatusCode status) : base(message, innerException)
    {
        StatusCode = status;
    }

    protected HttpStatusException(SerializationInfo info, StreamingContext context, HttpStatusCode status) : base(info, context)
    {
        StatusCode = status;
    }
}