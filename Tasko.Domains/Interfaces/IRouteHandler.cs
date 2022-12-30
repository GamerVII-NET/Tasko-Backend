using Microsoft.AspNetCore.Builder;


namespace Tasko.Domains.Interfaces
{
    public interface IRouteHandler<T>
    {
        public void Initialzie(T webApplication);
        public virtual void Getters(T webApplication) { }
        public virtual void Creators(T webApplication) { }
        public virtual void Updaters(T webApplication) { }
        public virtual void Deleters(T webApplication) { }
    }
}
