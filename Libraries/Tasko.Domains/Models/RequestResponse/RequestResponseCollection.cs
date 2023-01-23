namespace Tasko.Domains.Models.RequestResponses;

public class RequestResponseCollection<T> : IRequestResponseCollection<T>, IBaseRequestResponse
{
    public RequestResponseContentCollection<T> Response { get; set; }
    public int StatusCode { get; set; }
    public RequestResponseCollection()
    {

    }

    public RequestResponseCollection(IEnumerable<T> response)
    {
        Response = new RequestResponseContentCollection<T>(response);
        StatusCode = StatusCodes.Status200OK;
    }
}
