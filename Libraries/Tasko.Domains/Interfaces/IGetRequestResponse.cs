using Tasko.Domains.Models.RequestResponses;

namespace Tasko.Domains.Interfaces;

public interface IGetRequestResponse<T>
{
    public GetRequestResponseContent<T> Response { get; set; }
}
