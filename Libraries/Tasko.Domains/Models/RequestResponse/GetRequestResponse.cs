namespace Tasko.Domains.Models.RequestResponses;

public class GetRequestResponse<T> : IGetRequestResponse<T>, IBaseRequestResponse
{
    public GetRequestResponseContent<T> Response { get; set; }
    public int StatusCode { get; set; }
    public GetRequestResponse()
    {

    }

    public GetRequestResponse(IEnumerable<T> response)
    {
        Response = new GetRequestResponseContent<T>(response);
        StatusCode = StatusCodes.Status200OK;
    }
}
