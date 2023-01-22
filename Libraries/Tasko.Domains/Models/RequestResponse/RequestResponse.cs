namespace Tasko.Domains.Models.RequestResponses;

public class RequestResponse<T> : IRequestResponse<T>
{
    public T Response { get; set; }
    public int StatusCode { get; set; }

    public RequestResponse(T responseContent, int statusCode)
    {
        Response = responseContent;
        StatusCode = statusCode;
    }
}