using Microsoft.AspNetCore.Http;

namespace Tasko.General.Models.RequestResponses;

#region Interfaces
public interface IRequestResponse<T> : IBaseRequestResponse
{
    public T Response { get; set; }
}

public interface IGetRequestResponseContent<T>
{
    public int Count { get; set; }

    public IEnumerable<T> Items { get; set; }
}

public interface IGetRequestResponse<T>
{
    public IGetRequestResponseContent<T> Response { get; set; }
}
#endregion

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

public class GetRequestResponseContent<T> : IGetRequestResponseContent<T>
{
    public int Count { get; set; }
    public IEnumerable<T> Items { get; set; }

    public GetRequestResponseContent(IEnumerable<T> items)
    {
        Count = items.Count();
        Items = items;
    }
}

public class GetRequestResponse<T> : IGetRequestResponse<T>, IBaseRequestResponse
{
    public IGetRequestResponseContent<T> Response { get; set; }
    public int StatusCode { get; set; }

    public GetRequestResponse(IEnumerable<T> response)
    {
        Response = new GetRequestResponseContent<T>(response);
        StatusCode = StatusCodes.Status200OK;
    }
}