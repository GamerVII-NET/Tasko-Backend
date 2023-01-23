using Tasko.Domains.Models.RequestResponses;

namespace Tasko.Domains.Interfaces;

public interface IRequestResponseCollection<T>
{
    public RequestResponseContentCollection<T> Response { get; set; }
}
