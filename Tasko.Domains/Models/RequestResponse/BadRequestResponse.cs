using Microsoft.AspNetCore.Http;

namespace Tasko.General.Models.RequestResponses;

public interface IBadRequestResponse<T> : IBaseRequestResponse
{
    string Message { get; set; }
    public T Error { get; set; }
}
public class BadRequestResponse<T> : IBadRequestResponse<T>
{
    public string Message { get; set; }
    public T Error { get; set; }
    public int StatusCode { get; set; }

    public BadRequestResponse()
    {

    }

    public BadRequestResponse(T responseContent, string message)
    {
        Message = message;
        Error = responseContent;
        StatusCode = StatusCodes.Status400BadRequest;
    }

    public BadRequestResponse(T responseContent, string message, int statusCode)
    {
        Message = message;
        Error = responseContent;
        StatusCode = statusCode;
    }
}

