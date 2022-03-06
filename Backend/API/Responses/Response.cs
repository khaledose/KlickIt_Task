using Newtonsoft.Json;
using System.Net;

namespace API.Responses;

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class Response<T>
{
    public T? Entity { get; set; }
    public List<T>? Entities { get; set; }
    public int Count { get; set; } = 0;
    public bool IsSuccess { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public string? Error { get; set; }

    public void SetSuccess(HttpStatusCode status = HttpStatusCode.OK)
    {
        IsSuccess = true;
        StatusCode = status;
    }

    public void SetFailure(string error, HttpStatusCode status)
    {
        IsSuccess = false;
        Error = error;
        StatusCode = status;
    }
}

