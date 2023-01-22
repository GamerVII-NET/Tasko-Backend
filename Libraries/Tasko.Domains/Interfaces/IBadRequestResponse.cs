namespace Tasko.Domains.Interfaces;

public interface IBadRequestResponse<T> : IBaseRequestResponse
{
    string Message { get; set; }
    public T Error { get; set; }
}

