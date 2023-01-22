namespace Tasko.Domains.Interfaces;
public interface IRequestResponse<T> : IBaseRequestResponse
{
    public T Response { get; set; }
}
