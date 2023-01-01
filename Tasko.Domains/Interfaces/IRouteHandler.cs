namespace Tasko.Domains.Interfaces
{
    public interface IRouteHandler<T>
    {
        public void Initialzie(T webApplication);
    }
}
